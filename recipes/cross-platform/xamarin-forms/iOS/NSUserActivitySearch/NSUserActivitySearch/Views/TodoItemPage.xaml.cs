using System;
using Xamarin.Forms;

namespace NSUserActivitySearch
{
	public partial class TodoItemPage : ContentPage
	{
		IUserActivity userActivity;
		bool isNewItem;

		public TodoItemPage (bool isNew = false)
		{
			InitializeComponent ();
			isNewItem = isNew;

			if (Device.OS == TargetPlatform.iOS) {
				userActivity = DependencyService.Get<IUserActivity> ();
			}
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (Device.OS == TargetPlatform.iOS) {
				var todoItem = (TodoItem)BindingContext;
				userActivity.Start (todoItem);
			}
		}

		protected override void OnDisappearing ()
		{
			if (Device.OS == TargetPlatform.iOS) {
				userActivity.Stop ();
			}

			base.OnDisappearing ();
		}

		async void OnSaveActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			if (isNewItem) {
				App.TodoManager.Insert (todoItem);
			} else {
				App.TodoManager.Update (todoItem);
			}
			await Navigation.PopAsync ();
		}

		async void OnDeleteActivated (object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;
			App.TodoManager.Delete (todoItem.ID);
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

