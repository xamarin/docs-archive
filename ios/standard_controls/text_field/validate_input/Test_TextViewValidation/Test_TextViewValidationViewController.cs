using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace Test_TextViewValidation
{
	public partial class Test_TextViewValidationViewController : UIViewController
	{
		public Test_TextViewValidationViewController () : base ("Test_TextViewValidationViewController", null)
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
			

			this.FirstText.ShouldReturn += (textField) => {
				textField.ResignFirstResponder (); return true; };
			this.SecondText.ShouldReturn += (textField) => {
				textField.ResignFirstResponder (); return true; };

			// to validate after the user has entered text and moved away from that 
			// text field, use EditingDidEnd
			this.FirstText.EditingDidEnd += (object sender, EventArgs e) => {

				// perform a simple "required" validation
				if ( ((UITextField)sender).Text.Length <= 0 ) {
					// need to update on the main thread to change the border color
					InvokeOnMainThread ( () => {
						this.FirstText.BackgroundColor = UIColor.Yellow;
						this.FirstText.Layer.BorderColor = UIColor.Red.CGColor;
						this.FirstText.Layer.BorderWidth = 3;
						this.FirstText.Layer.CornerRadius = 5;
					} );
				}
			};

			// we can also validate on the touch of a button
			this.ValidateButton.TouchUpInside += (object sender, EventArgs e) => {
				if ( this.SecondText.Text.Length <= 0 ) {
						InvokeOnMainThread ( () => {
						this.SecondText.BackgroundColor = UIColor.Cyan;
						this.SecondText.Layer.BorderColor = UIColor.Brown.CGColor;
						this.SecondText.Layer.BorderWidth = 3;
						this.SecondText.Layer.CornerRadius = 5;
					} );
				}
			};
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

