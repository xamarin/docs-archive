
using System;
using System.Threading;
using UIKit;
using CoreGraphics;

namespace AnimateUIView
{
    public partial class AnimationViewController : UIViewController
	{
		UIImageView imageView;
		UIImage image;

		public AnimationViewController () : base ("AnimationViewController", null)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            imageView = new UIImageView(new CGRect(0, 100, 50, 50));
            image = UIImage.FromFile("Sample.png");
            imageView.Image = image;
            imageView.Alpha = 0.25f;
            View.AddSubview(imageView);

            Action setCenterRight = () =>
            {
                var xpos = UIScreen.MainScreen.Bounds.Right - imageView.Frame.Width / 2;
                var ypos = imageView.Center.Y;
                imageView.Center = new CGPoint(xpos, ypos);
            };

            Action setCenterLeft = () =>
            {
               var xpos = UIScreen.MainScreen.Bounds.Left + imageView.Frame.Width / 2;
               var ypos = imageView.Center.Y;
               imageView.Center = new CGPoint(xpos, ypos);
            };

            Action setOpacity = () =>
            {
                imageView.Alpha = 1;
            };

            UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(4, UIViewAnimationCurve.EaseInOut, setCenterRight);
            propertyAnimator.AddAnimations(setOpacity);

            Action<object> reversePosition = (o) =>
			{
                InvokeOnMainThread(() => {
                    propertyAnimator.AddAnimations(setCenterLeft);
                });
			};

			TimerCallback abortPositionDelegate = new TimerCallback(reversePosition);
            Timer abortPosition = new Timer(abortPositionDelegate, null, 3000, Timeout.Infinite);

            propertyAnimator.StartAnimation();
		}

	}
}

