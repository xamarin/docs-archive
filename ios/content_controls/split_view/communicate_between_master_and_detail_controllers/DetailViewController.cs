using System;
using CoreGraphics;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class DetailViewController : UIViewController
	{
		public UIPopoverController Popover {get;set;}
		
		UILabel label;
		UIToolbar toolbar;
		string content = "This is the detail view for row {0}";

		public DetailViewController () : base()
		{
			View.BackgroundColor = UIColor.White;

			label = new UILabel(new CGRect(100,100,450,60));
			label.Font = UIFont.SystemFontOfSize(24f);
			Update (1); // defaults to "1"
			View.AddSubview (label);
			// add a toolbar to host the master view popover (when it is required, in portrait)
			toolbar = new UIToolbar(new CGRect(0, 0, View.Frame.Width, 30));
			toolbar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			View.AddSubview(toolbar);
		}
		
		/// <summary>
		/// Update the view's contents depending on what was selected in the master list
		/// </summary>
		public void Update (int row) {
			label.Text = String.Format (content, row.ToString ());
			// dismiss the popover if currently visible
			if (Popover != null) 
				Popover.Dismiss (true);
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

