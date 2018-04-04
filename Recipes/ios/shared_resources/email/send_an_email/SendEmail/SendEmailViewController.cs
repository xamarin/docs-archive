using System;
using CoreGraphics;

using Foundation;
using UIKit;
using MessageUI;

namespace SendEmail
{
    public partial class SendEmailViewController : UIViewController
    {
        MFMailComposeViewController mailController;

        public SendEmailViewController () : base ("SendEmailViewController", null)
        {
        }
        
        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
			// wire up the send-mail compose view controller
			sendButton.TouchUpInside += (sender, e) => {       
				if (MFMailComposeViewController.CanSendMail) {
            
					var to = new string[]{ "john@doe.com" };

					if (MFMailComposeViewController.CanSendMail) {
						mailController = new MFMailComposeViewController ();
						mailController.SetToRecipients (to);
						mailController.SetSubject ("mail test");
						mailController.SetMessageBody ("this is a test", false);
						mailController.Finished += (object s, MFComposeResultEventArgs args) => {

							Console.WriteLine ("result: " + args.Result.ToString ()); // sent or cancelled

							BeginInvokeOnMainThread (() => {
								args.Controller.DismissViewController (true, null);
							});
						};
					}

					this.PresentViewController (mailController, true, null);
				} else {
					new UIAlertView("Mail not supported", "Can't send mail from this device", null, "OK");
				}
            };
        }
    }
}