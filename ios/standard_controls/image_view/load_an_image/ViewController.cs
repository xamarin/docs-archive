using System;

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

			// Create new image view and display it
			var imageView = new UIImageView (UIImage.FromBundle ("MonkeySFO"));
			imageView.Frame = new CoreGraphics.CGRect (10, 32, imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
			View.AddSubview(imageView);

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
