---
title: "Synchronizing Offline Data with Azure Mobile Apps"
description: "This article explains how to add offline sync functionality to a Xamarin.Forms application that consumes an Azure Mobile Apps backend."
ms.prod: xamarin
ms.assetid: DBB343B0-2709-4C20-A669-5522B9956D9B
ms.technology: xamarin-forms
author: davidbritch
ms.author: dabritch
ms.date: 10/02/2017
---

# Synchronizing Offline Data with Azure Mobile Apps

[![Download Sample](~/media/shared/download.png) Download the sample](https://developer.xamarin.com/samples/xamarin-forms/WebServices/TodoAzureAuthOfflineSync/)

_Offline sync allows users to interact with a mobile application, viewing, adding, or modifying data, even where there isn't a network connection. Changes are stored in a local database, and once the device is online, the changes can be synced with the Azure Mobile Apps instance. This article explains how to add offline sync functionality to a Xamarin.Forms application._

## Overview

The [Azure Mobile Client SDK](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client/) provides the `IMobileServiceTable` interface, which represents the operations that can be performed on tables stored in the Azure Mobile Apps instance. These operations connect directly to the Azure Mobile Apps instance and will fail if the mobile device doesn't have a network connection.

To support offline sync, the Azure Mobile Client SDK supports *sync tables*, which are provided by the `IMobileServiceSyncTable` interface. This interface provides the same Create, Read, Update, Delete (CRUD) operations as the `IMobileServiceTable` interface, but the operations read from or write to a local store. The local store isn't populated with new data from the Azure Mobile Apps instance until there is a call to *pull* data. Similarly, data isn't sent to the Azure Mobile Apps instance until there is a call to *push* local changes.

Offline sync also includes support for detecting conflicts when the same record has changed in both the local store and in the Azure Mobile Apps instance, and custom conflict resolution. Conflicts can either be handled in the local store, or in the Azure Mobile Apps instance.

For more information about offline sync, see [Offline Data Sync in Azure Mobile Apps](/azure/app-service-mobile/app-service-mobile-offline-data-sync/) and [Enable offline sync for your Xamarin.Forms mobile app](/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-offline-data/).

## Setup

The process for integrating offline sync between a Xamarin.Forms application and an Azure Mobile Apps instance is as follows:

1. Create an Azure Mobile Apps instance. For more information, see [Consuming an Azure Mobile App](~/xamarin-forms/data-cloud/consuming/azure.md).
1. Add the [Microsoft.Azure.Mobile.Client.SQLiteStore](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client.SQLiteStore/) NuGet package to all projects in the Xamarin.Forms solution.
1. (Optional) Enable authentication in the Azure Mobile Apps instance and the Xamarin.Forms application. For more information, see [Authenticating Users with Azure Mobile Apps](~/xamarin-forms/data-cloud/authentication/azure.md).

The following section provides additional setup instructions for configuring Universal Windows Platform (UWP) projects to use the Microsoft.Azure.Mobile.Client.SQLiteStore NuGet package. No additional setup is required to use the Microsoft.Azure.Mobile.Client.SQLiteStore NuGet package on iOS and Android.

### Universal Windows Platform

To use SQLite on the Universal Windows Platform (UWP), follow these steps:

1. Install the [SQLite for the Universal Windows Platform](http://sqlite.org/2016/sqlite-uwp-3120200.vsix) Visual Studio Extension in your development environment.
1. In the UWP project in Visual Studio, right click **References > Add Reference**, navigate to **Extensions** and add the **SQLite for Universal Windows Platform** and **Visual C++ 2015 Runtime for Universal Windows Platform Apps** extensions to the UWP project.

## Initializing the Local Store

The local store must be initialized before any sync table operations can be performed. This is achieved in the Portable Class Library (PCL) project of the Xamarin.Forms solution:

```csharp
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TodoAzure
{
    public partial class TodoItemManager
    {
        static TodoItemManager defaultInstance = new TodoItemManager();
        IMobileServiceClient client;
        IMobileServiceSyncTable<TodoItem> todoTable;

        private TodoItemManager()
        {
            this.client = new MobileServiceClient(Constants.ApplicationURL);
            var store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<TodoItem>();
            this.client.SyncContext.InitializeAsync(store);
            this.todoTable = client.GetSyncTable<TodoItem>();
        }
        ...
  }
}
```

A new local SQLite database is created by the `MobileServiceSQLiteStore` class, provided that it doesn't already exist. Then, the `DefineTable<T>` method creates a table in the local store that matches the fields in the `TodoItem` type, provided that it doesn't already exist.

A *sync context* is associated with a `MobileServiceClient` instance, and tracks changes that are made with sync tables. The sync context maintains a queue that keeps an ordered list of Create, Update, and Delete (CUD) operations that will be sent to the Azure Mobile Apps instance later. The `IMobileServiceSyncContext.InitializeAsync()` method is used to associate the local store with the sync context.

The `todoTable` field is an `IMobileServiceSyncTable`, and so all CRUD operations use the local store.

## Performing Synchronization

The local store is synchronized with the Azure Mobile Apps instance when the `SyncAsync` method is invoked:

```csharp
public async Task SyncAsync()
{
  ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

  try
  {
    await this.client.SyncContext.PushAsync();

    // The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
    // Use a different query name for each unique query in your program.
    await this.todoTable.PullAsync("allTodoItems", this.todoTable.CreateQuery());
  }
  catch (MobileServicePushFailedException exc)
  {
    if (exc.PushResult != null)
    {
      syncErrors = exc.PushResult.Errors;
    }
  }

  // Simple error/conflict handling.
  if (syncErrors != null)
  {
    foreach (var error in syncErrors)
    {
      if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
      {
        // Update failed, revert to server's copy
        await error.CancelAndUpdateItemAsync(error.Result);
      }
      else
      {
        // Discard local change
        await error.CancelAndDiscardItemAsync();
      }

      Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
    }
  }
}
```

The `IMobileServiceSyncTable.PushAsync` method operates on the sync context, rather than a specific table, and sends all CUD changes since the last push.

Pull is performed by the `IMobileServiceSyncTable.PullAsync` method on a single table. The first parameter to the `PullAsync` method is a query name that is used only on the mobile device. Providing a non-null query name results in the Azure Mobile Client SDK performing an *incremental sync*, where each time a pull operation returns results, the latest `updatedAt` timestamp from the results is stored in the local system tables. Subsequent pull operations then only retrieve records after that timestamp. Alternatively, *full sync* can be achieved by passing `null` as the query name, which results in all records being retrieved on each pull operation. Following any sync operation, received data is inserted into the local store.

> [!NOTE]
> If a pull is executed against a table that has pending local updates, the pull will first execute a push on the sync context. This minimizes conflicts between changes that are already queued and new data from the Azure Mobile Apps instance.

The `SyncAsync` method also includes a basic implementation for handling conflicts when the same record has changed in both the local store and in the Azure Mobile Apps instance. When the conflict is that data has been updated both in the local store and in the Azure Mobile Apps instance, the `SyncAsync` method updates the data in the local store from the data stored in the Azure Mobile Apps instance. When any other conflict occurs, the `SyncAsync` method discards the local change. This handles the scenario where a local change exists for data that's been deleted from the Azure Mobile Apps instance.

In a production application, developers should write a custom `IMobileServiceSyncHandler` conflict-handling implementation that's suited to their use case. For more information, see [Use Optimistic Concurrency for conflict resolution](https://azure.microsoft.com/documentation/articles/app-service-mobile-dotnet-how-to-use-client-library/#optimisticconcurrency) on the Azure portal, and [Deep dive on the offline support in the managed client SDK](https://blogs.msdn.microsoft.com/azuremobile/2014/04/07/deep-dive-on-the-offline-support-in-the-managed-client-sdk/) on MSDN blogs.

## Purging Data

Tables in the local store can be cleared of data with the `IMobileServiceSyncTable.PurgeAsync` method. This method supports scenarios such as removing stale data that an application no longer requires. For example, the sample application only displays `TodoItem` instances that aren't complete. Therefore, completed items no longer need to be stored locally. Purging completed items from the local store can be accomplished as follows:

```csharp
await todoTable.PurgeAsync(todoTable.Where(item => item.Done));
```

A call to `PurgeAsync` also triggers a push operation. Therefore, any items that are marked as complete locally will be sent to the Azure Mobile Apps instance before being removed from the local store. However, if there are operations pending synchronization with the Azure Mobile Apps instance, the purge will throw an `InvalidOperationException` unless the `force` parameter is set to `true`. An alternative strategy is to examine the `IMobileServiceSyncContext.PendingOperations` property, which returns the number of pending operations that haven't been pushed to the Azure Mobile Apps instance, and only perform the purge if the property is zero.

> [!NOTE]
> Invoking `PurgeAsync` with the `force` parameter set to `true` will lose any pending changes.

## Initiating Synchronization

In the sample application, the `SyncAsync` method is invoked through the `TodoList.OnAppearing` method:

```csharp
protected override async void OnAppearing()
{
  base.OnAppearing();

  // Set syncItems to true to synchronize the data on startup when running in offline mode
  await RefreshItems(true, syncItems: true);
}
```

This means that the application will attempt to sync with the Azure Mobile Apps instance when it starts.

In addition, sync can be initiated in iOS and Android by using pull to refresh on the list of data, and on the Windows platforms by using the **Sync** button on the user interface. For more information, see [Pull to Refresh](~/xamarin-forms/user-interface/listview/interactivity.md#Pull_to_Refresh).

## Summary

This article explained how to add offline sync functionality to a Xamarin.Forms application. Offline sync allows users to interact with a mobile application, viewing, adding, or modifying data, even where there isn't a network connection. Changes are stored in a local database, and once the device is online, the changes can be synced with the Azure Mobile Apps instance.


## Related Links

- [TodoAzureAuthOfflineSync (sample)](https://developer.xamarin.com/samples/xamarin-forms/WebServices/TodoAzureAuthOfflineSync/)
- [Consuming an Azure Mobile App](~/xamarin-forms/data-cloud/consuming/azure.md)
- [Authenticating Users with Azure Mobile Apps](~/xamarin-forms/data-cloud/authentication/azure.md)
- [Azure Mobile Client SDK](https://www.nuget.org/packages/Microsoft.Azure.Mobile.Client/)
- [MobileServiceClient](https://msdn.microsoft.com/library/azure/microsoft.windowsazure.mobileservices.mobileserviceclient(v=azure.10).aspx)
