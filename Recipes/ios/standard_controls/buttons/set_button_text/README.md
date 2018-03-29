---
id: D01169DB-BC0D-3DDA-1D37-D1873EA1688A
title: "Set Button Text"
brief: "This recipe shows you how to set the text on a UIButton."
article:
  - title: "Create Different Types of Button" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/standard_controls/buttons/create_different_types_of_buttons
sdk:
  - title: "UIButton Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIButton_Class/UIButton/UIButton.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Buttons have a number of different states, and you can set the button text
once for all states or assign different values for each state.

To set the ‘default’ text for a button, call `SetTitle` for
`UIControlState.Normal` and that text will be used for all states:

```
buttonRect = UIButton.FromType(UIButtonType.RoundedRect);
buttonRect.SetTitle ("Click me", UIControlState.Normal);
```

Alternatively you can make each state different, as this image shows:

 ![](Images/ButtonStates.png)

The code for these three states is:

```
buttonRect = UIButton.FromType(UIButtonType.RoundedRect);
buttonRect.SetTitle ("Click me", UIControlState.Normal);
buttonRect.SetTitle ("Clicking me", UIControlState.Highlighted);
buttonRect.SetTitle ("Disabled", UIControlState.Disabled);
buttonRect.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
```

You can also set the different states using the Xamarin Designer for iOS by choosing
each one from the drop-down-list of the properties panel.

