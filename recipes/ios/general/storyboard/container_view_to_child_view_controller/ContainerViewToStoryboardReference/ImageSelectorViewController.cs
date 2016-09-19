using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ContainerViewToStoryboardReference
{
    public class ImageSelectedEventArgs : EventArgs
    {
        public string Value { get; protected set; }

        public ImageSelectedEventArgs(String imgName)
        {
            this.Value = imgName;    
        }
    }

	partial class ImageSelectorViewController : UIViewController
	{
        public EventHandler<ImageSelectedEventArgs> ImageSelected;

        String[] imageNames = new [] { "Mountain", "Boulders", "Wilderness" };


		public ImageSelectorViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //Set up scrolling
            this.scrollView.ContentSize = new CoreGraphics.CGSize(1800, 450);

            //Fire ImageSelected when paging occurs

            scrollView.Scrolled += (object sender, EventArgs e) => {
                var page = (int) (scrollView.ContentOffset.X / 600);

                /* C# 6 
                ImageSelected?.Invoke(this,  new ImageSelectedEventArgs(imageNames[page]));
                */
                EventHandler<ImageSelectedEventArgs> handler = ImageSelected;
                if (handler != null)
                {
                    handler(this, new ImageSelectedEventArgs(imageNames[page]));
                }   
            };
        }
	}
}
