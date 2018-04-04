---
id: CC372492-D4F2-471E-A88D-BAF602383A73
title: "Display an Image based on Screen Orientation"
subtitle: "Handling the SizeChanged event"
brief: "This recipe shows how to use Xamarin.Forms to detect the screen orientation and display an image based on the orientation."
api:
  - title: "SizeChanged" 
    url: https://developer.xamarin.com/api/event/Xamarin.Forms.VisualElement.SizeChanged/
---

# Overview

There are a number of approaches that can be used to detect when the screen orientation changes in a Xamarin.Forms app. The simplest approach is to handle the [`SizeChanged`](https://developer.xamarin.com/api/event/Xamarin.Forms.VisualElement.SizeChanged/) event of the [`Page`](https://developer.xamarin.com/api/type/Xamarin.Forms.Page/). This event fires when either the width or height of the `Page` changes. Then, an image can be chosen for display by comparing the height and width of the `Page`.

## Detecting Screen Orientation

The following code example shows how to display an image based on the screen orientation:

```
public partial class HomePage : ContentPage
{
  public HomePage ()
  {
    InitializeComponent ();
    SizeChanged += OnSizeChanged;
  }

  void OnSizeChanged (object sender, EventArgs e)
  {
    image.Source = ImageSource.FromFile (Height > Width ? "portrait.jpg" : "landscape.jpg");
  }
}
```

The `OnSizeChanged` event handler sets the [`Image.Source`](https://developer.xamarin.com/api/property/Xamarin.Forms.Image.Source/) property to a portrait or landscape image, depending on whether the [`Height`](https://developer.xamarin.com/api/property/Xamarin.Forms.VisualElement.Height/) of the [`ContentPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.ContentPage/) is greater than the [`Width`](https://developer.xamarin.com/api/property/Xamarin.Forms.VisualElement.Width/) of the `ContentPage`.

# Summary

This recipe showed how to use Xamarin.Forms to detect the screen orientation and display an image based on the orientation.

