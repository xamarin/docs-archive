using System;

using Xamarin.Forms;

namespace iPadFormatting
{
	public class TableViewPage : ContentPage
	{
		public TableViewPage ()
		{
			Title = "TableView";

			// sample code from http://developer.xamarin.com/api/type/Xamarin.Forms.TableView/
			var root = new TableRoot ("Table Title") {
				new TableSection ("Section 1 Title") {
					new TextCell {
						Text = "TextCell Text",
						Detail = "TextCell Detail"
					},
					new EntryCell {
						Label = "EntryCell:",
						Placeholder = "default keyboard",
						Keyboard = Keyboard.Default
					}
				},
				new TableSection ("Section 2 Title") {
					new EntryCell {
						Label = "Another EntryCell:",
						Placeholder = "phone keyboard",
						Keyboard = Keyboard.Telephone
					},
					new SwitchCell {
						Text = "SwitchCell:"
					}
				}
			};

			var tv = new TableView {
				Intent = TableIntent.Form,
				Root = root
			};

			Content = tv;
		}
	}
}


