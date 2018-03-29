---
id: 5081B5B0-0FC0-BFCC-B4F7-056D8FD9F82D
title: "Use CoreMotion with Accelerometer"
brief: "This recipe shows how to use the Core Motion framework to receive data from the accelerometer."
samplecode:
  - title: "CoreMotion" 
    url: https://github.com/xamarin/recipes/tree/master/ios/input/accelerometer/use_coremotion_with_accelerometer
sdk:
  - title: "CMMotionManager" 
    url: http://developer.apple.com/library/ios/#documentation/CoreMotion/Reference/CMMotionManager_Class/Reference/Reference.html
  - title: "CMAccelerometerData" 
    url: https://developer.xamarin.com/api/type/CoreMotion.CMAccelerometerData/
---

<a name="Recipe" class="injected"></a>


# Recipe

Create an instance of `CoreMotion.CMMotionManager`, and begin
listening to updates from the Accelerometer with the method
`StartAccelerometerUpdates`:

The following code snippet will display the accelerometer data for each axis
in UILabels:

```
motionManager = new CMMotionManager ();
motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) =>
{
       this.lblX.Text = data.Acceleration.X.ToString ("0.00000000");
       this.lblY.Text = data.Acceleration.Y.ToString ("0.00000000");
       this.lblZ.Text = data.Acceleration.Z.ToString ("0.00000000");
});
```

The first parameter, `data` is a  [CMAccelerometerData](https://developer.xamarin.com/api/type/CoreMotion.CMAccelerometerData/) instance holding the accelerometer data for the event.

# Additional Information

The `CoreMotion.CMMotionManager` class is a gateway to the motion
services provided by iOS. An application should only have a single instance of
the `CMMotionManager` class.

The iOS simulator has no support for the features from Core Motion – the
code may only be tested on a device.

