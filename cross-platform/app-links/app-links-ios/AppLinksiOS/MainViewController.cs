
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MonoTouch.Dialog;

namespace AppLinksiOS
{
	public partial class MainViewController : DialogViewController
	{
		public MainViewController () : base (UITableViewStyle.Grouped, null)
		{
			const string YOUR_URL = "http://location/of/your/html/file.html";

			Root = new RootElement ("App Links Sample") {
				new Section {
					new StringElement (YOUR_URL, () => {

						// App Link Navigation
						Rivets.AppLinks.Navigator.Navigate(YOUR_URL);

					})
				}
			};
		}
	}
}
