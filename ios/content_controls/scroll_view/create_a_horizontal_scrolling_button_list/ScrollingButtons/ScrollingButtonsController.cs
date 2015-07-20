using System.Collections.Generic;
using CoreGraphics;
using UIKit;
using System;

namespace ScrollingButtons
{
	public class ScrollingButtonsController : UIViewController
	{
		UIScrollView scrollView;
		List<UIButton> buttons;
        
		public ScrollingButtonsController ()
		{
			buttons = new List<UIButton> ();
		}
        
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            
			nfloat h = 50.0f;
			nfloat w = 50.0f;
			nfloat padding = 10.0f;
			nint n = 25;
            
			scrollView = new UIScrollView {
                Frame = new CGRect (0, 100, View.Frame.Width, h + 2 * padding),
                ContentSize = new CGSize ((w + padding) * n, h),
                BackgroundColor = UIColor.White,
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth
            };
         
			for (int i=0; i<n; i++) {
				var button = UIButton.FromType (UIButtonType.RoundedRect);
				button.SetTitle (i.ToString (), UIControlState.Normal);
				button.Frame = new CGRect (padding * (i + 1) + (i * w), padding, w, h);
				scrollView.AddSubview (button);
				buttons.Add (button);
			}
            
			View.AddSubview (scrollView);
		}
        
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}