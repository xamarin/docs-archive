using System.IO;
using DisplayPDF;
using DisplayPDF.iOS;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace DisplayPDF.iOS
{
	public class CustomWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			if (e.NewElement != null) {
				var customWebView = Element as CustomWebView;
				string fileName = Path.Combine (NSBundle.MainBundle.BundlePath, string.Format ("Content/{0}", customWebView.Uri));
				LoadRequest (new NSUrlRequest (new NSUrl (fileName, false)));
				ScalesPageToFit = true;
			}
		}
	}
}
