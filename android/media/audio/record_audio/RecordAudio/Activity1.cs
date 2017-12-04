using System;
using System.IO;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Java.Lang;

namespace RecordAudio
{
    [Activity(Label = "RecordAudio", MainLauncher = true)]
    public class Activity1 : AppCompatActivity
    {
        static readonly string TAG = typeof(Activity1).FullName;
        MediaRecorder _recorder;
        MediaPlayer _player;
        Button _startButton;
        Button _stopButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _startButton = FindViewById<Button>(Resource.Id.start);
            _startButton.Click += StartRecording_ClickHandler;

            _stopButton = FindViewById<Button>(Resource.Id.stop);
            _stopButton.Click += StopRecording_ClickHandler;
        }

        void StartRecording_ClickHandler(object sender, EventArgs e)
        {
            if (this.HasPermissionToRecord())
            {
                RecordAudio();
            }
            else
            {
                this.PerformRuntimePermissionsCheckForRecording();
            }
        }

        void RecordAudio()
        {
            string filenameOfRecording = this.GetFileNameForRecording();
            if (this.IsExternalStorageWriteable())
            {
                if (File.Exists(filenameOfRecording))
                {
                    File.Delete(filenameOfRecording);
                }

                _stopButton.Enabled = true;
                _startButton.Enabled = false;

                StartRecordingAudioTo(filenameOfRecording);
            }
            else
            {
                throw new ApplicationException("External storage is not available, cannot write to " + filenameOfRecording);
            }
        }

        /// <summary>
        /// This method will start the recording. It assumes that 
        /// Permissions have been granted and that it is possible to write to 
        /// the specificied location.
        /// </summary>
        /// <param name="fileName">Name of the file for the recording.</param>
        void StartRecordingAudioTo(string fileName)
        {

            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
            _recorder.SetOutputFile(fileName);

            _recorder.Prepare();
            _recorder.Start();

        }

        void StopRecording_ClickHandler(object sender, EventArgs e)
        {
            if (_recorder == null)
            {
                return;
            }

            bool playback = true;
            try
            {
                _recorder.Stop();
            }
            catch (IllegalStateException ise) 
            {
                Log.Debug(TAG, "Seems that we're trying to stop the recording before it's started?");
                playback = false;
            }
            _recorder.Reset();

            string file = this.GetFileNameForRecording();
            if (File.Exists(file) && playback)
            {
                _player.SetDataSource(this.GetFileNameForRecording());
                _player.Prepare();
                _player.Start();
            }
        }

        void ResetAudioPlayer_Handler(object sender, EventArgs e)
        {
            _player.Reset();
            _stopButton.Enabled = false;
            _startButton.Enabled = true;
        }

        protected override void OnResume()
        {
            base.OnResume();

            _recorder = new MediaRecorder();
            _player = new MediaPlayer();
            _player.Completion+= ResetAudioPlayer_Handler;
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (_player != null)
            {
                _player.Completion -= ResetAudioPlayer_Handler;
                _player.Release();
                _player.Dispose();

            }

            if (_recorder != null)
            {
                _recorder.Release();
                _recorder.Dispose();
            }

            _player = null;
            _recorder = null;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == PermissionExtensions.REQUEST_ALL_PERMISSIONS)
            {
                if (grantResults.AllPermissionsGranted())
                {
                    RecordAudio();
                }
            }
        }
    }
}