using Foundation;
using UIKit;
using Xamarin.Forms;

namespace NSUserActivitySearch.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public static AppDelegate Current { get; private set; }

		public UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			Current = this;
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			App.Speech = new Speech ();
			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		public override bool ContinueUserActivity (UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
		{
			if (userActivity.ActivityType == ActivityTypes.View) {
				string id = userActivity.UserInfo.ObjectForKey (new NSString ("Id")).ToString ();
				if (!string.IsNullOrWhiteSpace (id)) {
					MessagingCenter.Send<NSUserActivitySearch.App, string> (Xamarin.Forms.Application.Current as NSUserActivitySearch.App, "ShowItem", id);
				}
			}
			return true;
		}
	}
}

