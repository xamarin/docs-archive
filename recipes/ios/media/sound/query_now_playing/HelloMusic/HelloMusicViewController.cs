using System;
using CoreGraphics;
using Foundation;
using UIKit;
using MediaPlayer;

namespace HelloMusic
{
	public partial class HelloMusicViewController : UIViewController
	{
		MPMusicPlayerController MyPlayer = new MPMusicPlayerController();

		public HelloMusicViewController () : base ("HelloMusicViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();

			//get currently playing item
			MPMediaItem Now = MyPlayer.NowPlayingItem;

			//get item's properties

			UIImage MyArtwork = Now.Artwork.ImageWithSize (new CGSize(100, 100));

			Track.Text = Now.Title;
			Artist.Text = Now.Artist;
			Album.Text = Now.AlbumTitle;
			Lyrics.Text = Now.Lyrics;
			Artwork.Image = MyArtwork;
		}
	}
}

