using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Foundation;
using UIKit;

namespace CreateDatabaseWithAdoNet
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        CreateDatabaseWithAdoNetViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            
            DeleteDatabaseIfItAlreadyExists();

            viewController = new CreateDatabaseWithAdoNetViewController();

            window = new UIWindow(UIScreen.MainScreen.Bounds);
            window.BackgroundColor = UIColor.White;
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();
            
            return true;
        }

        private void DeleteDatabaseIfItAlreadyExists()
        {
            var dbName = "db_adonet.db";
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(documents, dbName);

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
        }
    }
}

