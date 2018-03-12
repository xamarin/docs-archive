---
id: DF80ADC5-9941-427E-ABAE-0FCF7BD0B2A9
title: "Setting the Focus to a UITextField"
brief: "Code can manually change the focus of a view to bring up the keyboard for entry in a UITextField without tapping it."
sdk:
  - title: "UITextField" 
    url: https://developer.apple.com/LIBRARY/ios/documentation/UIKit/Reference/UITextField_Class/index.html
---


# Recipe


![Focus Screen](Images/focusScreenShot.png)

To have our `UITextField`, in this case named `FocusTextField`, selected as soon as we open the view, we can place the following line of code in the `ViewDidLoad()` method:

```
FocusTextField.BecomeFirstResponder ();
```

Additional Information
----------------------

We can call `BecomeFirstResponder` on a `UITextField` at any point to switch focus - not just inside `ViewDidLoad`.

