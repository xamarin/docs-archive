---
id: B620BDFA-8377-4FD0-AC9E-7BB0D4289863
title: "SeekBar"
brief: "This recipe demonstrates how to use the SeekBar control. The SeekBar control is visually similar to the ProgressBar but it has a draggable slider that will allow the user to change the value displayed by the control. This will provide an example showing how to respond to changes in the SeekBar using .NET events or the SeekBar.IOnSeekBarrChangeListenerinterface."
article:
  - title: "Seek Bars and Sliders" 
    url: http://developer.android.com/design/building-blocks/seek-bars.html
sdk:
  - title: "SeekBar" 
    url: http://developer.android.com/reference/android/widget/SeekBar.html
  - title: "OnSeekBarChangeListener" 
    url: http://developer.android.com/reference/android/widget/SeekBar.OnSeekBarChangeListener.html
---

<a name="Recipe" class="injected"></a>


# Recipe

The `SeekBar` widget is an interactive slider that allows the user to select one value from a range of values. As the user moves the slider left or right, the value of the `SeekBar` will change. The following image shows an example of the `SeekBar`:

 ![](images/seekbar.png)

A `SeekBar` is added to the layout of an Activity or a Fragment. The following snippet is an example of the `SeekBar` XML:

```
<SeekBar android:layout_width="fill_parent" android:layout_height="wrap_content" android:id="@+id/seekBar1" android:layout_marginTop="@dimen/seekbar_margin" android:layout_marginBottom="@dimen/seekbar_margin"/>
```

Xamarin.Android provides some events that allow applications to respond to changes in the value of the `SeekBar`, the most significant of these is the `ProgressChanged` event.

This code snippet shows an example of attaching a lambda to the `SeekBar.ProgressChanged` event:

```
_seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);
_textView = FindViewById<TextView>(Resource.Id.textView1);

_seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
    if (e.FromUser)
    {
        _textView.Text = string.Format("The value of the SeekBar is {0}", e.Progress);
    }
};
```

This lambda will update a `TextView` widget with the value of the `SeekBar` when the change is triggered by the user. The following screen shot is an example of what an Activity might look like after the user has moved the slider:

 ![](images/seekbar2.png)

 <a name="UsingAListener" class="injected"></a>


## Using A Listener

As an alternative to using event handlers, classes that implement the interface `SeekBar.IOnSeekBarChangeListener` may act as listeners to the `SeekBar` events. The following code snippet shows an example of an Activity that implements `SeekBar.IOnSeekBarChangeListener`:

```
public class MainActivity : Activity, SeekBar.IOnSeekBarChangeListener
{
    SeekBar _seekBar;
    TextView _textView;

    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        // Set our view from the "main" layout resource
        SetContentView(Resource.Layout.Main);

        _seekBar = FindViewById<SeekBar>(Resource.Id.seekBar1);
        _textView = FindViewById<TextView>(Resource.Id.textView1);

        // Assign this class as a listener for the SeekBar events
        _seekBar.SetOnSeekBarChangeListener(this);
    }

    public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
    {
        if (fromUser)
        {
            _textView.Text = string.Format("The user adjusted the value of the SeekBar to {0}", seekBar.Progress);
        }
    }

    public void OnStartTrackingTouch(SeekBar seekBar)
    {
        System.Diagnostics.Debug.WriteLine("Tracking changes.");
    }

    public void OnStopTrackingTouch(SeekBar seekBar)
    {
        System.Diagnostics.Debug.WriteLine("Stopped tracking changes.");
    }
}
```

 <a name="Summary" class="injected"></a>


# Summary

This recipe provided a quick overview on how to add the `SeekBar` widget to an Activity. It demonstrated how the Activity is notified of `SeekBar` events by using an event handler or by implementing `SeekBar.IOnSeekBarChangeListener`.

