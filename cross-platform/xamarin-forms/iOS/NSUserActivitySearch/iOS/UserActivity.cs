using CoreSpotlight;
using Foundation;
using NSUserActivitySearch.iOS;
using Xamarin.Forms;

[assembly:Dependency (typeof(UserActivity))]
namespace NSUserActivitySearch.iOS
{
	public static class ActivityTypes
	{
		public const string View = "com.companyname.nsuseractivitysearch.activity.view";
	}

	public class UserActivity : IUserActivity
	{

		public void Start (TodoItem item)
		{
			var window = NSUserActivitySearch.iOS.AppDelegate.Current.window;
			window.UserActivity = CreateActivity (item);

			// Force the UserInfo to be preserved for AppDelegate.ContinueUserActivity
			// https://forums.developer.apple.com/thread/9690
			window.UpdateUserActivityState (CreateActivity (item));
		}

		public void Stop ()
		{
			var window = NSUserActivitySearch.iOS.AppDelegate.Current.window;
			window.UserActivity.ResignCurrent ();
		}

		NSUserActivity CreateActivity (TodoItem item)
		{
			// Create app search activity
			var activity = new NSUserActivity (ActivityTypes.View);

			// Populate activity
			activity.Title = item.Name;

			// Add additional data
			var attributes = new CSSearchableItemAttributeSet ();
			attributes.DisplayName = item.Name;
			attributes.ContentDescription = item.Notes;

			activity.ContentAttributeSet = attributes;
			activity.AddUserInfoEntries (NSDictionary.FromObjectAndKey (new NSString (item.ID), new NSString ("Id")));

			// Add app search ability
			activity.EligibleForSearch = true;
			activity.BecomeCurrent ();	// Don't forget to ResignCurrent() later

			return activity;
		}
	}
}
