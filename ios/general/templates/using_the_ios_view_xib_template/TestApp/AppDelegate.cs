using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace TestApp
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		TestAppViewController viewController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new TestAppViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();
			viewController.View.BackgroundColor = UIColor.White;
			return true;
		}
	}
}

