using CoreGraphics;
using UIKit;

namespace Buttons {

	public class ButtonsViewController : UIViewController {
		
		UIButton buttonStarRect, buttonStarCustom;
		UISwitch switchEnabled;
		UILabel rectLabel, customLabel, switchLabel;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Buttons";
			View.BackgroundColor = UIColor.White;
			
			buttonStarRect = UIButton.FromType(UIButtonType.RoundedRect);
			buttonStarRect.SetImage (UIImage.FromFile ("star-gold45.png"), UIControlState.Normal);
			buttonStarRect.SetImage (UIImage.FromFile ("star-grey45.png"), UIControlState.Disabled);

			buttonStarRect.Frame = new CGRect(160, 20, 45, 42);
			buttonStarRect.TouchUpInside += HandleTouchUpInside;
			

			buttonStarCustom = UIButton.FromType(UIButtonType.Custom);
			buttonStarCustom.SetImage (UIImage.FromFile ("star-gold45.png"), UIControlState.Normal);
			buttonStarCustom.SetImage (UIImage.FromFile ("star-gold45_sel.png"), UIControlState.Highlighted);
			buttonStarCustom.SetImage (UIImage.FromFile ("star-grey45.png"), UIControlState.Disabled);

			buttonStarCustom.Frame = new CGRect(160, 70, 45, 42);
			buttonStarCustom.TouchUpInside += HandleTouchUpInside;


			switchEnabled = new UISwitch(new CGRect(140, 130, 50, 30));
			switchEnabled.ValueChanged += (sender, e) => {
				buttonStarRect.Enabled = switchEnabled.On;
				buttonStarCustom.Enabled = switchEnabled.On;
			};
			switchEnabled.On = true;


			#region Not related to sample
			rectLabel = new UILabel(new CGRect(10, 20, 150, 30));
			rectLabel.Text = "RoundedRect:";
			customLabel = new UILabel(new CGRect(10, 70, 150, 30));
			customLabel.Text = "Custom:";
			switchLabel = new UILabel(new CGRect(10, 130, 150, 30));
			switchLabel.Text = "Enabled:";
			View.AddSubview (rectLabel);
			View.AddSubview (customLabel);
			View.AddSubview (switchLabel);
			#endregion

			View.AddSubview (buttonStarRect);
			View.AddSubview (buttonStarCustom);
			View.AddSubview (switchEnabled);

		}
		protected void HandleTouchUpInside (object sender, System.EventArgs e)
		{
			var button = sender as UIButton;
			new UIAlertView (button.Title(UIControlState.Normal) + " click"
					, "TouchUpInside Handled"
					, null
					, "OK"
					, null).Show();

		}
	}
}