using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace Unwind
{
	partial class YellowViewController : UIViewController
	{
		public YellowViewController (IntPtr handle) : base (handle)
		{
		}

		[Action ("UnwindToYellowViewController:")]
		public void UnwindToYellowViewController (UIStoryboardSegue segue)
		{
			Console.WriteLine ("We've unwinded to Yellow!");
		}
	}
}
