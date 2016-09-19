using System;

using UIKit;
using CoreGraphics;

namespace AnimateUsingBlocks
{
	public partial class ViewController : UIViewController
	{
		CGPoint pt;
		UIImage image;
		UIImageView imageView;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			imageView = new UIImageView(new CGRect (50, 50, 57, 57));
			image = UIImage.FromFile("Icon.png");
			imageView.Image = image;
			View.AddSubview(imageView);

			pt = imageView.Center;

			UIView.Animate (2, 0, UIViewAnimationOptions.CurveEaseInOut | UIViewAnimationOptions.Autoreverse,
				() => {
					imageView.Center = 
						new CGPoint (UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2, imageView.Center.Y);}, 
				() => {
					imageView.Center = pt; }
			);

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

