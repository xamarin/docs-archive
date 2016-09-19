using CoreGraphics;
using UIKit;

namespace MapView {

	public class ImageViewController : UIViewController {
		
		UIImageView imageView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "ImageView";
			View.BackgroundColor = UIColor.White;
			
			// a simple image
			imageView = new UIImageView (UIImage.FromBundle ("MonkeySFO.png")); // Build Action:Content
			imageView.Frame = new CGRect (10, 80, imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
			View.AddSubview (imageView);
		}
	}
}