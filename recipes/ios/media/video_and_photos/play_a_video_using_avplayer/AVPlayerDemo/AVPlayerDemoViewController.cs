using System;

using Foundation;
using UIKit;
using AVFoundation;

namespace AVPlayerDemo
{
    public partial class AVPlayerDemoViewController : UIViewController
    {
        AVPlayer _player;
        AVPlayerLayer _playerLayer;
        AVAsset _asset;
        AVPlayerItem _playerItem;
        
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
            
            _asset = AVAsset.FromUrl (NSUrl.FromFilename ("sample.m4v"));
            _playerItem = new AVPlayerItem (_asset);   
            
            _player = new AVPlayer (_playerItem);  
            _playerLayer = AVPlayerLayer.FromPlayer (_player);
            _playerLayer.Frame = View.Frame;
            
            View.Layer.AddSublayer (_playerLayer);
            
            _player.Play ();
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

