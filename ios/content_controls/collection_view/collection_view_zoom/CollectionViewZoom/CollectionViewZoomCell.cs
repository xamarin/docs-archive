using System;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CollectionViewZoom {

	public class CollectionViewZoomCell : UICollectionViewCell {

		public static readonly NSString Key = new NSString ("CollectionViewZoomCell");

		[Export ("initWithFrame:")]
		public CollectionViewZoomCell (RectangleF frame) : base (frame)
		{
			BackgroundColor = UIColor.Cyan;
			ImageView = new UIImageView (Bounds);
			ImageView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			ImageView.Layer.BorderWidth = 3.0f;
			ImageView.ClipsToBounds = true;
			ImageView.Layer.BorderColor = UIColor.White.CGColor;
			ImageView.Layer.EdgeAntialiasingMask = CAEdgeAntialiasingMask.LeftEdge | CAEdgeAntialiasingMask.RightEdge | CAEdgeAntialiasingMask.BottomEdge | CAEdgeAntialiasingMask.TopEdge;
			ContentView.AddSubview (ImageView);
		}

		public UIImageView ImageView { get; private set; }
	}
}