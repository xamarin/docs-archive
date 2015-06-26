using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Media;
using System.IO;

namespace RecordAudio
{
    [Activity (Label = "RecordAudio", MainLauncher = true)]
    public class Activity1 : Activity
    {
        MediaRecorder _recorder;
        MediaPlayer _player;
        Button _start;
        Button _stop;
     
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);
         
            _start = FindViewById<Button> (Resource.Id.start);
            _stop = FindViewById<Button> (Resource.Id.stop);
                    
            string path = "/sdcard/test.3gpp";
         
            _start.Click += delegate {
                _stop.Enabled = !_stop.Enabled;
                _start.Enabled = !_start.Enabled;
                
                _recorder.SetAudioSource (AudioSource.Mic);      
                _recorder.SetOutputFormat (OutputFormat.ThreeGpp);            
                _recorder.SetAudioEncoder (AudioEncoder.AmrNb);           
                _recorder.SetOutputFile (path);
                _recorder.Prepare ();               
                _recorder.Start ();
            };
         
            _stop.Click += delegate {
                _stop.Enabled = !_stop.Enabled;
    
                _recorder.Stop ();
                _recorder.Reset ();
             
                _player.SetDataSource (path);
                _player.Prepare ();
                _player.Start ();
            };       
        }
     
        protected override void OnResume ()
        {
            base.OnResume ();
            
            _recorder = new MediaRecorder ();
            _player = new MediaPlayer ();
         
            _player.Completion += (sender, e) => {
                _player.Reset ();
                _start.Enabled = !_start.Enabled;
            };
        }

        protected override void OnPause ()
        {
            base.OnPause ();
            
            _player.Release ();
            _recorder.Release ();
         
            _player.Dispose ();
            _recorder.Dispose ();
            _player = null;
            _recorder = null;
        }
    }
}


