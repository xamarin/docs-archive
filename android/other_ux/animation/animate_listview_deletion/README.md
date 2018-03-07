---
id: 5FDEEA86-E1CA-47F3-B121-5847757165B3
title: "Animate a ListView Deletion"
brief: "This recipe will show how to animate the deletion of a ListView row in Jellybean."
api:
  - title: "ListView" 
    url: http://developer.android.com/reference/android/widget/ListView.html
  - title: "ValueAnimator" 
    url: http://developer.android.com/reference/android/animation/ValueAnimator.html
  - title: "ViewPropertyAnimator" 
    url: http://developer.android.com/reference/android/view/ViewPropertyAnimator.html
---

<a name="Overview" class="injected"></a>


# Recipe

A visually pleasant effect when deleting items from a ListView is to animate the row being deleted by gradually changing the `.Alpha` value of the view from 1.0 to 0.0, causing the row to fade out of existence before being completely removed. If you've tried to animate the deletion of a row from a ListView in a Xamarin.Android application, you may observe some curious behavior when rapidly scrolling through a ListView with many rows: the animation may appear on rows other than then one that is being deleted.

This happens because the ListView will recycle views for each row - the content changes but not the view itself. The side-effect of this is that the animation is transferred along with the recycled view. What is necessary is to convince the ListView not to recycle the view in while the animation is still in progress. There are two ways to handle this in Xamarin.Android when you're targeting Android 4.2 (API level 16) or higher:

-  Use a ViewPropertyAnimator
-  Use a ValueAnimator


## Using a ViewPropertyAnimator

The `ViewPropertyAnimator` is a fluent interface that can be used to animate several properties all at once. The framework will not recycle the view until after all of the animations are complete.

```
private void HandleItemClick(object sender, AdapterView.ItemClickEventArgs e)
{
    e.View.Animate()
        .SetDuration(1000)
        .Alpha(0)
        .WithEndAction(new Runnable(() =>{
                _adapter.Remove(e.Id);
                e.View.Alpha = 1f;
        }));
}
```

This code is deceptively simple - animate the value of `e.View.Alpha` from 1.0 to 0.0 over the duration of one section. The `WithEndAction` is a helper method that was introduced in API 16. that specifies some action to be performed when the animation is complete. The ListView will not recycle the view while the ViewPropertyAnimator is active.

## Using A ValueAnimator

The other technique is to use a `ValueAnimator` and provide handlers for the `.Update` and `.AnimationEnded` events. To prevent the framework from recycling the view while the animation is in progress, the `HasTransientState` property on the view must be set to `true`. The following code snippet is an example of how to do:

```
private void HandleItemClick(object sender, AdapterView.ItemClickEventArgs e)
{
    View view = e.View;
    view.HasTransientState = true;

    ValueAnimator animator = ValueAnimator.OfFloat(new[] { 1f, 0f });
    animator.SetDuration(1000);
    animator.Update += (o, animatorUpdateEventArgs) =>{
        view.Alpha = (float)animatorUpdateEventArgs.Animation.AnimatedValue;
    };

    animator.AnimationEnd += delegate{
        _adapter.Remove(e.Id);
        view.Alpha = 1f;
    };
    animator.Start();
}
```

This code is a fairly straight forward `ValueAnimator`. The property `HasTransientState` was introduced in API 16, and tells the framework that the view is tracking some transient state (i.e. the animation) and needs to be preserved.

