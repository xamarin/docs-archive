using Xamarin.Forms;

namespace DisplayPDF
{
	public class CustomWebView : WebView
	{
		public static readonly BindableProperty UriProperty = BindableProperty.Create<CustomWebView, string> (p => p.Uri, default(string));

		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}
	}
}
