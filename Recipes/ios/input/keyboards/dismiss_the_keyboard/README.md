---
id: 6468AABB-C81A-D68E-C315-0654E4234BC3
title: "Dismiss the Keyboard"
brief: "This recipe will show how to hide the keyboard."
sdk:
  - title: "Responder Object" 
    url: http://developer.apple.com/library/ios/#documentation/general/conceptual/Devpedia-CocoaApp/Responder.html
---

<a name="Recipe" class="injected"></a>


# Recipe

The keyboard does not automatically disappear when the return key is pressed
and may cover or obscure certain parts of the UI. To dismiss the keyboard, send
the UIResponder.ResignFirstResponder message to the text field that is currently
the first responder.

1.  Provide  `ShouldReturn` with a delegate or anonymous method that will call  `ResignFirstResponder` on the field:


```
this.txtDefault.ShouldReturn += (textField) => {
    textField.ResignFirstResponder();
    return true;
};
```

