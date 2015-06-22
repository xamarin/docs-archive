using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using CoreImage;
using CoreGraphics;

/*
"Sunrise near Atkeison Plateau" © 2012 Charles Atkeison, 
used under a Creative Commons Attribution-ShareAlike license: http://creativecommons.org/licenses/by-sa/3.0/
*/

namespace ColorControl {

	public class ImageViewController : UIViewController {
		
		UIImage sourceImage;

		UIButton resetButton;
		UIImageView imageView;
		UISlider sliderContrast, sliderSaturation, sliderBrightness;
		UILabel labelC, labelS, labelB;

		// the CoreImage filter		
		CIColorControls colorCtrls;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Color Controls";
			View.BackgroundColor = UIColor.White;

			resetButton = UIButton.FromType (UIButtonType.RoundedRect);
			resetButton.Frame = new CGRect(110, 60, 90, 40);
			resetButton.SetTitle ("Reset", UIControlState.Normal);
			resetButton.TouchUpInside += (sender, e) => {
				sliderSaturation.Value = 1;	
				sliderBrightness.Value = 0;
				sliderContrast.Value = 1;
				HandleValueChanged (sender, e);
			};
			View.Add (resetButton);
	

			labelC = new UILabel(new CGRect(10, 110, 90, 20));
			labelS = new UILabel(new CGRect(10, 160, 90, 20));
			labelB = new UILabel(new CGRect(10, 210, 90, 20));

			labelC.Text = "Contrast";
			labelS.Text = "Saturation";
			labelB.Text = "Brightness";

			View.Add (labelC);
			View.Add (labelS);
			View.Add (labelB);

			sliderBrightness = new UISlider(new CGRect(100,  110, 210, 20));
			sliderSaturation = new UISlider(new CGRect(100, 160, 210, 20));
			sliderContrast = new UISlider(new CGRect(100, 210, 210, 20));
			
			// http://developer.apple.com/library/mac/#documentation/graphicsimaging/reference/CoreImageFilterReference/Reference/reference.html#//apple_ref/doc/filter/ci/CIColorControls
			// set min/max values on slider to match CIColorControls filter
			sliderSaturation.MinValue = 0;
			sliderSaturation.MaxValue = 2;
			sliderBrightness.MinValue = -1;
			sliderBrightness.MaxValue = 1;
			sliderContrast.MinValue = 0;
			sliderContrast.MaxValue = 4;
			// set default values
			sliderSaturation.Value = 1;
			sliderBrightness.Value = 0;
			sliderContrast.Value = 1;
			
			sliderContrast.TouchUpInside += HandleValueChanged;
			sliderSaturation.TouchUpInside += HandleValueChanged;
			sliderBrightness.TouchUpInside += HandleValueChanged;
			
			View.Add (sliderContrast);
			View.Add (sliderSaturation);
			View.Add (sliderBrightness);

			imageView = new UIImageView(new CGRect(10, 240, 300, 200));
			sourceImage = UIImage.FromFile ("clouds.jpg");
			imageView.Image = sourceImage;
			View.Add (imageView);
		}

		void HandleValueChanged (object sender, EventArgs e)
		{	// use the low-res version
			if (colorCtrls == null)
				colorCtrls = new CIColorControls () {
					Image = CIImage.FromCGImage (sourceImage.CGImage),
				};
		
			colorCtrls.Brightness = sliderBrightness.Value; 
			colorCtrls.Saturation = sliderSaturation.Value; 
			colorCtrls.Contrast = sliderContrast.Value;
			
			var output = colorCtrls.OutputImage;
			var context = CIContext.FromOptions (null);
			var result = context.CreateCGImage (output, output.Extent);

			imageView.Image = UIImage.FromImage(result);

			// UNCOMMENT to save the image each time it is generated
//			var someImage = imageView.Image;
//			someImage.SaveToPhotosAlbum((image, error) => {
//				// Called on completion...
//				//new UIAlertView("Saved", "Photo saved to Camera Roll", null, "OK", null).Show ();
//			    Console.WriteLine("CIColorControls image saved to Photos");
//			});
		}
	}
}