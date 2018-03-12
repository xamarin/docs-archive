---
id: CC85CDF8-974D-68CF-B0C5-56B60562FA82
title: "Create an Animation Group"
brief: "This recipe shows how to create an animation that groups multiple animations together using Core Animation."
sdk:
  - title: "Core Animation Programming Guide" 
    url: http://developer.apple.com/library/mac/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to create the animation:

-  Add an image named `sample.png` to the project with a [*Build Action*](http://developer.xamarin.com/guides/ios/application_fundamentals/working_with_images/) of **Content**.
-  Add the following using directives:

```
using CoreAnimation
using CoreGraphics;
using Foundation;
```
-  In a UIViewController subclass, declare a `CALayer` class variable from the CoreAnimation namespace.


```
CALayer layer;
```

-  In `ViewDidLoad` method, create the layer and set its content to be the image.


```
 layer = new CALayer ();
 layer.Bounds = new CGRect (0, 0, 50, 50);
 layer.Position = new CGPoint (100, 100);
 layer.Contents = UIImage.FromFile ("sample.png").CGImage;
 layer.ContentsGravity = CALayer.GravityResizeAspectFill;
```

-  Add the layer as a sub layer of the view’s layer.


```
View.Layer.AddSublayer (layer);
```

-  Override the `ViewWillAppear` method,  add the following code to create the animations:


```
public override void ViewWillAppear (bool animated)
{
	base.ViewDidAppear (animated);

	//Creates basic moving animation
	var pt = layer.Position;
	layer.Position = new CGPoint (100, 300);
	var basicAnimation = CABasicAnimation.FromKeyPath ("position");
	basicAnimation.TimingFunction =
					CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut);
	basicAnimation.From = NSValue.FromCGPoint (pt);
	basicAnimation.To = NSValue.FromCGPoint (new CGPoint (100, 300));


	//Creates transformation animation
	layer.Transform = CATransform3D.MakeRotation ((float)Math.PI * 2, 0, 0, 1);
	var animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");
	animRotate.Values = new NSObject[] {
		NSNumber.FromFloat (0f),
		NSNumber.FromFloat ((float)Math.PI / 2f),
		NSNumber.FromFloat ((float)Math.PI),
		NSNumber.FromFloat ((float)Math.PI * 2)};
	animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);

	//Adds the animations to a group, and adds the group to the layer
	var animationGroup = CAAnimationGroup.CreateAnimation ();
	animationGroup.Duration = 2;
	animationGroup.Animations = new CAAnimation[] { basicAnimation, animRotate };
	layer.AddAnimation (animationGroup, null);
}
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The code creates 2 separate explicit animations. The first animation is a
`CABasicAnimation` that animates the layer’s position. The second animation is a
`CAKeyFrameAnimation` that rotates the layer. These animations are combined into a
`CAAnimationGroup` that is added to the layer.

