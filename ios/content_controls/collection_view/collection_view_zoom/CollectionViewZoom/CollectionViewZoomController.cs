using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace CollectionViewZoom
{
	public class CollectionViewZoomController : UICollectionViewController
	{
		public const int MAX_COUNT = 35;
		float scaleStart;
		float scale;

		public CollectionViewZoomController (UICollectionViewLayout layout) : base (layout)
		{
			CollectionView.Delegate = new CollectionZiewZoomDelegate (this);
			CollectionView.RegisterClassForCell (typeof (CollectionViewZoomCell), CollectionViewZoomCell.Key);

			scale = 1.0f;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIPinchGestureRecognizer pinch = new UIPinchGestureRecognizer (handlePinchGesture);
			this.CollectionView.AddGestureRecognizer (pinch);
		}

		public override int NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			return MAX_COUNT;
		}
			
		public void handlePinchGesture (UIPinchGestureRecognizer gesture)
		{
			if (gesture.State == UIGestureRecognizerState.Began)
			{
				scaleStart = this.scale;
			}
			else if (gesture.State == UIGestureRecognizerState.Changed)
			{
				this.scale = scaleStart * gesture.Scale;

				this.CollectionView.CollectionViewLayout.InvalidateLayout ();
			}
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell (CollectionViewZoomCell.Key, indexPath) as CollectionViewZoomCell;
			cell.ImageView.Image = UIImage.FromFile ("Images/sa" + indexPath.Item + ".jpg");
			return cell;
		}

		class CollectionZiewZoomDelegate: UICollectionViewDelegateFlowLayout
		{
			CollectionViewZoomController parent;
			public CollectionZiewZoomDelegate(CollectionViewZoomController parent) : base() {
				this.parent = parent;
			}

			public override System.Drawing.SizeF GetSizeForItem (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
			{
				return new System.Drawing.SizeF (50 * parent.scale, 50 * parent.scale);
			}
		}
	}
}

