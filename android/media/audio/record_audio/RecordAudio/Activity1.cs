using System;
using System.IO;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Widget;

namespace RecordAudio
{
    [Activity(Label = "RecordAudio", MainLauncher = true)]
    public class Activity1 : Activity
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
                StartRecordingAudioTo(filenameOfRecording);
            }
            else
            {
                _recorder = null;
                _player = null;
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
            _stopButton.Enabled = true;
            _startButton.Enabled = false;

            if (_recorder == null)
            {
                _recorder = new MediaRecorder();
            }

            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
            _recorder.SetOutputFile(fileName);

            _recorder.Prepare();
            _recorder.Start();

        }

        void StopRecording_ClickHandler(object sender, EventArgs e)
        {
            _stopButton.Enabled = false;
            _startButton.Enabled = true;

            _recorder.Stop();
            _recorder.Reset();

            string file = this.GetFileNameForRecording();
            if (File.Exists(file))
            {
                _player.SetDataSource(this.GetFileNameForRecording());
                _player.Prepare();
                _player.Start();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            _recorder = new MediaRecorder();
            _player = new MediaPlayer();

            _player.Completion += (sender, e) =>
            {
                _player.Reset();
                _startButton.Enabled = !_startButton.Enabled;
            };
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (_player != null)
            {
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