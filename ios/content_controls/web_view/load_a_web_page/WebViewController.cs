using System;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;
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

			var url = "https://xamarin.com"; // NOTE: https required for iOS 9 ATS
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));
			
			// if this is false, page will be 'zoomed in' to normal size
			//webView.ScalesPageToFit = true;


			// iOS 9 ATS docs
			// http://developer.xamarin.com/guides/ios/platform_features/introduction_to_ios9/ats/
		}
	}
}