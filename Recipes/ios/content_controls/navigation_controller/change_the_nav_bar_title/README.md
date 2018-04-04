---
id: 98BFE003-F1B3-D4FF-30FF-FEBF63C83020
title: "Change the Nav Bar Title"
sdk:
  - title: "UINavigationBar Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UINavigationBar_Class/Reference/UINavigationBar.html
---

This recipe shows how to change the title displayed in a navigation bar.

 <a name="Recipe" class="injected"></a>


# Recipe

To set the text displayed in the navigation bar, set the `Title` property of
the `UIViewController`:

```
NavigationItem.Title = "Custom Title";
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The Title text can also appear in other places in the UI:

-  If your `UIViewController` has been pushed onto a `UINavigationController` stack, the Title will appear in the ‘back’ button that returns to it.
-  If your `UIViewController` has been added to a `UITabBarContoller` the Title will be used as the tab’s display text (if both `Title` and `TabBarItem.Title` are assigned, whichever is set last overrides the other).


If you dynamically change the title as your app runs, the updated text will
also be reflected in the ‘back’ button and the tab.

