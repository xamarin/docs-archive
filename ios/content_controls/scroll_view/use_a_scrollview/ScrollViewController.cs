using CoreGraphics;
using UIKit;

namespace ScrollView {

	public class ImageViewController : UIViewController {
		
		UIScrollView scrollView;
		UIImageView imageView;
		

		#region -= constructors =-

		public ImageViewController () : base()
		{
		}
		
		#endregion
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// set the background color of the view to white
			this.View.BackgroundColor = UIColor.White;
			
			this.Title = "Scroll View";
			
			// create our scroll view
			scrollView = new UIScrollView (
				new CGRect (0, 0, View.Frame.Width, View.Frame.Height));
			View.AddSubview (scrollView);
			
			// create our image view
			imageView = new UIImageView (UIImage.FromFile ("halloween.jpg"));
			scrollView.ContentSize = imageView.Image.Size;
			scrollView.AddSubview (imageView);

		}
	}
}