using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Buttons
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;
		UIViewController viewController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			bool useXIB = true;
			if (useXIB)     // uses Interface Builder XIB file
				viewController = new ButtonsScreen_iPhone ();
			else // uses c# and controls created in code
				viewController = new ButtonsViewController ();

			navigationController = new UINavigationController ();
			navigationController.PushViewController (viewController, false);

			window.RootViewController = navigationController;

			// make the window visible
			window.MakeKeyAndVisible ();

			return true;
		}
	}
}

