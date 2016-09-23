---
id: {058BB883-0550-FAD7-15EC-D4EE88B734E6}  
title: ActionSheet Date Picker  
brief: This recipe shows how to pop up an Action Sheet that contains a Date Picker.  
samplecode: [Browse on GitHub](https: //github.com/xamarin/recipes/tree/master/ios/standard_controls/actionsheet/actionsheet_date_picker)  
article: [Display an ActionSheet](/recipes/ios/standard_controls/actionsheet/display_an_actionsheet)  
sdk: [UIActionSheet Class Reference](https: //developer.apple.com/library/ios/#documentation/UIKit/Reference/UIActionSheet_Class/Reference/Reference.html)  
sdk: [UIDatePicker Class Reference](http: //developer.apple.com/library/ios/#documentation/uikit/reference/UIDatePicker_Class/Reference/UIDatePicker.html)  
---

<a name="Recipe" class="injected"></a>


<div class="note">WARNING:  <code>UIActionSheet</code> is <a href="https: //developer.apple.com/library/ios/documentation/UIKit/Reference/UIActionSheet_Class/">deprecated in iOS 8</a>, and has been replaced with UIAlertController. You can find a recipe on how to implement an Action Sheet in iOS <a href="/recipes/ios/standard_controls/alertcontroller/">here</a>.</br></br> Furthermore, Apple's documentation states "UIActionSheet is not designed to be subclassed, nor should you add views to its hierarchy." - this sample calls <code>actionSheet.AddSubview()</code> which does not work at all in iOS 8.<br/>
If you have used the code in this recipe in the past, you should update your code with a different approach. DO NOT USE THIS RECIPE FOR iOS 8 APPS.</div>


# Recipe (iOS 7 and earlier ONLY)

An Action Sheet is a convenient way to modally request input from the user.
This recipe shows you how to use an Action Sheet to allow the user to input a
date.

 ![](Images/ActionSheetDatePicker.png)

To display an Action Sheet that selects a date: 

-  The sample code includes a custom class ActionSheetDatePicker that will be used in this recipe. It contains a custom UIActionSheet implementation that contains a UIDatePicker, UILabel and UIButton.
-  Create the ActionSheetDatePicker and set the Title: 


```
actionSheetDatePicker = new ActionSheetDatePicker (this.View);
actionSheetDatePicker.Title = "Choose Date: ";
```

-  Set the type and validation properties of the UIDatePicker property: 


```
actionSheetDatePicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
actionSheetDatePicker.DatePicker.MinimumDate = DateTime.Today.AddDays (-7);
actionSheetDatePicker.DatePicker.MaximumDate = DateTime.Today.AddDays (7);
```

-  Handle the UIDatePicker value changing (in this case, we apply the selected date directly to the UILabel): 


```
actionSheetDatePicker.DatePicker.ValueChanged += (s, e) => {
    dateLabel.Text = (s as UIDatePicker).Date.ToString ();
};
```

-  In the Choose a date button TouchUpInside handler call Show to display the Action Sheet: 


```
actionSheetDatePicker.Show ();
```

 <a name="Additional_Information" class="injected"></a>


### Additional Information

The sample includes a custom ActionSheetDatePicker class that works by
creating a UIActionSheet and adding other subviews directly to it. *Apple discourages this practice and the sample no longer works on iOS 8.*

**SEE WARNING at the top of the page. This recipe is no longer supported and is left here purely for reference (for now).**