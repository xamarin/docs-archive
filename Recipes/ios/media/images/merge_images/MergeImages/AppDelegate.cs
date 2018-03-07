using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MergeImages
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        MergeImagesViewController viewController;
        UIImage image1;
        UIImage image2;
        UIImage combinedImage;
        UIImageView combinedImageView;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            
            viewController = new MergeImagesViewController ();         
            
            image1 = UIImage.FromFile ("monkey1.png");
            image2 = UIImage.FromFile ("monkey2.png");
                 
            UIGraphics.BeginImageContext (UIScreen.MainScreen.Bounds.Size);
            
            image1.Draw (new CGRect (
                0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));
            
            image2.Draw (new CGRect (
                UIScreen.MainScreen.Bounds.Width / 4, 
                UIScreen.MainScreen.Bounds.Height / 4, 
                UIScreen.MainScreen.Bounds.Width / 2, 
                UIScreen.MainScreen.Bounds.Height / 2));
            
            combinedImage = UIGraphics.GetImageFromCurrentImageContext ();
            
            UIGraphics.EndImageContext ();
            
            combinedImageView = new UIImageView (
                new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));
            
            combinedImageView.Image = combinedImage;
            
            viewController.View.AddSubview (combinedImageView);
            
            window.RootViewController = viewController;
            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}

