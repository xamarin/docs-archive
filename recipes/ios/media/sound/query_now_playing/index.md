id:{7c9d42c9-c8b5-43ea-a905-b0235ba51d70}  
title:Query Now Playing  
brief:Get information about the song playing in the Music Player.  
samplecode:[Browse on GitHub](https://github.com/xamarin/recipes/tree/master/ios/media/sound/query_now_playing)  
sdk:[iPod Library Access Guide](https://developer.apple.com/library/ios/documentation/Audio/Conceptual/iPodLibraryAccess_Guide/UsingTheiPodLibrary/UsingTheiPodLibrary.html)  
sdk:[MPMusicPlayerController Class Reference](https://developer.apple.com/library/ios/documentation/mediaplayer/reference/MPMusicPlayerController_ClassReference/Reference/Reference.html)  

# Recipe

1. Add a reference to `MediaPlayer`
```
  using MediaPlayer;
```
<ol start="2">
  <li>Create a new <code>MPMusicPlayerController</code></li>
</ol>
```
  MPMusicPlayerController MyPlayer = new MPMusicPlayerController();
```
<ol start="3">
  <li>To get information about the currently playing song, use the <code>NowPlaying</code> property:</li>
</ol>

```
MPMediaItem Now = MyPlayer.NowPlayingItem;

UIImage MyArtwork = Now.Artwork.ImageWithSize (new CGSize(100, 100));

Track.Text = Now.Title;
Artist.Text = Now.Artist;
Album.Text = Now.AlbumTitle;
Lyrics.Text = Now.Lyrics;
Artwork.Image = MyArtwork;</code></pre>
```
![]("Images/00.png")

# Additional Information

The MediaPlayer API does not run properly on a simulator. Always test on a device.
