using System;

using UIKit;
using CoreAnimation;
using CoreGraphics;

namespace ImplicitAnimation
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
			layer.Bounds = new CGRect (0, 0, 80, 80);
			layer.Position = new CGPoint (100, 100);
			layer.Contents = UIImage.FromFile("sample.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResizeAspectFill;

			View.Layer.AddSublayer (layer);

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			CATransaction.Begin ();
			CATransaction.AnimationDuration = 2;
			layer.Position = new CGPoint (100, 400);
			CATransaction.Commit ();

		}
	}
}

