---
id: 8F0A8F14-11AF-4B75-A697-FA21AB9B522D
title: "Select A Date in a Fragment"
brief: "This recipe will show how to use a DatePickerDialog inside of a Fragment."
article:
  - title: "Create a Fragment" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/android/other_ux/fragment/create_a_fragment
  - title: "Fragments" 
    url: https://developer.xamarin.com/guides/android/platform_features/fragments
  - title: "Fragments Walkthrough" 
    url: https://developer.xamarin.com/guides/android/platform_features/fragments/fragments_walkthrough
sdk:
  - title: "DatePickerDialog" 
    url: http://developer.android.com/reference/android/app/DialogFragment.html
  - title: "Dialog Fragment" 
    url: http://developer.android.com/reference/android/app/DialogFragment.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to use a DatePickerDialog inside a Fragment:

1.  Create a Fragment in your Xamarin.Android project, and have the Fragment implement `Android.App.DatePickerDialog.IOnDateSetListener`.
2.  Implement the method required by the interface:


```
public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
{
    var date = new DateTime(year, monthOfYear + 1, dayOfMonth);
    View.FindViewById<TextView>(Resource.Id.display_date_text).Text = "You picked " + date.ToString("yyyy-MMM-dd");
}
```

<ol start="3">
  <li>Create a new class that inherits from <code>DialogFragment</code>:</li>
</ol>

```
public class DatePickerDialogFragment : DialogFragment
{
    private readonly Context _context;
    private  DateTime _date;
    private readonly Android.App.DatePickerDialog.IOnDateSetListener _listener;

    public DatePickerDialogFragment(Context context, DateTime date, Android.App.DatePickerDialog.IOnDateSetListener listener  )
    {
        _context = context;
        _date = date;
        _listener = listener;
    }

    public override Dialog OnCreateDialog(Bundle savedState)
    {
        var dialog = new Android.App.DatePickerDialog(_context, _listener, _date.Year, _date.Month - 1, _date.Day);
        return dialog;
    }
}
```

<ol start="4">
  <li>In the Fragment that was created in step one, modify it so that it will use the <code>DialogFragment</code> subclass:</li>
</ol>

```
public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
{
    var view = inflater.Inflate(Resource.Layout.fragment_layout, container, false);
    view.FindViewById<Button>(Resource.Id.pick_date_button).Click += (sender, args) =>
                                                                         {
                                                                             var dialog = new DatePickerDialogFragment(Activity, DateTime.Now, this);
                                                                             dialog.Show(FragmentManager, null);
                                                                         };
    return view;
}
```

 <a name="Additional_Information" class="injected"></a>

# Additional Information

The `DialogFragment` in Android displays a dialog onto of the Activity that is
hosting the fragment. This example uses the `DialogFragment` class around the
standard `DatePickerDialog`. By having the Fragment implement
`Android.App.DatePickerDialog.IOnDateSetListener` it can provide a callback to the
`DatePickerDialog` to respond to when a date is selected.

