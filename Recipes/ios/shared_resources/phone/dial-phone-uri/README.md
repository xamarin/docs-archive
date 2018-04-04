---
id: CD0EE2D2-F5DC-48F1-B859-37200A39ABD0
title: "Dialing the Phone by URI"
brief: "iOS apps utilize uniform resource locators (URLs), a form of uniform resource identifiers (URIs), to access schemes that integrate with system apps. The tel URL scheme launches the Phone app and dials the number contained in the NSUrl."
sdk:
  - title: "NSUrl Class Reference" 
    url: https://developer.apple.com/library/mac/documentation/Cocoa/Reference/Foundation/Classes/NSURL_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>

# Recipe


![Dial By URI](Images/Screenshot.png)

<ol>
  <li>First, we must generate a <code>NSUrl</code> from the string <code>"tel:"</code> appended to the number we want to dial. For this example, lets place the following code in the <code>CallButton</code> <code>TouchUpInside</code> event handler:</li>
</ol>
```
var url = new NSUrl ("tel:" + PhoneTextField.Text);
```
<ol start="2">
  <li>To create a scheme to access the Phone app, we would place the following code after we generate our <code>NSUrl</code> variable:</li>
</ol>
```
    UIApplication.SharedApplication.OpenUrl (url);
```
<ol start="3">
  <li>Since emulators don't support the system Phone app, we should instead attempt to create our scheme inside the following conditional statement:</li>
</ol>
```
    if (!UIApplication.SharedApplication.OpenUrl (url)) {
    	var av = new UIAlertView ("Not supported",
	      "Scheme 'tel:' is not supported on this device",
          null,
	      "OK",
		  null);
	    av.Show ();
    };
```
If the scheme cannot be generated, we alert the user using a `UIAlertView`.

