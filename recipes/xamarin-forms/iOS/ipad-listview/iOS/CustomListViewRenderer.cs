using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using iPadFormatting.iOS;
using UIKit;

[assembly: ExportRenderer (typeof(ListView), typeof(CustomListViewRenderer))]

namespace iPadFormatting.iOS
{
	/// <summary>
	/// Credit to MiniyahilArekew
	/// https://forums.xamarin.com/discussion/comment/159290/#Comment_159290
	/// </summary>
	public class CustomListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.ListView> e)
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

