using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace Unwind
{
	partial class PinkViewController : UIViewController
	{
		public PinkViewController (IntPtr handle) : base (handle)
		{
		}

		[Action ("UnwindToPinkViewController:")]
		public void UnwindToPinkViewController (UIStoryboardSegue segue)
		{
			Console.WriteLine ("We've unwinded to Pink!");
		}
	}
}
