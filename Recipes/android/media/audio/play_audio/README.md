---
id: 1B4B92C7-3A0C-6678-167C-F042E0B3B5B2
title: "Play Audio"
brief: "This recipe shows how to play audio from a raw resource using the MediaPlayer class."
sdk:
  - title: MediaPlayer Class Reference
  - url: http://developer.android.com/reference/android/media/MediaRecorder.html
dateupdated: 2018-02-16
---

<a name="Recipe" class="injected"></a>

# Recipe

 [ ![](Images/playaudio.png)](Images/playaudio.png)

1. Create a new Xamarin.Android application named PlayAudio.

2. Add a sub folder named *raw*
under *Resources*.

3. Add a file named *test.mp3* under *raw*.

4. In the Activity, create a class variable for the `MediaPlayer`.

```
MediaPlayer _player;
```

<ol start="5">
  <li>In the <code>OnCreate</code> method, call <code>MediaPlayer.Create()</code>, passing the context and the resource identifier for the mp3.</li>
</ol>

```
_player = MediaPlayer.Create(this, Resource.Raw.test);
```

<ol start="6">
  <li>Call the <code>Start</code> method of the <code>MediaPlayer</code>.</li>
</ol>

```
_player.Start();
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

When the audio to play is included as a resource, the `MediaPlayer.Create` method can be used to set up the data source to
the audio file and prepare the player for playback automatically. If the audio
were at a location such as on the web or an SD card, the application would have
to set the datasource and call `Prepare` (or `PrepareAsync`) before starting playback.
