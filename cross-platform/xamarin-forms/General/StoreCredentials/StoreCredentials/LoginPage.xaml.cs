using System;
using Xamarin.Forms;

namespace StoreCredentials
{
	public partial class LoginPage : ContentPage
	{
		ICredentialsService storeService;

		public LoginPage ()
		{
			InitializeComponent ();

			storeService = DependencyService.Get<ICredentialsService> ();
		}

		async void OnLoginButtonClicked (object sender, EventArgs e)
		{
			string userName = usernameEntry.Text;
			string password = passwordEntry.Text;

			var isValid = AreCredentialsCorrect (userName, password);
			if (isValid) {
				bool doCredentialsExist = storeService.DoCredentialsExist ();
				if (!doCredentialsExist) {
					storeService.SaveCredentials (userName, password);
				}

				Navigation.InsertPageBefore (new HomePage (), this);
				await Navigation.PopAsync ();
			} else {
				messageLabel.Text = "Login failed";
				passwordEntry.Text = string.Empty;
			}
		}

		bool AreCredentialsCorrect (string username, string password)
		{
			return username == Constants.Username && password == Constants.Password;
		}
	}
}
