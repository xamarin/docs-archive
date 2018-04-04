---
id: 2388137F-B8F4-1AE3-75A0-B1026D9D915E
title: "Enable or Disable Airplay"
brief: "This recipe shows how to enable (or disable) AirPlay when playing a video using MPMoviePlayerController."
sdk:
  - title: "MPMoviePlayerController Class References" 
    url: http://developer.apple.com/library/ios/#documentation/mediaplayer/reference/MPMoviePlayerController_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>


# Recipe

> ℹ️ **Note**: In iOS 8 and above, the airplay button will no longer appear in playback controls. Airplay can be enabled or disabled in the control center.

-  Add the following class variable in a `UIViewController` subclass:


```
MPMoviePlayerController moviePlayer;
```

-  Add a movie file named sample.m4v to the project in Xamarin or Visual Studio. There is a sample file included in the example project.
-  Ensure that the __Build Action__ property of the file is set to __Content__.
-  In the `ViewDidLoad` method, create the `MPMoviePlayer`:


```
moviePlayer = new MPMoviePlayerController (NSUrl.FromFilename("sample.m4v");
moviePlayer.View.Frame = View.Bounds;
moviePlayer.ShouldAutoplay = true; // starts automatically
moviePlayer.PrepareToPlay (); //required to enable the control to start playing
```

-  To enable AirPlay for this movie, set `AllowsAirPlay` to true:


```
moviePlayer.AllowsAirPlay = true;
```

-  To disable AirPlay, set `AllowsAirPlay` to false:


```
moviePlayer.AllowsAirPlay = false;
```

