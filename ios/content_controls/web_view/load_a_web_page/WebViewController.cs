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

			string url = "http://xamarin.com";
			webView.LoadRequest (new NSUrlRequest (new NSUrl (url)));
			
			// if this is false, page will be 'zoomed in' to normal size
			//webView.ScalesPageToFit = true;
		}
	}
}