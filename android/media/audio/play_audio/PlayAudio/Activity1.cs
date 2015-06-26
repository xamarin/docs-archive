using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;

namespace PlayAudio
{
    [Activity (Label = "PlayAudio", MainLauncher = true)]
    public class Activity1 : Activity
    {
        MediaPlayer _player;
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);
            
            _player = MediaPlayer.Create(this, Resource.Raw.test);

            var playButton = FindViewById<Button> (Resource.Id.playButton);
            
            playButton.Click += delegate {
                _player.Start();
            };
        }
    }
}


