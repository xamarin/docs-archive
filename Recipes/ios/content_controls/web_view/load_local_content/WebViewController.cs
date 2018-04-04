using System.IO;
using Foundation;
using UIKit;

namespace WebView {

	public class WebViewController : UIViewController {
		
		UIWebView webView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			Title = "WebView";
			View.BackgroundColor = UIColor.White;

			webView = new UIWebView(View.Bounds);			
			View.AddSubview(webView);
			
			// html, image, css files must have Build Action:Content
			string fileName = "Content/Home.html"; // remember case sensitive
			
			string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);
			webView.LoadRequest (new NSUrlRequest (new NSUrl (localHtmlUrl, false)));
			webView.ScalesPageToFit = true;
			webView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			
			#region Additional Info
			// can also manually create html
			// passing the 'base' path to LoadHtmlString lets relative Urls for for links, images, etc
			//string contentDirectoryPath = Path.Combine(NSBundle.MainBundle.BundlePath,"Content/");
			//webView.LoadHtmlString ("<html><a href='Home.html'>Click Me</a>", new NSUrl (contentDirectoryPath, true));
			#endregion
		}
	}
}