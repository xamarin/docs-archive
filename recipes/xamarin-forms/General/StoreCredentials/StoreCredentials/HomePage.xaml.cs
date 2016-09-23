using System;
using Xamarin.Forms;

namespace StoreCredentials
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}

		async void OnLogoutButtonClicked (object sender, EventArgs e)
		{
			DependencyService.Get<ICredentialsService> ().DeleteCredentials ();
			Navigation.InsertPageBefore (new LoginPage (), this);
			await Navigation.PopAsync ();
		}
	}
}
