---
id: 6DD7B2FE-201E-F353-E80A-080992F43334
title: "Handle Clicks"
brief: "This recipe shows how to write code that is triggered by a button ‘click’."
sdk:
  - title: "UIButton Class Reference" 
    url: http://developer.apple.com/library/ios/#DOCUMENTATION/UIKit/Reference/UIButton_Class/UIButton/UIButton.html
---

<a name="Recipe" class="injected"></a>


# Recipe

There are a number of different ways that you can code against user-triggered
events on a `UIButton`:

## Anonymous Delegate


```
testButton1.TouchUpInside += delegate {
    new UIAlertView("Touch1", "TouchUpInside handled", null, "OK", null).Show();
};
```

## Lambda Expression


```
testButton2.TouchUpInside += (sender, ea) => {
    new UIAlertView("Touch2", "TouchUpInside handled", null, "OK", null).Show();
};
```

## Assign a delegate method


```
testButton3.TouchUpInside += HandleTouchUpInside;
```

and add the method:


```
void HandleTouchUpInside (object sender, EventArgs ea) {
    new UIAlertView("Touch3", "TouchUpInside handled", null, "OK", null).Show();
}
```

Make sure to clean up event handlers like this at some point in your code – either when the object has been disposed, or perhaps when the event has been raised. This can be done by declaring:

```
testButton3.TouchUpInside -= HandleTouchUpInside;
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

`UIButton` has a number of events that can be triggered by the user:

The `TouchUpInside` event occurs when the user performs a touch operation that
begins and ends inside the bounds of the button.

The `TouchUpOutside` event, on the other hand, is triggered when the user begins a
touch inside the button but lifts their finger outside its bounds.

The `TouchDown` event occurs as soon as a touch starts in the button, regardless of
where the touch ends.

Generally the `TouchUpInside` event is used to respond to user interaction with
a button.

