using System;

using Foundation;
using UIKit;
using AVFoundation;

namespace AVPlayerDemo
{
    public partial class AVPlayerDemoViewController : UIViewController
    {
		AVPlayer player;
		AVPlayerLayer playerLayer;
        AVAsset asset;
		AVPlayerItem playerItem;
        
        public AVPlayerDemoViewController () : base ("AVPlayerDemoViewController", null)
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
            
            asset = AVAsset.FromUrl (NSUrl.FromFilename ("sample.m4v"));
            playerItem = new AVPlayerItem (asset);   
            
            player = new AVPlayer (playerItem);  
            playerLayer = AVPlayerLayer.FromPlayer (player);
            playerLayer.Frame = View.Frame;
            
            View.Layer.AddSublayer (playerLayer);
            
            player.Play ();
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

