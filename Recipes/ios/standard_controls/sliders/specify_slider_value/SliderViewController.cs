using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;
using CoreImage;
using CoreGraphics;

namespace Slider {

	public class ImageViewController : UIViewController {

		UILabel label;		
		UISlider slider;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Slider";
			View.BackgroundColor = UIColor.White;
			label = new UILabel(new CGRect(10, 80, 90, 20));
			label.Text = "Value: ?";			
			View.Add (label);
			
			slider = new UISlider(new CGRect(100,  80, 210, 20));
			View.Add (slider);
			
			slider.MinValue = 0f;
			slider.MaxValue = 1f;
			slider.Value = 0.5f;
			slider.ValueChanged += HandleValueChanged; // defined below
		}

		void HandleValueChanged (object sender, EventArgs e)
		{   // display the value in a label
			label.Text = slider.Value.ToString ();
		}
	}
}