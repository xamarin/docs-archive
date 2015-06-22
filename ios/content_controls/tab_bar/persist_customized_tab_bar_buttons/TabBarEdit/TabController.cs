using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace TabBarEdit{
	public partial class TabController : UITabBarController 
	{
		UIViewController tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9;

		public TabController () : base ("TabController", null)
		{
			tab1 = new UIViewController();
			tab1.Title = "Red";
			tab1.TabBarItem = new UITabBarItem ("Red", UIImage.FromFile("Images/first.png"), 0); // Tag == initial order
			tab1.View.BackgroundColor = UIColor.Red;

			tab2 = new UIViewController();
			tab2.Title = "Orange";
			tab2.TabBarItem = new UITabBarItem ("Orange", UIImage.FromFile("Images/second.png"), 1);
			tab2.View.BackgroundColor = UIColor.Orange;

			tab3 = new UIViewController();
			tab3.Title = "Yellow";
			tab3.TabBarItem = new UITabBarItem ("Yellow", UIImage.FromFile("Images/third.png"), 2);
			tab3.View.BackgroundColor = UIColor.Yellow;

			tab4 = new UIViewController();
			tab4.Title = "Yellow";
			tab4.TabBarItem = new UITabBarItem ("Green", UIImage.FromFile("Images/fourth.png"), 3);
			tab4.View.BackgroundColor = UIColor.Green;

			tab5 = new UIViewController();
			tab5.Title = "Blue";
			tab5.TabBarItem = new UITabBarItem ("Blue", UIImage.FromFile("Images/fifth.png"), 4);
			tab5.View.BackgroundColor = UIColor.Blue;

			tab6 = new UIViewController();
			tab6.Title = "Indigo";
			tab6.TabBarItem = new UITabBarItem ("Indigo", UIImage.FromFile("Images/sixth.png"), 5);
			tab6.View.BackgroundColor = UIColor.FromRGB(75, 0, 130);

			tab7 = new UIViewController();
			tab7.Title = "Violet";
			tab7.TabBarItem = new UITabBarItem ("Violet", UIImage.FromFile("Images/seventh.png"), 6);
			tab7.View.BackgroundColor = UIColor.FromRGB(143, 0, 255);

			tab8 = new UIViewController();
			tab8.Title = "White";
			tab8.TabBarItem = new UITabBarItem ("White", UIImage.FromFile("Images/eighth.png"), 7);
			tab8.View.BackgroundColor = UIColor.White;

			tab9 = new UIViewController();
			tab9.Title = "Black";
			tab9.TabBarItem = new UITabBarItem ("Black", UIImage.FromFile("Images/ninth.png"), 8);
			tab9.View.BackgroundColor = UIColor.Black;

			// The order in this array controls the order of tabs in the Tab Bar
			var tabs = new UIViewController[] {
				tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9
			};

			ViewControllers = tabs;

			SelectedViewController = tab1;

			// tab1 and tab2 are missing, so they will not be customizable
			var customizableControllers = new UIViewController[] {
				tab3, tab4, tab5, tab6, tab7, tab8, tab9
			};

			// Tell the tab bar which controllers are allowed to customize. 
			// If we don't set this, it assumes all controllers are customizable.
			CustomizableViewControllers = customizableControllers;

			// To turn off customization (the 'Edit' button is removed from the More screen)
			//CustomizableViewControllers = null;

			// Change the order IF required
			SetCustomTabBarOrder ();

			// If tab bar order has been edited, save to UserPrefs as a comma-seperated list of Tag ids
			// NOTE: assumes Tag id == order in the initial ViewControllers array
			FinishedCustomizingViewControllers += delegate(object sender, UITabBarCustomizeChangeEventArgs e) {
				Console.WriteLine ("FinishedCustomizingViewControllers - Changed=" + e.Changed);
				if (e.Changed) {
					var count = e.ViewControllers.Length;
					var tabOrderArray = new List<string>();
					foreach(var viewController in e.ViewControllers)  {
						var tag = viewController.TabBarItem.Tag;
						tabOrderArray.Add(tag.ToString());
						Console.WriteLine ("Tag = " + tag);
					}
					NSArray arr = NSArray.FromStrings(tabOrderArray.ToArray());
					NSUserDefaults.StandardUserDefaults["tabBarOrder"] = arr;
				}
			};
		}

		void SetCustomTabBarOrder() 
		{
			var initialViewController = this.ViewControllers;
			var tabBarOrder = NSUserDefaults.StandardUserDefaults.StringArrayForKey("tabBarOrder");
			if (tabBarOrder == null)
				Console.WriteLine ("No custom tab order, use default");
			else {
				Console.WriteLine ("initialViewController = " + initialViewController.Length );

				if (tabBarOrder.Length == initialViewController.Length) {
					Console.WriteLine ("Setting order based on UserPref");
					var newViewControllers = new List<UIViewController>();
					foreach (var tabBarNumber in tabBarOrder) {
						Console.WriteLine ("Tab bar number = " + tabBarNumber);
						newViewControllers.Add(initialViewController[Convert.ToInt32(tabBarNumber)]);	
					}
					this.ViewControllers = newViewControllers.ToArray();
				}
			}
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

