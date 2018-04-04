---
id: 44631BAB-7D39-42D8-B48A-8D352C7C1021
title: "Perform map-based navigation"
subtitle: "How to perform street-to-street navigation in each platforms native Maps app"
brief: "This recipe shows how to open each platforms native maps app to perform navigation from the current location to an address entered in a Xamarin.Forms app."
api:
  - title: "Device" 
    url: https://developer.xamarin.com/api/type/Xamarin.Forms.Device/
---

# Overview

The `OpenUri` method of the `Device` class can be used to trigger operations on the underlying platform. This approach can be used to open a URI in a native maps app to display a map and directions.

The URI formats to open the native maps apps on each platform are:

- iOS - `http://maps.apple.com/?q=my-street-address`
- Android - `geo:0,0?q=my+street+address`
- Windows/Windows Phone - `bingmaps:?where=my street address`

## Performing street-to-street navigation

In the code building the user interface for a page, use the `OpenURI` method to request the device to open the specified Uri. The Uri will open the maps app on each platform to the location of a user entered street address. The maps app can then be used to navigate from the current location to the user entered street address.

```
var address = inputEntry.Text;

switch (Device.RuntimePlatform) {
case Device.iOS:
  Device.OpenUri (
    new Uri (string.Format ("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
  break;
case Device.Android:
  Device.OpenUri (
    new Uri (string.Format ("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
  break;
case Device.UWP:
case Device.WinPhone:
  Device.OpenUri (
    new Uri (string.Format ("bingmaps:?where={0}", Uri.EscapeDataString(address))));
  break;
}
```

The user entered street address must be formatted as a query string suitable for consumption by the native apps. This is accomplished by URL encoding the string for each platform.

# Summary

This recipe shows how to open each platforms native maps app to perform navigation from the current location to an address entered in a Xamarin.Forms app.

