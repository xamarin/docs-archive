using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace AppLinksiOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		UINavigationController navController;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			navController = new UINavigationController (new MainViewController());
			window.RootViewController = navController;

			window.MakeKeyAndVisible ();
			return true;
		}

		public override bool OpenUrl (UIApplication app, NSUrl url, string sourceApp, NSObject annotation)
		{
			var rurl = new Rivets.AppLinkUrl (url.ToString ());

			var id = string.Empty;

			if (rurl.InputQueryParameters.ContainsKey("id"))
				id = rurl.InputQueryParameters ["id"];

			if (rurl.InputUrl.Host.Equals ("products") && !string.IsNullOrEmpty (id)) {
				var c = new ProductViewController (id);
				navController.PushViewController (c, true);
				return true;
			} else {
				navController.PopToRootViewController (true);
				return true;
			}
		}
	}
}

