
using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace InitialScreenDemo
{
    public partial class ViewController2 : UIViewController
    {
        public ViewController2 () : base ("ViewController2", null)
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
            
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}

