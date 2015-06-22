using System;
using System.Drawing;

using Foundation;
using UIKit;
using CoreGraphics;

namespace AlertControllerSample
{
	public partial class AlertControllerSampleViewController : UIViewController
	{

		UIButton okayButton, okayCancelButton, textInputButton, actionSheetButton;

		public AlertControllerSampleViewController () : base ()
		{
			Title = "Alert Controller";

			View.BackgroundColor = UIColor.White;
				
		}

		public override void LoadView ()
		{
			base.LoadView ();

			//Alert with One Button
			okayButton = UIButton.FromType (UIButtonType.System);
			okayButton.Frame = new CGRect (10, 30, 300, 40);
			okayButton.SetTitle ("Okay Button", UIControlState.Normal);

			okayButton.TouchUpInside += (sender, e) => {

				//Create Alert
				var okAlertController = UIAlertController.Create ("OK Alert", "This is a sample alert with an OK button.", UIAlertControllerStyle.Alert);

				//Add Action
				okAlertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));

				// Present Alert
				PresentViewController (okAlertController, true, null);
			};

			//Alert with Two Buttons

			okayCancelButton = UIButton.FromType (UIButtonType.System);
			okayCancelButton.Frame = new CGRect (10, 80, 300, 40);
			okayCancelButton.SetTitle ("Okay / Cancel Button", UIControlState.Normal);

			okayCancelButton.TouchUpInside += ((sender, e) => {

				//Create Alert
				var okCancelAlertController = UIAlertController.Create("OK / Cancel Alert", "This is a sample alert with an OK / Cancel Button", UIAlertControllerStyle.Alert);

				//Add Actions
				okCancelAlertController.AddAction(UIAlertAction.Create("Okay", UIAlertActionStyle.Default, alert => Console.WriteLine ("Okay was clicked")));
				okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine ("Cancel was clicked")));

				//Present Alert
				PresentViewController(okCancelAlertController, true, null);
			});

			//Text Input Alert
			textInputButton = UIButton.FromType (UIButtonType.System);
			textInputButton.Frame = new CGRect (10, 130, 300, 40);
			textInputButton.SetTitle ("Text Input", UIControlState.Normal);

			textInputButton.TouchUpInside += ((sender, e) => {

				//Create Alert
				var textInputAlertController = UIAlertController.Create("Text Input Alert", "Hey, input some text", UIAlertControllerStyle.Alert);

				//Add Text Input
				textInputAlertController.AddTextField(textField => {
				});

				//Add Actions
				var cancelAction = UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, alertAction => Console.WriteLine ("Cancel was Pressed"));
				var okayAction = UIAlertAction.Create ("Okay", UIAlertActionStyle.Default, alertAction => Console.WriteLine ("The user entered '{0}'", textInputAlertController.TextFields[0].Text));

				textInputAlertController.AddAction(cancelAction);
				textInputAlertController.AddAction(okayAction);

				//Present Alert
				PresentViewController(textInputAlertController, true, null);
			});

			actionSheetButton = UIButton.FromType (UIButtonType.System);
			actionSheetButton.Frame = new CGRect (10, 180, 300, 40);
			actionSheetButton.SetTitle ("Action Sheet", UIControlState.Normal);

			actionSheetButton.TouchUpInside += ((sender, e) => {

				// Create a new Alert Controller
				UIAlertController actionSheetAlert = UIAlertController.Create("Action Sheet", "Select an item from below", UIAlertControllerStyle.ActionSheet);

				// Add Actions
				actionSheetAlert.AddAction(UIAlertAction.Create("Item One",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item One pressed.")));

				actionSheetAlert.AddAction(UIAlertAction.Create("Item Two",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item Two pressed.")));

				actionSheetAlert.AddAction(UIAlertAction.Create("Item Three",UIAlertActionStyle.Default, (action) => Console.WriteLine ("Item Three pressed.")));

				actionSheetAlert.AddAction(UIAlertAction.Create("Cancel",UIAlertActionStyle.Cancel, (action) => Console.WriteLine ("Cancel button pressed.")));

				// Required for iPad - You must specify a source for the Action Sheet since it is
				// displayed as a popover
				UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
				if (presentationPopover!=null) {
					presentationPopover.SourceView = this.View;
					presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
				}

				// Display the alert
				this.PresentViewController(actionSheetAlert,true,null);
			});

			View.AddSubview (okayButton);
			View.AddSubview (okayCancelButton);
			View.AddSubview (textInputButton);
			View.AddSubview (actionSheetButton);

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

