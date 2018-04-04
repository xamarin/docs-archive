using CoreGraphics;
using UIKit;

namespace Buttons {

	public class ButtonsViewController : UIViewController {
		
		UIButton buttonRect, buttonCustom;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Buttons";
			View.BackgroundColor = UIColor.White;

			buttonRect = UIButton.FromType(UIButtonType.RoundedRect);
			buttonRect.SetTitle ("Click me", UIControlState.Normal);
			buttonRect.SetTitle ("Clicking me", UIControlState.Highlighted);
			buttonRect.SetTitle ("Disabled", UIControlState.Disabled);
			buttonRect.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);


			buttonCustom = UIButton.FromType(UIButtonType.RoundedRect);
			buttonCustom.SetTitle ("Button", UIControlState.Normal);
			buttonCustom.SetTitle ("Button!!!!", UIControlState.Highlighted);
			buttonCustom.Font = UIFont.FromName ("Helvetica-BoldOblique", 26f);
			buttonCustom.SetTitleColor (UIColor.Brown, UIControlState.Normal);
			buttonCustom.SetTitleColor (UIColor.Yellow, UIControlState.Highlighted);
			buttonCustom.SetTitleShadowColor (UIColor.DarkGray, UIControlState.Normal);
			
			buttonRect.Frame 		= new CGRect(160, 100, 140, 40);
			buttonCustom.Frame 		= new CGRect(160, 180, 140, 60);

			buttonRect.TouchUpInside += HandleTouchUpInside;
			buttonCustom.TouchUpInside += HandleTouchUpInside;
		
			View.AddSubview (buttonRect);
			View.AddSubview (buttonCustom);

		}
		protected void HandleTouchUpInside (object sender, System.EventArgs e)
		{
			var button = sender as UIButton;
			new UIAlertView (button.Title(UIControlState.Normal) + " click"
					, "TouchUpInside Handled"
					, null
					, "OK"
					, null).Show();

			// change state of the top button, so you can see the effect
			buttonRect.Enabled = !buttonRect.Enabled;
		}
	}
}