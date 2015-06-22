using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace TestApp
{
	public partial class TestAppViewController : UIViewController
	{
		SomeView v;

		public TestAppViewController () : base ("TestAppViewController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			v = new SomeView (){Frame = View.Frame};
			View.AddSubview (v);
		}
	}
}

