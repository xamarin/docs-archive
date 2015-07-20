using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreFoundation;
using ImageIO;

// Image Properties reference
// http://developer.apple.com/library/ios/#documentation/GraphicsImaging/Reference/CGImageProperties_Reference/Reference/reference.html#//apple_ref/doc/uid/TP40005103

namespace AccessImageMetadata
{
	public class ImageViewController : UIViewController
	{
		// UI controls
		UIImageView imageView;
		UITextView output;
		UILabel overlabel;

		public ImageViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;
			Title = "Image Metadata";

			var imageFilename = "img.jpg";

			// original sample
			// http://developer.apple.com/library/ios/ipad/#qa/qa1654/_index.html
			var url = new NSUrl(imageFilename, false);
			ImageIO.CGImageSource myImageSource;
			myImageSource = ImageIO.CGImageSource.FromUrl (url, null);
			var ns = new NSDictionary();
			var imageProperties = myImageSource.CopyProperties(ns, 0);
			// Output ALL teh things
			//Console.WriteLine(imageProperties.DescriptionInStringsFileFormat);

			// Basic Properties
			var width = imageProperties[CGImageProperties.PixelWidth];
			var height = imageProperties[CGImageProperties.PixelHeight];
			var orientation = imageProperties[CGImageProperties.Orientation];
			var dimensions = String.Format ("Dimensions: {0}x{1} (orientation {2})", width, height, orientation);
			Console.WriteLine(dimensions); 
	
			// TIFF Properties
			var tiff = imageProperties.ObjectForKey(CGImageProperties.TIFFDictionary) as NSDictionary;
			var make = tiff[CGImageProperties.TIFFMake];
			var model = tiff[CGImageProperties.TIFFModel];
			var dt = tiff[CGImageProperties.TIFFDateTime];
			var tprops = String.Format ("TIFF: {0} {1} {2}", make, model, dt);
			Console.WriteLine(tprops); 

			// GPS Properties
			var gps = imageProperties.ObjectForKey(CGImageProperties.GPSDictionary) as NSDictionary;
			var lat = gps[CGImageProperties.GPSLatitude];
			var latref = gps[CGImageProperties.GPSLatitudeRef];
			var lon = gps[CGImageProperties.GPSLongitude];
			var lonref = gps[CGImageProperties.GPSLongitudeRef];
			var loc = String.Format ("GPS: {0} {1}, {2} {3}", lat, latref, lon, lonref);
			Console.WriteLine(loc); 
			// http://stackoverflow.com/questions/4043685/problem-in-writing-metadata-to-image

			// EXIF Properties
			var exif = imageProperties.ObjectForKey(CGImageProperties.ExifDictionary) as NSDictionary;
			var fn = exif[CGImageProperties.ExifFNumber];
			var focal = exif[CGImageProperties.ExifFocalLength];
			var eprops = String.Format ("EXIF: Fstop {0} FocalLength {1}", fn, focal);
			Console.WriteLine(eprops); 


			// Now create UI controls to display this stuff
			imageView = new UIImageView(new CoreGraphics.CGRect(0,0,320,240));
			output = new UITextView(new CoreGraphics.CGRect(0,235,320,View.Bounds.Height - 320));
			overlabel = new UILabel(new CoreGraphics.CGRect(0, 0, 220, 60));

			imageView.Image = UIImage.FromFile(imageFilename);
			output.Text = imageProperties.DescriptionInStringsFileFormat;
			output.Editable = false;
			output.ScrollEnabled = true;
			overlabel.TextColor = UIColor.White;
			overlabel.BackgroundColor = UIColor.Clear;
			overlabel.Font = UIFont.SystemFontOfSize(10f);
			overlabel.Lines = 4;
			overlabel.Text = dimensions + "\n" + loc + "\n" + tprops + "\n" + eprops;

			View.AddSubview (imageView);
			View.AddSubview (output);
			View.AddSubview (overlabel);
		}
	}
}

