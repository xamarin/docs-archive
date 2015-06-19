// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace UILabelExample
{
	[Register ("UILabelExampleViewController")]
	partial class UILabelExampleViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel CharWrapLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ClipLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel HeadLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel MiddleLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TailLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel WordWrapLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CharWrapLabel != null) {
				CharWrapLabel.Dispose ();
				CharWrapLabel = null;
			}
			if (ClipLabel != null) {
				ClipLabel.Dispose ();
				ClipLabel = null;
			}
			if (HeadLabel != null) {
				HeadLabel.Dispose ();
				HeadLabel = null;
			}
			if (MiddleLabel != null) {
				MiddleLabel.Dispose ();
				MiddleLabel = null;
			}
			if (TailLabel != null) {
				TailLabel.Dispose ();
				TailLabel = null;
			}
			if (WordWrapLabel != null) {
				WordWrapLabel.Dispose ();
				WordWrapLabel = null;
			}
		}
	}
}
