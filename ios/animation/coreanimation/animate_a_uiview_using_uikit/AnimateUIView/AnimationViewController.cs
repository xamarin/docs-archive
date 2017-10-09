
using System;
using System.Drawing;

using Foundation;
using UIKit;
using ObjCRuntime;
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

        public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Action setCenterRight = () => {
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

			Action[] setEnds = new Action[2];

            int repeatCount = 5;

            setEnds[(repeatCount + 1) % 2] = setCenterLeft;
            setEnds[repeatCount % 2] = setCenterRight;

			imageView = new UIImageView(new CGRect(0, 100, 50, 50));
			image = UIImage.FromFile("Sample.png");
			imageView.Image = image;
            setCenterLeft();
			View.AddSubview(imageView);

            UIViewPropertyAnimator propertyAnimator = new UIViewPropertyAnimator(2, UIViewAnimationCurve.EaseInOut, setEnds[repeatCount % 2]);

            Action<UIViewAnimatingPosition> animationCompletion = null;
            animationCompletion = (UIViewAnimatingPosition obj) =>
            {
                repeatCount--;
                if (repeatCount > 0)
                {
                    propertyAnimator = new UIViewPropertyAnimator(2, UIViewAnimationCurve.EaseInOut, setEnds[repeatCount % 2]);
                    propertyAnimator.AddCompletion(animationCompletion);
                    propertyAnimator.StartAnimation();

                }
            };

            propertyAnimator.AddCompletion(animationCompletion);
            propertyAnimator.StartAnimation();
		}

	}
}

