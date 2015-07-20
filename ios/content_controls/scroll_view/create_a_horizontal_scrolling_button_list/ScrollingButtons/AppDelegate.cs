using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;

using Foundation;
using UIKit;

namespace ScrollingButtons
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        UIViewController vc;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {     
            window = new UIWindow (UIScreen.MainScreen.Bounds);          
            vc = new ScrollingButtonsController ();
            window.RootViewController = vc;
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}