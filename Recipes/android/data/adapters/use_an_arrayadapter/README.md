---
id: F8D057EB-A890-4831-952C-3538862F04C5
title: "Use an ArrayAdapter"
brief: "This recipe shows how to bind an array to a list using an ArrayAdapter."
sdk:
  - title: "ArrayAdapter Class Reference" 
    url: http://developer.android.com/reference/android/widget/ArrayAdapter.html
---

<a name="Recipe" class="injected"></a>


# Recipe

Follow these steps to display an array of strings in a ListView, which have been provided by `ListActivity`.

-  Add a file named TextViewItem.xml under the Resources/layout folder containing the following XML.


```
<?xml version="1.0" encoding="UTF-8"?>
<TextView xmlns:android="http://schemas.android.com/apk/res/android"
    android:id="@+id/textItem"
    android:textSize="44sp"
    android:layout_width="fill_parent"
    android:layout_height="wrap_content" />
```

-  In a `ListActivity` subclass, add the following code.


```
public class Activity1 : ListActivity
{
string[] data = {"one", "two", "three", "four", "five"} ;

protected override void OnCreate (Bundle bundle)
{
base.OnCreate (bundle);
ArrayAdapter adapter = new ArrayAdapter (this,
Resource.Layout.TextViewItem, data);
ListAdapter = adapter;
}

protected override void OnListItemClick (ListView l, View v,
int position, long id)
{
base.OnListItemClick (l, v, position, id);
Toast.MakeText (this, data [position],
ToastLength.Short).Show ();
}

}
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

An `ArrayAdapter` binds each object in an array to a `TextView`. The `ListActivity`
class used here implicitly contains a `ListView` that can be bound to a data
source through the `ListAdapter` property, causing each `TextView` instance to be
presented in a list. Overriding `OnListItemClick` allows us to handle item
selection.

