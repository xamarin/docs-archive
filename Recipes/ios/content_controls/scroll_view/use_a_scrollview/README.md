---
id: 2D35598D-04B2-BE01-C1A0-5C17810C77E0
title: "Use a ScrollView"
brief: "This recipe shows how to load a large image into a scroll view."
sdk:
  - title: "UIScrollView Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIScrollView_Class/Reference/UIScrollView.html
  - title: "Scroll View Programming Guide" 
    url: https://developer.apple.com/library/ios/#documentation/WindowsViews/Conceptual/UIScrollView_pg/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To display an image inside a scroll view:

1. Add the image to your Xamarin.iOS project and ensure the <span class="s2"><strong>Build Action</strong></span> is set to <span class="s2"><strong>BundleResource</strong></span>. The example code loads the image
“halloween.jpg”

2. Declare class level fields for a `UIScrollView` and `UIImageVIew`.

```
UIScrollView scrollView;
UIImageView imageView;
```

<ol start="3">
  <li>Create a <code>UIScrollView</code> and it to the View Controller:</li>
</ol>

```
scrollView = new UIScrollView (
new CGRect (0, 0, View.Frame.Width, View.Frame.Height));
View.AddSubview (scrollView);
```

<ol start="4">
  <li>Create a <code>UIImageView</code> with the image <b>halloween.jpg</b>. and add it to the Scroll View:</li>
</ol>

```
imageView = new UIImageView (UIImage.FromFile ("halloween.jpg"));
scrollView.ContentSize = imageView.Image.Size;
scrollView.AddSubview (imageView);
```

Only a portion of the image will appear on the iPhone screen, as we set the `ContentSize` of the Scroll View to the full size of the image, which is large than the size of the display. The user can
pan around the image by dragging. By default, the image cannot be zoomed (see
the [Zoom a Scroll View](/Recipes/ios/content_controls/scroll_view/zoom_a_scrollview) recipe).

This screenshot shows how the remainder of the image is outside the viewable
area on the device.

 [ ![](Images/fullimage.png)](Images/fullimage.png)

