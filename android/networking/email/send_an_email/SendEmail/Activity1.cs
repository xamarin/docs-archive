using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace SendEmail
{
    [Activity (Label = "SendEmail", MainLauncher = true)]
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
              
                var email = new Intent (Android.Content.Intent.ActionSend);
                email.PutExtra(Android.Content.Intent.ExtraEmail, new string[]{"person1@xamarin.com", "person2@xamrin.com"});
                email.PutExtra(Android.Content.Intent.ExtraCc, new string[]{"person3@xamarin.com"});
                email.PutExtra(Android.Content.Intent.ExtraSubject, "Hello Email");
                email.PutExtra(Android.Content.Intent.ExtraText, "Hello from Xamarin.Android!");
                email.SetType("message/rfc822");
                StartActivity(email);
            };
 
        }
    }
}


