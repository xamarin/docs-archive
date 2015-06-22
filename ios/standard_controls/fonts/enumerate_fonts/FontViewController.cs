using System;
using CoreGraphics;
using System.Text;
using UIKit;

namespace MapView {

	public class FontViewController : UIViewController {
		
		UITextView fontListTextView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Fonts";
			View.BackgroundColor = UIColor.White;
			
			fontListTextView = new UITextView(View.Bounds);
			fontListTextView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			fontListTextView.Editable = false;
			Add (fontListTextView);

			var fontList = new StringBuilder();
			var familyNames = UIFont.FamilyNames;
			foreach (var familyName in familyNames ){
				fontList.Append(String.Format("Family: {0}\n", familyName));
				var fontNames = UIFont.FontNamesForFamilyName(familyName);
				foreach (var fontName in fontNames ){
					fontList.Append(String.Format("\tFont: {0}\n", fontName));
				}
			}
			fontListTextView.Text = fontList.ToString();
		}
	}
}