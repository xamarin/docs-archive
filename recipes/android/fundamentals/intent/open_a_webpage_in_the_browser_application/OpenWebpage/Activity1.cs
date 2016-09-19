using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace OpenWebpage
{
    [Activity (Label = "OpenWebpage", MainLauncher = true)]
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
            
            button.Click += (sender, e) => {
                
                var uri = Android.Net.Uri.Parse ("http://www.xamarin.com");
                var intent = new Intent (Intent.ActionView, uri);  
                StartActivity (intent);
            };
        }
    }
}


