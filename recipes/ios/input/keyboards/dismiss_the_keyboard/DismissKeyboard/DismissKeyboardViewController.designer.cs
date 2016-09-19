// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;

namespace DismissKeyboard
{
	[Register ("DismissKeyboardViewController")]
	partial class DismissKeyboardViewController
	{
		[Outlet]
		UIKit.UITextField txtTextBox { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (txtTextBox != null) {
				txtTextBox.Dispose ();
				txtTextBox = null;
			}
		}
	}
}
