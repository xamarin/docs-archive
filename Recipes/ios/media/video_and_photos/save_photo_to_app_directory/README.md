---
id: F8EEE9FF-2CC1-A280-4493-74D8AAC9D1BF
title: "Save Photo to App Directory"
brief: "This recipe shows how to save a photo to an application’s Documents directory."
samplecode:
  - title: "Save_Photo_to_app_directory" 
    url: https://github.com/xamarin/recipes/tree/master/ios/media/video_and_photos/save_photo_to_app_directory
article:
  - title: "Save Photo to Album with Metadata" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/media/video_and_photos/save_photo_to_album_with_metadata
sdk:
  - title: "UIImagePickerControllerDelegate Protocol Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UIImagePickerControllerDelegate_Protocol/UIImagePickerControllerDelegate/UIImagePickerControllerDelegate.html
---

<a name="Recipe" class="injected"></a>


# Recipe

The sample code uses the [ <span class="s2">Camera helper from TweetStation</span>](https://github.com/migueldeicaza/TweetStation/blob/master/TweetStation/UI/Camera.cs) to
take a picture, and then demonstrates how to save it in the completion handler.
The photo is saved to the applications Documents directory, like this:

```
TweetStation.Camera.TakePicture (this, (obj) =>{
   var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
   var documentsDirectory = Environment.GetFolderPath
                         (Environment.SpecialFolder.Personal);
   string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg"); // hardcoded filename, overwritten each time
   NSData imgData = photo.AsJPEG();
   NSError err = null;
   if (imgData.Save(jpgFilename, false, out err)) {
       Console.WriteLine("saved as " + jpgFilename);
   } else {
       Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
   }
});
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

Saving the image data in this way does NOT include the metadata supplied by
the camera (such as GPS location, camera model, exposure, etc).

