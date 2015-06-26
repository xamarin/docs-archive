using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace LaunchMaps
{
    [Activity (Label = "LaunchMaps", MainLauncher = true)]
    public class Activity1 : Activity
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.myButton);
            
            button.Click += delegate {
                var geoUri = Android.Net.Uri.Parse ("geo:42.374260,-71.120824");
                var mapIntent = new Intent (Intent.ActionView, geoUri);
                StartActivity (mapIntent);
            };
        }
    }
}


