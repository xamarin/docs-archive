---
id: FFC8A717-915A-4A07-BD4F-CF63D7B7F168
title: "Securely store credentials"
subtitle: "Using Xamarin.Auth to store data in the account store"
brief: "This recipe shows how to securely store data in an account store that's backed by Keychain services in iOS, and the KeyStore class in Android."

sdk:
  - title: "Xamarin.Auth" 
    url: https://www.nuget.org/packages/Xamarin.Auth
dateupdated: 2017-08-04
---

# Overview

Xamarin.Auth is a cross-platform SDK for authenticating users and storing their accounts. It can be used to securely store `Account` objects in an account store so that applications do not always have to re-authenticate users. For information about using Xamarin.Auth to authenticate users, see [Authenticating Users with an Identity Provider](https://developer.xamarin.com/guides/xamarin-forms/web-services/authentication/oauth/).

## Storing Account Information

The following code example shows how an `Account` object is securely saved:

```
public void SaveCredentials (string userName, string password)
{
  if (!string.IsNullOrWhiteSpace (userName) && !string.IsNullOrWhiteSpace (password)) {
    Account account = new Account {
      Username = userName
    };
    account.Properties.Add ("Password", password);
    AccountStore.Create ().Save (account, App.AppName);
  }
}
```

Saved accounts are uniquely identified using a key composed of the account's `Username` property and a service ID, which is a string that's used when fetching accounts from the account store. The user's password is stored in the `Account.Properties` collection, which is a key-value store whose values are encrypted when the `Account` instance is stored.

If an `Account` was previously saved, calling the `Save` method again will overwrite it.

## Retrieving Account Information

`Account` objects can be retrieved by calling the `FindAccountsForService` method, as shown in the following code example:

```
public string UserName {
  get {
    var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
    return (account != null) ? account.Username : null;
  }
}

public string Password {
  get {
    var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
    return (account != null) ? account.Properties ["Password"] : null;
  }
}
```

The `FindAccountsForService` method returns an `IEnumerable` collection of `Account` objects, with the first item in the collection being set as the matched account.

## Deleting Account Information

`Account` objects can be deleted by calling the `Delete` method, as shown in the following code example:

```
public void DeleteCredentials ()
{
  var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
  if (account != null) {
    AccountStore.Create ().Delete (account, App.AppName);
  }
}
```

# Summary

This recipe showed how to use Xamarin.Auth to securely store data in an account store that's backed by Keychain services in iOS, and the `KeyStore` class in Android.

