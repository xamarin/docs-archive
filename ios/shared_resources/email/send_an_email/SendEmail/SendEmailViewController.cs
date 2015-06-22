using System;
using CoreGraphics;

using Foundation;
using UIKit;
using MessageUI;

namespace SendEmail
{
    public partial class SendEmailViewController : UIViewController
    {
        UIButton button;
        MFMailComposeViewController mailController;
		string[] to;

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
            
            // Perform any additional setup after loading the view, typically from a nib.
            
            button = UIButton.FromType (UIButtonType.RoundedRect);
            button.Frame = new CGRect (10, 10, 100, 44);
            button.SetTitle ("Send Email", UIControlState.Normal);
            View.AddSubview (button);
			to = new string[]{ "john@doe.com" };

			if (MFMailComposeViewController.CanSendMail) {
				mailController = new MFMailComposeViewController ();
				mailController.SetToRecipients (to);
				mailController.SetSubject ("mail test");
				mailController.SetMessageBody ("this is a test", false);
				mailController.Finished += ( object s, MFComposeResultEventArgs args) => {
                
					Console.WriteLine (args.Result.ToString ());
                
					BeginInvokeOnMainThread (() => {
						args.Controller.DismissViewController (true, null);
					});
				};
			}
            button.TouchUpInside += (sender, e) => {       
				if (MFMailComposeViewController.CanSendMail) {
                	this.PresentViewController (mailController, true, null);
				} else {
					new UIAlertView("Mail not supported", "Can't send mail from this device", null, "OK");
				}
            };
        }
        
        
    }
}

