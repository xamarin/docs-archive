using System;
using CoreGraphics;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using AddressBook;

namespace FindContact
{
    public partial class FindContactViewController : UIViewController
    {
        public FindContactViewController () : base ("FindContactViewController", null)
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
            
            // query the address book directly
            
            using (var addressBook = new ABAddressBook ()) {
            
                // Get all people with a specfied name
                //var people = addressBook.GetPeopleWithName ("John Doe");
                //people.ToList ().ForEach (p => Console.WriteLine ("{0} {1} - ", p.FirstName, p.LastName));
            
                // Use Linq to query contacts based upon any criteria
				addressBook.RequestAccess((bool x, NSError z) => {});
                var people = addressBook.GetPeople ();
                people.ToList ().FindAll (p => p.LastName == "Smith").ForEach (
                p => Console.WriteLine ("{0} {1}", p.FirstName, p.LastName));
            }
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

