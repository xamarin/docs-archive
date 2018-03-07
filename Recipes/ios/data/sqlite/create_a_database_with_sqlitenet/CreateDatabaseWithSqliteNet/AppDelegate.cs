using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Foundation;
using UIKit;

namespace CreateDatabaseWithSqliteNet
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        CreateDatabaseWithSqliteNetViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            DeleteDatabaseIfItAlreadyExists(); 

            window = new UIWindow(UIScreen.MainScreen.Bounds);
            
            viewController = new CreateDatabaseWithSqliteNetViewController();
            window.BackgroundColor = UIColor.White;
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();
            
            return true;
        }

        private void DeleteDatabaseIfItAlreadyExists()
        {
            var dbName = "db_sqlite-net.db";
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(documents, dbName);

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
        }
    }
}

