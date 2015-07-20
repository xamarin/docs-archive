using System;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;

namespace BasicTable {
	public class HomeScreen : UIViewController {
		UITableView table;

		public HomeScreen ()
		{	
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new UITableView(new CGRect(0,20,View.Bounds.Width, View.Bounds.Height - 20)); // defaults to Plain style
			table.AutoresizingMask = UIViewAutoresizing.All;
			string[] tableItems = new string[] {"Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers"};
			table.Source = new TableSource(tableItems, this);
			Add (table);
		}
	}
}