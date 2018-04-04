---
id: C3E20584-4062-0062-8377-CB87BAB3E6E5
title: "Specify Slider Appearance"
brief: "This recipe shows how to specify the appearance of a slider, either with a custom image or colors."
article:
  - title: "Specify Slider Value" 
    url: https://github.com/xamarin/recipes/tree/master/Recipes/ios/standard_controls/sliders/specify_slider_value
sdk:
  - title: "UISlider Class Reference" 
    url: https://developer.apple.com/library/ios/#documentation/UIKit/Reference/UISlider_Class/Reference/Reference.html
---

<a name="Recipe" class="injected"></a>


# Recipe

A slider control can be customized by changing the image used for the
drag-able portion, or by changing the colors of the control, like this:

 ![](Images/SliderAppearance.png)

See the [Specify Slider Value](/Recipes/ios/standard_controls/sliders/specify_slider_value) recipe for instructions on creating slider controls. To change the
appearance of a slider:

1. To set the slider’s “thumb” to an image:

```
sliderImage.SetThumbImage(UIImage.FromFile("29_icon.png"), UIControlState.Normal);
```

<ol start="2"><li>Or to set the colors of each different element of a slider:</li></ol>

```
sliderColor.ThumbTintColor = UIColor.Red;
sliderColor.MinimumTrackTintColor = UIColor.Orange;
sliderColor.MaximumTrackTintColor = UIColor.Yellow;
```

