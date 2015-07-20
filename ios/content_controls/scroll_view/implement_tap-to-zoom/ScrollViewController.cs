using CoreGraphics;
using UIKit;

namespace ScrollView {

	public class ImageViewController : UIViewController {
		
		UIScrollView scrollView;
		UIImageView imageView;
		

		public ImageViewController () : base()
		{
		}

		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// set the background color of the view to white
			this.View.BackgroundColor = UIColor.White;
			
			this.Title = "Scroll View";
			
			// create our scroll view
			scrollView = new UIScrollView (
				new CGRect (0, 0, View.Frame.Width
				, View.Frame.Height - NavigationController.NavigationBar.Frame.Height));
			View.AddSubview (scrollView);
			
			// create our image view
			imageView = new UIImageView (UIImage.FromFile ("halloween.jpg"));
			scrollView.ContentSize = imageView.Image.Size;
			scrollView.AddSubview (imageView);
			
			// set allow zooming
			scrollView.MaximumZoomScale = 3f;
			scrollView.MinimumZoomScale = .1f;			
			scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return imageView; };

			// Create a new Tap Gesture Recognizer
			UITapGestureRecognizer doubletap = new UITapGestureRecognizer(OnDoubleTap) {
				NumberOfTapsRequired = 2 // double tap
			};
			scrollView.AddGestureRecognizer(doubletap); // detect when the scrollView is double-tapped
		}

		//implement doubletap handler
		private void OnDoubleTap (UIGestureRecognizer gesture) {
			if (scrollView.ZoomScale >= 1)
				scrollView.SetZoomScale(0.25f, true);
			else
				scrollView.SetZoomScale(2f, true);
		}
	}
}