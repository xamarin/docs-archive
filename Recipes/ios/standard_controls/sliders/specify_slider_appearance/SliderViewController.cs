using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using CoreImage;
using CoreGraphics;

namespace Slider {

	public class ImageViewController : UIViewController {

		UILabel label, label2;
		UISlider sliderImage;
		UISlider sliderColor;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Slider";
			View.BackgroundColor = UIColor.White;
			label = new UILabel(new CGRect(10, 80, 90, 20));
			label.Text = "Value: ?";
			label2 = new UILabel(new CGRect(10, 140, 90, 20));
			label2.Text = "Value: ?";			
			View.Add (label);
			View.Add (label2);
			
			sliderImage = new UISlider(new CGRect(100,  80, 210, 20));
			View.Add (sliderImage);
			
			sliderImage.SetThumbImage(UIImage.FromFile("29_icon.png"), UIControlState.Normal);

			sliderImage.MinValue = 0f;
			sliderImage.MaxValue = 1f;
			sliderImage.Value = 0.5f;
			sliderImage.ValueChanged += HandleValueChanged; // defined below


			sliderColor = new UISlider(new CGRect(100,  140, 210, 20));
			View.Add (sliderColor);
			sliderColor.Value = 0.25f;
			sliderColor.ThumbTintColor = UIColor.Red;
			sliderColor.MinimumTrackTintColor = UIColor.Orange;
			sliderColor.MaximumTrackTintColor = UIColor.Yellow;
			sliderColor.ValueChanged += HandleValueChanged2; // defined below
		}

		void HandleValueChanged (object sender, EventArgs e)
		{   // display the value in a label
			label.Text = sliderImage.Value.ToString ();
		}
		void HandleValueChanged2 (object sender, EventArgs e)
		{   // display the value in a label
			label2.Text = sliderColor.Value.ToString ();
		}
	}
}