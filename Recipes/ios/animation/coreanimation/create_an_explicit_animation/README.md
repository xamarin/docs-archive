---
id: D45B584A-4619-A966-A036-320F139448D4
title: "Create An Explicit Animation"
brief: "This recipe shows how to create an explicit animation using Core Animation."
sdk:
  - title: "Core Animation Programming Guide" 
    url: http://developer.apple.com/library/mac/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe


Follow these steps to create the animation:

-  Add an image named `sample.png` to the project with a Build Action of **Content**.
-  Add the following Using directives:

```
using CoreGraphics;
using CoreAnimation;
using Foundation;
```
- In a UIViewController subclass, declare a **CALayer** class variable from the CoreAnimation namespace.


```
CALayer layer;
```

-  In `ViewDidLoad` method, create the layer and set its content to be the image.


```
layer = new CALayer ();
layer.Bounds = new CGRect (0, 0, 50, 50);
layer.Position = new CGPoint (150, 150);
layer.Contents = UIImage.FromFile ("sample.png").CGImage;
layer.ContentsGravity = CALayer.GravityResizeAspectFill;
```

-  Add the layer as a sub layer of the view’s layer.


```
View.Layer.AddSublayer (layer);
```

-  In the `ViewDidAppear` method, create a `CABasicAnimation` to animate the position of the layer.


```
public override void ViewDidAppear (bool animated)
{
	base.ViewDidAppear (animated);

	var pt = layer.Position;
	layer.Position = new CGPoint (150, 350);

	var basicAnimation = CABasicAnimation.FromKeyPath ("position");
	basicAnimation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
	basicAnimation.From = NSValue.FromCGPoint (pt);
	basicAnimation.To = NSValue.FromCGPoint (new CGPoint (150, 350));
	basicAnimation.Duration = 2;

	layer.AddAnimation (basicAnimation, "position");

}
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The code creates a layer and adds an image to it. Before creating the
animation the final interpolated value of the layer’s position is set so that
the image will remain at the final position at the end of the animation. A
timing function is used to determine how the position is animated over time.
When the animation is added to the layer, the animation will happen on the next
pass through the run loop, animating the property set in the key path, which in
this case is the position.

