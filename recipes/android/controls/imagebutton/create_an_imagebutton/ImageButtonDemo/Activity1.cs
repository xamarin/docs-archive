using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ImageButtonDemo
{
    [Activity (Label = "ImageButtonDemo", MainLauncher = true)]
    public class Activity1 : Activity
    {
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var button = FindViewById<ImageButton> (Resource.Id.myButton);
            
            button.Click += delegate {
                Console.WriteLine ("button clicked");
            };
        }
    }
}


