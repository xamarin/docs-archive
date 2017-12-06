using System;
using System.IO;
using Android.App;
using Android.Media;
using Android.OS;
using Android.Support.Design.Widget;
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

        Button _recordButton;
        MediaRecorder _recorder;

        Button _playbackButton;
        MediaPlayer _player;

        bool _startPlaying = false;
        bool _startRecording = true;


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            bool permissionsGranted = false;

            if (requestCode == RecordAudioExtensions.REQUEST_ALL_PERMISSIONS)
            {
                if (grantResults.AllPermissionsGranted())
                {
                    permissionsGranted = true;
                }
                else
                {
                    permissionsGranted = false;
                }

                if (permissionsGranted)
                {
                    OnRecord(_startRecording);
                }
                else
                {
                    var snackbar = Snackbar.Make(this.GetLayoutForSnackbar(), Resource.String.missing_permissions, Snackbar.LengthIndefinite);
                    snackbar.SetAction(Resource.String.ok, (obj) => { Finish(); });
                }
            }
        }

        void OnRecord(bool start)
        {
            if (start)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }

        void StartRecording()
        {
            _recorder = new MediaRecorder();
            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
            _recorder.SetOutputFile(this.GetFileNameForRecording());
            _recorder.SetAudioEncoder((AudioEncoder.AmrNb));

            try
            {
                _recorder.Prepare();
            }
            catch (IOException ioe)
            {
                Log.Error(TAG, ioe.ToString());
            }

            _recorder.Start();
        }


        void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }
            _recorder.Stop();
            _recorder.Release();
            _recorder = null;

            if (File.Exists(this.GetFileNameForRecording()))
            {
                _startPlaying = true;
            }
        }

        void OnPlay(bool start)
        {
            if (start)
            {
                if (File.Exists(this.GetFileNameForRecording()))
                {
                    StartPlaying();
                }
                else
                {
                    _startPlaying = false;
                    _startRecording = true;
                    Toast.MakeText(this, Resource.String.record_sound_first, ToastLength.Long).Show();
                }
            }
            else
            {
                StopPlaying();
            }
        }


        void StartPlaying()
        {
            _player = new MediaPlayer();
            try
            {
                _player.SetDataSource(this.GetFileNameForRecording());
                _player.Prepare();
                _player.Start();
            }
            catch (IOException e)
            {
                Log.Error(TAG, "There was an error trying to start the MediaPlayer!");
                Toast.MakeText(this, Resource.String.unexpected_playback_error, ToastLength.Long).Show();
            }
        }

        void StopPlaying()
        {
            if (_player == null)
            {
                return;
            }
            _player.Release();
            _player = null;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _recordButton = FindViewById<Button>(Resource.Id.start);
            _recordButton.Click += OnRecordButtonClick;

            _playbackButton = FindViewById<Button>(Resource.Id.stop);
            _playbackButton.Click += OnPlayButtonClick;
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (_recorder != null)
            {
                _recorder.Release();
                _recorder = null;
            }
            if (_player != null)
            {
                _player.Release();
                _player = null;
            }
        }

        void OnRecordButtonClick(object sender, EventArgs e)
        {
            if (this.HasPermissionToRecord())
            {
                OnRecord(_startRecording);
                if (_startRecording)
                {
                    _recordButton.SetText(Resource.String.stop_recording);
                }
                else
                {
                    _recordButton.SetText(Resource.String.start_recording);
                }
                _startRecording = !_startRecording;
            }
            else
            {
                this.PerformRuntimePermissionsCheckForRecording();
            }
        }

        void OnPlayButtonClick(object sender, EventArgs e)
        {
            OnPlay(_startPlaying);
            if (_startPlaying)
            {
                _playbackButton.SetText(Resource.String.stop_playing);
            }
            else
            {
                _playbackButton.SetText(Resource.String.start_playing);
            }
            _startPlaying = !_startPlaying;
        }
    }

}