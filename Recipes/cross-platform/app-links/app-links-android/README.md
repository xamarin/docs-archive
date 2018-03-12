---
id: A61CF22F-B15D-4824-A472-48DBF4DBF185  
title: App Links for Android with Rivits
subtitle: How to Link to other apps and Handle Incoming links using App Links  
brief: This recipe will show you how to link to other apps using the Rivets package from the NuGet Gallery, as well as register your app to receive and parse incoming App Links.   
link:
  - title: [Creating Connected App Experiences with App Links]
    url: (http://blog.xamarin.com/creating-connected-app-experiences-with-app-links-and-rivets-with-xamarin/)  
  - title: [Rivets]
    url: (https://www.nuget.org/packages/Rivets/)  
  - title: [Official App Links Documentation]
    url: (http://applinks.org/documentation/)
dateupdated: 2017-10-05
---

<a name="Overview" class="injected"></a>

# Overview

This recipe will show you how to link to other apps using the Rivets
NuGet package from the NuGet Gallery, as well as register your app to
receive and parse incoming App Links.

There are two main parts to implementing App Links in your apps

1. **Linking to other apps** &ndash; Instead of opening a web link in a
   browser directly, you should let the App Links navigation take care
   of how to open a link

2. **Handling incoming links from other apps** &ndash; You can
   advertise on web page links App Link metadata which tells other apps
   how to deep link to the same content in your app.


<a name="Linking to other Apps" class="injected"></a>

# Linking to other apps

-  Install the
   [Rivets](https://www.nuget.org/packages/Rivets/) NuGet package.

-  Find any instances where you navigate to a URL in your app (either
   by starting an Activity with an `Intent.ActionView` Intent or by
   loading the page into a custom web view) and replace them with this:


```csharp
Rivets.AppLinks.Navigator.Navigate(&quot;http://any.old.url&quot;)
```

-  Your app will now attempt to Navigate to another installed app for
   the URL using App Links, or will fall back to using an intent with a
   view action if no App Link meta data is found, or no apps for the
   metadata are installed.


## Handling incoming App Links

-  Install the
   [Rivets](https://www.nuget.org/packages/Rivets/) package from
   the NuGet Gallery if you have not already done so.

-  Create the following Layout Resource named **ProductLayout.axml** :


```csharp
<?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?>
<LinearLayout xmlns:android=&quot;http://schemas.android.com/apk/res/android&quot;
    android:orientation=&quot;vertical&quot;
    android:layout_width=&quot;fill_parent&quot;
    android:layout_height=&quot;fill_parent&quot;
    android:padding=&quot;10dp&quot;>
    <TextView
        android:text=&quot;Product ID:&quot;
        android:textAppearance=&quot;?android:attr/textAppearanceMedium&quot;
        android:layout_width=&quot;match_parent&quot;
        android:layout_height=&quot;wrap_content&quot;
        android:id=&quot;@+id/textView1&quot; />
    <TextView
        android:text=&quot;Large Text&quot;
        android:textAppearance=&quot;?android:attr/textAppearanceLarge&quot;
        android:layout_width=&quot;match_parent&quot;
        android:layout_height=&quot;wrap_content&quot;
        android:id=&quot;@+id/textViewProductId&quot; />
</LinearLayout>
```

-  Add the following `Activity` to your app:


```csharp
[Activity (Label = &quot;Product Details&quot;)]
[IntentFilter(new [] {Android.Content.Intent.ActionView },
    DataScheme=&quot;example&quot;,
    DataHost=&quot;products&quot;,
    Categories=new [] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]
public class ProductActivity : Activity
{
    protected override void OnCreate (Bundle bundle)
    {
        base.OnCreate (bundle);

        SetContentView (Resource.Layout.ProductLayout);

        var id = &quot;No Product ID Found&quot;;

        if (Intent.HasExtra (&quot;al_applink_data&quot;)) {

            var appLinkData = Intent.GetStringExtra (&quot;al_applink_data&quot;);

            var alUrl = new Rivets.AppLinkUrl (Intent.Data.ToString (), appLinkData);

            // InputQueryParameters will contain our product id
            if (alUrl != null &amp;&amp; alUrl.InputQueryParameters.ContainsKey (&quot;id&quot;))
                id = alUrl.InputQueryParameters [&quot;id&quot;];
        }

        FindViewById<TextView> (Resource.Id.textViewProductId).Text = id;
    }
}
```

-  When your app is opened with the url `example://products?id=12345`
   the product Activity will be displayed and you should see the id
   `12345` from the query string displayed.

-  Create an HTML page with the following HTML:


```html
<html>
 <head>
  <title>Product 12345</title>
  <meta property=&quot;al:android:url&quot; content=&quot;example://products?id=12345&quot; />
 </head>
 <body>
  <h1>Product 12345</h1>
  <p>This could be a page about Product 12345</p>
 </body>
</html>
```

-  Publish this page somewhere on the internet, or at a location your
   Android app can reach.

-  Test navigating using App Links from your Android app by calling
   this method somewhere:


```csharp
Rivets.AppLinks.Navigator.Navigate(&quot;http://location/of/your/html/file.html&quot;);
```

<a name="Summary" class="injected"></a>

# Summary

This recipe will show you how to link to other apps using App Links, as
well as register your app to receive and parse incoming App Links.
