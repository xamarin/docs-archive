---
id: 3510AD02-2FC1-A034-9362-B5B0BF1B8EE1
title: "Record Video"
brief: "This recipe shows how to record video using the MediaRecorder class and play it back with the MediaPlayer class."
sdk:
  - title: "MediaRecorder Class Reference" 
    url: http://developer.android.com/reference/android/media/MediaRecorder.html
---

<a name="Recipe" class="injected"></a>

# Recipe

 [ ![](Images/recordvideo.png)](Images/recordvideo.png)

-  Create a new Xamarin.Android application named RecordVideo.
-  Add the `CAMERA`, `RECORD_AUDIO`, and `WRITE_EXTERNAL_STORAGE` permissions to the AndroidManifest.xml.
-  Add the buttons to the Main.axml file to start, stop and play the recording. Also add a `VideoView` for video preview while recording and for playback:


```
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="fill_parent"
    android:layout_height="fill_parent">

    <Button  
        android:id="@+id/Record"
        android:layout_width="fill_parent"
        android:layout_height="wrap_content"
        android:text="Record"/>
     <Button  
        android:id="@+id/Stop"
        android:layout_width="fill_parent"
        android:layout_height="wrap_content"
        android:text="Stop"/>
      <Button  
        android:id="@+id/Play"
        android:layout_width="fill_parent"
        android:layout_height="wrap_content"
        android:text="Play"/>
     <VideoView
        android:id="@+id/SampleVideoView"
        android:layout_width="fill_parent"
        android:layout_height="fill_parent"/>
</LinearLayout>
```

-  In the Activity, add a class variable for the `MediaRecorder`.


```
MediaRecorder recorder;
```

-  In the `OnCreate` method, set the content view and add code to get references to the buttons and the `VideoView`.


```
SetContentView (Resource.Layout.Main);
...
var record = FindViewById<Button> (Resource.Id.Record);
var stop = FindViewById<Button> (Resource.Id.Stop);
var play = FindViewById<Button> (Resource.Id.Play);
var video = FindViewById<VideoView> (Resource.Id.SampleVideoView);
```

-  Set the path where the video file will be written.


```
string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp4";
```

-  In the `Click` event handler for the record button, initialize the recorder and start recording.


```
record.Click += delegate {
video.StopPlayback ();

recorder = new MediaRecorder ();
recorder.SetVideoSource (VideoSource.Camera);
recorder.SetAudioSource (AudioSource.Mic);
recorder.SetOutputFormat (OutputFormat.Default);
recorder.SetVideoEncoder (VideoEncoder.Default);
recorder.SetAudioEncoder (AudioEncoder.Default);
recorder.SetOutputFile (path);
recorder.SetPreviewDisplay (video.Holder.Surface);
recorder.Prepare ();
recorder.Start ();   } ;
```

-  In the `Click` event handler for the stop button, stop the recording and release the recorder.


```
stop.Click += delegate {
if (recorder != null) {
recorder.Stop ();
recorder.Release ();
}
};
```

-  Finally, in the `Click` event handler for the play button, play the video.


```
play.Click += delegate {
var uri = Android.Net.Uri.Parse (path);
video.SetVideoURI (uri);
video.Start ();
};
```

-  Clean up the recorder in `OnDestroy`.


```
protected override void OnDestroy ()
{
base.OnDestroy ();

if (recorder != null) {
recorder.Release ();
recorder.Dispose ();
recorder = null;
}
}
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `MediaRecorder` class sets up a recording state and then records. When
recording stops, the state needs to be reset and reinitialized to record
again.

