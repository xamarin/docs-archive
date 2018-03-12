---
id: 4538054F-6C77-40CC-9DBB-7285053E4029
title: "Zoom a Collection View"
brief: "This recipe shows how to use a gesture recognizer to allow the user to zoom in a Collection View."
sdk:
  - title: "Collection View Programming Guide" 
    url: https://developer.apple.com/library/ios/documentation/WindowsViews/Conceptual/CollectionViewPGforIOS/Introduction/Introduction.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To allow a user to zoom in a Collection View, you first need to add a gesture
	recognizer to your View:

```
public override void ViewDidLoad ()
	{
		base.ViewDidLoad ();

		UIPinchGestureRecognizer pinch = new UIPinchGestureRecognizer (handlePinchGesture);
		this.CollectionView.AddGestureRecognizer (pinch);
	}
```

Then add a handler method to respond to the gesture:

```
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
```

Finally, in the Collection View's delegate, you will need to override
	the GetSizeForItem() method to apply the zoom's scale factor to the size of each item:

```
public override System.Drawing.CGSize GetSizeForItem (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
	{
		return new System.Drawing.CGSize (50 * parent.scale, 50 * parent.scale);
	}
```

