using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StyledText
{
	/// <summary>
	/// Show colored/underlined text in a UILabel and UITextField
	/// </summary>
	public partial class StyledTextViewController : UIViewController
	{
		UILabel label1;
		UITextField textField1;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			View.BackgroundColor = UIColor.White;


			//
			// Configure attributes for text formatting
			//
			var firstAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.Blue,
				BackgroundColor = UIColor.Yellow,
				Font = UIFont.FromName("Courier", 18f)
			};

			var secondAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.Red,
				BackgroundColor = UIColor.Gray,
				StrikethroughStyle = NSUnderlineStyle.Single
			};

			var thirdAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.Green,
				BackgroundColor = UIColor.Black
			};
			// NOTE: UIStringAttributes are NOT the same as CTStringAttributes in Core Text.
			// Core Text is a separate framework with different use cases and capabilities!


			//
			// UITextField
			//
			textField1 = new UITextField (new CGRect (10, 110, 300, 60));
			textField1.BackgroundColor = UIColor.LightGray;
			View.Add (textField1);

			// Apply the same style to the entire control
			//textField1.AttributedText = new NSAttributedString("UITextField is pretty!", firstAttributes);

			// Apply different styles to different parts of the displayed text
			var prettyString = new NSMutableAttributedString ("UITextField is not pretty!");
			prettyString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, 11));
			prettyString.SetAttributes (secondAttributes.Dictionary, new NSRange (15, 3));
			prettyString.SetAttributes (thirdAttributes.Dictionary, new NSRange (19, 6));

			textField1.AttributedText = prettyString;


			//
			// UILabel
			//
			label1 = new UILabel (new CGRect (10, 60, 300, 30));
			View.Add (label1);

			// Apply the same style to the entire control
			//label1.AttributedText = new NSAttributedString("UILabel is pretty!", firstAttributes);

			// Apply different styles to different parts of the displayed text
			var myString = new NSMutableAttributedString ("UILabel text formatting...");
			myString.SetAttributes (firstAttributes.Dictionary, new NSRange (0, 7));
			myString.SetAttributes (secondAttributes.Dictionary, new NSRange (8, 4));
			myString.SetAttributes (thirdAttributes.Dictionary, new NSRange (12, 11));

			label1.AttributedText = myString;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation == UIInterfaceOrientation.Portrait);
		}
	}
}

