---
id: 1FD0ABA3-95A1-4830-B279-36CDB7E2B5AD
title: "Call JavaScript from C#"
subtitle: "Evaluating JavaScript hosted in a WebView"
brief: "This recipe shows how to call a JavaScript function from C#, where the JavaScript function is defined in a web page hosted by the WebView control."
article:
  - title: "WebView" 
    url: https://developer.xamarin.com/guides/xamarin-forms/user-interface/webview/
api:
  - title: "WebView" 
    url: https://developer.xamarin.com/api/type/Xamarin.Forms.WebView/
  - title: "HtmlWebViewSource" 
    url: https://developer.xamarin.com/api/type/Xamarin.Forms.HtmlWebViewSource/
---

# Overview

The Xamarin.Forms [`WebView`](https://developer.xamarin.com/api/type/Xamarin.Forms.WebView/) control displays HTML and other web content in an app. Unlike [`Device.OpenUri`](https://developer.xamarin.com/api/member/Xamarin.Forms.Device.OpenUri/p/System.Uri/), which takes the user to the web browser on the device, the `WebView` control displays the web content inside the app. For more information about the `WebView` control, see [WebView](https://developer.xamarin.com/api/type/Xamarin.Forms.WebView/).

## Calling JavaScript

The following code example shows how a JavaScript function can be invoked from C#:

```
void OnCallJavaScriptButtonClicked (object sender, EventArgs e)
{
  ...
  int number = int.Parse (numberEntry.Text);
  int end = int.Parse (stopEntry.Text);

  webView.Eval (string.Format ("printMultiplicationTable({0}, {1})", number, end));
}
```

The [`WebView.Eval`](https://developer.xamarin.com/api/member/Xamarin.Forms.WebView.Eval/p/System.String/) method evaluates the JavaScript that's specified as the method argument. In this example the `printMultiplicationTable` JavaScript function is invoked, which in turn displays a multiplication table for the passed parameters.

The `printMultiplicationTable` JavaScript function is defined in the local HTML file that the `WebView` control loads, as shown in the following code example:

```
<html>
<body>
<script src="http://code.jquery.com/jquery-2.1.4.min.js"></script>
<div id='multiplicationtable'></div>
<script type="text/javascript">
function printMultiplicationTable(num, stop)
{
	var number = parseInt(num);
	var stopNumber = parseInt(stop);

	$('#multiplicationtable').empty();
	for (var index = 1; index <= stopNumber; index++) {
		$('#multiplicationtable').append(number + ' x ' + index + " = " + number * index + '<br/>');
	}
}
</script>
</body>
</html>
```

# Summary

This recipe showed how to call a JavaScript function from C#, where the JavaScript function is defined in a web page hosted by the [`WebView`](https://developer.xamarin.com/api/type/Xamarin.Forms.WebView/) control.

