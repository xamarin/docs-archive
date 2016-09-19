using System;

using UIKit;

namespace TestApp
{
	public partial class ViewController : UIViewController
	{
		SomeView v;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			v = SomeView.Create();
			v.Frame = View.Frame;

			//Uncomment this line, and comment out the AwakeFromNib method in SomeView.cs,
			//to set the label on a per-instance basis.
//			v.label.Text = "Hello from label!";

			View.AddSubview (v);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

