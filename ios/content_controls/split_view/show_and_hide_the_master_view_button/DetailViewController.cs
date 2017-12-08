using System;
using CoreGraphics;
using UIKit;
using CoreFoundation;
namespace SplitView
{
	public class DetailViewController : UIViewController
	{
		UILabel label;
		UIToolbar toolbar;

        public DetailViewController() : base()
        {
            View.BackgroundColor = UIColor.White;
            // add a toolbar to host the master view popover (when it is required, in portrait)
            toolbar = new UIToolbar();
            toolbar.TranslatesAutoresizingMaskIntoConstraints = false;
            var tbConstraints = new[]
            {
                  toolbar.LeadingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.LeadingAnchor),
                  toolbar.TrailingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TrailingAnchor),
                  toolbar.TopAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.TopAnchor, 0.0f),
                  toolbar.HeightAnchor.ConstraintEqualTo(toolbar.IntrinsicContentSize.Height)
            };
            View.AddSubview(toolbar);
            NSLayoutConstraint.ActivateConstraints(tbConstraints);
            label = new UILabel();
            label.Text = "This is the detail view";
            label.TranslatesAutoresizingMaskIntoConstraints = false;
            var lblConstraints = new[]
            {
                label.LeadingAnchor.ConstraintEqualTo(this.View.SafeAreaLayoutGuide.LeadingAnchor, 20.0f),
                label.WidthAnchor.ConstraintEqualTo(label.IntrinsicContentSize.Width),
                label.TopAnchor.ConstraintEqualTo(this.toolbar.BottomAnchor, 20.0f),
                label.HeightAnchor.ConstraintEqualTo(label.IntrinsicContentSize.Height)
            };
            View.AddSubview(label);
            NSLayoutConstraint.ActivateConstraints(lblConstraints);
        }
		
		/// <summary>
		/// Shows the button that allows access to the master view popover
		/// </summary>
		public void AddContentsButton (UIBarButtonItem button)
		{
			button.Title = "Contents";
			toolbar.SetItems (new UIBarButtonItem[] { button }, false );
		}
		/// <summary>
		/// Hides the button that allows access to the master view popover
		/// </summary>
		public void RemoveContentsButton ()
		{
			toolbar.SetItems (new UIBarButtonItem[0], false);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}

