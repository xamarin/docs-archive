//
// TapGestureRecognizer.cs: Single screen that illustrates using a tap gesture recognizer and rotating and image
//
// Author:
//   Nina Vyedin (nina.vyedin@xamarin.com)
//
// Copyright (C) 2013 Xamarin, Inc (http://www.xamarin.com)
// Distributed under MIT License (http://opensource.org/licenses/MIT)
//

using System;
using UIKit;
using CoreGraphics;

namespace TapGesture
{
	public partial class TapGestureViewController : UIViewController
	{
		bool tapped;

		public TapGestureViewController (IntPtr handle) : base (handle)
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

			imageView.UserInteractionEnabled = true;

			UITapGestureRecognizer tapGesture = new UITapGestureRecognizer (TapThat);
			//tapGesture.NumberOfTapsRequired = 2;

			imageView.AddGestureRecognizer (tapGesture);

		}

		void TapThat (UITapGestureRecognizer tap)
		{
			UIAlertController alert;

			if (!tapped) {
				tap.View.Transform *= CGAffineTransform.MakeRotation ((float)Math.PI / 2);
				tapped = true;
				alert = UIAlertController.Create ("Card Tapped", "This card has been tapped", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				PresentViewController (alert, true, null);
			} else {
				tap.View.Transform *= CGAffineTransform.MakeRotation ((float)-Math.PI / 2);
				tapped = false;
				alert = UIAlertController.Create ("Card Untapped", "This card has been untapped", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				PresentViewController (alert, true, null);
			}
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

