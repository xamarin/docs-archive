id:{2690BBD5-0ACB-DDA3-7808-CBB0094165F8}  
title:Animate an ImageView  
brief:This recipe shows how to animate a UIImageView with an array of image frames  
samplecode:[Browse on GitHub](https://github.com/xamarin/recipes/tree/master/ios/standard_controls/image_view/animate_an_imageview)  
article:[Load an Image](/recipes/ios/standard_controls/image_view/load_an_image)  
sdk:[UIImageView Class Reference](http://developer.apple.com/library/ios/#documentation/uikit/reference/UIImage_Class/Reference/Reference.html)  

<a name="Recipe" class="injected"></a>


# Recipe

To display an image:

-  Add all the image frames to your Xamarin.iOS project and ensure the Build Action is set to BundleResource.
-  Create a `UIImageView`:


```
var imageView = new UIImageView ();
animatedCircleImage.Frame = new CoreCraphics.CGRect(110, 20, 100, 100);
```

-  Create an array of the image frames and assign it to `AnimationImages`:


```
animatedCircleImage.AnimationImages = new UIImage[] {
      UIImage.FromBundle ("Spinning Circle_1.png")
    , UIImage.FromBundle ("Spinning Circle_2.png")
    , UIImage.FromBundle ("Spinning Circle_3.png")
    , UIImage.FromBundle ("Spinning Circle_4.png")
};
```

-  Set the animation properties and start animating:


```
animatedCircleImage.AnimationRepeatCount = 0;
animatedCircleImage.AnimationDuration = .5;
animatedCircleImage.StartAnimating();
```

The sample code shows the circle animating (difficult to demonstrate in a
screenshot):

 [ ![](Images/ImageViewAnim1.png)](Images/ImageViewAnim1.png)
