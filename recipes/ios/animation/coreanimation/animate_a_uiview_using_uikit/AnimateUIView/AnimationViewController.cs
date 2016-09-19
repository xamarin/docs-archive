
using System;
using System.Drawing;

using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;

namespace AnimateUIView
{
	public partial class AnimationViewController : UIViewController
	{
		UIImageView imageView;
		UIImage image;
		CGPoint pt;

		public AnimationViewController () : base ("AnimationViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			imageView = new UIImageView(new CGRect(0, 100, 50, 50));
			image = UIImage.FromFile("Sample.png");
			imageView.Image = image;
			View.AddSubview(imageView);

			pt = imageView.Center;

			UIView.BeginAnimations ("slideAnimation");

			UIView.SetAnimationDuration (2);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseInOut);
			UIView.SetAnimationRepeatCount (2);
			UIView.SetAnimationRepeatAutoreverses (true);

			UIView.SetAnimationDelegate (this);
			UIView.SetAnimationDidStopSelector (new Selector ("animationDidStop:finished:context:"));

			var xpos = UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2;
			var ypos = imageView.Center.Y;

			imageView.Center = new CGPoint (xpos, ypos);

			UIView.CommitAnimations ();

		}

		[Export("animationDidStop:finished:context:")]
		void SlideStopped (NSString animationID, NSNumber finished, NSObject context)
		{
			imageView.Center = pt;
		}
	}
}

