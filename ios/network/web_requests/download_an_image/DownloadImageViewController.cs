using System;
using CoreGraphics;
using UIKit;

using System.Net;
using System.IO;
using System.Text;

namespace Buttons {

	public class DownloadImageViewController : UIViewController {
		
		UIButton buttonRect;
		UIImageView imageView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Download Image";
			View.BackgroundColor = UIColor.White;

			buttonRect = UIButton.FromType(UIButtonType.RoundedRect);
			buttonRect.SetTitle ("Download Image", UIControlState.Normal);
			buttonRect.Frame = new CGRect(20, 80, 200, 40);
			buttonRect.TouchUpInside += HandleTouchUpInside;
			
			imageView = new UIImageView (new CGRect(0, 130, 320, 340));
			
			View.AddSubview (buttonRect);
			View.AddSubview (imageView);
		}
		protected void HandleTouchUpInside (object sender, System.EventArgs ea)
		{
			var webClient = new WebClient();
			webClient.DownloadDataCompleted += (s, e) => {
				var bytes = e.Result;
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);	
				string localFilename = "downloaded.png";
				string localPath = Path.Combine (documentsPath, localFilename);
				
				Console.WriteLine("localPath:"+localPath);

				File.WriteAllBytes (localPath, bytes);
	
				// IMPORTANT: this is a background thread, so any interaction with
				// UI controls must be done via the MainThread
				InvokeOnMainThread (() => {
					
					imageView.Image = UIImage.FromFile (localPath);

					new UIAlertView ("Done"
						, "Image downloaded and saved."
						, null
						, "OK"
						, null).Show();
				});
			};
			
			var url = new Uri ("http://xamarin.com/resources/design/home/devices.png");
			webClient.DownloadDataAsync (url);
		}
	}
}