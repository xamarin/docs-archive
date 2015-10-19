using System.Linq;
using StoreCredentials.iOS;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Dependency (typeof(CredentialsService))]
namespace StoreCredentials.iOS
{
	public class CredentialsService : ICredentialsService
	{
		public string UserName { 
			get {
				var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
				return (account != null) ? account.Username : null;
			}
		}

		public string Password {
			get {
				var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
				return (account != null) ? account.Properties ["Password"] : null;
			}
		}

		public void SaveCredentials (string userName, string password)
		{
			if (!string.IsNullOrWhiteSpace (userName) && !string.IsNullOrWhiteSpace (password)) {
				Account account = new Account {
					Username = userName	
				};
				account.Properties.Add ("Password", password);
				AccountStore.Create ().Save (account, App.AppName);
			}

		}

		public void DeleteCredentials ()
		{
			var account = AccountStore.Create ().FindAccountsForService (App.AppName).FirstOrDefault ();
			if (account != null) {
				AccountStore.Create ().Delete (account, App.AppName);
			}
		}

		public bool DoCredentialsExist ()
		{
			return AccountStore.Create ().FindAccountsForService (App.AppName).Any () ? true : false;
		}
	}
}
