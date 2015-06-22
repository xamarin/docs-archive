using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using AudioToolbox;
using AVFoundation;

namespace SysSound_iOS

{
	public partial class SysSound_iOSViewController : UIViewController
	{
	


		public SysSound_iOSViewController (IntPtr handle) : base (handle)
		{
		}

		//private SystemSound Sound; 
		//private NSUrl url;

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Generate the NSUrl to the sound file
			var url = NSUrl.FromFilename ("Sounds/tap.aif");

			// Generate the SystemSound instance with the NSUrl
			SystemSound newSound = new SystemSound (url);


			// This handles the playSystemButton being pressed
			playSystemButton.TouchUpInside += (object sender, EventArgs e) => {
				// Plays the sound
				newSound.PlaySystemSound(); 

			};


			// This handles the playAlertButton being pressed
			playAlertButton.TouchUpInside += (object sender, EventArgs e) => {
				// PlayAlertSound Plays the sound as well as vibrates
				newSound.PlayAlertSound();

			};

			// This handles the VibrateButton being pressed
			VibrateButton.TouchUpInside += (object sender, EventArgs e) => {
				// Just vibrates the device
				SystemSound.Vibrate.PlaySystemSound ();
			}; 

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

		}


		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}



		#endregion
	}
}