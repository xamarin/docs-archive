using System;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class SplitViewContoller : UISplitViewController
	{
		UIViewController masterView;
		DetailViewController detailView;

		public SplitViewContoller () : base()
		{
			// create our master and detail views
			masterView = new MasterViewController ();
			detailView = new DetailViewController ();

			ViewControllers = new UIViewController[] 
				{ masterView, detailView }; // order is important: master first, detail second

			Delegate = new SplitViewDelegate();
		}

		public class SplitViewDelegate : UISplitViewControllerDelegate
		{
			public override bool ShouldHideViewController (UISplitViewController svc, UIViewController viewController, UIInterfaceOrientation inOrientation)
			{
				//return true; // always hide
				//return true; // never hide
				return inOrientation == UIInterfaceOrientation.Portrait
					|| inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
			}
			
			public override void WillHideViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem barButtonItem, UIPopoverController pc)
			{
				DetailViewController detailView = svc.ViewControllers[1] as DetailViewController;
				
				detailView.AddContentsButton (barButtonItem);
			}
			
			public override void WillShowViewController (UISplitViewController svc, UIViewController aViewController, UIBarButtonItem button)
			{
				DetailViewController detailView = svc.ViewControllers[1] as DetailViewController;
				
				detailView.RemoveContentsButton ();
			}
		}
	}
}