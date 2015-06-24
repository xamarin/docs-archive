using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace EditTextDemo
{
    [Activity (Label = "EditTextDemo", MainLauncher = true)]
    public class Activity1 : Activity
    {


        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var editText = FindViewById<EditText> (Resource.Id.editText);
            var textView = FindViewById<TextView> (Resource.Id.textView);
            
            editText.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {      
                textView.Text = e.Text.ToString ();
            };
        }
    }
}


