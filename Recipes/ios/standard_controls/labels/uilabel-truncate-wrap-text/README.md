---
id: 8DDFDE78-64B1-4334-B4F5-5AAF9CE40D3F
title: "How to Truncate and Wrap Text in a UILabel"
brief: "It is possible to change how text appears when it is too long for a given UILabel. This recipe illustrates the possible ways to truncate text using UILineBreakMode."
sdk:
  - title: "UILabel" 
    url: https://developer.apple.com/library/ios/documentation/UIKit/Reference/UILabel_Class/
---


<a name="Recipe" class="injected"></a>


# Recipe


![UILabel Example](Images/UILabelScreenshot.png)

* `CharacterWrap` sets the text in the `UILabel` to wrap at the first character that doesn't fit. We can set a `UILabel` to this mode with the following line of code:

```
CharWrapLabel.LineBreakMode = UILineBreakMode.CharacterWrap;
```
* We can put this, and all other UILabel manipulations inside the `ViewDidLoad` method of the `ViewController`, or whatever method you are using to manipulate the UI element.


* `Clip` mode has the text which does not fit to remain unrendered. We can set a  `UILabel` to this mode with the following line of code:

````
ClipLabel.LineBreakMode = UILineBreakMode.Clip;
````


* `HeadTruncation` mode has the `UILabel` show the end of the text and truncates the head to an ellipse. We can set a  `UILabel` to this mode with the following line of code:

```
HeadLabel.LineBreakMode = UILineBreakMode.HeadTruncation;
```


* `MiddleTruncation` mode has the `UILabel` show the start and end of the text and truncates the middle to an ellipse. We can set a  `UILabel` to this mode with the following line of code:

````
MiddleLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
````


* `TailTruncation` mode has the `UILabel` show the start of the text and truncates the end to an ellipse. We can set a  `UILabel` to this mode with the following line of code:

````
TailLabel.LineBreakMode = UILineBreakMode.TailTruncation;
````


* `WordWrap` mode has the `UILabel` wrap the first word that does not fit. We can set a  `UILabel` to this mode with the following line of code:

````
TailLabel.LineBreakMode = UILineBreakMode.WordWrap;
````

