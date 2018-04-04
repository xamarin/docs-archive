using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;

namespace ImageView {

	public class ImageViewController : UIViewController {
		
		UIButton cameraButton;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Save to Album";
			View.BackgroundColor = UIColor.White;

			cameraButton = UIButton.FromType (UIButtonType.RoundedRect);
			cameraButton.Frame = new CGRect(10, 20, 100,40);
			cameraButton.SetTitle ("Camera", UIControlState.Normal);
			cameraButton.TouchUpInside += (sender, e) => {
			
				TweetStation.Camera.TakePicture (this, (obj) =>{
					// https://developer.apple.com/library/ios/#documentation/uikit/reference/UIImagePickerControllerDelegate_Protocol/UIImagePickerControllerDelegate/UIImagePickerControllerDelegate.html#//apple_ref/occ/intfm/UIImagePickerControllerDelegate/imagePickerController:didFinishPickingMediaWithInfo:
					var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
					var meta = obj.ValueForKey(new NSString("UIImagePickerControllerMediaMetadata")) as NSDictionary;
					
// This bit of code saves to the Photo Album with metadata
					ALAssetsLibrary library = new ALAssetsLibrary();
					library.WriteImageToSavedPhotosAlbum (photo.CGImage, meta, (assetUrl, error) =>{
						Console.WriteLine ("assetUrl:"+assetUrl);
					});
					

// This bit of code does basic 'save to photo album', doesn't save metadata
//			var someImage = UIImage.FromFile("someImage.jpg");
//			someImage.SaveToPhotosAlbum ((image, error)=> {
//				var o = image as UIImage;
//				Console.WriteLine ("error:" + error);
//			});
					

// This bit of code saves to the application's Documents directory, doesn't save metadata
//					var documentsDirectory = Environment.GetFolderPath
//					                          (Environment.SpecialFolder.Personal);
//					string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg");
//					NSData imgData = photo.AsJPEG();
//					NSError err = null;
//					if (imgData.Save(jpgFilename, false, out err))
//					{
//					    Console.WriteLine("saved as " + jpgFilename);
//					} else {
//					    Console.WriteLine("NOT saved as" + jpgFilename + " because" + err.LocalizedDescription);
//					}


				});
			};
			View.Add (cameraButton);
			
			if (!UIImagePickerController.IsSourceTypeAvailable (UIImagePickerControllerSourceType.Camera)) {
				cameraButton.SetTitle ("No camera", UIControlState.Disabled);
				cameraButton.SetTitleColor (UIColor.Gray, UIControlState.Disabled);
				cameraButton.Enabled = false;
			}
		}
	}
}