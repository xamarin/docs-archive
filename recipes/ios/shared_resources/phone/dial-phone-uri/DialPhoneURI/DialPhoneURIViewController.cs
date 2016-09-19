using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace DialPhoneURI
{
	public partial class DialPhoneURIViewController : UIViewController
	{

		public DialPhoneURIViewController (IntPtr handle) : base (handle)
		{
		}

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
			
			// Perform any additional setup after loading the view, typically from a nib.
			//CallButton.Enabled = false;
			PhoneTextField.KeyboardType = UIKeyboardType.PhonePad;
			//PhoneTextField.BecomeFirstResponder();

			PhoneTextField.TouchUpInside += (object sender, EventArgs e) => 
			{
				CallButton.Enabled = true; 

			};

			CallButton.TouchUpInside += (object sender, EventArgs e) => {
			
				// Create a NSUrl 
				var url = new NSUrl ("tel:" + PhoneTextField.Text);

				// Use URL handler with tel: prefix to invoke Apple's Phone app, 
				// otherwise show an alert dialog                


				if (!UIApplication.SharedApplication.OpenUrl (url)) {
					var av = new UIAlertView ("Not supported",
						         "Scheme 'tel:' is not supported on this device",
						         null,
						         "OK",
						         null);
					av.Show ();


				};


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

