using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NSUserActivitySearch
{
	public class App : Application
	{
		public static TodoItemManager TodoManager { get; private set; }

		public static ITextToSpeech Speech { get; set; }

		public App ()
		{
			TodoManager = new TodoItemManager ();
			MainPage = new NavigationPage (new TodoListPage ());

			MessagingCenter.Subscribe <NSUserActivitySearch.App, string> (this, "ShowItem", async (sender, arg) => {
				var todoItems = TodoManager.All;
				var item = todoItems.FirstOrDefault (i => i.ID == arg);

				if (item != null) {
					await MainPage.Navigation.PopToRootAsync ();
					var todoItemPage = new TodoItemPage ();
					todoItemPage.BindingContext = item;
					await MainPage.Navigation.PushAsync (todoItemPage);
				}
			});
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
