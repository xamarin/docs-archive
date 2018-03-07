using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace TextInputLayout
{
    [Activity(Label = "TextInputLayout", MainLauncher = true)]
	public class MainActivity : Activity
	{

		Android.Support.Design.Widget.TextInputLayout usernameLayout;
		TextInputEditText userNameView;

		Android.Support.Design.Widget.TextInputLayout passwordLayout;
		TextInputEditText passwordView;

		Button submitButton;
		View rootView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			rootView = FindViewById<View>(Android.Resource.Id.Content);
			userNameView = FindViewById<TextInputEditText>(Resource.Id.username_edittext);
			usernameLayout = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.username_layout);
			passwordView = FindViewById<TextInputEditText>(Resource.Id.password_edittext);
			passwordLayout = FindViewById<Android.Support.Design.Widget.TextInputLayout>(Resource.Id.password_layout);


			submitButton = FindViewById<Button>(Resource.Id.submitButton);
			submitButton.Click += SubmitButton_Click;
		}

		void SubmitButton_Click(object sender, System.EventArgs e)
		{
			bool isValid = ValidateUserName() && ValidatePassword();



			if (isValid)
			{
				Snackbar.Make(rootView, Resource.String.enter_required_fields_thank_you, Snackbar.LengthLong)
						.Show();
			}
			else 
			{
				Snackbar.Make(rootView, Resource.String.enter_required_fields_error_message, Snackbar.LengthLong)
						.Show();
			}
		}

		bool ValidatePassword()
		{
			if (string.IsNullOrWhiteSpace(passwordView.Text))
			{
				passwordLayout.ErrorEnabled = true;
				passwordLayout.Error = GetString(Resource.String.password_required_error_message);
				return false;
			}
			else
			{
				return true;
			}
		}

		bool ValidateUserName()
		{
			if (string.IsNullOrWhiteSpace(userNameView.Text))
			{
				usernameLayout.ErrorEnabled = true;
				usernameLayout.Error = GetString(Resource.String.username_required_error_message);
				return false;

			}
			else 
			{
				return true;
			}
		}
	}
}

