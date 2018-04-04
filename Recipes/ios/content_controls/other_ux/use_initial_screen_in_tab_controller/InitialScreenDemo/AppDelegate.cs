using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace InitialScreenDemo
{
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
	}
}

