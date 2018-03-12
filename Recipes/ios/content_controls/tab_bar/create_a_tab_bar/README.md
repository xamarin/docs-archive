---
id: 1C61F57E-E1F5-8287-3356-387067A85247
title: "Create a Tab Bar"
brief: "This recipe shows you how to create a Tab Bar in your Xamarin.iOS application."
sdk:
  - title: "UITabBarController Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UITabBarController_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To add a tab bar to your Xamarin.iOS application:

<ol start="1">
	<li>Create a subclass of <code>UITabBarController</code>:</li>
</ol>
```
public class TabBarController : UITabBarController {}
```
<ol start="2">
	<li>Create class-level fields for each <code>UIViewController</code> that corresponds to a tab.</li>
</ol>
```
//Declare string for application temp path and tack on the file extension
UIVIewController tab1, tab2, tab3;
```
<ol start="3">
	<li>In the <code>ViewDidLoad</code> method, instantiate each <code>UIViewController</code> and ensure that a Title has been set to display on the tab bar. In this example each view is given a different color, but in your code each one will be a specific subclass of <code>UIViewController</code> (or even a UINavigationController) that performs a specific function. </li>
</ol>
```
tab1 = new UIViewController();
tab1.Title = "Green";
tab1.View.BackgroundColor = UIColor.Green;
tab2 = new UIViewController();
tab2.Title = "Orange";
tab2.View.BackgroundColor = UIColor.Orange;
tab3 = new UIViewController();
tab3.Title = "Red";
tab3.View.BackgroundColor = UIColor.Red;
```
<ol start="4">
	<li>Create an array of <code>UIViewControllers</code> and assign that to the <code>ViewControllers</code> property of the <code>UITabBarController</code>. This creates the items in the tab bar. </li>
</ol>
```
var tabs = new UIViewController []{
	tab1, tab2, tab3
	};
ViewControllers = tabs;
```
<ol start="5">
	<li>Set which tab is selected when the tab bar is displayed: </li>
</ol>
```
SelectedViewController = tab2;
```
<ol start="6">
	<li>Instantiate the <code>TabBarController</code> in your <code>AppDelegate</code> and make it the root view controller of your application </li>
</ol>
```
window.RootViewController = new TabBarController();
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

 <a name="Images" class="injected"></a>


### Images

Tabs should also have an image associated with them, which has been omitted
from the code above for clarity. The image is assigned by setting the TabBarItem
property of each view controller.

Tab bar images are generally 30x30 pixels in size. The alpha (transparency)
values in the source image are used to create the final tab image – blue on
black when it appears in the tab bar and black on white if it appears in the
More list.

You can use a built-in tab image from the `UITabBarSystemItem` enumeration.
This automatically sets the tab’s image and text.

```
tab1.TabBarItem = new UITabBarItem (UITabBarSystemItem.History, 0);
```

Custom images can be set using a different `UITabBarItem` constructor:

```
tab2.TabBarItem = new UITabBarItem ("l'orange", UIImage.FromFile("Images/first.png"), 1);
```

The `TabBarItem`’s properties can also be set directly, like this:

```
tab3.TabBarItem = new UITabBarItem();
tab3.TabBarItem.Image = UIImage.FromFile ("Images/second.png");
tab3.TabBarItem.Title = "Rouge";
```

 <a name="Other_Properties" class="injected"></a>


### Other Properties

The `TabBarItem` also exposes other properties, such as `BadgeValue` (which sets
the little red and white number/circle):

```
tab3.TabBarItem.BadgeValue = "4";
```

and Enabled, which allows you to disable the tab if required:

```
tab3.TabBarItem.Enabled = false;
```

 <a name="Tab_Count" class="injected"></a>


### Tab Count

iPhone/iPod devices can only display five tabs in portrait orientation, the
iPad can display eight. If you create more tabs than the device can display, the
last tab automatically becomes More and the remaining tabs are presented in a
list.

While it is common for an iPhone/iPod application to have more than five
tabs, Apple discourages more than eight tabs on the iPad. This is because the
More list looks very sparse on the larger screen.

