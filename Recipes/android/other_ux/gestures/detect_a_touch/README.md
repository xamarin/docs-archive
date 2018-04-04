---
id: E921D8BC-5FAA-D4EB-2755-5F14CCDDACEA
title: "Detect a Touch"
brief: "This recipe will show how to detect and response to a touch event. The user can touch a Button on the screen and then slide it left or right."
sdk:
  - title: "View.OnTouchListener" 
    url: http://developer.android.com/reference/android/view/View.OnTouchListener.html
  - title: "MotionEvent" 
    url: http://developer.android.com/reference/android/view/MotionEvent.html
---

<a name="Recipe" class="injected"></a>


# Recipe

1. Create a new Xamarin.Android application named DetectATouch.

2. Modify Main.axml to resemble the following:

```
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
   android:orientation="vertical"
   android:layout_width="fill_parent"
   android:layout_height="fill_parent"
   android:minWidth="25px"
   android:minHeight="25px">
   <Button
       android:text="My View"
       android:padding="6dip"
       android:textAppearance="?android:attr/textAppearanceLarge"
       android:layout_width="wrap_content"
       android:layout_height="wrap_content"
       android:id="@+id/myView"
        />
</LinearLayout>
```

<ol start="3">
  <li>Add two instance variables to Activity1:</li>
</ol>

• **_myButton** – This will hold a reference to the button on
the activity.

• **_viewX** – This will hold the X coordinate of the button
inside it’s parent container.

```
[Activity(Label = "DetectATouch", MainLauncher = true, Icon = "@drawable/icon")]
public class Activity1 : Activity, View.IOnTouchListener
{
   private Button _myButton;
   private float _viewX;
}
```

<ol start="4">
  <li>Modify <code>OnCreate</code> to obtain a reference to the button. Then call <code>SetOnTouchListener</code> to provide a handler for touch event:</li>
</ol>

```
protected override void OnCreate(Bundle bundle)
{
   base.OnCreate(bundle);
   SetContentView(Resource.Layout.Main);
   _myButton = FindViewById<Button>(Resource.Id.myView);
   _myButton.SetOnTouchListener(this);
}
```

<ol start="5">
  <li>Change <code>MainActivity.cs</code> so that it implements <code>View.IOnTouchListener</code>, and add the <code>OnTouch</code> method that is required by the interface. Add the following code to reposition the Button in response to the touch moving across the screen:</li>
</ol>

```
public class Activity1 : Activity, View.IOnTouchListener
{
   public bool OnTouch(View v, MotionEvent e)
   {
       switch (e.Action)
       {
           case MotionEventActions.Down:
               _viewX = e.GetX();
               break;
           case MotionEventActions.Move:
               var left = (int) (e.RawX - _viewX);
               var right = (int) (left + v.Width);
               v.Layout(left, v.Top, right, v.Bottom);
               break;
       }
       return true;
   }
}
```

<ol start="6">
  <li>Run the application. Touch the button, and move your finger to the right. The Button should move to the right accordingly:</li>
</ol>

 [ ![](Images/moved_screen.png)](Images/moved_screen.png)

 <a name="Additional_Information" class="injected"></a>


# Additional Information

The `MotionEvent` contains information to describe
movements of an object. It has an action code which describes what state change
occurred, and a set of values to describe the position of the view and other
movement.

Prior to Android 3.0 (API level 11) only one view could receive the touch
event.

