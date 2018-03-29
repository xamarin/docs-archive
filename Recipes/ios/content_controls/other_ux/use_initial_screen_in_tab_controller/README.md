---
id: 0DCD1F4A-E228-FAA3-F67A-E82D6963362D
title: "Use Initial Screen in Tab Controller"
brief: "This recipe shows how include the initial screen in a tab controller after a user interacts with the screen."
article:
  - title: "Creating Tabbed Appliations" 
    url: https://developer.xamarin.com/guides/ios/user_interface/creating_tabbed_applications/
---

<a name="Recipe" class="injected"></a>


# Recipe

This code assumes there are three view
controllers, `ViewController1`, `ViewController2`, `ViewController3`,
where `ViewController1` is the initial one shown to the
user.

Add the following code
to `ViewController1`, `aButton` is an
outlet to a button created in Interface Builder:

```
public partial class ViewController1 : UIViewController
{
    public event EventHandler InitialActionCompleted;
    public ViewController1 () : base ("ViewController1", null)
    {
    }
    public override void ViewDidLoad ()
    {
        base.ViewDidLoad ();
        aButton.TouchUpInside += (sender, e) => {
            if (InitialActionCompleted != null) {
                aButton.Hidden = true;
                InitialActionCompleted.Invoke (this, new EventArgs ());
            }
        };
    }
}
```

The event is used to call back to the app delegate when the user touches the
button. The app delegate sets `ViewController1`’s view
as a subview of the root initially and then handles creating
the `UITabController` in the event handler:

```
[Register ("AppDelegate")]
public partial class AppDelegate : UIApplicationDelegate
{
    UIWindow window;
    UIViewController root;
    ViewController1 vc1;
    ViewController2 vc2;
    ViewController3 vc3;
    UITabBarController tabController;
    public override bool FinishedLaunching (UIApplication app, NSDictionary options)
    {
        window = new UIWindow (UIScreen.MainScreen.Bounds);
        root = new UIViewController ();
        vc1 = new ViewController1 ();
        root.View.AddSubview (vc1.View);
        vc1.InitialActionCompleted += (object sender, EventArgs e) => {
            vc1.View.RemoveFromSuperview ();
            tabController = new UITabBarController ();
            vc2 = new ViewController2 ();
            vc3 = new ViewController3 ();
            tabController.ViewControllers = new UIViewController[] {
                vc1,
                vc2,
                vc3
            };
            tabController.ViewControllers [0].TabBarItem.Title = "One";
            tabController.ViewControllers [1].TabBarItem.Title = "Two";
            tabController.ViewControllers [2].TabBarItem.Title = "Three";
            root.AddChildViewController (tabController);
            root.Add (tabController.View);
        };
       window.RootViewController = root;
       window.MakeKeyAndVisible ();
    return true;
}
```

