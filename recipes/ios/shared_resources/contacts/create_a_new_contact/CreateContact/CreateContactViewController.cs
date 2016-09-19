using System;
using CoreGraphics;

using Foundation;
using UIKit;
using AddressBookUI;
using AddressBook;

namespace CreateContact
{
    public partial class CreateContactViewController : UIViewController
    {
        ABNewPersonViewController _newPersonController;
        UIButton _createContact;
        UILabel _contactName;
        
        public CreateContactViewController () : base ("CreateContactViewController", null)
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
            
            Title = "How to Create a Contact";
            
            View.BackgroundColor = UIColor.White;
            
            _createContact = UIButton.FromType (UIButtonType.RoundedRect);
            _createContact.Frame = new CGRect (10, 60, 300, 50);
            _createContact.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            _createContact.SetTitle ("Create a Contact", UIControlState.Normal);
            _contactName = new UILabel{Frame = new CGRect (10, 120, 300, 50)};
            _contactName.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            
            View.AddSubviews (_createContact, _contactName);
            
            _newPersonController = new ABNewPersonViewController ();
            
            _createContact.TouchUpInside += (sender, e) => {
                           
                var person = new ABPerson ();
                person.FirstName = "John";
                person.LastName = "Doe";
                
                _newPersonController.DisplayedPerson = person;

                NavigationController.PushViewController (_newPersonController, true);
            };
            
            _newPersonController.NewPersonComplete += (object sender, ABNewPersonCompleteEventArgs e) => {
                
                if (e.Completed) {
                    _contactName.Text = String.Format ("new contact: {0} {1}", e.Person.FirstName, e.Person.LastName);
                } else {
                    _contactName.Text = "cancelled";
                }
                
                NavigationController.PopViewController (true);
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

