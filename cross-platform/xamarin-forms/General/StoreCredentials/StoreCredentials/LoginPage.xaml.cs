using System;
using Xamarin.Forms;

namespace StoreCredentials
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string userName = usernameEntry.Text;
            string password = passwordEntry.Text;

            var isValid = AreCredentialsCorrect(userName, password);
            if (isValid)
            {
                bool doCredentialsExist = App.CredentialsService.DoCredentialsExist();
                if (!doCredentialsExist)
                {
                    App.CredentialsService.SaveCredentials(userName, password);
                }

                Navigation.InsertPageBefore(new HomePage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        bool AreCredentialsCorrect(string username, string password)
        {
            return username == Constants.Username && password == Constants.Password;
        }
    }
}
