using System;
using System.Collections.Generic;
using CoreSpotlight;
using Foundation;
using UIKit;
using Xamarin.Forms;

namespace CoreSpotlightSearch.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		List<TodoItem> todoItems;

		public SpotlightSearch SpotlightSearch { get; private set; }

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			App.Speech = new Speech ();
			LoadApplication (new App ());
			todoItems = App.TodoManager.All;

			if (UIDevice.CurrentDevice.CheckSystemVersion (9, 0)) {
				SpotlightSearch = new SpotlightSearch (todoItems);
			} else {
				throw new NotSupportedException ("CoreSpotlight not supported");
			}

			return base.FinishedLaunching (app, options);
		}

		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			if (userActivity.ActivityType == CSSearchableItem.ActionType) {
				string id = userActivity.UserInfo.ObjectForKey (CSSearchableItem.ActivityIdentifier).ToString ();
				if (!string.IsNullOrEmpty (id)) {
					MessagingCenter.Send<CoreSpotlightSearch.App, string> (Xamarin.Forms.Application.Current as CoreSpotlightSearch.App, "ShowItem", id);
				}
			}
			return true;
		}
	}
}
