using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

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