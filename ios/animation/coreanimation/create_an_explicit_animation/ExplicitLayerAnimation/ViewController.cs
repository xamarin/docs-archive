using System;

using UIKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace ExplicitLayerAnimation
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
			layer.Position = new CGPoint (150, 150);
			layer.Contents = UIImage.FromFile ("sample.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResizeAspectFill;

			View.Layer.AddSublayer (layer);

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var pt = layer.Position;
			layer.Position = new CGPoint (150, 350);

			var basicAnimation = CABasicAnimation.FromKeyPath ("position");
			basicAnimation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
			basicAnimation.From = NSValue.FromCGPoint (pt);
			basicAnimation.To = NSValue.FromCGPoint (new CGPoint (150, 350));
			basicAnimation.Duration = 2;

			layer.AddAnimation (basicAnimation, "position");

		}
	}
}

