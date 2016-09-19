using System;

using UIKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace AnimationGroupSample
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
			layer.Position = new CGPoint (100, 100);
			layer.Contents = UIImage.FromFile ("sample.png").CGImage;
			layer.ContentsGravity = CALayer.GravityResizeAspectFill;

			View.Layer.AddSublayer (layer);
		}

		public override void ViewWillAppear (bool animated)
		{

			base.ViewDidAppear (animated);

			//Creates basic moving animation
			var pt = layer.Position;
			layer.Position = new CGPoint (100, 300);
			var basicAnimation = CABasicAnimation.FromKeyPath ("position");
			basicAnimation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
			basicAnimation.From = NSValue.FromCGPoint (pt);
			basicAnimation.To = NSValue.FromCGPoint (new CGPoint (100, 300));


			//Creates transformation animation
			layer.Transform = CATransform3D.MakeRotation ((float)Math.PI * 2, 0, 0, 1);       
			var animRotate = (CAKeyFrameAnimation)CAKeyFrameAnimation.FromKeyPath ("transform");           
			animRotate.Values = new NSObject[] { 
				NSNumber.FromFloat (0f), 
				NSNumber.FromFloat ((float)Math.PI / 2f), 
				NSNumber.FromFloat ((float)Math.PI),
				NSNumber.FromFloat ((float)Math.PI * 2)};
			animRotate.ValueFunction = CAValueFunction.FromName (CAValueFunction.RotateX);

			//Adds the animations to a group, and adds the group to the layer
			var animationGroup = CAAnimationGroup.CreateAnimation ();
			animationGroup.Duration = 2;
			animationGroup.Animations = new CAAnimation[] { basicAnimation, animRotate };
			layer.AddAnimation (animationGroup, null);
		}
	}
}

