using System;
using CoreGraphics;

using Foundation;
using UIKit;
using AddressBookUI;

namespace ChooseContact
{
    public partial class ChooseContactViewController : UIViewController
    {
        ABPeoplePickerNavigationController _contactController;
        UIButton _chooseContact;
        UILabel _contactName;
        
        public ChooseContactViewController () : base ("ChooseContactViewController", null)
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
            
            _chooseContact = UIButton.FromType (UIButtonType.RoundedRect);
            _chooseContact.Frame = new CGRect (10, 10, 200, 50);
            _chooseContact.SetTitle ("Choose a Contact", UIControlState.Normal);
            _contactName = new UILabel{Frame = new CGRect (10, 70, 200, 50)};
            
            View.AddSubviews (_chooseContact, _contactName);
  
            _contactController = new ABPeoplePickerNavigationController ();
            
            _chooseContact.TouchUpInside += delegate {
                this.PresentModalViewController (_contactController, true); };
            
            _contactController.Cancelled += delegate {
                this.DismissModalViewController (true); };
            
            _contactController.SelectPerson2 += delegate(object sender, ABPeoplePickerSelectPerson2EventArgs e) {
                           
                _contactName.Text = String.Format ("{0} {1}", e.Person.FirstName, e.Person.LastName);
                              
                this.DismissModalViewController (true);
            };
        }
        
        public override void ViewDidUnload ()
        {
            base.ViewDidUnload ();
            
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
            
            ReleaseDesignerOutlets ();
        }
        
        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}

