using System;
using CoreGraphics;
using AssetsLibrary;
using UIKit;
using Foundation;

namespace ImageView
{

	public class ImageViewController : UIViewController
	{
		
		UIImagePickerController imagePicker;
		UIButton choosePhotoButton;
		UIImageView imageView;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Choose Photo";
			View.BackgroundColor = UIColor.White;

			imageView = new UIImageView(new CGRect(10, 150, 300, 300));
			Add(imageView);

			choosePhotoButton = UIButton.FromType(UIButtonType.RoundedRect);
			choosePhotoButton.Frame = new CGRect(10, 80, 100, 40);
			choosePhotoButton.SetTitle("Picker", UIControlState.Normal);
			choosePhotoButton.TouchUpInside += (s, e) => {
				// create a new picker controller
				imagePicker = new UIImagePickerController();
				
				// set our source to the photo library
				imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
								
				// set what media types
				imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
				
				imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
				imagePicker.Canceled += Handle_Canceled;
				
				// show the picker
				NavigationController.PresentModalViewController(imagePicker, true);
				//UIPopoverController picc = new UIPopoverController(imagePicker);

			};
			View.Add(choosePhotoButton);
		}
		
		// Do something when the
		void Handle_Canceled(object sender, EventArgs e)
		{
			Console.WriteLine("picker cancelled");
			imagePicker.DismissModalViewController(true);
		}

		// This is a sample method that handles the FinishedPickingMediaEvent
		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;
					
				case "public.video":
					Console.WriteLine("Video selected");
					break;
			}
			
			Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");
			
			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if(referenceURL != null)
				Console.WriteLine(referenceURL.ToString());
			
			// if it was an image, get the other image info
			if(isImage)
			{
				
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null)
				{
					// do something with the image
					Console.WriteLine("got the original image");
					imageView.Image = originalImage;
				}
				
				// get the edited image
				UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
				if(editedImage != null)
				{
					// do something with the image
					Console.WriteLine("got the edited image");
					imageView.Image = editedImage;
				}
				
				//- get the image metadata
				NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
				if(imageMetadata != null)
				{
					// do something with the metadata
					Console.WriteLine("got image metadata");
				}
				
			}
			// if it's a video
			else
			{
				// get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if(mediaURL != null)
				{
					//
					Console.WriteLine(mediaURL.ToString());
				}
			}
			
			// dismiss the picker
			imagePicker.DismissModalViewController(true);
		}
	}
}