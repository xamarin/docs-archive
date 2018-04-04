---
id: 3ED7298A-1B29-EBAE-2203-F2F4919A8221
title: "Save Photo to Album with Metadata"
brief: "This recipe shows how to save a photo to the Photos Camera Roll Album, including image metadata."
samplecode:
  - title: "Save_Photo_to_Album_with_Metadata" 
    url: https://github.com/xamarin/recipes/tree/master/ios/media/video_and_photos/save_photo_to_album_with_metadata
sdk:
  - title: "UIImagePickerControllerDelegate Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/uikit/reference/UIImagePickerControllerDelegate_Protocol/UIImagePickerControllerDelegate/UIImagePickerControllerDelegate.html
---

<a name="Recipe" class="injected"></a>


# Recipe

The sample code uses the&nbsp; [Camera helper from TweetStation](https://github.com/migueldeicaza/TweetStation/blob/master/TweetStation/UI/Camera.cs)&nbsp;to take a picture, then
demonstrates how to save it (with metadata) in the completion handler:

```
TweetStation.Camera.TakePicture (this, (obj) =>{
    var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
    var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;
    ALAssetsLibrary library = new ALAssetsLibrary();
    library.WriteImageToSavedPhotosAlbum (photo.CGImage, meta, (assetUrl, error) =>{
        Console.WriteLine ("assetUrl:"+assetUrl);
    });
});;
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

There is a simpler mechanism to save an existing UIImage to the Photo Album,
but it does not include metadata:

```
var someImage = UIImage.FromFile("someImage.jpg");
someImage.SaveToPhotosAlbum((image, error) => {
    var o = image as UIImage;
    Console.WriteLine("error:" + error);
});
```

