// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace SetUITextFieldFocus
{
	[Register ("SetUITextFieldFocusViewController")]
	partial class SetUITextFieldFocusViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField FocusTextField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (FocusTextField != null) {
				FocusTextField.Dispose ();
				FocusTextField = null;
			}
		}
	}
}
