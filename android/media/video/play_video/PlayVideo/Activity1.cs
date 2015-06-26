using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PlayVideo
{
    [Activity (Label = "PlayVideo", MainLauncher = true)]
    public class Activity1 : Activity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
   
            var videoView = FindViewById<VideoView> (Resource.Id.SampleVideoView);
   
            var uri = Android.Net.Uri.Parse ("http://ia600507.us.archive.org/25/items/Cartoontheater1930sAnd1950s1/PigsInAPolka1943.mp4");
            
            videoView.SetVideoURI (uri);
			videoView.Visibility = ViewStates.Visible;
            videoView.Start ();   
        }
    }
}


