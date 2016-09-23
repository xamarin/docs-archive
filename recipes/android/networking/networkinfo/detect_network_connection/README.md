---
id: {38C9FC4E-D563-5B6E-A5E7-DA4DDD91F5A3}  
title: Detect Network Connection  
brief: This recipe provides some quick snippets on how detect the type of network connection in use or how to query a specific type of network connection.  
samplecode: [Browse on GitHub](https: //github.com/xamarin/recipes/tree/master/android/networking/networkinfo/detect_network_connection)  
sdk: [ConnectivityManager](/api/type/Android.Net.ConnectivityManager/)  
sdk: [NetworkInfo](/api/type/Android.Net.NetworkInfo/)  
sdk: [ConnectivityType](/api/type/Android.Net.ConnectivityType/)
sdk: [NetworkInfo.State](/api/type/Android.Net.NetworkInfo+State/)
---

<!-- Updated:  2015-09-30 -->

# Recipe


In order to query the network state, the application must request a `NetworkInfo` object for a type of network. The `NetworkInfo` class which holds information about a given network type. A instance of `NetworkInfo` is obtained by calling the `GetNetworkInfo` member of the `ConnectivityManager` class. This method takes a `ConnectivityType` describing which network to query for more information.

This image shows the sample application for this recipe running while connected to different networks: 

![](Images/Image01.png)

## Using ConnectivityManager

The first thing the sample application must do is obtain a reference to the `ConnectivityManager`: 
```
ConnectivityManager connectivityManager = (ConnectivityManager) GetSystemService(ConnectivityService);
```

In order to query the device for network state, the app must request the `android.permission.ACCESS_NdETWORK_STATE` in the manifest: 

```
<uses-permission android: name="android.permission.ACCESS_NETWORK_STATE"></uses-permission>
```

## Connected to a Network

To check if the device is connected to any type of network the `ActiveNetworkInfo` property of `ConnectivityManager` returns information about the type of network the device is using. The app uses this `NetworkInfo` object to see if the device is connected: 

```
NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
```

## Connected to Wifi

To determine if the device is connected to a Wifi network, call  `ConnectivityManager.GetNetworkInfo` and pass `ConnectivityType.Wifi` to get information about the Wifi network.

The following code snippet from the sample application will display a green square when the device is connected via Wifi, and a red square when there is no Wifi connection: 

```
NetworkInfo wifiInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Wifi);
if(wifiInfo.IsConnected)
{
  Log.Debug(TAG, "Wifi connected.");
  _wifiImage.SetImageResource(Resource.Drawable.green_square);
} else
{
  Log.Debug(TAG, "Wifi disconnected.");
  _wifiImage.SetImageResource(Resource.Drawable.red_square);
}
```

## Detect When Roaming

To determine if the device is roaming on the mobile network, call `ConnectivityManager.GetNetworkInfo` and pass `ConnectivityType.Mobile` to get information about the mobile network.

The following code snippet from the sample application will display a green square when the device is roaming, and a red square when it is not: 
```
NetworkInfo mobileInfo = connectivityManager.GetNetworkInfo(ConnectivityType.Mobile);
if(mobileInfo.IsRoaming && mobileInfo.IsConnected)
{
  Log.Debug(TAG, "Roaming.");
  _roamingImage.SetImageResource(Resource.Drawable.green_square);
} else
{
  Log.Debug(TAG, "Not roaming.");
  _roamingImage.SetImageResource(Resource.Drawable.red_square);
}				
```