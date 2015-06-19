using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace ActionSheet {
	public class ActionSheetViewController: UIViewController {
	
		UIButton showSimpleButton, showComplexButton;
		UIActionSheet actionSheet;

		public ActionSheetViewController ()
		{
			Title = "Action Sheet";

			showSimpleButton = UIButton.FromType(UIButtonType.RoundedRect);
			showSimpleButton.Frame = new RectangleF(10, 10, 300, 40);
			showSimpleButton.SetTitle ("Show Simple ActionSheet", UIControlState.Normal);
			showSimpleButton.TouchUpInside += (sender, e) => {
				actionSheet = new UIActionSheet ("Simple ActionSheet", null, "Cancel", "Delete", null);
				actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
					Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked");
				};
				actionSheet.ShowInView (View);
			};
			
			showComplexButton = UIButton.FromType(UIButtonType.RoundedRect);
			showComplexButton.Frame = new RectangleF(10, 60, 300, 40);
			showComplexButton.SetTitle ("Show Complex ActionSheet", UIControlState.Normal);
			showComplexButton.TouchUpInside += (sender, e) => {
				actionSheet = new UIActionSheet ("ActionSheet with Buttons");
				actionSheet.AddButton ("Delete");
				actionSheet.AddButton ("Cancel");
				actionSheet.AddButton ("A different option!");
				actionSheet.AddButton ("Another option");
				actionSheet.DestructiveButtonIndex = 0;
				actionSheet.CancelButtonIndex = 1;
				//actionSheet.FirstOtherButtonIndex = 2;
				actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
					Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked");
				};
				actionSheet.ShowInView (View);
			};

			View.AddSubview(showSimpleButton);
			View.AddSubview(showComplexButton);
		}
	}
}

