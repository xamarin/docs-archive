using System;
using CoreGraphics;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class DetailViewController : UIViewController
	{
		UILabel label;
		public DetailViewController () : base()
		{
			View.BackgroundColor = UIColor.White;
			label = new UILabel(new CGRect(100,100,300,50));
			label.Text = "This is the detail view";
			View.AddSubview (label);
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

