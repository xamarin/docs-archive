using CoreGraphics;
using UIKit;

namespace MapView {

	public class ImageViewController : UIViewController {
		
		UIImageView animatedCircleImage;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Animated ImageView";
			View.BackgroundColor = UIColor.White;
			
			// an animating image
			animatedCircleImage = new UIImageView();
			animatedCircleImage.AnimationImages = new UIImage[] {
				  UIImage.FromBundle ("Spinning Circle_1.png")
				, UIImage.FromBundle ("Spinning Circle_2.png")
				, UIImage.FromBundle ("Spinning Circle_3.png")
				, UIImage.FromBundle ("Spinning Circle_4.png")
			} ;
			animatedCircleImage.AnimationRepeatCount = 0;
			animatedCircleImage.AnimationDuration = .5;
			animatedCircleImage.Frame = new CGRect(110, 80, 100, 100);
			View.AddSubview(animatedCircleImage);
			animatedCircleImage.StartAnimating ();
		}
	}
}