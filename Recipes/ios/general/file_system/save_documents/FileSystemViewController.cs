using System;
using CoreGraphics;
using System.Linq;
using UIKit;
using Foundation;

using System.IO;

namespace FileSystem {
	public class FileSystemViewController : UIViewController {
		UIButton btnFiles, btnWrite;
		UITextView txtView;

		public override void ViewDidLoad ()
		{	
			base.ViewDidLoad ();
			
			// Create the buttons and TextView to run the sample code
			btnFiles = UIButton.FromType(UIButtonType.RoundedRect);
			btnFiles.Frame = new CGRect(165,10,145,50);
			btnFiles.SetTitle("Open 'Write.txt'", UIControlState.Normal);

			btnWrite = UIButton.FromType(UIButtonType.RoundedRect);
			btnWrite.Frame = new CGRect(10,10,145,50);
			btnWrite.SetTitle("Create 'Write.txt'", UIControlState.Normal);

			txtView = new UITextView(new CGRect(10, 190, 300, 250));
			txtView.Editable = false;
			txtView.ScrollEnabled = true;
			
			btnFiles.TouchUpInside += (sender, e) => {
				txtView.Text = "";
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine (documents, "Write.txt");
				if (File.Exists (filename)) {
					var text = File.ReadAllText(filename);
				
					// Output to app UITextView
					txtView.Text = "Text was read from a file." + Environment.NewLine
									+ "-----------------" + Environment.NewLine
									+ text;
				} else {
					txtView.Text = String.Format("Ooops, file {0} does not exist yet", filename);
				}
			};
			
			btnWrite.TouchUpInside += (sender, e) => {
				var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				var filename = Path.Combine (documents, "Write.txt");
				File.WriteAllText(filename, "Write this text into a file!");
				
				// Output to app UITextView
				txtView.Text = "Text was written to a file." + Environment.NewLine
							+ "-----------------" + Environment.NewLine
							+ File.ReadAllText(filename);
			};

			
			// Add the controls to the view
			Add(btnFiles);
			Add(btnWrite);		
			Add(txtView);
			
			// Write out this special folder, just for curiousity's sake
			Console.WriteLine("MyDocuments:"+Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
		}
	}
}