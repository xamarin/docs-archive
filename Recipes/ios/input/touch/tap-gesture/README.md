---
id: 754EDCD7-9555-4FF2-B031-9BA7A5B6E43F
title: "Use a Tap Gesture"
brief: "In iOS, tap gestures are registered with the UITapGestureRecognizer class. This recipe demonstrates how to use this class to recognize single and multiple tap gestures."
sdk:
  - title: "UITapGestureRecognizer" 
    url: https://developer.apple.com/library/ios/documentation/UIKit/Reference/UITapGestureRecognizer_Class/
---


<a name="Recipe" class="injected"></a>

# Recipe

![Tap Gesture](Images/tap.png) ![Tap Gesture](Images/tap2.png)

<ol>

	<li><p>First, let's enable user interaction on the UI element that's going to receive the taps. We can do that by setting the <code>UserInteractionEnabled</code> property to true:</p>
		<pre><code>imageView.UserInteractionEnabled = true;</code></pre>
		<p>We can do this inside the <code>ViewDidLoad</code> method of the <code>ViewController</code>, or whatever method you are using to manipulate the UI element.</p>
	</li>
	<li><p>Next we'll create a new <code>UITapGestureRecognizer</code>, passing in the name of the Action that will handle the tap gesture:</p>
		<pre><code>UITapGestureRecognizer tapGesture = new UITapGestureRecognizer (TapThat);</code></pre>
	</li>
	<li><p>Next we'll create the Action to handle the tap gesture. In our example, we're using a method called <code>TapThat</code> to rotate the View 90 degrees clockwise and show an alert view:</p>
		<pre><code>tap.View.Transform *= CGAffineTransform.MakeRotation ((float)Math.PI / 2);
				tapped = true;
				alert = UIAlertController.Create ("Card Tapped", "This card has been tapped", UIAlertControllerStyle.Alert);
				alert.AddAction (UIAlertAction.Create ("OK", UIAlertActionStyle.Default, null));
				PresentViewController (alert, true, null);</code></pre>
	</li>
	<li><p>Finally, we can add the gesture recognizer to our UI element:</p>
		<pre><code>imageView.AddGestureRecognizer (tapGesture);</code></pre>
	</li>
</ol>

# Additional Information


We can control the number of taps our gesture recognizer responds to by editing the <code>NumberOfTapsRequired</code> property of the tap gesture recognizer. For example, we could change the code above to respond to two taps with the following code:

<pre><code>tapGesture.NumberOfTapsRequired = 2;</code></pre>

The default number of taps required is 1.

