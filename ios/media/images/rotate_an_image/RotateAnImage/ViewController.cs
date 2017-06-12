using System;

using UIKit;
using CoreGraphics;

namespace RotateAnImage
{
    public partial class ViewController : UIViewController
    {
		UIImage image;
		UIImageView imageView;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            image = UIImage.FromBundle("dino-sml.jpeg");
            imageView = new UIImageView(new CGRect(50, 50, 100, 100))
            {
                ContentMode = UIViewContentMode.ScaleAspectFit,
                Image = image,
                Transform = CGAffineTransform.MakeRotation((float)Math.PI / 4)                   
            };

            View.AddSubview(imageView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}
