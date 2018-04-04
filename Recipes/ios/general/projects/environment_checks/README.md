---
id: F4D6F2C3-0B5C-1977-00BA-E38AAB660FC3
title: "Environment Checks"
---

This recipe shows how to make various environment checks from your code
to handle different runtime environments.

 <a name="Check_Simulator_vs_Device" class="injected"></a>


### Check Simulator vs. Device

You can detect whether you are running on the simulator or device by looking
up the value of the [ObjCRuntime.Runtime.Arch](https://developer.xamarin.com/api/field/ObjCRuntime.Runtime.Arch/) field. If the value is [ARCH.Device](https://developer.xamarin.com/api/type/ObjCRuntime.Arch/), you are running on the physical hardware,
otherwise you are running on the simulator.

 <a name="Check_your_iOS_Version" class="injected"></a>


### Check the iOS/tvOS Version

The iOS and tvOS operating system version can be checked like this:

```
if (UIDevice.CurrentDevice.CheckSystemVersion (9,0))
{
   // Code that uses features from iOS 9.0 and later
} else {
   // Code to support earlier iOS versions
}
```

the [UIDevice](https://developer.xamarin.com/api/type/UIKit.UIDevice/) class exposes other environment properties that can be checked.




### Check the watchOS Version

The watchOS operating system version can be checked like this:

    if (WKInterfaceDevice.CurrentDevice.CheckSystemVersion (3, 0)) {
    // Code that uses features from watchOS 9.0 and later
    } else {
    // Code to support earlier watchOS versions
    }

 <a name="Check_your_Xamarin_iOS_Version" class="injected"></a>

### Check your Xamarin.iOS Version

The version of Xamarin.iOS is stored in the field ObjCRuntime.Constants.Version. This is a string, you can turn this
into a Version object with code like this:

```
Version version = new Version (ObjCRuntime.Constants.Version);
if (version > new Version (7,0))
{
   // Code that uses features from Xamarin.iOS 7.0
}
```

