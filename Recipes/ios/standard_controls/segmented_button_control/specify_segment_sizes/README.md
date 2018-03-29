---
id: 735DF15C-218B-EC0F-3663-DCFF372CE93A
title: "Specify Segment Sizes"
brief: "This recipe shows how to create a UISegmentedControl with different sized segments."
article:
  - title: "Configure Segments" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/standard_controls/segmented_button_control/configure_segments_(uisegmentedcontrol)
sdk:
  - title: "UISegmentedControl Class Reference" 
    url: http://developer.apple.com/library/ios/#documentation/uikit/reference/UISegmentedControl_Class/Reference/UISegmentedControl.html
---

<a name="Recipe" class="injected"></a>


# Recipe

To create a `UISegementedControl` where the segments are different widths:

-  Create and position the control:


```
var segmentControl = new UISegmentedControl();
segmentControl.Frame = new CGRect(20,20,200,40);
```

-  Add the segments and select the default:


```
segmentControl.InsertSegment("Map", 0, false);
segmentControl.InsertSegment("Road", 1, false);
segmentControl.InsertSegment("Satellite", 2, false);
segmentControl.SelectedSegment = 1;
```

-  Set the sizes for each segment


```
segmentControl.SetWidth (40f, 0);
segmentControl.SetWidth (80f, 1);
segmentControl.SetWidth (120f, 2);
```

 <a name="Additional_Information" class="injected"></a>


# Additional Information

If the sum of the segment widths is different to the width of the control,
the segment widths will override the `Frame.Width`.

