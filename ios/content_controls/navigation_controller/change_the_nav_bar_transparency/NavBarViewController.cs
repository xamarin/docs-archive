using System;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;
using Foundation;
using UIKit;

namespace NavBarTrans
{
	/// <summary>
	/// Recipe that demonstrates changing the transparency of the Nav Bar
	/// </summary>
	public class NavBarViewController : UIViewController
	{
		UISwitch swchTransparent;
		UILabel lblTransparent;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			View.BackgroundColor = UIColor.Blue;

			NavigationItem.Title = "Translucent Nav Bar";
			
			lblTransparent = new UILabel  (new CGRect(10,40, 200, 40));
			lblTransparent.Text = "Nav Bar Transparency";
			lblTransparent.TextColor = UIColor.Yellow;
			swchTransparent = new UISwitch (new CGRect(220,45, 50, 40));
			View.AddSubview(lblTransparent);
			View.AddSubview(swchTransparent);

			// toggle the navigation bar transparency
			this.swchTransparent.ValueChanged += (s, e) => {
				this.NavigationController.NavigationBar.Translucent = this.swchTransparent.On;
			};
		}
	}
}