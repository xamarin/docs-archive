using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Xamarin.Forms;

namespace CustomizeTabs.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : 
	global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate 
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// sets the color of the selected tab image & text
			UITabBar.Appearance.SelectedImageTintColor = UIColor.Brown;

			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new CustomizeTabs.App ());  

			return base.FinishedLaunching (app, options);
		}
	}
}
