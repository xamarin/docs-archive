
using System;
using CoreGraphics;

using Foundation;
using UIKit;

namespace InitialScreenDemo
{
	public partial class ViewController1 : UIViewController
	{
		public event EventHandler InitialActionCompleted;

		public ViewController1 () : base ("ViewController1", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
        
			aButton.TouchUpInside += (sender, e) => {

				if (InitialActionCompleted != null) {
					aButton.Hidden = true;
					InitialActionCompleted.Invoke (this, new EventArgs ());
				}
			};
		}
	}
}

