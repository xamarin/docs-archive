---
id: 5EDB69DB-A5D5-40B2-8FD4-14013A815700
title: "Hide separator lines in TableView"
subtitle: "How to remove the lines between rows in a Xamarin.Forms table view"
brief: "This recipe shows how to build a custom renderer for the TableView to hide the lines between each cell on iOS and Android."
article:
  - title: "Customizing Controls for Each Platform" 
    url: https://developer.xamarin.com/guides/xamarin-forms/custom-renderer
api:
  - title: "ListView" 
    url: http://developer.xamarin.com/api/type/Xamarin.Forms.ListView/
  - title: "TableView" 
    url: http://developer.xamarin.com/api/type/Xamarin.Forms.TableView/
dateupdated: 2015-12-11
---

# Overview

To hide the lines between rows in a Xamarin.Forms `TableView` a [custom-renderer](http://developer.xamarin.com/guides/xamarin-forms/custom-renderer)  is required for iOS and Android. There are no lines on Windows Phone so no additional code is required on that platform.

> ℹ️ **Note**: For the `ListView` control, Xamarin.Forms 1.4 introduced the `SeparatorVisibility` property which can be set to `Default` or `None`. There is also a `SeparatorColor` property to change the color of the separator.

## Common Xamarin.Forms code

Create a subclass of `TableView` - in this case we do not need to implement any addition properties or methods on the class. Adding this subclass in the common code does not affect the rendering in any way (until the custom renderers are added below).


```
/// <summary>
/// Required for Custom Renderer to target just this Type
/// </summary>
public class MenuTableView : TableView
{
}
```

In the code building the user interface for a page, create an instance of the subclass:

```
var lv = new MenuTableView ();
// ...
Content = lv; // or it could be inside another layout
```

If the user interface is defined in XAML then a custom `xmlns` must be declared that references the common code assembly, and then the custom prefix used when declaring the new control:

```
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:YOUR_APP_NAMESPACE;assembly=YOUR_APP_ASSEMBLY"
             x:Class="YOUR_APP_NAMESPACE.MenuListPage">
    <local:MenuTableView x:Name="lv" />
</ContentPage>
```


## iOS application project

In the iOS project implement a custom renderer as shown (remember to update the namespace to reflect your project):

```
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MonoTouch.UIKit;

[assembly:ExportRenderer(typeof(YOUR_APP_NAMESPACE.MenuTableView), typeof(YOUR_APP_NAMESPACE.MenuTableViewRenderer))]
namespace YOUR_APP_NAMESPACE
{
	public class MenuTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var tableView = Control as UITableView;
			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}
	}
}
```


## Android application project

In the Android project implement a custom renderer as shown (remember to update the namespace to reflect your project):

```
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(YOUR_APP_NAMESPACE.MenuTableView), typeof(YOUR_APP_NAMESPACE.MenuTableViewRenderer))]
namespace YOUR_APP_NAMESPACE
{
	public class MenuTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var listView = Control as global::Android.Widget.ListView;
			listView.DividerHeight = 0;
		}
	}
}
```

> ℹ️ **Note**: A bug has been raised against the Android TableViewRenderer (Xamarin.Forms 1.3.2 thru 2.0) that might affect this recipe. Setting `DividerHeight=0` does not work with these versions. We are investigating.

# Summary

This recipe shows how to hide the the lines between cells in a `TableView` on iOS and Android. The Windows Phone platform does not render lines between each row, so no additional code is required.

