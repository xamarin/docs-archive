using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using ObjCRuntime;

namespace TestApp
{
	partial class SomeView : UIView
	{
		//We can make the control public so that it can be controlled on a per-instance basis in the ViewController.
		public UILabel label {get {return MyLabel;}}

		public SomeView (IntPtr handle) : base (handle)
		{
			
		}

		public static SomeView Create()
		{
			
			var arr = NSBundle.MainBundle.LoadNib ("SomeView", null, null);
			var v = Runtime.GetNSObject<SomeView> (arr.ValueAt(0));

			return v;


		}

		//To set the text for all instances of SomeView, do it in the AwakeFromNib method.
		//If you would rather set the label text on a per-instance basis, comment the method below
		//and uncomment the line `v.label.Text = "Hello from label!";` in the ViewController.cs
		public override void AwakeFromNib()
		{

			MyLabel.Text = "hello from the SomeView class";
		}
	}

}
