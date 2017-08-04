using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StoreCredentials
{
    public class App : Application
    {
        public static string AppName { get { return "StoreAccountInfoApp"; } }

        public static ICredentialsService CredentialsService { get; private set; }

        public App()
        {
            CredentialsService = new CredentialsService();
            if (CredentialsService.DoCredentialsExist())
            {
                MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

