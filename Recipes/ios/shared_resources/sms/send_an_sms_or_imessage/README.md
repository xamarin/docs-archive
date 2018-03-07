---
id: 3E0CD68A-800E-A5CD-62DF-4E4DB09EF367
title: "Send an SMS or iMessage"
brief: "This recipe shows how to send an SMS or iMessage."
sdk:
  - title: "UIApplication Class Reference" 
    url: http://developer.apple.com/library/ios/#DOCUMENTATION/UIKit/Reference/UIApplication_Class/Reference/Reference.html
  - title: "Apple URL Scheme Reference" 
    url: http://developer.apple.com/library/ios/#featuredarticles/iPhoneURLScheme_Reference/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To send an SMS using the `UIApplication.SharedApplication.OpenUrl` method,
follow these steps.

-  Create an `NSUrl` containing the telephone number to send the SMS to:


```
var smsTo = NSUrl.FromString("sms:18015551234");
```

-  Call `OpenUrl` with the `NSUrl`:


```
UIApplication.SharedApplication.OpenUrl(smsTo);
```

The Messages application will detect if the number you requested has iMessage
enabled and will automatically switch to iMessage if possible. To trigger an
iMessage to an Apple ID, replace the telephone number with the Apple ID:

```
var imessageTo = NSUrl.FromString("sms:john@doe.com");
UIApplication.SharedApplication.OpenUrl(imessageTo);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `OpenUrl` method returns a bool which is false if there is no application
to handle the request. You can detect whether an application is present using
the `CanOpenUrl` method like this:

```
var smsTo = NSUrl.FromString("sms:18015551234");
if (UIApplication.SharedApplication.CanOpenUrl(smsTo)) {
    UIApplication.SharedApplication.OpenUrl(smsTo);
} else {
    // warn the user, or hide the button...
}
```

You cannot specify the text that is placed in the message, nor can you force
a message to be sent via code. Only the user can type and send the message.

