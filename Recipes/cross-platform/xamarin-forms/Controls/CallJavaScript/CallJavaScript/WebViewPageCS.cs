using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace CallJavaScript
{
	public class WebViewPageCS : ContentPage
	{
		Entry numberEntry;
		Entry stopEntry;
		WebView webView;

		public WebViewPageCS ()
		{
			numberEntry = new Entry { Text = "5", WidthRequest = 40 };
			stopEntry = new Entry { Text = "10", WidthRequest = 40 };

			var button = new Button { Text = "Call JavaScript" };
			button.Clicked += OnCallJavaScriptButtonClicked;

			webView = new WebView {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			webView.Source = LoadHTMLFileFromResource ();

			Padding = new Thickness (0, 40, 0, 0);
			Content = new StackLayout { 
				Children = {
					new Label {
						Text = "Multiplication Table",
						FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
						HorizontalOptions = LayoutOptions.Center
					},
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.Center,
						Children = {
							new Label { Text = "Number:", VerticalOptions = LayoutOptions.Center }, 
							numberEntry,
							new Label { Text = "Stop:", VerticalOptions = LayoutOptions.Center },
							stopEntry
						}
					},
					button,
					webView
				}
			};
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
