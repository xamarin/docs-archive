---
id: DE7F5B11-E7E5-AD46-1BA2-BD53D3465901
title: "Change the Back Button"
brief: "This recipe shows how to change the Back button that appears in a navigation controller."
sdk:
  - title: "UINavigationBar Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UINavigationBar_Class/Reference/UINavigationBar.html
---


# Recipe

The Back button text is related to the Title of the ViewController that it
will return to. The sample code demonstrates three different ways the Back
button can be set:

-  If the `ViewController.Title` has not been set, the button text will be Back. 
-  If the `ViewController.Title` has been set, the button will display the same text. In the sample code when the Title property is set and then the ViewController ‘pushed’, the back button will contain the same value: Home. 


```
customVC.Title = "Home";
NavigationController.PushViewController (customVC, true);
```

-  The button itself can be replaced with a custom UIBarButtonItem using the `NavigationItem.SetLeftBarButtonItem`. The new button should implement the PopViewControllerAnimated method to behave the same way as the default Back button: 


```
customVC.NavigationItem.SetLeftBarButtonItem (new UIBarButtonItem(
UIImage.FromFile("29_icon.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
    NavigationController.PopViewControllerAnimated(true);
}), true);
```

These screenshots show the three different Back buttons:

 [ ![Screenshots of updated back button](Images/NavBack.png)](Images/NavBack.png)

