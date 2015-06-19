// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace Test_TextViewValidation
{
	[Register ("Test_TextViewValidationViewController")]
	partial class Test_TextViewValidationViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField FirstText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField SecondText { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ValidateButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FirstText != null) {
				FirstText.Dispose ();
				FirstText = null;
			}

			if (SecondText != null) {
				SecondText.Dispose ();
				SecondText = null;
			}

			if (ValidateButton != null) {
				ValidateButton.Dispose ();
				ValidateButton = null;
			}
		}
	}
}
