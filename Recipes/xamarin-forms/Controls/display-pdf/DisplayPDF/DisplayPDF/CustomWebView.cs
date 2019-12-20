using Xamarin.Forms;

namespace DisplayPDF
{
	public class CustomWebView : WebView
	{
        	public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(string), typeof(CustomWebView), default(string));

		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}
	}
}
