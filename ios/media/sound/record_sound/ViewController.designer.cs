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

namespace Sound
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LengthOfRecordingLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton PlayRecordedSoundButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel RecordingStatusLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StartRecordingButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton StopRecordingButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LengthOfRecordingLabel != null) {
				LengthOfRecordingLabel.Dispose ();
				LengthOfRecordingLabel = null;
			}
			if (PlayRecordedSoundButton != null) {
				PlayRecordedSoundButton.Dispose ();
				PlayRecordedSoundButton = null;
			}
			if (RecordingStatusLabel != null) {
				RecordingStatusLabel.Dispose ();
				RecordingStatusLabel = null;
			}
			if (StartRecordingButton != null) {
				StartRecordingButton.Dispose ();
				StartRecordingButton = null;
			}
			if (StopRecordingButton != null) {
				StopRecordingButton.Dispose ();
				StopRecordingButton = null;
			}
		}
	}
}
