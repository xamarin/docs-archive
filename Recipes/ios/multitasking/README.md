---
id: BF3749F7-1921-726A-417E-D9018D048923
title: "Multitasking & Location"
brief: "Quick guides to performing tasks while the app is in the background and tracking the user's location."
---

-   [Detect Multitasking](/Recipes/ios/multitasking/detect_if_multitasking_is_available) - Detect if multitasking is available on the device, and enabled for your application. 
-   [Track Significant Location Change](/Recipes/ios/multitasking/track_significant_location_change) - Use Significant Location Changes Service to track changes in cell towers, and handle the result in the background. 
-   [Check Background Refresh Setting](/Recipes/ios/multitasking/check_background_refresh_setting) - Determine the background refresh setting programmatically. 
-   [Create Geofence](/Recipes/ios/multitasking/geofencing) - Geofences let you track a user's movement in and out of a geographical region. Geofences work by specifying a circular region -  `CLCircularRegion` - and firing an event when the user crosses the  `CLCircularRegion` 's boundary. A geofence subscribes to updates from the system's location manager, and will wake an application in the background to handle a boundary crossing event. This recipe explains how to set up a simple geofence. 
-   [Test Location Changes in Simulator](/Recipes/ios/multitasking/test_location_changes_in_simulator) - Some location APIs such as  [Significant Location Change](Recipes/ios/multitasking/track_significant_location_change/) and  [Geofences](Recipes/ios/multitasking/geofencing) require big changes in location to trigger events. The iOS Simulator offers an easy way to test location APIs without leaving your desk.
