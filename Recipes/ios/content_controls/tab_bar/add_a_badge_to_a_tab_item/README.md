---
id: F743415E-9120-34FA-452C-3B7D54AA33CD
title: "Add a Badge to a Tab Item"
brief: "This recipe shows how to display a badge on a tab."
article:
  - title: "Create a Tab Bar" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/tab_bar/create_a_tab_bar
sdk:
  - title: "UINavigationBar Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UINavigationBar_Class/Reference/UINavigationBar.html
  - title: "UITabBarItem Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UITabBarItem_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>


# Recipe

A badge is a small red oval with white border than is displayed over the
top-right corner of a tab. It is usually used to indicate the number of new
items that will be displayed when the tab is selected (such as the Updates tab
in the App Store).

 ![](Images/Picture_1.png)

A badge will appear on a tab if the BadgeValue property is set:

```
tab.TabBarItem.BadgeValue = "3";
```

To remove a badge that has been previously displayed, set the property to
null:

```
tab.TabBarItem.BadgeValue = null;
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

You cannot use an empty string to clear the badge – that will result in the
red and white circle appearing without any content.
