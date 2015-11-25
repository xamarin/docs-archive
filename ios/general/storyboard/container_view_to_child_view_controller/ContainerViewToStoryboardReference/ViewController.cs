using System;

using UIKit;

namespace ContainerViewToStoryboardReference
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
        {
            switch (segue.Identifier)
            {
                case "PictureAEmbed":
                    var topSelectorViewController = (ImageSelectorViewController)segue.DestinationViewController;
                    topSelectorViewController.ImageSelected += (s, e) => topPicture.Text = e.Value;
                    break;
               default:
                    //Another segue
                    break;
            }
        }
    }
}

