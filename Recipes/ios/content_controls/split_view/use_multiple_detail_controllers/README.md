---
id: 4AA231B2-45A9-8D2A-90D1-3F676CF3ACC1
title: "Use Multiple Detail Controllers"
brief: "This recipe shows how to use multiple detail controllers, where the controller that is loaded changes based upon the row that is selected in the master controller’s table."
---


# Recipe

This recipes uses [MonoTouch.Dialog](https://developer.xamarin.com/guides/ios/user_interface/monotouch.dialog/)
to build the user interface. To include **MonoTouch.Dialog** in your app,
right click on the iOS project's **References** node and select **Edit References**,
then ensure the **MonoTouch.Dialog-1** assembly is ticked.

<ol>
  <li>Create a new solution from the iPad Empty Project template named <code>SplitMultiDetailDemo</code>.</li>
</ol>

<ol start="2">
  <li>Set the <code>RootViewController</code> of the window to a <code>UISplitViewController</code>, which we’ll implement next, in the <code>AppDelegate</code>:</li>
</ol>

```
public partial class AppDelegate : UIApplicationDelegate
{
      UIWindow window;
      SplitViewContoller splitController;
      public override bool FinishedLaunching (UIApplication app, NSDictionary options)
      {
              window = new UIWindow (UIScreen.MainScreen.Bounds);
              splitController = new SplitViewContoller ();
              window.RootViewController = splitController;
              window.MakeKeyAndVisible ();
              return true;
      }
}
```

<ol start="3">
  <li>Add a class called <code>SpiltViewController</code> that inherits from <code>UISplitViewController</code> containing the following implementation:</li>
</ol>

```
public class SplitViewContoller : UISplitViewController
{
      SplitDelegate sd;
      ColorsController colorsController;
      UIViewController redVC, greenVC;
      UIViewController detailContainer;
      public SplitViewContoller () : base()
      {
              sd = new SplitDelegate ();
              Delegate = sd;
              colorsController = new ColorsController ();
              redVC = new UIViewController ();
              redVC.View.BackgroundColor = UIColor.Red;
              colorsController.ColorSelected += (sender, e) => {
                     if (e.Color == "Red") {
                            detailContainer = redVC;
                     } else if (e.Color == "Green") {
                            if (greenVC == null) {
                                   greenVC = new UIViewController ();
                                   greenVC.View.BackgroundColor = UIColor.Green;
                            }
                            detailContainer = greenVC;
                    }
                    ViewControllers = new UIViewController[] {
                        colorsController,
                        detailContainer
                   };
            };
            detailContainer = redVC;
            ViewControllers = new UIViewController[] {
                     colorsController,
                     detailContainer
             };
      }
      public override void ViewDidLoad ()
      {
              base.ViewDidLoad ();
      }
      class SplitDelegate : UISplitViewControllerDelegate
      {
              public override bool ShouldHideViewController (UISplitViewController svc,
        UIViewController viewController, UIInterfaceOrientation inOrientation)
              {
                     return false;
              }
      }
}
```

<ol start="4">
  <li>Add a new class called <code>ColorsController</code> that inherits from <code>DialogViewController</code>
  (you may need to add <code>using MonoTouch.Dialog;</code> to the top of the page, and ensure the
  <b>MonoTouch.Dialog-1</b> assembly is referenced, as described at the top of this page).
  This class will be the master controller in the master-detail scenario.</li>
  <li>Add the following code for the <code>ColorsController</code>:</li>
</ol>
```
using MonoTouch.Dialog;
...
public class ColorsController : DialogViewController
{
      public event EventHandler<ColorSelectedEventArgs> ColorSelected;
      List<string> colors = new List<string>{"Red", "Green"};
      public List<string> Colors {
              get {
                     return colors;
              }
      }
      public ColorsController () : base (null)
      {
              Root = new RootElement ("Colors") {
        new Section () {
            from color in colors
                select (Element) new StringElement(color, () => {
                    if(ColorSelected != null)
                        ColorSelected(this, new ColorSelectedEventArgs{Color = color});
                })
        }
    };
      }
}
public class ColorSelectedEventArgs : EventArgs
{
      public string Color { get; set; }
}
```

<ol start="7">
  <li>Run the project and select a row in the master table and a new controller instance will load for the detail.</li>
</ol>

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `UISplitViewController` sets the master and detail controllers
based upon the controllers added to the `ViewControllers` array. By
raising an event from the master controller, the `UISplitViewController` can reload this array, keeping the same instance of the master controller but changing the detail controller in the array as needed.

