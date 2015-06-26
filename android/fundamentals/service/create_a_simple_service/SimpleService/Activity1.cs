using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SimpleService
{
    [Activity (Label = "SimpleService", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var start = FindViewById<Button> (Resource.Id.startService);
            
            start.Click += delegate {
                StartService (new Intent (this, typeof(SimpleService)));
            };
            
            var stop = FindViewById<Button> (Resource.Id.stopService);
            
            stop.Click += delegate {
                StopService (new Intent (this, typeof(SimpleService)));
            };
        }
        
     
    }
}


