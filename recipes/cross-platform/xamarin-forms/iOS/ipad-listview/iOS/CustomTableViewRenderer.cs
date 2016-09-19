using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using iPadFormatting.iOS;

[assembly: ExportRenderer (typeof(TableView), typeof(CustomTableViewRenderer))]

namespace iPadFormatting.iOS
{
	/// <summary>
	/// Credit to MiniyahilArekew
	/// https://forums.xamarin.com/discussion/comment/159290/#Comment_159290
	/// </summary>
	public class CustomTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.TableView> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				// Unsubscribe
			}

			if (e.NewElement != null) {
				var listView = Control as UITableView;

				listView.CellLayoutMarginsFollowReadableWidth =false;   
			}
		}

	}
}

