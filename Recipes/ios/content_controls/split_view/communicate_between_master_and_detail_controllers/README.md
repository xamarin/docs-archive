---
id: 47A7FC7B-30FB-227E-0A67-4CD7134FA34B
title: "Communicate Between Master and Detail Controllers"
brief: "This recipe shows how to wire up the master view in a UISplitViewController so that a new selection will change the contents of the detail view."
article:
  - title: "Using a Split View to Show Two Controllers" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/split_view/use_split_view_to_show_two_controllers
  - title: "Showing and Hiding the Master View Button" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/content_controls/split_view/show_and_hide_the_master_view_button
sdk:
  - title: "UISplitViewController Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UISplitViewController_class/Reference/Reference.html
  - title: "Split View Controllers" 
    url: https://developer.apple.com/library/ios/#documentation/WindowsViews/Conceptual/ViewControllerCatalog/Chapters/SplitViewControllers.html
---

<a name="Recipe" class="injected"></a>


# Recipe

When a selection is made in the master view the detail view should be updated
accordingly (and if the master view is in a popover, the popover should be
automatically dismissed). This recipe shows how to communicate between the
master and detail views to make that happen.

To get row selection in the master view to change the detail view:

<ol start="1">
  <li>Start with an existing implementation of <code>UISplitViewController</code> (such as the <a href="http://developer.xamarin.com/Recipes/ios/content_controls/split_view/use_split_view_to_show_two_controllers/">Using a Split View to Show Two Controllers</a> recipe).</li>
</ol>
<ol start="2">
  <li>Add an event handler and an <code>EventArgs</code> subclass to the <code>MasterViewController</code> which will be used to communicate with the detail view:</li>
</ol>


```
public event EventHandler<RowClickedEventArgs> RowClicked;

public class RowClickedEventArgs : EventArgs
{
       public int Item { get; set; }

       public RowClickedEventArgs(int item) : base()
       { this.Item = item; }
}
```

<ol start="3">
  <li>Add a delegate to each row in the <code>MasterViewController</code> constructor, when clicked it will call the <code>RowClicked</code> event handler passing a new <code>RowClickedEventArgs</code> containing information about the row that was clicked.</li>
</ol>


```
Root = new RootElement ("Items") {
    new Section () {
        from num in Enumerable.Range (1, 10)
        select (Element) new MonoTouch.Dialog.StringElement("Item " + num,
        delegate {
            if (RowClicked != null)
                RowClicked (this, new RowClickedEventArgs(num));
        })
    }
};}
```

<ol start="4">
  <li>Add a public property <code>Popover</code> to the <code>DetailViewController</code> so that we can track when it is showing and hide it when a selection is made:</li>
</ol>


```
public UIPopoverController Popover {get;set;}
```

<ol start="5">
  <li>Create an <code>Update()</code> method in the <code>DetailViewController</code> which will be called when a new selection is made in the master view:</li>
</ol>




```
public void Update (string text) {
       label.Text = String.Format (content, text);
       // dismiss the popover if currently visible
       if (Popover != null)
              Popover.Dismiss (true);
}
```

<ol start="6">
  <li>Attach a handler to the <code>RowClicked</code> event to the <code>MasterViewController</code> in the <code>SplitViewController</code>. This lets the <code>SplitViewController</code> act as the go-between, listening for <code>RowClicked</code> events that happen in the master view, and calling the <code>Update()</code> method in the detail view:</li>
</ol>


```
masterView.RowClicked += (object sender, MasterViewController.RowClickedEventArgs e) => {
    detailView.Update (e.Item);
};
```

<ol start="7">
  <li>Finally, set the new <code>DetailViewController</code> <code>Popover</code> property whenever the <code>SplitViewController</code> shows or hides the popover. This will allow the <code>DetailViewController</code> to dismiss the popover when a selection is made: </li>
</ol>


```
WillHideViewController += (object sender, UISplitViewHideEventArgs e) => {
       detailView.Popover = e.Pc;
       detailView.AddContentsButton(e.BarButtonItem);
};

WillShowViewController += (object sender, UISplitViewShowEventArgs e) => {
       detailView.Popover = null;
       detailView.RemoveContentsButton ();
};
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

In this example we just pass an <code>int</code> value from the selected row to the detail view, however you can modify the <code>RowClickedEventArgs</code> to pass whatever
information you need.

