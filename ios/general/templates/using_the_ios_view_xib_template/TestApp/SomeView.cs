using System;
using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace TestApp
{
	[Register("SomeView")]
	public partial class SomeView : UIView
	{
		public SomeView(IntPtr h): base(h)
		{
		}

		public SomeView ()
		{
		    var arr = NSBundle.MainBundle.LoadNib("SomeView", this, null);			
			var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
			v.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
			AddSubview(v);

			MyLabel.Text = "hello from the SomeView class";
		}
	}
}