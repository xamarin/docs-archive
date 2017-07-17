using System;

using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Content;

namespace ImageButtonDemo
{
    [Activity (Label = "ImageButtonDemo", MainLauncher = true, Theme = "@style/Theme.MyTheme")]
    public class Activity1 : Android.Support.V7.App.AppCompatActivity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.Main);

            //AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            DisplayImageInButton();
        }

        void DisplayImageInButton() 
        {
            var button = FindViewById<ImageButton>(Resource.Id.myButton3);
			var tv = FindViewById<TextView>(Resource.Id.dynamic_load_textview);
			
            if (Build.VERSION.SdkInt >=  BuildVersionCodes.Lollipop ) 
            {
                // Android 5.0 and higher can load the SVG image from resources.
                var image = ContextCompat.GetDrawable(ApplicationContext, Resource.Drawable.ic_mood_black);
				button.SetImageDrawable(image);
                tv.SetText(Resource.String.dynamically_loaded_image);
			}
            else 
            {
                // Devices running < Android 5.0 should use the VectorDrawableCompat
                var image = Android.Support.Graphics.Drawable.VectorDrawableCompat.Create(this.Resources, Resource.Drawable.ic_sentiment_neutral_black_24dp, null);
				button.SetImageDrawable(image);
                tv.SetText(Resource.String.dynamically_loaded_image_compat);
			}
        }
    }
}


