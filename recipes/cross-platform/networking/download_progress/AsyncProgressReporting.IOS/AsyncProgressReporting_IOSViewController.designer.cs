// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace AsyncProgressReporting.IOS
{
	[Register ("AsyncProgressReporting_IOSViewController")]
	partial class AsyncProgressReporting_IOSViewController
	{
		[Outlet]
		UIKit.UIProgressView ProgressBar { get; set; }

		[Outlet]
		UIKit.UIButton StartDownloadButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}

			if (StartDownloadButton != null) {
				StartDownloadButton.Dispose ();
				StartDownloadButton = null;
			}
		}
	}
}
