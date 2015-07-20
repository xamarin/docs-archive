using System;
using CoreGraphics;

using Foundation;
using UIKit;
using CoreMotion;

namespace CoreMotion
{
	public partial class CoreMotionViewController : UIViewController
	{
		private CMMotionManager motionManager;

		public CoreMotionViewController () : base ("CoreMotionViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			motionManager = new CMMotionManager ();
				
			motionManager.StartAccelerometerUpdates (NSOperationQueue.CurrentQueue, (data, error) => 
			{
				this.lblX.Text = data.Acceleration.X.ToString ("0.00000000");
				this.lblY.Text = data.Acceleration.Y.ToString ("0.00000000");
				this.lblZ.Text = data.Acceleration.Z.ToString ("0.00000000");
			});

		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();

			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

