---
id: 8882FBE4-084A-7759-EED7-FE9EABBFEEBF
title: "Persist Customized Tab Bar Buttons"
brief: "This recipe provides one possible solution for how to save and reload a customized tab bar."
article:
  - title: "Create a Tab Bar" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/tab_bar/create_a_tab_bar
  - title: "Add a Badge to a Tab Item" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/tab_bar/add_a_badge_to_a_tab_item
  - title: "Specify Customizable Tab Bar Buttons" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/tab_bar/specify_customizable_tab_bar_buttons
sdk:
  - title: "UITabBarController Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UITabBarController_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>


# Recipe

When a Tab Bar has more options than can fit on the screen, you can allow the
user to edit the order of the tabs. The changes are not automatically saved.

To save the changes made when a tab bar is edited:
<ol start="1">
	<li>Implement a tab bar. See the [Specifying Customizable Tab Bar Buttons] recipe. Ensure that the Tag is set on each TabBarItem to match its position in the default ViewControllers array.</li>
</ol>

<ol start="2">
	<li>Implement the FinishedCustomizingViewControllers delegate which gets called whenever the user touches Done after editing a tab bar. In this example the Tag of each item must match its default position. </li>
</ol>

```
FinishedCustomizingViewControllers += delegate(object sender, UITabBarCustomizeChangeEventArgs e) {
    if (e.Changed) {
        var tabOrderArray = new List<string>();
        foreach(var viewController in e.ViewControllers)  {
            var tag = viewController.TabBarItem.Tag;
            tabOrderArray.Add(tag.ToString());
        }
        NSArray arr = NSArray.FromStrings(tabOrderArray.ToArray());
        NSUserDefaults.StandardUserDefaults["tabBarOrder"] = arr;
    }
};
```

<ol start="3">
	<li>Create a method SetCustomTabBarOrder() that retrieves the user default setting and re-creates the correct tab order. It re-sets the ViewControllers property directly with the custom order. </li>
</ol>

```
void SetCustomTabBarOrder()
{
    var initialViewController = this.ViewControllers;
    var tabBarOrder = NSUserDefaults.StandardUserDefaults.StringArrayForKey("tabBarOrder");
    if (tabBarOrder == null)
        Console.WriteLine ("No custom tab order, use default");
    else {
        if (tabBarOrder.Length == initialViewController.Length) {
            Console.WriteLine ("Setting order based on UserDefault");
            var newViewControllers = new List<UIViewController>();
            foreach (var tabBarNumber in tabBarOrder) {
                newViewControllers.Add(initialViewController
                    [Convert.ToInt32(tabBarNumber)]);
            }
            ViewControllers = newViewControllers.ToArray();
        }
    }
}
```

<ol start="4">
	<li>Now call the new method SetCustomTabBarOrder() after the ViewControllers property has been set. The application will store the customized tab order after each edit and reload it when the application starts. </li>
</ol>


 <a name="Additional_Information" class="injected"></a>


# Additional Information

This recipe shows one option for persisting a customized tab bar, using the
Tag values to help order the objects. This solution will ignore the customized
values if a later version of your application has more or fewer tabs than
previously.

You can write your own solution by providing a different implementation for
the FinishedCustomizingViewControllers delegate.

