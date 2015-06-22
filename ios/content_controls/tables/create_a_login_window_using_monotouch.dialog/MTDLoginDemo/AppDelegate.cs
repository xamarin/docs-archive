using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MonoTouch.Dialog;
using System.Diagnostics;

namespace MTDLoginDemo
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow window;
        EntryElement login, password;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            window = new UIWindow (UIScreen.MainScreen.Bounds);
                   
            window.RootViewController = new DialogViewController (new RootElement ("Login") {
                new Section ("Credentials"){
                    (login = new EntryElement ("Login", "Enter your login", "")),
                    (password = new EntryElement ("Password", "", "", true))
                },
                new Section () {
                    new StringElement ("Login", delegate{ 
                        Console.WriteLine ("User {0} log-in", login.Value); })   
                }
            });

            window.MakeKeyAndVisible ();
            
            return true;
        }
    }
}


