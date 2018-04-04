using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace CallJavaScript
{
	public partial class WebViewPage : ContentPage
	{
		public WebViewPage ()
		{
			InitializeComponent ();

			webView.Source = LoadHTMLFileFromResource ();
		}

		HtmlWebViewSource LoadHTMLFileFromResource ()
		{
			var source = new HtmlWebViewSource ();

			// Load the HTML file embedded as a resource in the PCL
			var assembly = typeof(WebViewPage).GetTypeInfo ().Assembly;
			var stream = assembly.GetManifestResourceStream ("CallJavaScript.index.html");
			using (var reader = new StreamReader (stream)) {
				source.Html = reader.ReadToEnd ();
			}
			return source;
		}

		void OnCallJavaScriptButtonClicked (object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace (numberEntry.Text) || string.IsNullOrWhiteSpace (stopEntry.Text)) {
				return;
			}

			int number = int.Parse (numberEntry.Text);
			int end = int.Parse (stopEntry.Text);
            
			webView.Eval (string.Format ("printMultiplicationTable({0}, {1})", number, end));
		}
	}
}
