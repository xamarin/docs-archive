using System;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class SplitViewContoller : UISplitViewController
	{
		MasterViewController masterView;
		DetailViewController detailView;
		
		public SplitViewContoller () : base()
		{
			// create our master and detail views
			masterView = new MasterViewController ();
			detailView = new DetailViewController ();

			// create an array of controllers from them and then assign it to the 
			// controllers property
			ViewControllers = new UIViewController[] 
				{ masterView, detailView }; // order is important: master first, detail second
			

			// in this example, i expose an event on the master view called RowClicked, and i listen 
			// for it in here, and then call a method on the detail view to update. this class thereby 
			// becomes the defacto controller for the screen (both views).
			masterView.RowClicked += (object sender, MasterViewController.RowClickedEventArgs e) => {
				detailView.Update (e.Item);
			};


			// when the master view controller is hidden (portrait mode), we add a button to 
			// the detail view that will show the master view in a popover
			WillHideViewController += (object sender, UISplitViewHideEventArgs e) => {
				detailView.Popover = e.Pc;
				detailView.AddContentsButton(e.BarButtonItem);
			};
			
			// when the master view controller is shown (landscape mode), remove the button
			WillShowViewController += (object sender, UISplitViewShowEventArgs e) => {
				detailView.Popover = null;
				detailView.RemoveContentsButton ();
			};

			// hide the master view controller 
			ShouldHideViewController = (svc, viewController, inOrientation) => {
				return inOrientation == UIInterfaceOrientation.Portrait ||
					inOrientation == UIInterfaceOrientation.PortraitUpsideDown;
			};
		}
	}
}