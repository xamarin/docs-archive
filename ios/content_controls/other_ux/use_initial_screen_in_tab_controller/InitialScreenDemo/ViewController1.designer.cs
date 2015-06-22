// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace InitialScreenDemo
{
	[Register ("ViewController1")]
	partial class ViewController1
	{
		[Outlet]
		MonoTouch.UIKit.UIButton aButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (aButton != null) {
				aButton.Dispose ();
				aButton = null;
			}
		}
	}
}
