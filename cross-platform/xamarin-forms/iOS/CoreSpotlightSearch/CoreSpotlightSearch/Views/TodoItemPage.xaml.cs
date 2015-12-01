using System;
using Xamarin.Forms;

namespace CoreSpotlightSearch
{
	public partial class TodoItemPage : ContentPage
	{
		ISpotlightSearch spotlightSearch;
		bool isNewItem;

		public TodoItemPage (bool isNew = false)
		{
			InitializeComponent ();
			isNewItem = isNew;

			if (Device.OS == TargetPlatform.iOS) {
				spotlightSearch = DependencyService.Get<ISpotlightSearch> ();
			}
		}

		async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			if (isNewItem) {
				App.TodoManager.Insert (todoItem);
			} else {
				App.TodoManager.Update (todoItem);
			}

			if (Device.OS == TargetPlatform.iOS) {
				spotlightSearch.CreateSearchItem (todoItem);
			}
			await Navigation.PopAsync ();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.TodoManager.Delete (todoItem.ID);

			if (Device.OS == TargetPlatform.iOS) {
				spotlightSearch.DeleteSearchItem (todoItem);
			}
			await Navigation.PopAsync ();
		}

		async void OnCancelActivated (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		void OnSpeakActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.Speech.Speak (todoItem.Name + " " + todoItem.Notes);
		}
	}
}
