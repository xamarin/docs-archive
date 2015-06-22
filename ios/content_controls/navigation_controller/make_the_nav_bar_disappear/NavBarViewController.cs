using CoreGraphics;
using UIKit;

namespace NavBarRightBtn
{
	public class NavBarViewController : UIViewController
	{
		UISwitch swchTransparent;
		UILabel lblTransparent;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			View.BackgroundColor = UIColor.White;

			NavigationItem.Title = "Customizing Nav Bar";

			lblTransparent = new UILabel  (new CGRect(10,40, 200, 40));
			lblTransparent.Text = "Nav Bar Transparency";
			swchTransparent = new UISwitch (new CGRect(220,50, 50, 40));
			View.AddSubview(lblTransparent);
			View.AddSubview(swchTransparent);

			// toggle the navigation bar transparency
			this.swchTransparent.ValueChanged += (s, e) => {
				this.NavigationController.NavigationBar.Translucent = this.swchTransparent.On;
	
				// if you also want to change the StatusBar (ie time, network status, etc)
//				if (this.NavigationController.NavigationBar.Translucent) 
//					UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackTranslucent;
//				else
//					UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
			};

			// Show or Hide the nav bar
			UIButton hideShowButton = UIButton.FromType (UIButtonType.RoundedRect);
			hideShowButton.Frame = new CGRect(10, 80, 120, 40);
			hideShowButton.SetTitle ("Hide Nav Bar", UIControlState.Normal);
			hideShowButton.TouchUpInside += (sender, e) => {
				if (NavigationController.NavigationBarHidden) {
					// if you also want to change the StatusBar (ie time, network status, etc)
					//UIApplication.SharedApplication.SetStatusBarHidden (false, false);

					NavigationController.SetNavigationBarHidden(false, true);
					hideShowButton.SetTitle ("Hide Nav Bar", UIControlState.Normal);
				} else {
					NavigationController.SetNavigationBarHidden(true, true);
					hideShowButton.SetTitle ("Show Nav Bar", UIControlState.Normal);
					
					// if you also want to change the StatusBar (ie time, network status, etc)
					//UIApplication.SharedApplication.SetStatusBarHidden (true, false);
				}
			};
			Add (hideShowButton);
		}
	}
}