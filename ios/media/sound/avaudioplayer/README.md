---
title: "Playing Sound with AVAudioPlayer"
dateupdated: 2016-02-01
brief: "This sample shows how to use a helper class to control the playback of sound using an AVAudioPlayer."
sdk:
  - title: "AVAudioPlayer Reference" 
    url: https://developer.apple.com/library/ios/documentation/AVFoundation/Reference/AVAudioPlayerClassReference/
---

# About the AVAudioPlayer

The `AVAudioPlayer` is used to playback audio data from either memory or a file. Apple recommends using this class to play audio in your app unless you are doing network streaming or require low latency audio I/O.

You can use the `AVAudioPlayer` to do the following:

* Play sounds of any duration with optional looping.
* Play multiple sounds at the same time with optional synchronization.
* Control volume, playback rate and stereo positioning for each sounds playing.
* Support features such as fast forward or rewind.
* Obtain playback level metering data.

`AVAudioPlayer` supports sounds in any audio format provided by iOS, tvOS and OS X such as `.aif`, `.wav` or `.mp3`.

# Recipe

First, create a Single View Application for Xamarin.iOS and call it `AVAudioPlayerSounds`. Add a `Sounds` folder to the project and add a few sounds files to the folder (ensure to select `BundleResource` as the **Build Action**):

[ ![](Images/Sound01.png)](Images/Sound01.png)

Next, add a `Classes` folder to the project and add a new `GameAudioManager` class to the folder and make it look like the following:

```
using System;
using AVFoundation;
using AudioToolbox;
using Foundation;
using UIKit;

namespace AudioToolbox
{
	public class GameAudioManager
	{
		#region Private Variables
		private AVAudioPlayer backgroundMusic;
		private AVAudioPlayer soundEffect;
		private string backgroundSong="";
		#endregion

		#region Computed Properties
		public float BackgroundMusicVolume {
			get { return backgroundMusic.Volume; }
			set { backgroundMusic.Volume = value; }
		}

		public bool MusicOn { get; set; } = true;
		public float MusicVolume { get; set; } = 0.5f;

		public bool EffectsOn { get; set; } = true;
		public float EffectsVolume { get; set; } = 1.0f;
		#endregion

		#region Constructors
		public GameAudioManager ()
		{
			// Initialize
			ActivateAudioSession();
		}
		#endregion

		#region Public Methods
		public void ActivateAudioSession(){
			// Initialize Audio
			var session = AVAudioSession.SharedInstance(); 
			session.SetCategory(AVAudioSessionCategory.Ambient);
			session.SetActive(true);
		}

		public void DeactivateAudioSession(){
			var session = AVAudioSession.SharedInstance(); 
			session.SetActive(false);
		}

		public void ReactivateAudioSession(){
			var session = AVAudioSession.SharedInstance(); 
			session.SetActive(true);
		}

		public void PlayBackgroundMusic(string filename){
			NSUrl songURL;

			// Music enabled?
			if (!MusicOn) return;

			// Any existing background music?
			if (backgroundMusic!=null) {
				//Stop and dispose of any background music
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}

			// Initialize background music
			songURL = new NSUrl("Sounds/"+filename);
			NSError err;
			backgroundMusic = new AVAudioPlayer (songURL, "wav", out err);
			backgroundMusic.Volume = MusicVolume;
			backgroundMusic.FinishedPlaying += delegate { 
				// backgroundMusic.Dispose(); 
				backgroundMusic = null;
			};
			backgroundMusic.NumberOfLoops=-1;
			backgroundMusic.Play();
			backgroundSong=filename;

		}

		public void StopBackgroundMusic(){

			// If any background music is playing, stop it
			backgroundSong="";
			if (backgroundMusic!=null) {
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}
		}

		public void SuspendBackgroundMusic(){

			// If any background music is playing, stop it
			if (backgroundMusic!=null) {
				backgroundMusic.Stop();
				backgroundMusic.Dispose();
			}
		}

		public void RestartBackgroundMusic(){

			// Music enabled?
			if (!MusicOn) return;

			// Was a song previously playing?
			if (backgroundSong=="") return;

			// Restart song to fix issue with wonky music after sleep
			PlayBackgroundMusic(backgroundSong);
		}

		public void PlaySound(string filename){
			NSUrl songURL;

			// Music enabled?
			if (!EffectsOn) return;

			// Any existing sound effect?
			if (soundEffect!=null) {
				//Stop and dispose of any sound effect
				soundEffect.Stop();
				soundEffect.Dispose();
			}

			// Initialize background music
			songURL = new NSUrl("Sounds/"+filename);
			NSError err;
		 	soundEffect = new AVAudioPlayer (songURL, "wav", out err);
			soundEffect.Volume = EffectsVolume;
			soundEffect.FinishedPlaying += delegate { 
				soundEffect = null;
			};
			soundEffect.NumberOfLoops=0;
			soundEffect.Play();

		}
		#endregion
	}
}
```

Let's take a look at several elements of this class in detail. First, we create two different `AVAudioPlayers` (one for background music, the other for sound effects):

```
private AVAudioPlayer backgroundMusic;
private AVAudioPlayer soundEffect;
```

Because we don't want our music or sound effects to continue playing when the app enters the background, we add the following methods to handle entering and leaving the background:

```
public void ActivateAudioSession(){
	// Initialize Audio
	var session = AVAudioSession.SharedInstance(); 
	session.SetCategory(AVAudioSessionCategory.Ambient);
	session.SetActive(true);
}

public void DeactivateAudioSession(){
	var session = AVAudioSession.SharedInstance(); 
	session.SetActive(false);
}

public void ReactivateAudioSession(){
	var session = AVAudioSession.SharedInstance(); 
	session.SetActive(true);
}
```

We'll see how these are used later in our `AppDelegate` class. The `ActivateAudioSession` is also called when an instance of the class is created:

```
public GameAudioManager ()
{
	// Initialize
	ActivateAudioSession();
}
```

We've added some computed properties to control whether on not music or sound effects gets played and the playback volume (these make great user controlled preferences):

```
public bool MusicOn { get; set; } = true;
public float MusicVolume { get; set; } = 0.5f;

public bool EffectsOn { get; set; } = true;
public float EffectsVolume { get; set; } = 1.0f;
```

Next, we use the following code to play looping background music:

```
public void PlayBackgroundMusic(string filename){
	NSUrl songURL;

	// Music enabled?
	if (!MusicOn) return;

	// Any existing background music?
	if (backgroundMusic!=null) {
		//Stop and dispose of any background music
		backgroundMusic.Stop();
		backgroundMusic.Dispose();
	}

	// Initialize background music
	songURL = new NSUrl("Sounds/"+filename);
	NSError err;
	backgroundMusic = new AVAudioPlayer (songURL, "wav", out err);
	backgroundMusic.Volume = MusicVolume;
	backgroundMusic.FinishedPlaying += delegate { 
		backgroundMusic = null;
	};
	backgroundMusic.NumberOfLoops=-1;
	backgroundMusic.Play();
	backgroundSong=filename;

}
```

In this method we first check to see if music playback is allowed and we stop any currently playing background music. Next, we configure the audio playback and load the given audio file from the `Sounds` directory we created above.

By setting the `NumberOfLoops` property to `-1`, we are telling the `AVAudioPlayer` to loop the song indefinitely.

We have also added methods to `StopBackgroundMusic`, `SuspendBackgroundMusic` and `RestartBackgroundMusic`.

Finally, we use the following method to play sound effects:

```
public void PlaySound(string filename){
	NSUrl songURL;

	// Music enabled?
	if (!EffectsOn) return;

	// Any existing sound effect?
	if (soundEffect!=null) {
		//Stop and dispose of any sound effect
		soundEffect.Stop();
		soundEffect.Dispose();
	}

	// Initialize background music
	songURL = new NSUrl("Sounds/"+filename);
	NSError err;
	soundEffect = new AVAudioPlayer (songURL, "wav", out err);
	soundEffect.Volume = EffectsVolume;
	soundEffect.FinishedPlaying += delegate { 
		soundEffect = null;
	};
	soundEffect.NumberOfLoops=0;
	soundEffect.Play();

}
```

This method is nearly identical to the `PlayBackgroundMusic` method, except we are setting the `NumberOfLoops` property to `0` so the sound only plays once.

## Playing Sounds

With the helper file in place, edit your `AppDelegate.cs` file and make it look like the following:

```
using Foundation;
using UIKit;
using AudioToolbox;

namespace AVAudioPlayerSounds
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		#region Computed Properties
		public override UIWindow Window { get; set;}
		public GameAudioManager AudioManager { get; set; } = new GameAudioManager();
		#endregion

		#region Override Methods
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			
		}

		public override void DidEnterBackground (UIApplication application)
		{
			AudioManager.SuspendBackgroundMusic();
			AudioManager.DeactivateAudioSession();
		}

		public override void WillEnterForeground (UIApplication application)
		{
			AudioManager.ReactivateAudioSession();
			AudioManager.RestartBackgroundMusic();
		}

		public override void OnActivated (UIApplication application)
		{
			
		}

		public override void WillTerminate (UIApplication application)
		{
			AudioManager.StopBackgroundMusic();
			AudioManager.DeactivateAudioSession ();
		}
		#endregion
	}
}
```

First, we create an instance of the `GameAudioManager` class that we will use throughout the app. Next, in response to the app entering and leaving the background, we stop and restart the audio session.

Later, when we want to play sounds in other parts of the app, we can use the following code to gain access to our `AppDelegate` and the `GameAudioManager` instance:

```
public static AppDelegate App {
	get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
}
```

To start background music playing, do the following:

```
// Start background music playing
App.AudioManager.PlayBackgroundMusic ("musicloop01.wav");
```

To play a sound effect, use the following code:

```
// Play sound effect
App.AudioManager.PlaySound("levelUp.mp3");
```

# Additional Information


For more information, please see Apple's [AVAudioPlayer Reference](https://developer.apple.com/library/ios/documentation/AVFoundation/Reference/AVAudioPlayerClassReference/).

