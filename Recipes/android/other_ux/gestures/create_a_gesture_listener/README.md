---
id: 5D7E958B-287E-5D68-9BDD-E94E4B2FAFFB
title: "Create a Gesture Listener"
brief: "This recipe will show how to recognize a fling gesture. A fling gesture is when the user presses on the screen, and while maintaining contact with the screen moves their finger in a given direction."
sdk:
  - title: "GestureDetector" 
    url: http://developer.android.com/reference/android/view/GestureDetector.html
  - title: "GestureDetector.OnGestureListener" 
    url: http://developer.android.com/reference/android/view/GestureDetector.OnGestureListener.html
---

<a name="Recipe" class="injected"></a>


# Recipe

1. Create a new Xamarin.Android application named `RecognizeGesture`.

2. Edit `Main.axml` so that its contents match the
following:

```
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
   android:orientation="vertical"
   android:layout_width="fill_parent"
   android:layout_height="wrap_content">
   <TextView
       android:text="Small Text"
       android:textAppearance="?android:attr/textAppearanceMedium"
       android:layout_width="fill_parent"
       android:layout_height="wrap_content"
       android:id="@+id/velocity_text_view" />
</LinearLayout>
```

<ol start="3">
  <li>Edit <span class="s2">MainActivity.cs</span>, and add the following instance variable to the class:</li>
</ol>

```
private GestureDetector _gestureDetector;
```

<ol start="4">
  <li>Edit <code>MainActivity.cs</code> so that it implements <code>Android.Views.GestureDetector.IOnGestureListener</code> and the methods required by that interface. More functionality will be added to the <code>OnFling</code> method further on.</li>
</ol>

```
public class Activity1 : Activity, GestureDetector.IOnGestureListener
{
   public bool OnDown(MotionEvent e)
   {
   }
   public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
   {
   }
   public void OnLongPress(MotionEvent e) {}
   public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
   {
   }
   public void OnShowPress(MotionEvent e) {}
   public bool OnSingleTapUp(MotionEvent e)
   {
       return false;
   }
}
```

<ol start="5">
  <li>Edit the <code>OnFling</code> method so that it will display some values captured when the user "flings" on the screen:</li>
</ol>

```
public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
{
   _textView.Text = String.Format("Fling velocity: {0} x {1}", velocityX, velocityY);
  return true;
}
```

<ol start="6">
  <li>Create an instance of <code>GestureDetector</code> for the <code>Activity</code>. Modify <code>OnCreate</code> as shown in the following snippet:</li>
</ol>

```
protected override void OnCreate(Bundle bundle)
{
   base.OnCreate(bundle);
   SetContentView(Resource.Layout.Main);
   _textView = FindViewById<TextView>(Resource.Id.velocity_text_view);
   _textView.Text = "Fling Velocity: ";
   _gestureDetector = new GestureDetector(this);
}
```

<ol start="7">
  <li>Override the method <code>OnTouchEvent</code> in the <code>Activity</code>. This will delegate the handling of the gesture to the <code>GestureDetector.IOnGestureListener</code> methods implemented by the class.</li>
</ol>

```
public override bool OnTouchEvent(MotionEvent e)
{
   _gestureDetector.OnTouchEvent(e);
   return false;
}
```

<ol start="8">
  <li>Run the application on a device, and fling your finger across the screen. You will see the X and Y velocities change:</li>
</ol>

 <a name="Additional_Information" class="injected"></a>


# Additional Information

Android provides the `GestureDetector` class to
simplify the detection and handling of gestures on a View.
`GestureDetectore.IOnGestureListener` is not the only interface for
gesture detection, there is also `GestureDetector.IOnDoubleTapListener` and `GestureDetector.SimpleOnGestureListener`.

