---
id: 6216DE35-0CDF-42E5-8719-4A01C90F659C
title: "Choose a keyboard for an Entry"
subtitle: "How to display the correct virtual keyboard for an Entry"
brief: "This recipe shows how to display a virtual keyboard that is appropriate for the input text type, and how to specify additional keyboard options such as capitalization, spellcheck, and suggestions."
api:
  - title: "Entry" 
    url: https://developer.xamarin.com/api/type/Xamarin.Forms.Entry/
---

# Overview

The `Entry` control defines a `Keyboard` property that allows an app to select the virtual keyboard that is displayed for the control. For example, a keyboard for entering a phone number should be different from a keyboard for entering an email address.

## Specifying a virtual Keyboard

The `Keyboard` property is of type `Keyboard`. This class defines read-only properties that are appropriate for different keyboard uses:

- `Default`
- `Text`
- `Chat`
- `Url`
- `Email`
- `Telephone`
- `Numeric`

### Default Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Default`. This specifies that the default virtual keyboard will be displayed. This keyboard is displayed if a `Keyboard` property isn't specified.

```xml
<Entry Keyboard="Default" />
```

The `Default` keyboard is shown below for the three phone platforms.

![](Images/default-winphone.png) ![](Images/default-ios.png) ![](Images/default-android.png)

### Text Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Text`. This specifies that a virtual keyboard suitable for entering text will be displayed.

```xml
<Entry Keyboard="Text" />
```

The `Text` keyboard is shown below for the three phone platforms.

![](Images/text-winphone.png) ![](Images/text-ios.png) ![](Images/text-android.png)

### Chat Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Chat`. This specifies that a virtual keyboard suitable for chatting will be displayed.

```xml
<Entry Keyboard="Chat" />
```

The `Chat` keyboard is shown below for the three phone platforms.

![](Images/chat-winphone.png) ![](Images/chat-ios.png) ![](Images/chat-android.png)

### Url Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Url`. This specifies that a virtual keyboard suitable for entering a URL will be displayed.

```xml
<Entry Keyboard="Url" />
```

The `Url` keyboard is shown below for the three phone platforms.

![](Images/url-winphone.png) ![](Images/url-ios.png) ![](Images/url-android.png)

### Email Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Email`. This specifies that a virtual keyboard suitable for entering an email address will be displayed.

```xml
<Entry Keyboard="Email" />
```

The `Email` keyboard is shown below for the three phone platforms.

![](Images/email-winphone.png) ![](Images/email-ios.png) ![](Images/email-android.png)

### Telephone Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Telephone`. This specifies that a virtual keyboard suitable for entering a phone number will be displayed.

```xml
<Entry Keyboard="Telephone" />
```

The `Telephone` keyboard is shown below for the three phone platforms.

![](Images/telephone-winphone.png) ![](Images/telephone-ios.png) ![](Images/telephone-android.png)

### Numeric Keyboard

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and set the property to `Numeric`. This specifies that a virtual keyboard suitable for entering numeric data will be displayed.

```xml
<Entry Keyboard="Numeric" />
```

The `Numeric` keyboard is shown below for the three phone platforms.

![](Images/numeric-winphone.png) ![](Images/numeric-ios.png) ![](Images/numeric-android.png)

## Specifying additional keyboard options

The `Create` method of the `Keyboard` class accepts a `KeyboardFlags` enumeration that specifies additional keyboard options such as capitalization, spellcheck, and suggestions.

### Sentence capitalization

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and use the `Create` method to specify the `CapitalizeSentence` constant of the `KeyboardFlags` enumeration. This specifies that the first words of sentences will be automatically capitalized.

```
Content = new StackLayout {
  Padding = new Thickness(0,20,0,0),
  Children = {
    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence) }
  }
};
```

### Spellcheck

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and use the `Create` method to specify the `SpellCheck` constant of the `KeyboardFlags` enumeration. This specifies that a spellcheck will be performed on text that the user enters.

```
Content = new StackLayout {
  Padding = new Thickness(0,20,0,0),
  Children = {
    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.Spellcheck) }
  }
};
```

### Suggestions

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and use the `Create` method to specify the `Suggestions` constant of the `KeyboardFlags` enumeration. This specifies that suggested word completions will be offered on text that the user enters.

```
Content = new StackLayout {
  Padding = new Thickness(0,20,0,0),
  Children = {
    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.Suggestions) }
  }
};
```

### All

In the code building the user interface for a page, add a `Keyboard` property to an `Entry`, and use the `Create` method to specify the `All` constant of the `KeyboardFlags` enumeration. This specifies that the first words of sentences will be automatically capitalized, that a spellcheck will be performed on text that the user enters, and that suggested word completions will be offered on text that the user enters.

```
Content = new StackLayout {
  Padding = new Thickness(0,20,0,0),
  Children = {
    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.All) }
  }
};
```

# Summary

This recipe shows how to display a virtual keyboard that is appropriate for the input text type, and how to specify additional keyboard options such as capitalization, spellcheck, and suggestions.

