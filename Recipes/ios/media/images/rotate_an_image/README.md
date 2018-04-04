---
id: 0E44C0E9-241F-506F-9358-36BEA3DB4665
title: "Rotate An Image View"
brief: "This recipe shows how to rotate an image on the screen using a UIImageView and a CGAffineTransform."
article:
  - title: "Working with Images" 
    url: https://developer.xamarin.com/guides/ios/application_fundamentals/working_with_images
sdk:
  - title: "Quartz 2D Programming Guide" 
    url: https://developer.apple.com/library/mac/#documentation/graphicsimaging/conceptual/drawingwithquartz2d/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

 [ ![](Images/RotateImage.png)](Images/RotateImage.png)

1.  Add a sample image to your project. 
2.  In a `UIViewController` subclass, add class variables for a `UIImage` and `UIImageView`.

  ```
  UIImage image;
  UIImageView imageView;
  ```

3. In the `ViewDidLoad` method create the `UIImage` and the `UIImageView`.

  ```
  image = UIImage.FromFile("monkey.png");
  imageView = new UIImageView(new CGRect(50,50,100,100));
  imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
  imageView.Image = image;
  ```

4. Set the `Transform` property of the `UIImageView` and add it as a sub view.

  ```
  imageView.Transform = CGAffineTransform.MakeRotation((float)Math.PI/4);
  View.AddSubview (imageView);
  ```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

`CGAffineTransform` has helper functions to return various affine
transforms such as rotation, scale and translation. Setting a CGAffineTransform
to the `Transform` property of a UIView applies the transformation matrix to the
view.

