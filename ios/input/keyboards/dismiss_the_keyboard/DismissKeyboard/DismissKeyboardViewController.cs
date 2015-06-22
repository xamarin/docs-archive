using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace DismissKeyboard
{
	public partial class DismissKeyboardViewController : UIViewController
	{
		public DismissKeyboardViewController() : base ("DismissKeyboardViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Perform any additional setup after loading the view, typically from a nib.

			this.txtTextBox.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder(); 
				return true;
			};
			txtTextBox.Frame = new CGRect (10, 20, UIScreen.MainScreen.Bounds.Width - 20, 40);

		}
		
		public override void ViewDidUnload()
		{
			base.ViewDidUnload();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

