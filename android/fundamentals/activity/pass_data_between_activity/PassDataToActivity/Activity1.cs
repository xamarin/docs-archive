using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PassDataToActivity
{
    [Activity (Label = "PassDataToActivity", MainLauncher = true)]
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
                var intent = new Intent (this, typeof(Activity2));
                intent.PutExtra ("MyData", "Data from Activity1");
                StartActivity (intent);
            };
        }
    }
}


