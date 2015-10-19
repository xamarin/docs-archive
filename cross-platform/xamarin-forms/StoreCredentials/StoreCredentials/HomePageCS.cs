using System;
using Xamarin.Forms;

namespace StoreCredentials
{
	public class HomePageCS : ContentPage
	{
		public HomePageCS ()
		{
			var toolbarItem = new ToolbarItem {
				Text = "Logout"
			};
			toolbarItem.Clicked += OnLogoutButtonClicked;
			ToolbarItems.Add (toolbarItem);

			Title = "Home Page";
			Content = new StackLayout { 
				Children = {
					new Label {
						Text = "Main app content goes here",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}

		async void OnLogoutButtonClicked (object sender, EventArgs e)
		{
			DependencyService.Get<ICredentialsService> ().DeleteCredentials ();
			Navigation.InsertPageBefore (new LoginPage (), this);
			await Navigation.PopAsync ();
		}
	}
}
