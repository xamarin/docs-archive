using System;
using CoreGraphics;
using UIKit;

using System.Net;
using System.IO;
using System.Text;

namespace Buttons {

	public class DownloadFileViewController : UIViewController {
		
		UIButton buttonRect;
		UITextView textView;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Download File";
			View.BackgroundColor = UIColor.White;

			buttonRect = UIButton.FromType(UIButtonType.RoundedRect);
			buttonRect.SetTitle ("Download File", UIControlState.Normal);
			buttonRect.Frame = new CGRect(20, 80, 200, 40);
			buttonRect.TouchUpInside += HandleTouchUpInside;
			
			textView = new UITextView (new CGRect(0, 140, 320, 340));
			textView.Editable = false;

			View.AddSubview (buttonRect);
			View.AddSubview (textView);
		}
		protected void HandleTouchUpInside (object sender, System.EventArgs ea)
		{
			var webClient = new WebClient();
			webClient.DownloadStringCompleted += (s, e) => {
				var text = e.Result;
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);	
				string localFilename = "downloaded.txt";
				string localPath = Path.Combine (documentsPath, localFilename);
				
				Console.WriteLine("localPath:"+localPath);

				File.WriteAllText (localPath, text);
				
				// IMPORTANT: this is a background thread, so any interaction with
				// UI controls must be done via the MainThread
				InvokeOnMainThread (() => {
					
					textView.Text = text;

					new UIAlertView ("Done"
						, "Text file downloaded and saved."
						, null
						, "OK"
						, null).Show();
				});
			};
			
			var url = new Uri ("http://xamarin.com"); // Html home page
			webClient.Encoding = Encoding.UTF8;
			webClient.DownloadStringAsync (url);
		}
	}
}