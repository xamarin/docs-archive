using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DisplayAnImage
{
    [Activity (Label = "DisplayAnImage", MainLauncher = true)]
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
                var imageView = FindViewById<ImageView> (Resource.Id.demoImageView);
                imageView.SetImageResource (Resource.Drawable.sample2);            
            };
        }
    }
}


