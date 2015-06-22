using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace TabBar{
	public partial class TabController : UITabBarController 
	{
		UIViewController tab1, tab2, tab3;

		public TabController () : base ("TabController", null)
		{
			tab1 = new UIViewController();
			tab1.Title = "Green";
			tab1.View.BackgroundColor = UIColor.Green;

			tab2 = new UIViewController();
			tab2.Title = "Orange";
			tab2.View.BackgroundColor = UIColor.Orange;

			tab3 = new UIViewController();
			tab3.Title = "Red";
			tab3.View.BackgroundColor = UIColor.Red;

			#region Additional Info
			//			tab1.TabBarItem = new UITabBarItem (UITabBarSystemItem.History, 0); // sets image AND text
			//			tab2.TabBarItem = new UITabBarItem ("Orange", UIImage.FromFile("Images/first.png"), 1);
			//			tab3.TabBarItem = new UITabBarItem ();
			//			tab3.TabBarItem.Image = UIImage.FromFile("Images/second.png");
			//			tab3.TabBarItem.Title = "Rouge"; // this overrides tab3.Title set above
			//			tab3.TabBarItem.BadgeValue = "4";
			//			tab3.TabBarItem.Enabled = false;
			#endregion

			var tabs = new UIViewController[] {
				tab1, tab2, tab3
			};

			ViewControllers = tabs;

			SelectedViewController = tab2; // normally you would default to the left-most tab (ie. tab1)
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

