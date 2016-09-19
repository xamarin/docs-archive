using System;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class SplitViewContoller : UISplitViewController
	{
		UIViewController masterView, detailView;

		public SplitViewContoller () : base()
		{
			// create our master and detail views
			masterView = new MasterViewController ();
			detailView = new DetailViewController ();

			// create an array of controllers from them and then assign it to the 
			// controllers property
			ViewControllers = new UIViewController[] 
				{ masterView, detailView }; // order is important: master first, detail second
			
			#region Additional Information
			// for iOS5 only, we can force the master view to ALWAYS be visible, even in portrait
//			ShouldHideViewController = (svc, viewController, inOrientation) => {
//				return false; // default behaviour is true
//			};
			#endregion
		}
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

