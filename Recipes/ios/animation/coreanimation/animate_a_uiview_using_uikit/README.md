---
id: 041A249C-F116-E49E-DB3C-1DCCE6378BC9
title: "Animate a UIView using UIKit"
brief: "This recipe shows how to implement and dynamically update simultaneous animations for a UIView using the UIViewPropertyAnimator class in UIKit. This recipe shows how to animate a UIImageView, but the same technique can be applied to any UIView."
sdk:
  - title: "Core Animation Programming Guide" 
    url: https://developer.apple.com/library/ios/#documentation/Cocoa/Conceptual/CoreAnimation_guide/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to create the animation:

-  Add an image named **Sample.png** to the project with the [**Build Action**](http://developer.xamarin.com/guides/ios/application_fundamentals/working_with_images/) set to **Content**.

-  In a `UIViewController` subclass create class variables for a `UIImageView` and `UIImage`.


```
UIImageView imageView;
UIImage image;
```

-  Create the `UIImageVIew`, assign a `UIImage` to it, set its initial opacity, and add it as a subview.


```
imageView = new UIImageView(new CGRect(0, 100, 50, 50));
image = UIImage.FromFile("Sample.png");
imageView.Image = image;
imageView.Alpha = 0.25f;
View.AddSubview(imageView);
```

-  Create actions that set the `Center` and `Alpha` properties of `imageView` to the desired final values of the animations that will run.


```
Action setCenterRight = () =>
{
    var xpos = UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2;
    var ypos = imageView.Center.Y;
    imageView.Center = new CGPoint(xpos, ypos);
};

Action setCenterLeft = () =>
{
   var xpos = UIScreen.MainScreen.Bounds.Left + imageView.Frame.Width / 2;
   var ypos = imageView.Center.Y;
   imageView.Center = new CGPoint(xpos, ypos);
};

Action setOpacity = () =>
{
    imageView.Alpha = 1;
};
```

-  Create a `UIViewPropertyAnimator` with a duration of 4 seconds, an    animation curve that eases in and out, and a final animation value that is set by the `setCenterRight` action.


```
UIViewPropertyAnimator propertyAnimator =
    new UIViewPropertyAnimator(4, UIViewAnimationCurve.EaseInOut, setCenterRight);
```


-  Add the `setOpacity` animation to fade in to full opacity over the duration of the animation.

```
propertyAnimator.AddAnimations(setOpacity);
```

-  Create an `Action<object>` callback that wraps a call to `setCenterLeft` on the main thread so that it can be called by a `Timer`.


```
Action<object> reversePosition = (o) =>
{
    InvokeOnMainThread(() => {
        propertyAnimator.AddAnimations(setCenterLeft);
    });
};
```

-  Create a `TimerCallback` with the `reversePosition` action and add it to a new `Timer` that will call it in 3 seconds and never repeat.


```
TimerCallback abortPositionDelegate = new TimerCallback(reversePosition);
Timer abortPosition = new Timer(abortPositionDelegate, null, 3000, Timeout.Infinite);
```

-  Finally, start the animation.


```
propertyAnimator.StartAnimation();
```


 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `UIImageView.Center` will begin to animate from its initial value to the value that was set with the `setCenterRight` action that was passed to the `UIViewPropertyAnimator` constructor. Additionally, the `UIImageView.Alpha` value will animate from its initial 0.25f value to 1.0f, since the `setOpacity` animation was added after the `UIViewPropertyAnimator` was created. When the `Timer` completes after 3 seconds and calls `reversePosition`, the _position_ animation stops and proceeds to the new value, which is set to the left edge of the screen by `setCenterLeft`. (The _opacity_ animation is unaffected; the image continues to fade in.) In practice, the animation would likely be updated in response to a user action, such as a gesture that indicates that the user wants to cancel an action, rather than in response to a timer.

