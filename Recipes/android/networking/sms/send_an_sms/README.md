---
id: ABDD6F9D-BA78-ECD5-3D2A-7E8A5CB8E6E1
title: "Send an SMS"
brief: "This recipe shows how to send an SMS message using the SMSManager or an Intent."
sdk:
  - title: "SmsManager Class Reference" 
    url: http://developer.android.com/reference/android/telephony/gsm/SmsManager.html
---

# Recipe

 [ ![](Images/SmsIntent.png)](Images/SmsIntent.png)

There are two options for sending SMS messages on Android:
- Use `SmsManager` to send messages in the background.
- Use an intent to send the user to the SMS application with a preset number and message.

Follow these steps to send an SMS message using the `SmsManager` class.

-  Add the `SEND_SMS` permission to the Android manifest.
-  Call the `SendTextMessage` method of the `SmsManager`.

```
SmsManager.Default.SendTextMessage ("1234567890", null,
"Hello from Xamarin.Android", null, null);
```

To send an SMS message using an Intent, create the Intent with a `ActionSendto`
action and a `Uri` that begins with `smsto:`. Include the message body in the
Intent’s payload by calling `PutExtra`.

```
var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
var smsIntent = new Intent (Intent.ActionSendto, smsUri);
smsIntent.PutExtra ("sms_body", "Hello from Xamarin.Android");  
StartActivity (smsIntent);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The permission is needed for the case where the `SmsManager` is used to send
the SMS message programmatically. When using the `Intent`, this permission is not
required.

