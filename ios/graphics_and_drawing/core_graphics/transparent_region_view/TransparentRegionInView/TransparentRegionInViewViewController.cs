using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace TransparentRegionInView
{
	public partial class TransparentRegionInViewViewController : UIViewController
	{
		TransparentRegionView v;

		public TransparentRegionInViewViewController () : base ("TransparentRegionInViewViewController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			v = new TransparentRegionView ();
			v.Frame = UIScreen.MainScreen.Bounds;
			View.AddSubview (v);
		}
	}
}

