using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NavBarToolbar {
	public class NavBarViewController : UIViewController {
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			View.BackgroundColor = UIColor.White;

			NavigationItem.Title = "Adding a Toolbar";
			
			// setup the toolbar items. the navigation controller gets the items from the controller
			// because they're specific to each controller on the stack, hence 'this.SetToolbarItems' rather
			// than this.NavigationController.Toolbar.SetToolbarItems
			this.SetToolbarItems( new UIBarButtonItem[] {
				new UIBarButtonItem(UIBarButtonSystemItem.Refresh, (s,e) => {
					Console.WriteLine("Refresh clicked");
				})
				, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace) { Width = 50 }
				, new UIBarButtonItem(UIBarButtonSystemItem.Pause, (s,e) => {
					Console.WriteLine ("Pause clicked");
				})
			}, false);
			
			this.NavigationController.ToolbarHidden = false;
		}
	}
}