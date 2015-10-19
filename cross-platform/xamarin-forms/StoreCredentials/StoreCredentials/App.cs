using Xamarin.Forms;

namespace StoreCredentials
{
	public class App : Application
	{
		public static string AppName { get { return "StoreAccountInfoApp"; } }

		public App ()
		{
			if (DependencyService.Get<ICredentialsService> ().DoCredentialsExist ()) {
				MainPage = new NavigationPage (new HomePage ());
			} else {
				MainPage = new NavigationPage (new LoginPage ());
			}
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

