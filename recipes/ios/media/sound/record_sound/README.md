---
id:{D73A5C97-CB9C-0556-8D66-9AD37F178CFA}  
title:Record Sound  
brief:This recipe illustrates how to record a sound file from the active audio input (either the built-in microphone or the audio input) using the AVAudioRecorder class.  
samplecode:[Browse on GitHub](https://github.com/xamarin/recipes/tree/master/ios/media/sound/record_sound)  
sdk:[Multimedia Programming Guide](https://developer.apple.com/library/ios/#documentation/AudioVideo/Conceptual/MultimediaPG/Introduction/Introduction.html)  
sdk:[AVAudioRecorder](https://developer.apple.com/library/ios/#documentation/AVFoundation/Reference/AVAudioRecorder_ClassReference/Reference/Reference.html)  
---

<a name="Recipe" class="injected"></a>


# Recipe

The easiest way to record sound in iOS is to use the built-in `AVAudioRecorder`
class:

1. Declare class-level variables:
```
AVAudioRecorder recorder;
NSError error;
NSUrl url;
NSDictionary settings;
```

<ol start="2">
  <li>You must initialize an audio session before trying to record:</li>
</ol>

```
var audioSession = AVAudioSession.SharedInstance ();
var err = audioSession.SetCategory (AVAudioSessionCategory.PlayAndRecord);
if (err != null) {
    Console.WriteLine ("audioSession: {0}", err);
    return false;
}
err = audioSession.SetActive (true);
if (err != null ){
    Console.WriteLine ("audioSession: {0}", err);
    return false;
}
```
<ol start="3">
  <li>Specify the recording format and location to save the recording to. The
  recording format is specified as an <code>NSDictionary</code> with two <code>NSObject</code> arrays
  containing the keys and values of the format specification:</li>
</ol>

```
//Declare string for application temp path and tack on the file extension
string fileName = string.Format ("Myfile{0}.wav", DateTime.Now.ToString ("yyyyMMddHHmmss"));
string audioFilePath = Path.Combine (Path.GetTempPath (), fileName);

Console.WriteLine("Audio File Path: " + audioFilePath);

url = NSUrl.FromFilename(audioFilePath);
//set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
NSObject[] values = new NSObject[]
{
    NSNumber.FromFloat (44100.0f), //Sample Rate
    NSNumber.FromInt32 ((int)AudioToolbox.AudioFormatType.LinearPCM), //AVFormat
    NSNumber.FromInt32 (2), //Channels
    NSNumber.FromInt32 (16), //PCMBitDepth
    NSNumber.FromBoolean (false), //IsBigEndianKey
    NSNumber.FromBoolean (false) //IsFloatKey
};

//Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
NSObject[] keys = new NSObject[]
{
    AVAudioSettings.AVSampleRateKey,
    AVAudioSettings.AVFormatIDKey,
    AVAudioSettings.AVNumberOfChannelsKey,
    AVAudioSettings.AVLinearPCMBitDepthKey,
    AVAudioSettings.AVLinearPCMIsBigEndianKey,
    AVAudioSettings.AVLinearPCMIsFloatKey
};

//Set Settings with the Values and Keys to create the NSDictionary
settings = NSDictionary.FromObjectsAndKeys (values, keys);

//Set recorder parameters
recorder = AVAudioRecorder.Create(url, new AudioSettings(settings), out error);

//Set Recorder to Prepare To Record
recorder.PrepareToRecord();</code></pre>
```
<ol start="4">
  <li>Call <code>PrepareToRecord</code>, which initializes the recording framework.</li>
</ol>

```
//Set Recorder to Prepare To Record
recorder.PrepareToRecord();
```

<ol start="5">
  <li>Call <code>Record</code> when you’re ready to begin recording the audio.</li>
</ol>
```
recorder.Record();
```

<ol start="6">
  <li>When you’re finished recording call the <code>Stop</code> method on the
  recorder:</li>
</ol>
```
recorder.stop();
```
# Additional Information

iOS has a number of powerful frameworks for working with audio. The technique
shown in this recipe is one of the simplest, but there are a number of others
that serve different purposes. For more information, see the [Using Audio](https://developer.apple.com/library/ios/#documentation/AudioVideo/Conceptual/MultimediaPG/UsingAudio/UsingAudio.html) section of the [Multimedia Programming Guide](https://developer.apple.com/library/ios/#documentation/AudioVideo/Conceptual/MultimediaPG/Introduction/Introduction.html) in the Apple iOS
documentation.