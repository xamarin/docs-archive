using CoreGraphics;
using UIKit;

namespace Buttons {

	public class ButtonsViewController : UIViewController {
		
		UIButton buttonRect, buttonDisclosure, buttonInfo, buttonAdd, buttonImage, buttonCustom;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "ImageView";
			View.BackgroundColor = UIColor.White;
			

			buttonRect = UIButton.FromType(UIButtonType.System);
			buttonRect.SetTitle ("Button!", UIControlState.Normal);
			buttonDisclosure = UIButton.FromType(UIButtonType.DetailDisclosure);
			buttonInfo = UIButton.FromType(UIButtonType.InfoDark);
			buttonAdd = UIButton.FromType(UIButtonType.ContactAdd);
			buttonImage = UIButton.FromType(UIButtonType.Custom);
			buttonImage.SetImage(UIImage.FromFile ("50_icon.png"), UIControlState.Normal);
			buttonCustom = UIButton.FromType(UIButtonType.RoundedRect);
			buttonCustom.SetTitle ("Button!", UIControlState.Normal);
			buttonCustom.Font = UIFont.FromName ("Helvetica-BoldOblique", 26f);
			buttonCustom.SetTitleColor (UIColor.Brown, UIControlState.Normal);
			buttonCustom.SetTitleShadowColor (UIColor.DarkGray, UIControlState.Normal);
			
			buttonRect.Frame 		= new CGRect(200, 20, 80, 40);
			buttonDisclosure.Frame 	= new CGRect(200, 60, 50, 40);
			buttonInfo.Frame 		= new CGRect(200, 110, 50, 40);
			buttonAdd.Frame 		= new CGRect(200, 160, 50, 40);
			buttonImage.Frame 		= new CGRect(200, 220, 50, 40);
			buttonCustom.Frame 		= new CGRect(160, 270, 140, 60);

			buttonRect.TouchUpInside += HandleTouchUpInside;
			buttonDisclosure.TouchUpInside += HandleTouchUpInside;
			buttonInfo.TouchUpInside += HandleTouchUpInside;
			buttonAdd.TouchUpInside += HandleTouchUpInside;
			buttonImage.TouchUpInside += HandleTouchUpInside;
			buttonCustom.TouchUpInside += HandleTouchUpInside;
		
			View.AddSubview (buttonRect);
			View.AddSubview (buttonDisclosure);
			View.AddSubview (buttonInfo);
			View.AddSubview (buttonAdd);
			View.AddSubview (buttonImage);
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
		}
	}
}