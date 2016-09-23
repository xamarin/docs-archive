---
id: {23E8C6E6-E690-A11D-EA70-2E52E9CF1D4E}  
title: Play a Video Using MPMoviePlayerController  
brief: This recipe shows how to play a video using an MPMoviePlayerController.  
samplecode: [PlayMovieRecipe](https: //github.com/xamarin/recipes/tree/master/ios/media/video_and_photos/play_a_video_using_mpmovieplayercontroller)  
sample: [PDF for Offline Use: PlayMovie.pdf](http: //staging-docs.xamarin.com/@api/deki/files/3288/=PlayMovieRecipe.pdf)  
sdk: [MPMoviePlayerController](http: //developer.apple.com/library/ios/#documentation/mediaplayer/reference/MPMoviePlayerController_Class/Reference/Reference.html)  
---

<a name="Recipe" class="injected"></a>


# Recipe

1. Add a class variable for an MPMoviePlayerController.

```
MPMoviePlayerController moviePlayer;
```

<ol start="2"><li>Add code to play a movie. The following code does this in a <code>UIButton</code>’s <code>TouchUpInside</code> event handler: </li></ol>

```
playMovie.TouchUpInside += delegate {
    moviePlayer = new MPMoviePlayerController (NSUrl.FromFilename ("sample.m4v"));

    View.AddSubview (moviePlayer.View);
    moviePlayer.SetFullscreen (true, true);
    moviePlayer.Play ();
} ;
```
<ol start="3">
	<li>Ensure that any video files you intend to use are set with <strong>Build Action Content</strong>. You can do this by right-clicking on the video files and selecting **Build Action** in the context menu that appears.</li>
</ol>

 <a name="Additional_Information" class="injected"></a>