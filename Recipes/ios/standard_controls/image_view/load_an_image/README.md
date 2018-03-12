---
id: BDC026CA-1810-E853-419B-7B23AF94EE83
title: Load an Image
brief: This recipe shows how to load and display an image.
sdk:
  - title: UIImageView Class Reference
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIImageView_Class/Reference/Reference.html
---

# Recipe

To display an image from code:

1. Edit `Assets.xcassets` file that is automatically added to the project, right-click and select **New Image Set**.

2. Double-click the new image set **Name** and change it to `MonkeySFO`.

3. Click on *1x* under the **Universal** section and select an image to display from the hard drive. 

4.  Create a `UIImageView` and add the image using `UIImage.FromBundle`:

		var imageView = new UIImageView (UIImage.FromBundle("MonkeySFO"));
		imageView.Frame = new CoreGraphics.CGRect (10,10,imageView.Image.CGImage.Width, imageView.Image.CGImage.Height);
		View.AddSubview(imageView);

Optionally, to display an image in an Image View added to a Storyboard:

1. Edit `Assets.xcassets` file that is automatically added to the project, right-click and select **New Image Set**.

2. Double-click the new image set **Name** and change it to `MonkeySFO`.

3. Click on *1x* under the **Universal** section and select an image to display from the hard drive.

4. In the iOS Designer, drag an **Image View** onto a **View** and position and size it as desired.

5. In the **Properties Explorer**, under **Identity** set the **Name** to `imageView`.

6. Also in the **Properties Explorer**, under **Image View** use the **Image** dropdown to select `MonkeySFO`.

7. In code, the image can optionally be set or changed using the following:

		imageView.Image = UIImage.FromBundle("MonkeySFO");