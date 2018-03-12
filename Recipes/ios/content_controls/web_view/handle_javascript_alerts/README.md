---
id: 3EA5CD58-4281-43D7-8BD7-0B8830326294
title: "Handle JavaScript Alerts"
brief: "This recipe shows how to handle JavaScript alerts in a WKWebView by implementing WKUIDelegate."
sdk:
  - title: "WKUIDelegate Class Reference" 
    url: https://developer.apple.com/library/ios/documentation/WebKit/Reference/WKUIDelegate_Ref/
---


# Recipe

To handle JavaScript alerts in a <code>WKWebView</code>:

<ol><li>Add the html file (in this example <b>Alerts.html</b>) to your project. Set the <b>Build Action</b> to <b>BundleResource</b>. You can set the <b>Build Action</b> for a file by right-clicking on that file and and choosing <b>Build Action</b> in the menu.</li></ol>

<ol start=2><li>Inherit from the <code>IWKUIDelegate</code> protocol:</li></ol>

```
public partial class ViewController : UIViewController, IWKUIDelegate
```

<ol start=3><li>Create a <code>WKWebView</code> and add it to the <code>View</code>:</li></ol>

```
webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
View.AddSubview (webView);
```

<ol start=4>
  <li>Load the local <code>Alerts.html</code> file into the <code>WKWebView</code>. The easiest way to do this is with <code>LoadHtmlString</code>.</li>
</ol>


```
string htmlPath = NSBundle.MainBundle.PathForResource ("Alerts", "html");
string htmlContents = File.ReadAllText (htmlPath);
webView.LoadHtmlString (htmlContents, null);
```

<ol start=5>
<li>
Implement the <code>RunJavaScriptAlertPanel</code> method, which will handle a call to <code>alert()</code> in the <code>WKWebView</code>. This allows us to present a native <code>UIAlertController</code>. The alert should display the message and a single <b>OK</b> button.
</li>
</ol>

```		
[Foundation.Export ("webView:runJavaScriptAlertPanelWithMessage:initiatedByFrame:completionHandler:")]
public void RunJavaScriptAlertPanel (WebKit.WKWebView webView, string message, WebKit.WKFrameInfo frame, System.Action completionHandler)
{
	var alertController = UIAlertController.Create (null, message, UIAlertControllerStyle.Alert);
	alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, null));
	PresentViewController (alertController, true, null);

	completionHandler ();
}
```

<ol start=6>
<li>
Implement <code>RunJavaScriptConfirmPanel</code>, which will handle a call to <code>confirm()</code> in the <code>WKWebView</code>. The alert should display the message and two buttons - <b>OK</b> and <b>Cancel</b>.
</li>
</ol>

```
[Export ("webView:runJavaScriptConfirmPanelWithMessage:initiatedByFrame:completionHandler:")]
public void RunJavaScriptConfirmPanel (WKWebView webView, string message, WKFrameInfo frame, Action<bool> completionHandler)
{
	var alertController = UIAlertController.Create (null, message, UIAlertControllerStyle.Alert);

	alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, okAction => {
		completionHandler(true);
	}));
	alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Default, cancelAction => {
		completionHandler (false);
	}));

	PresentViewController (alertController, true, null);
}
```

<ol start=7>
<li>
Implement <code>RunJavaScriptTextInputPanel</code>, which will handle a call to <code>prompt()</code> in the <code>WKWebView</code>. The alert should display the prompt, default placeholder text and two buttons - <b>OK</b> and <b>Cancel</b>.
</li>
</ol>

```
[Foundation.Export ("webView:runJavaScriptTextInputPanelWithPrompt:defaultText:initiatedByFrame:completionHandler:")]
public void RunJavaScriptTextInputPanel (WebKit.WKWebView webView, string prompt, string defaultText, WebKit.WKFrameInfo frame, System.Action<string> completionHandler)
{
	var alertController = UIAlertController.Create (null, prompt, UIAlertControllerStyle.Alert);

	UITextField alertTextField = null;
	alertController.AddTextField ((textField) => {
		textField.Placeholder = defaultText;
		alertTextField = textField;
	});

	alertController.AddAction (UIAlertAction.Create ("Ok", UIAlertActionStyle.Default, okAction => {
		completionHandler (alertTextField.Text);
	}));

	alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Default, cancelAction => {
		completionHandler (null);
	}));

	PresentViewController (alertController, true, null);
}
```

# Additional Information

In this example, we are using a local html file, but this will work for any website that displays JavaScript alerts.

[ ![](Images/basic_alert.png)](Images/basic_alert.png)
