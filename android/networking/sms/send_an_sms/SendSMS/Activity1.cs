using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;

namespace SendSMS
{
    [Activity (Label = "SendSMS", MainLauncher = true)]
    public class Activity1 : Activity
    {

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var sendSMS = FindViewById<Button> (Resource.Id.sendSMS);
            
            sendSMS.Click += (sender, e) => {
                SmsManager.Default.SendTextMessage ("1234567890", null, "hello from Xamarin.Android", null, null);
            };
            
            var sendSMSIntent = FindViewById<Button> (Resource.Id.sendSMSIntent);
            
            sendSMSIntent.Click += (sender, e) => {
                var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
                var smsIntent = new Intent (Intent.ActionSendto, smsUri);
                smsIntent.PutExtra ("sms_body", "hello from Xamarin.Android");  
                StartActivity (smsIntent);
            };
        }
        
    }
}