---
id: {F4D6F2C3-0B5C-1977-00BA-E38AAB660FC3}  
title: Environment Checks  
---

This recipe shows how to make various environment checks from your code
to handle different runtime environments.

 <a name="Check_Simulator_vs_Device" class="injected"></a>


### Check Simulator vs. Device

You can detect whether you are running on the simulator or device by looking
up the value of the [ObjCRuntime.Runtime.Arch](http: //iosapi.xamarin.com/?link=F%3aMonoTouch.ObjCRuntime.Runtime.Arch) field. If the value is [ARCH.Device](http: //iosapi.xamarin.com/?link=T%3aMonoTouch.ObjCRuntime.Arch), you are running on the physical hardware,
otherwise you are running on the simulator.

 <a name="Check_your_iOS_Version" class="injected"></a>


### Check the iOS Version

The iOS operating system version can be checked like this: 

```
if (UIDevice.CurrentDevice.CheckSystemVersion (7,0))
{
   // Code that uses features from Xamarin.iOS 7.0 and later
} else {
   // Code to support earlier iOS versions
}
```

 <a name="Check_your_Xamarin_iOS_Version" class="injected"></a>


### Check your Xamarin.iOS Version

The version of Xamarin.iOS is stored in the field [ObjCRuntime.Constants.Version](http: //iosapi.xamarin.com/?link=F%3aObjCRuntime.Constants.Version). This is a string, you can turn this
into a Version object with code like this: 

```
Version version = new Version (ObjCRuntime.Constants.Version);
if (version > new Version (7,0))
{
   // Code that uses features from Xamarin.iOS 7.0
}
```

 <a name="Detecting_the_GC_Engine" class="injected"></a>