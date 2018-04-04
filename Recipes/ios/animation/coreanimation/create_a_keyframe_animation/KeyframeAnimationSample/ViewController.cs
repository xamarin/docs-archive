using System;

using UIKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace KeyframeAnimationSample
{
	public partial class ViewController : UIViewController
	{
		CALayer layer;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			layer = new CALayer ();
			layer.Bounds = new CGRect (0, 0, 50, 50);
			layer.Position = new CGPoint (UIScreen.MainScreen.Bounds.Width / 2,     UIScreen.MainScreen.Bounds.Height / 2);
			layer.Contents = UIImage.FromFile ("sample.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResizeAspectFill;

			View.Layer.AddSublayer (layer);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			layer.Transform = CATransform3D.MakeRotation ((float)Math.PI * 2, 0, 0, 1);

			CAKeyFrameAnimation animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");       

			animRotate.Values = new NSObject[] {
				NSNumber.FromFloat (0f),
				NSNumber.FromFloat ((float)Math.PI / 2f),
				NSNumber.FromFloat ((float)Math.PI),
				NSNumber.FromFloat ((float)Math.PI * 2)};

			animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);         

			animRotate.Duration = 2;

			layer.AddAnimation (animRotate, "transform");
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

