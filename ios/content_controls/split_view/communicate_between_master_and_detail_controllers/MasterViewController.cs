using System;
using System.Linq;
using MonoTouch.Dialog;
using UIKit;
using CoreFoundation;

namespace SplitView {
	public class MasterViewController : DialogViewController {

		public event EventHandler<RowClickedEventArgs> RowClicked;
		public class RowClickedEventArgs : EventArgs
		{
			public int Item { get; set; }
			
			public RowClickedEventArgs(int item) : base()
			{ this.Item = item; }
		}

		public MasterViewController () : base (UITableViewStyle.Plain, null)
		{
			Root = new RootElement ("Items") {
				new Section () {
					from num in Enumerable.Range (1, 10)
					select (Element) new MonoTouch.Dialog.StringElement("Item " + num, delegate {
						if (RowClicked != null)
							RowClicked (this, new RowClickedEventArgs(num));
					})
				}	
			};
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}