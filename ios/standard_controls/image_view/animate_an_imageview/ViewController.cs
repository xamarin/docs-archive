using System;
using CoreGraphics;
using UIKit;

namespace ImageView
{
	public partial class ViewController : UIViewController
	{
		protected ViewController (IntPtr handle) : base (handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Create new image view and add it to the view.
			var animatedCircleImage = new UIImageView ();
			animatedCircleImage.Frame = new CGRect((UIScreen.MainScreen.Bounds.Width / 2f) - 50f,
												   (UIScreen.MainScreen.Bounds.Height / 2f) - 50f, 100, 100);
			View.AddSubview(animatedCircleImage);

			// Create a list of animation frames
			animatedCircleImage.AnimationImages = new UIImage[] {
			      UIImage.FromBundle ("Spinning Circle_1")
			    , UIImage.FromBundle ("Spinning Circle_2")
			    , UIImage.FromBundle ("Spinning Circle_3")
			    , UIImage.FromBundle ("Spinning Circle_4")
			};

			// Annimate the view
			animatedCircleImage.AnimationRepeatCount = 0;
			animatedCircleImage.AnimationDuration = .5;
			animatedCircleImage.StartAnimating();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
