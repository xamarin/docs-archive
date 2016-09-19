using System;
using CoreGraphics;
using System.Linq;
using UIKit;
using Foundation;

namespace FileSystem
{
	/// <summary>
	/// View containing Buttons and TextView to show off the samples
	/// </summary>
	public class FileSystemViewController : UIViewController
	{
		UIButton btnDirectories;
		UITextView txtView;

		public override void ViewDidLoad ()
		{	
			base.ViewDidLoad ();
			
			// Create the buttons and TextView to run the sample code
		
			btnDirectories = UIButton.FromType(UIButtonType.RoundedRect);
			btnDirectories.Frame = new CGRect(10,10,145,50);
			btnDirectories.SetTitle("List Directories", UIControlState.Normal);
			
		

			txtView = new UITextView(new CGRect(10, 90, 300, 350));
			txtView.Editable = false;
			txtView.ScrollEnabled = true;
			
			

			btnDirectories.TouchUpInside += (sender, e) => {
				txtView.Text = "";

				// Enumerate directories in the app.bundle
				var directories = System.IO.Directory.EnumerateDirectories("./");
				foreach (var directory in directories) {
					Console.WriteLine(directory);
				}
				// Output to app UITextView
				foreach (var directory in directories) {
					txtView.Text += directory + Environment.NewLine;
				}

				txtView.Text += "\n------------------------\n";

				// Enumerate directories in the application's home directory
				var path = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
				directories = System.IO.Directory.EnumerateDirectories(path+"/..");
				foreach (var directory in directories) {
					Console.WriteLine(directory);
				}
				// Output to app UITextView
				foreach (var directory in directories) {
					txtView.Text += directory + Environment.NewLine;
				}
			};
			
			
			// Add the controls to the view
			Add(btnDirectories);			
			Add(txtView);
		}
	}
}