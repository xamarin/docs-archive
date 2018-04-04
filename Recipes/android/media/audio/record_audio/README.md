---
id: 1426450D-F53B-6708-2D79-DAC055668532
title: "Record Audio"
brief: "This recipe shows how to record audio using the MediaRecorder class and play it back with the MediaPlayer class."
sdk:
  - title: MediaRecorder Class Reference
  - url: http://developer.android.com/reference/android/media/MediaRecorder.html
dateupdated: 2018-02-16
---

This recipe will demonstrate how to use the **Android.Media.MediaRecorder** class to record audio on a physical device. It will not discuss how to perform run-time permission checks in Android

 [ ![](Images/recordaudio.png)](Images/recordaudio.png)

The logic for permission checking and for determining the location of the sound file maybe be found in the file **RecordAudioExtensions.cs** that is up on GitHub.

<a name="requirements"></a>
# Requirements

This recipe requires an Android device running Android 6.0 (API level 23) or higher. Familiarity with run-time permission checks in Androi 6.0 is also required.

> ⚠️ The Android Emulator cannot record audio. This recipe must be run on an Android device.

<a name="Recipe" ></a>

# Recipe
-  Create a new Xamarin.Android application named **RecordAudio**.
-  Add the  `RECORD_AUDIO` and `WRITE_EXTERNAL` permissions to the **AndroidManifest.xml**.
-  Add two buttons to the **Main.axml** file to start and stop recording.

        <?xml version="1.0" encoding="utf-8"?>
        <LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
            android:orientation="vertical"
            android:layout_width="fill_parent"
            android:layout_height="fill_parent"
            android:paddingStart="16dp"
            android:paddingEnd="16dp"
            android:paddingTop="48dp">
            <Button
                android:id="@+id/start"
                android:layout_width="fill_parent"
                android:layout_height="wrap_content"
                android:text="@string/start_recording"
                android:layout_marginBottom="16dp" />
            <Button
                android:id="@+id/stop"
                android:layout_width="fill_parent"
                android:layout_height="wrap_content"
                android:text="@string/start_playing"
                android:layout_marginTop="16dp" />
        </LinearLayout>

-  In  `Activity1`, add class variables for a `MediaRecorder` and `MediaPlayer`. Also add class variables for the two buttons, and some flags to so that we can track.

        Button _recordButton;
        MediaRecorder _recorder;

        Button _playbackButton;
        MediaPlayer _player;

        bool _permissionsAccepted = false;
        bool _startPlaying = false;
        bool _startRecording = true;

-  In the `OnCreate` method, set the content view and add code to get references to the buttons. The methods that are assigned to the `Click` events will be discussed in the following steps.

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _recordButton = FindViewById<Button>(Resource.Id.start);
            _recordButton.Click += OnRecordButtonClick;

            _playbackButton = FindViewById<Button>(Resource.Id.stop);
            _playbackButton.Click += OnPlayButtonClick;
        }


- Override the `OnRequestPermissionResult` method of `Activity1` to handle the results of the run-time permission check. If the user does not grant all the necessary permissions, then the application will quit.

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RecordAudioExtensions.REQUEST_ALL_PERMISSIONS)
            {
                bool permissionsGranted = false;
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

    The method `OnRecord` will be provided below.

-  Create the event handler for the `Click` event of the `_recordButton`. This method will check to see if the user has granted all the necessary permissions for recording the audio.  If the user has not granted permissions, then the run-time permission check will be initiated. If the user has granted permissions and depending on the state of the recording workflow, the app will either start or stop the recording:


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

-  The `OnRecord` method and it's associated code is responsible for initializing a `MediaRecorder` object (when starting a recording) or for freeing up resources (when the recording is halted):

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


-  The `OnPlayButtonClick` method is assigned to the `Click` event of the `_playbackButton`:

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

    The `OnPlay` method and it's associated code is responsible for playing back the recorded audio:

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

-  Release the `MediaRecorder` and `MediaPlayer` instances when they are no longer needed, such as in the `OnPause` method of the `Activity`.

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


 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `MediaRecorder` class sets up a recording state and then records. When
recording stops, the state needs to be reset and reinitialized to record again.
Similarly, for the `MediaPlayer` to play back audio subsequent times, its `Reset`
method must be called after it stops, and its data source must be reinitialized
and prepared each time.
