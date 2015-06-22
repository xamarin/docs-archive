using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
			
			View.BackgroundColor = UIColor.White;

			NavigationItem.Title = "Translucent Nav Bar";
			
			lblTransparent = new UILabel  (new RectangleF(10,40, 200, 40));
			lblTransparent.Text = "Nav Bar Transparency";
			swchTransparent = new UISwitch (new RectangleF(220,45, 50, 40));
			View.AddSubview(lblTransparent);
			View.AddSubview(swchTransparent);

			// toggle the navigation bar transparency
			this.swchTransparent.ValueChanged += (s, e) => {
				this.NavigationController.NavigationBar.Translucent = this.swchTransparent.On;
			};
		}
	}
}