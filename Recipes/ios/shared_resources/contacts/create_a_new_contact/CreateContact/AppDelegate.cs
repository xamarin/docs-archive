using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace CreateContact
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        UINavigationController navController;
        CreateContactViewController viewController;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            
            viewController = new CreateContactViewController ();
            navController = new UINavigationController (viewController);
            window.RootViewController = navController;
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}

