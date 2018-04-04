using System;
using Xamarin.Forms;

namespace NSUserActivitySearch
{
	public partial class TodoListPage : ContentPage
	{
		public TodoListPage ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.TodoManager.All;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			listView.ItemsSource = null;
		}

		async void OnAddItemActivated (object sender, EventArgs e)
		{
			var todoItem = new TodoItem () {
				ID = Guid.NewGuid ().ToString ()
			};
			var todoPage = new TodoItemPage (true);
			todoPage.BindingContext = todoItem;
			await Navigation.PushAsync (todoPage);
		}

		async void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var todoItem = e.SelectedItem as TodoItem;
			var todoPage = new TodoItemPage ();
			todoPage.BindingContext = todoItem;
			await Navigation.PushAsync (todoPage);
		}
	}
}

