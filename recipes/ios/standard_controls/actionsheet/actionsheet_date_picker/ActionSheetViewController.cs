using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace ActionSheetDatePicker {
	public class ActionSheetViewController: UIViewController {
	
		UIButton showSimpleButton;
		UILabel dateLabel;
		ActionSheetDatePicker actionSheetDatePicker;

		public ActionSheetViewController ()
		{
			Title = "Action Sheet Date Picker";

			showSimpleButton = UIButton.FromType(UIButtonType.RoundedRect);
			showSimpleButton.Frame = new RectangleF(10, 10, 300, 40);
			showSimpleButton.SetTitle ("Choose a date", UIControlState.Normal);
			showSimpleButton.TouchUpInside += (sender, e) => {
				actionSheetDatePicker.Show ();
			};
			

			dateLabel = new UILabel(new RectangleF (10, 60, 300, 40));
			dateLabel.Text = "(waiting for the date)";

			actionSheetDatePicker = new ActionSheetDatePicker (this.View);
			actionSheetDatePicker.Title = "Choose Date:";
			actionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
			actionSheetDatePicker.DatePicker.MinimumDate = DateTime.Today.AddDays (-7);
			actionSheetDatePicker.DatePicker.MaximumDate = DateTime.Today.AddDays (7);			

			actionSheetDatePicker.DatePicker.ValueChanged += (s, e) => {
				dateLabel.Text = (s as UIDatePicker).Date.ToString ();
			};
			
			
			View.AddSubview(showSimpleButton);
			View.AddSubview (dateLabel);
		}
	}
}