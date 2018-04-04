---
id: 7C9D42C9-C8B5-43EA-A905-B0235BA51D70
title: "Query Now Playing"
brief: "Get information about the song playing in the Music Player."
sdk:
  - title: "iPod Library Access Guide" 
    url: https://developer.apple.com/library/ios/documentation/Audio/Conceptual/iPodLibraryAccess_Guide/UsingTheiPodLibrary/UsingTheiPodLibrary.html
  - title: "MPMusicPlayerController Class Reference" 
    url: https://developer.apple.com/library/ios/documentation/mediaplayer/reference/MPMusicPlayerController_ClassReference/Reference/Reference.html
---

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
![](Images/00.png "Now playing screen example")

# Additional Information

The MediaPlayer API does not run properly on a simulator. Always test on a device.

