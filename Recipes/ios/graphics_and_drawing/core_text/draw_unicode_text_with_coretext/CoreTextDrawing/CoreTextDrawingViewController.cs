using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace CoreTextDrawing
{
    public partial class CoreTextDrawingViewController : UIViewController
    {
        TextDrawingView _textDrawingView;
        
        public CoreTextDrawingViewController () : base ("CoreTextDrawingViewController", null)
        {
			View.BackgroundColor = UIColor.Black;
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
            
            _textDrawingView = new TextDrawingView (){Frame = View.Frame};
            View.AddSubview (_textDrawingView);
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

