using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace AlertControllerSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		
		UIWindow window;



		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			UIViewController initialViewController;

			initialViewController = new AlertControllerSampleViewController();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			window.RootViewController = initialViewController;
			window.MakeKeyAndVisible ();
			return true;
		}
	}
}

