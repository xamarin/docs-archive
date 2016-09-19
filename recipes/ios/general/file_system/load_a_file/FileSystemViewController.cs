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
		UIButton btnFiles;
		UITextView txtView;

		public override void ViewDidLoad ()
		{	
			base.ViewDidLoad ();
			
			// Create the buttons and TextView to run the sample code
			btnFiles = UIButton.FromType(UIButtonType.RoundedRect);
			btnFiles.Frame = new CGRect(10,10,145,50);
			btnFiles.SetTitle("Open 'ReadMe.txt'", UIControlState.Normal);

			txtView = new UITextView(new CGRect(10, 90, 300, 350));
			txtView.Editable = false;
			txtView.ScrollEnabled = true;
			
			// Wire up the buttons to the SamplCode class methods
			btnFiles.TouchUpInside += (sender, e) => {
				txtView.Text = "";

				// Sample code from the article
				var text = System.IO.File.ReadAllText("TestData/ReadMe.txt");
				Console.WriteLine(text);
				
				// Output to app UITextView
				txtView.Text = text;
			};

			// Add the controls to the view
			Add(btnFiles);
			Add (txtView);
		}
	}
}