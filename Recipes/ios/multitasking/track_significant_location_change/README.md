---
id: 356BA06A-5BE9-4F4C-88A4-1DABD3B5C54F
title: "Track Significant Location Change"
brief: "The Significant Location Changes API tracks major changes in the user's location by keeping track of changes in cell towers. This API requires a device with a cellular radio."
---

[ ![](Images/01.png)](Images/01.png)

# Recipe

1. The Significant Location Changes API requires the **CoreLocation** library, so we'll start by adding the using directive:
```
using CoreLocation;
```

<ol start="2">
  <li>Next, create an instance of a <code>CLLocationManager</code>. The location manager listens to the system's location service:</li>
</ol>

```
var LocMgr = new CLLocationManager();
```

<ol start="3">
  <li>Starting with iOS 8, applications must call <code>requestWhenInUseAuthorization</code> on <code>CLLocationManager</code> to gain access to the user's application. If the app hasn't been granted permission before, the user will be prompted to allow or deny access.</li>
</ol>
```
  LocMgr.requestAlwaysAuthorization(); //to access user's location in the background
  LocMgr.requestWhenInUseAuthorization(); //to access user's location when the app is in use.
```
<ol start="4">
  <li>In addition to explicitly requesting access to the user's location, you must add two keys to the <strong>Info.plist</strong> file, by opening <strong>Info.plist</strong> and selecting <span class="UIItem">Source</span>. </li>
</ol>
* **NSLocationWhenInUseUsageDescription** - A description of why your app wants to access the user's location in the foreground.
* **NSLocationAlwaysInUsageDescription** - A description of why your app wants to access the user's location in the background.

<ol start="5">
  <li>Check if location services are enabled on the device and for your application. If location data is available, start listening for changes with the <code>StartMonitoringSignificantLocationChanges</code></li>
</ol>

```
if (CLLocationManager.LocationServicesEnabled) {
    LocMgr.StartMonitoringSignificantLocationChanges ();
  } else {
    Console.WriteLine ("Location services not enabled, please enable this in your Settings");
  }
```

<ol start="6">
  <li>When the service receives a location update, the system will wake the application in the background to handle the location changed event. To run your code, subscribe to the location service's <code>LocationsUpdated</code> event, and add a custom handler:</li>
</ol>

```
LocMgr.LocationsUpdated += (o, e) =&gt; Console.WriteLine ("Location change received");
```
  > ℹ️ **Note**: The application has approximately 10 seconds after the `LocationsUpdated` event fires to run code in the background. If you want to run a process that takes more than 10 seconds, wrap it in a [Background Task](/guides/cross-platform/application_fundamentals/backgrounding/part_3_ios_backgrounding_techniques/ios_backgrounding_with_tasks).

<ol start="7">
  <li>Call the following method to stop monitoring location:</li>
</ol>

```
LocMgr.StopMonitoringSignificantLocationChanges ();
```

<ol start="8">
  <li>The application output should resemble the following:</li>
</ol>

![]("Images/02.png")

To test significant location changes in the iOS simulator, refer to the <a href="Recipes/ios/multitasking/test_location_changes_in_simulator" target="_blank">Test Location Changes in Simulator recipe</a>.

