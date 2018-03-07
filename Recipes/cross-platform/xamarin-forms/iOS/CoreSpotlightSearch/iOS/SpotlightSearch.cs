using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CoreSpotlight;
using CoreSpotlightSearch.iOS;
using MobileCoreServices;
using Xamarin.Forms;

[assembly:Dependency (typeof(SpotlightSearch))]
namespace CoreSpotlightSearch.iOS
{
	public class SpotlightSearch : ISpotlightSearch
	{
		List<TodoItem> todoItems;

		public SpotlightSearch ()
		{
		}

		public SpotlightSearch (List<TodoItem> items)
		{
			todoItems = items;

			// Re-index the app's data
			ReIndexSearchItems (todoItems);
		}

		public void CreateSearchItem (TodoItem item)
		{
			// Create attributes to describe item
			var attributes = new CSSearchableItemAttributeSet (UTType.Text);
			attributes.Title = item.Name;
			attributes.ContentDescription = item.Notes;

			// Create item
			var searchableItem = new CSSearchableItem (item.ID, "com.companyname.corespotlightsearch", attributes);

			// Index item
			CSSearchableIndex.DefaultSearchableIndex.Index (new CSSearchableItem[]{ searchableItem }, error => {
				if (error != null) {
					Debug.WriteLine (error);
				} else {
					Debug.WriteLine ("Successfully indexed item");
				}
			});
		}

		public void DeleteSearchItem (TodoItem item)
		{
			CSSearchableIndex.DefaultSearchableIndex.Delete (new string[]{ item.ID }, error => {
				if (error != null) {
					Debug.WriteLine (error);
				} else {
					Debug.WriteLine ("Successfully deleted item");
				}
			});
		}

		void ReIndexSearchItems (List<TodoItem> items)
		{
			var searchableItems = new List<CSSearchableItem> ();
			foreach (var item in todoItems) {
				// Create attributes to describe item
				var attributes = new CSSearchableItemAttributeSet (UTType.Text);
				attributes.Title = item.Name;
				attributes.ContentDescription = item.Notes;

				// Create item 
				var searchableItem = new CSSearchableItem (item.ID, "com.companyname.corespotlightsearch", attributes);
				searchableItems.Add (searchableItem);
			}

			// Index items
			CSSearchableIndex.DefaultSearchableIndex.Index (searchableItems.ToArray<CSSearchableItem> (), error => {
				if (error != null) {
					Debug.WriteLine (error);
				} else {
					Debug.WriteLine ("Successfully indexed items");
				}
			});
		}
	}
}
