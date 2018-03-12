---
id: 4C7C6048-2E6E-0CB9-8A8B-E0E3E6F7F332
title: "Create an Implicit Animation"
brief: "This recipe shows how to create an implicit animation using Core Animation."
sdk:
  - title: "Core Animation Programming Guide" 
    url: https://developer.apple.com/library/ios/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to create the animation:

-  Add an image named `sample.png` to the project with a *Build Action* of **Content**.
- Add the following Using directives:

```
using CoreAnimation;
using CoreGraphics;
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

-  Add the layer as a sublayer of the view’s layer.


```
View.Layer.AddSublayer (layer);
```

-  In the `ViewDidAppear` method, create a CATransaction and set the properties on the `CALayer` for the animation.


```
CATransaction.Begin ();
CATransaction.AnimationDuration = 2;
layer.Position = new CGPoint (100, 400);
CATransaction.Commit ();
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `CATransaction` creates an implicit animation that will interpolate a
layer’s properties to the values set between the `CATransaction.Begin()` and
`CATransaction.Commit()` calls. Any properties set on the `CATransaction`, such as the
`AnimationDuration` in this example, will be used internally to control how the
layer’s properties are interpolated over time.

