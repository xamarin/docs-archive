using System;

using Xamarin.Forms;

namespace iPadFormatting
{
	public class App : Application
	{
		public App ()
		{
			var tabs = new TabbedPage ();
			// Icons are only in the iOS project
			tabs.Children.Add (new NavigationPage(new ListViewPage()){Title = "ListView", Icon="glyphish_wand"});
			tabs.Children.Add (new NavigationPage(new TableViewPage()){Title = "TableView", Icon="glyphish_palm"});

			// The root page of your application
			MainPage = tabs;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

