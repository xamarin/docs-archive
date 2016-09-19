id:{df80adc5-9941-427e-abae-0fcf7bd0b2a9}
title:Setting the Focus to a UITextField
brief:Code can manually change the focus of a view to bring up the keyboard for entry in a UITextField without tapping it.
sdk:[UITextField](https://developer.apple.com/LIBRARY/ios/documentation/UIKit/Reference/UITextField_Class/index.html)
samplecode:[Browse on GitHub](https://github.com/xamarin/recipes/tree/master/ios/standard_controls/text_field/set-uitextfield-focus)


# Recipe


![Focus Screen](Images/focusScreenShot.png)

To have our `UITextField`, in this case named `FocusTextField`, selected as soon as we open the view, we can place the following line of code in the `ViewDidLoad()` method:

```
FocusTextField.BecomeFirstResponder ();
```

Additional Information
----------------------

We can call `BecomeFirstResponder` on a `UITextField` at any point to switch focus - not just inside `ViewDidLoad`.
