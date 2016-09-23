---
id: {041A249C-F116-E49E-DB3C-1DCCE6378BC9}  
title: Animate a UIView using UIKit  
brief: This recipe shows how to animate a UIView using UIKit. In this case it shows how to animate a UIImageView, but the same technique can be applied to any UIView.  
samplecode: [Browse on GitHub](https: //github.com/xamarin/recipes/tree/master/ios/animation/coreanimation/animate_a_uiview_using_uikit)  
sdk: [Core Animation Programming Guide](https: //developer.apple.com/library/ios/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html)  
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to create the animation: 

-  Add an image named `Sample.png` to the project with the [*Build Action*](http: //developer.xamarin.com/guides/ios/application_fundamentals/working_with_images/) set to **Content**.

-  In a UIViewController subclass create class variables for a *UIImageView*, *UIImage* and *CGPoint*.


```
UIImageView imageView;
UIImage image;
CGPoint pt;
```

-  Create the UIImageVIew, assign it a UIImage and at it as a subview.


```
imageView = new UIImageView(new CGRect(0, 0, 50, 50));
image = UIImage.FromFile("Sample.png");
imageView.Image = image;
View.AddSubview(imageView);
```

-  Set the point to the Center property of the UIImageView.


```
pt = imageView.Center;
```

-  Begin the animation on the UIView, make a call to `UIView.BeginAnimations`, followed by setting animation properties.


```
UIView.BeginAnimations ("slideAnimation");

UIView.SetAnimationDuration (2);
UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
UIView.SetAnimationRepeatCount (2);
UIView.SetAnimationRepeatAutoreverses (true);
```

6.  Set the animation’s Delegate and declare the selector (function name) to call when the animation stops.


```
UIView.SetAnimationDelegate (this);
UIView.SetAnimationDidStopSelector (new Selector ("animationDidStop: finished: context: "));
```

-  Set the Center property of the UIImageView to the point where it will animate to and call `UIView.CommitAnimations`.


```
var xpos = UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2;
var ypos = imageView.Center.Y;

imageView.Center = new CGPoint (xpos, ypos);
```

-  Implement the function from two steps agao, setting `image.Center` to the original point.


```
[Export("animationDidStop: finished: context: ")]
void SlideStopped (NSString animationID, NSNumber finished, NSObject context)
{
	imageView.Center = pt;
}
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `UIImageView.Center` will be interpolated from its initial value to the
value set between the `BeginAnimations` and `CommitAnimations` calls. When the
animation completes, the UIImageView’s Center will be the value it was set to,
which is at the right side of the screen in this example. However, since the
animation autoreverses here, the final interpolated value from the animation
will be at the left side of the screen, causing the UIImageView to jump to the
right side of the screen after the animation is finished. To prevent this, the
code resets the actual final value to be the same as the final interpolated
value.