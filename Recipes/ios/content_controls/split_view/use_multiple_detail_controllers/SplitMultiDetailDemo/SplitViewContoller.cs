using System;
using UIKit;
using CoreFoundation;
using CoreGraphics;

namespace SplitMultiDetailDemo
{
	public class SplitViewContoller : UISplitViewController
	{
		SplitDelegate sd;
		ColorsController colorsController;
		UIViewController redVC, greenVC;
		UIViewController detailContainer;

		public SplitViewContoller () : base()
		{
			sd = new SplitDelegate ();
			Delegate = sd;

			colorsController = new ColorsController ();

			redVC = new UIViewController ();
			redVC.View.BackgroundColor = UIColor.Red;

			colorsController.ColorSelected += (sender, e) => {

				if (e.Color == "Red") {

					detailContainer = redVC;

				} else if (e.Color == "Green") {
					if (greenVC == null) {
						greenVC = new UIViewController ();
						greenVC.View.BackgroundColor = UIColor.Green;
					}
					detailContainer = greenVC;
				}

				ViewControllers = new UIViewController[] {
                    colorsController,
                    detailContainer
                };
			};

			detailContainer = redVC;

			ViewControllers = new UIViewController[] {
                colorsController,
                detailContainer
            };
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		class SplitDelegate : UISplitViewControllerDelegate
		{
			public override bool ShouldHideViewController (UISplitViewController svc, 
                UIViewController viewController, UIInterfaceOrientation inOrientation)
			{ 
				return false; 
			}
		}

	}
}

