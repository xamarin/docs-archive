using System.IO;
using Foundation;
using UIKit;
using CoreGraphics;

namespace WebView
{

	public class WebViewController : UIViewController
	{

		UIWebView webView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "WebView";
			View.BackgroundColor = UIColor.White;

			webView = new UIWebView (View.Bounds);
			View.AddSubview (webView);

			//string fileName = "Loading a Web Page.pdf";
			string fileName = "Loading a Web Page.docx";

			string localDocUrl = Path.Combine (NSBundle.MainBundle.BundlePath, fileName);
			webView.LoadRequest (new NSUrlRequest (new NSUrl (localDocUrl, false)));

			// if this is false, page will be 'zoomed in' to normal size
			webView.ScalesPageToFit = true;
		}
	}
}