---
id:{BDC026CA-1810-E853-419B-7B23AF94EE83}  
title:Load an Image  
brief:This recipe shows how to load and display an image.  
samplecode:[Browse on GitHub](https://github.com/xamarin/recipes/tree/master/ios/standard_controls/image_view/load_an_image)  
sdk:[UIImageView Class Reference](https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIImageView_Class/Reference/Reference.html)  
---

<a name="Recipe" class="injected"></a>


# Recipe

To display an image:

1.  Add the image to your Xamarin.iOS project and ensure the Build Action is set to BundleResource.
2.  Create a `UIImageView` and add the image using `UIImage.FromBundle`:


```
var imageView = new UIImageView (UIImage.FromBundle("MonkeySFO.png"));
imageView.Frame = new CoreGraphics.CGRect (10,10,imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
```