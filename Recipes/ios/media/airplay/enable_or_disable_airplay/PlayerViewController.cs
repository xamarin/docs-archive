using System;
using UIKit;
using MediaPlayer;
using Foundation;

namespace AVPlayerDemo
{
    public class PlayerViewController : UIViewController
    {
        public PlayerViewController () : base()
        {
        }

		MPMoviePlayerController moviePlayer;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

		    moviePlayer = new MPMoviePlayerController (NSUrl.FromFilename ("sample.m4v"));
		    moviePlayer.View.Frame = View.Bounds;
			moviePlayer.View.AutoresizingMask = UIViewAutoresizing.All;
			moviePlayer.ShouldAutoplay = true;
			moviePlayer.ControlStyle = MPMovieControlStyle.Default;
			moviePlayer.PrepareToPlay ();

			moviePlayer.AllowsAirPlay = true; // AirPlay button will appear in controls

			
			View.AddSubview (moviePlayer.View);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
		}
    }
}

