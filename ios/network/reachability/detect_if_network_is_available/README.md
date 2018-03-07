---
id: 5FB6747E-70A7-0410-2C5C-60870D775100
title: "Detect if Network is Available"
brief: "This recipe shows how to detect the type of network connection that is in use."
sample:
  - title: "Reachability Sample" 
    url: /samples/ReachabilitySample
sdk:
  - title: "Reachability Sample" 
    url: http://developer.apple.com/library/ios/#samplecode/Reachability/Introduction/Intro.html
---

<a name="Recipe" class="injected"></a>


# Recipe

The [Reachability sample](/samples/ReachabilitySample) provides a static class named [Reachability](https://github.com/xamarin/monotouch-samples/blob/master/ReachabilitySample/reachability.cs) that can be used to determine network status.
Add a copy of this class to your project, and then use the static methods it
provides.

-  To see if a web site is available you can check like this:


```
if(!Reachability.IsHostReachable("http://google.com")) {
    // Put alternative content/message here
}
else
{
    // Put Internet Required Code here
}
```

-  To check what type of internet connection is available, use the static method `Reachability.InternetConnectionStatus()`:


```
NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
```

 `Reachability.InternetConnectionStatus` returns the enum `NetworkStatus` which provides some information about the network connection that will be used for internet connectivity:

```
public enum NetworkStatus
{
       NotReachable,
       ReachableViaCarrierDataNetwork,
       ReachableViaWiFiNetwork
}
```

