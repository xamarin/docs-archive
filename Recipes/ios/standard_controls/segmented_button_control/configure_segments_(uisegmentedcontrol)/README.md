---
id: 13B08892-8DDC-CDDE-63A1-9994E1569249
title: "Configure Segments (UISegmentedControl)"
brief: "This recipe shows how to create a UISegmentedControl."
sdk:
  - title: "UISegmentedControl Class Reference" 
    url: https://developer.apple.com/library/ios/documentation/UIKit/Reference/UISegmentedControl_Class/
---

<a name="Recipe" class="injected"></a>


# Recipe

> ℹ️ **Note**: On iOS 7 and above, the UISegmentedControl styling has been deprecated.

To create a `UISegementedControl`:

1.  Create and position the control:


```
var segmentControl = new UISegmentedControl();
segmentControl.Frame = new CGRect(20,20,280,40);
```

<ol start="2">
  <li>Add the segments and select the default:</li>
</ol>


```
segmentControl.InsertSegment("One", 0, false);
segmentControl.InsertSegment("Two", 1, false);
segmentControl.SelectedSegment = 1;
```

<ol srart="3">
  <li>Optionally handle the <code>ValueChanged</code> event:</li>
</ol>


```
segmentControl.ValueChanged += (sender, e) => {
    var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;
    // do something with selectedSegmentId
};
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

You can create a `UISegmentedControl` in Xcode by dragging one to the design
surface and setting the Segment count and properties in the Attributes
Inspector.

