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

namespace DialPhoneURI
{
	[Register ("DialPhoneURIViewController")]
	partial class DialPhoneURIViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CallButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField PhoneTextField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}
			if (PhoneTextField != null) {
				PhoneTextField.Dispose ();
				PhoneTextField = null;
			}
		}
	}
}
