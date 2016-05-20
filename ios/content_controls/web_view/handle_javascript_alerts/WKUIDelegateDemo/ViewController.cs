using System;

using UIKit;
using WebKit;
using Foundation;
using System.IO;

namespace WKUIDelegateDemo
{
	// We'll use the IWKUIDelegate protocol to conform to WKUIDelegate

	public partial class ViewController : UIViewController, IWKUIDelegate
	{
		WKWebView webView;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Create a new WKWebView, assign the delegate and add it to the view
			webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
			webView.UIDelegate = this;
			View.AddSubview (webView);

			// Find the Alerts.html file and load it into the WKWebView
			string htmlPath = NSBundle.MainBundle.PathForResource ("Alerts", "html");
			string htmlContents = File.ReadAllText (htmlPath);
			webView.LoadHtmlString (htmlContents, null);
		}

		// Called when a Javascript alert() is called in the WKWebView 
		// The alert panel should display the message and a single "OK" button
		[Foundation.Export ("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptAlertPanel (WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
		{
			// Create and present a native UIAlertController with the message
			var alertController = UIAlertController.Create (null, message, UIAlertControllerStyle.Alert);
			alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
			PresentViewController (alertController, true, null);

			// Call the completion handler 
			completionHandler ();
		}

		// Called when a Javascript confirm() alert is called in the WKWebView
		// The alert panel should display the message with two buttons - "OK" and "Cancel"
		[Export ("webView:runJavaScriptConfirmPanelWithMessage:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptConfirmPanel (WKWebView webView, string message, WKFrameInfo frame, Action<bool> completionHandler)
		{
			// Create a native UIAlertController with the message
			var alertController = UIAlertController.Create (null, message, UIAlertControllerStyle.Alert);

			// Add two actions to the alert. Based on the result we call the completion handles and pass either true or false
			alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, okAction => {
				completionHandler(true);
			}));
			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Default, cancelAction => {
				completionHandler (false);
			}));

			// Present the alert
			PresentViewController (alertController, true, null);
		}

		// Called when a Javascript prompt() alert is called in the WKWebView
		// The alert panel should display the prompt, default placeholder text and two buttons - "OK" and "Cancel"
		[Foundation.Export ("webView:runJavaScriptTextInputPanelWithPrompt:defaultText:initiatedByFrame:completionHandler:")]
		public void RunJavaScriptTextInputPanel (WebKit.WKWebView webView, string prompt, string defaultText, WebKit.WKFrameInfo frame, System.Action<string> completionHandler)
		{
			// Create a native UIAlertController with the message
			var alertController = UIAlertController.Create (null, prompt, UIAlertControllerStyle.Alert);

			// Add a text field to the alert, set the placeholder text and keep a refernce to the field
			UITextField alertTextField = null;
			alertController.AddTextField ((textField) => {
				textField.Placeholder = defaultText;
				alertTextField = textField;
			});

			// Pass the text to the completion handler when the "OK" button is tapped
			alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, okAction => {
				completionHandler (alertTextField.Text);
			}));

			// If "Cancel" is tapped, we can just return null
			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Default, cancelAction => {
				completionHandler (null);
			}));

			// Present the alert
			PresentViewController (alertController, true, null);
		}
	}
}

