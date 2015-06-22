using System;
using CoreGraphics;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class DetailViewController : UIViewController
	{
		UILabel label;
		UIToolbar toolbar;

		public DetailViewController () : base()
		{
			View.BackgroundColor = UIColor.White;

			label = new UILabel(new CGRect(100,70,300,50));
			label.Text = "This is the detail view";
			View.AddSubview (label);
			// add a toolbar to host the master view popover (when it is required, in portrait)
			toolbar = new UIToolbar(new CGRect(0, 40, View.Frame.Width, 30));
			toolbar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			View.AddSubview(toolbar);
		}
		
		/// <summary>
		/// Shows the button that allows access to the master view popover
		/// </summary>
		public void AddContentsButton (UIBarButtonItem button)
		{
			button.Title = "Contents";
			toolbar.SetItems (new UIBarButtonItem[] { button }, false );
		}
		/// <summary>
		/// Hides the button that allows access to the master view popover
		/// </summary>
		public void RemoveContentsButton ()
		{
			toolbar.SetItems (new UIBarButtonItem[0], false);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

