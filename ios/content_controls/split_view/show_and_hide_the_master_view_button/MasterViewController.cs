using System;
using System.Linq;
using MonoTouch.Dialog;
using UIKit;
using CoreFoundation;

namespace SplitView {
	public class MasterViewController : DialogViewController {

		public MasterViewController () : base (UITableViewStyle.Plain, null)
		{
			Root = new RootElement ("Items") {
				new Section () {
					from num in Enumerable.Range (1, 10)
					select (Element) new MonoTouch.Dialog.StringElement("Item " + num)
				}	
			};
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}