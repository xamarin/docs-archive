---
id: 5D0C5AE0-C13D-2746-7B4C-2A47BB059622
title: "Change the Nav Bar Style"
sdk:
  - title: "UINavigationBar Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UINavigationBar_Class/Reference/UINavigationBar.html
---

This recipe shows how to change the style of a navigation bar.

 <a name="Recipe" class="injected"></a>


# Recipe

To change the style of the navigation bar to black set the `BarStyle` property of the UIViewController’s `NavigationBar` property:

```
NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
```

To reset to its default style, set this value to `UIBarStyle.Default`:

```
NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
```

