---
id: 79EB2AC9-4242-EF49-5DBF-35D5B9284DC1
title: "Create a Keyframe Animation"
brief: "This recipe shows how to create a keyframe animation using Core Animation."
sdk:
  - title: "Core Animation Programming Guide" 
    url: https://developer.apple.com/library/ios/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to create the animation:


-  Add an image named `sample.png `to the project with a [*Build Action*](http://developer.xamarin.com/guides/ios/application_fundamentals/working_with_images/) of **Content**.
-  Add the following using directives:

```
using CoreAnimation;
using CoreGraphics;
using Foundation;
```

-  In a UIViewController subclass, declare a CALayer class variable from the CoreAnimation namespace:


```
CALayer layer;
```

-  In `ViewDidLoad` method, create the layer and set its content to be the image:


```
layer = new CALayer ();
layer.Bounds = new CGRect (0, 0, 50, 50);
layer.Position = new CGPoint (UIScreen.MainScreen.Bounds.Width / 2,     UIScreen.MainScreen.Bounds.Height / 2);
layer.Contents = UIImage.FromFile ("sample.png").CGImage;
layer.ContentsGravity = CALayer.GravityResizeAspectFill;
```

-  Add the layer as a sublayer of the view’s layer:


```
View.Layer.AddSublayer (layer);
```

-  Override the `ViewWillAppear` method, and add the following code to create the transform animation:


```
layer.Transform = CATransform3D.MakeRotation ((float)Math.PI * 2, 0, 0, 1);

		CAKeyFrameAnimation animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");

		animRotate.Values = new NSObject[] {
			NSNumber.FromFloat (0f),
			NSNumber.FromFloat ((float)Math.PI / 2f),
			NSNumber.FromFloat ((float)Math.PI),
			NSNumber.FromFloat ((float)Math.PI * 2)};

		animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);

		animRotate.Duration = 2;

		layer.AddAnimation (animRotate, "transform");
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The code creates a `CAKeyframeAnimation` to animate the layer’s transform.
The animation interpolates across the values set via an array. The value
function is used to cause the animation to rotate the layer about the x-axis. As
an animation does not change the actual value of the transform, but only the
presentation, the code sets the transform to the final value presented by the
animation in order for the layer to remain at the final value when the animation
completes.

