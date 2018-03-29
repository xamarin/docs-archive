---
id: DACC6699-76BF-4F8C-8C72-AFA7021B3A00
title: "Check Background Refresh Settings"
brief: "The Background App Refresh Setting gives the user more control over what our application can do in the background. Let's see how changing this setting affects the backgrounded behavior of our location app."
---

# Recipe

In this example, we will use the `BackgroundRefreshStatus` to check whether background refresh is available for an application.

1. First, add the following code to the `OnActivated` method in the `AppDelegate` class. This will notify us of the application's background refresh status:

```
if (application.BackgroundRefreshStatus == UIBackgroundRefreshStatus.Available) {
  Console.WriteLine ("Background refresh available");
} else {
  Console.WriteLine ("Background refresh not available");
}
```

<ol start="2">
  <li>Deploy the application to device. This should print "Background refresh available" to the console, indicating that the application can operate in the background after the user has exited:</li>
</ol>
![]("Images/bg_start.png")
<ol start="3">
  <li>Let's see what happens when we change the refresh setting. While the app is running in the background, navigate to **Settings > General > Background App Refresh** on the device:</li>
</ol>
![]("Images/3_.png")
<ol start="4">
  <li>Find the location application in the list, and remove backgrounding privileges:</li>
</ol>
![]("Images/4_.png")
<ol start="5">
  <li>Run the application and put it in the background. The background location updates will stop:</li>
</ol>
![]("Images/bg_stop.png")

And the application output will print the updated status:</p>
![]("Images/bg_stop2.png")

Note that the application is not terminated, since the debugger remains connected. An application with the background refresh app setting turned off is simply suspended by the operating system as soon as it enters the background, and cannot do any background processing.

We can also track when the background refresh setting changes by subscribing to `ObserveBackgroundRefreshStatusDidChange` notifications. This will be called when the application enters the foreground, after the user changes the setting:

```
UIApplication.Notifications.ObserveBackgroundRefreshStatusDidChange ((sender, args) =&gt; {
    Console.WriteLine("Background refresh status changed");
  });
```

