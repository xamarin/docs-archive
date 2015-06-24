using System;
using UIKit;
using CoreGraphics;

namespace AppLinksiOS
{
	public partial class ProductViewController : UIViewController
	{
		public ProductViewController (string productId) : base ()
		{
			ProductId = productId;
		}

		public string ProductId { get; set; }

		public override void ViewDidLoad()
		{
			var label = new UILabel(new CGRect(10, 10, 100, 30));
			label.Text = ProductId ?? "No Product ID Found";

			View.AddSubview(label);
		}
	}
}

