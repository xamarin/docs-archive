---
id: F9255BC7-4F5D-F237-8629-D7FCDDCC265E
title: "Detect if Multitasking is Available"
brief: "Not all versions of iOS or iPhone support multi-tasking. This recipe will show how to detect if multi-tasking is supported on the device."
---

<a name="Recipe" class="injected"></a>


# Recipe

Because multitasking support is dependent on both OS version and hardware, if
your application relies on it and must change its behavior if multitasking
support is not available, you can check to see availability via the
IsMultitaskingSupported property on the UIDevice class, as shown below:

```
if(UIDevice.CurrentDevice.IsMultitaskingSupported)
{
   // Code dependent on multitasking.
}
```
