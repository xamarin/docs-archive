using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.UIKit;

namespace BasicTable {
	public class HomeScreen : UIViewController {
		UITableView table;

		public HomeScreen ()
		{	
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			table = new UITableView(new RectangleF(0, 20, View.Bounds.Width, View.Bounds.Width - 20)); // defaults to Plain style
			table.AutoresizingMask = UIViewAutoresizing.All;
			string[] tableItems = new string[] {"Vegetables","Fruits","Flower Buds","Legumes","Bulbs","Tubers"};
			table.Source = new TableSource(tableItems);
			Add (table);
		}
	}
}