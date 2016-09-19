using System;
using CoreGraphics;

using Foundation;
using UIKit;
using MediaPlayer;

namespace PlayMovieRecipe
{
	public partial class PlayMovieRecipeViewController : UIViewController
	{
		MPMoviePlayerController moviePlayer;

		public PlayMovieRecipeViewController () : base ("PlayMovieRecipeViewController", null)
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


			playMovie.TouchUpInside += delegate {
				moviePlayer = new MPMoviePlayerController (NSUrl.FromFilename ("sample.m4v"));

				View.AddSubview (moviePlayer.View);
				moviePlayer.SetFullscreen (true, true);
				moviePlayer.Play ();
			};
		}
        
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
            
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
            
			ReleaseDesignerOutlets ();
		}
        
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

