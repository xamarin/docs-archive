using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace SplitMultiDetailDemo
{
	[Register ("AppDelegate")]
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
}

