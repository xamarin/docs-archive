---
id: 79698AE6-8B70-7189-D52D-2D1801EEB833
title: "Add a Nav Bar Right Button"
brief: "This recipe shows how to add a right button to a navigation bar."
sdk:
  - title: "UINavigationBar Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UINavigationBar_Class/Reference/UINavigationBar.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To add a button to the `UINavigationBar` using a built-in icon from the
`UIBarButtonSystemItem` enumeration:

```
this.NavigationItem.SetRightBarButtonItem(
    new UIBarButtonItem(UIBarButtonSystemItem.Action, (sender,args) => {
       // button was clicked
})
, true);
```

To add a button to the `UINavigationBar` using a custom image:

```
this.NavigationItem.SetRightBarButtonItem(
    new UIBarButtonItem(UIImage.FromFile("some_image.png")
    , UIBarButtonItemStyle.Plain
    , (sender,args) => {
       // button was clicked
    })
, true);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

To remove the button:

```
this.NavigationItem.SetRightBarButtonItem(null, true);
```

