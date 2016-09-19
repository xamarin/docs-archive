using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppLinksAndroid
{
	[Activity (Label = "App Links Sample", MainLauncher = true)]
	public class MainActivity : Activity
	{
		const string YOUR_URL = "http://location/of/your/html/file.html";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			button.Text = YOUR_URL;

			button.Click += delegate {

				// App Links Navigation
				Rivets.AppLinks.Navigator.Navigate(YOUR_URL);

			};
		}
	}
}


