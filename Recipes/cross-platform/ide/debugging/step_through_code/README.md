---
id: 19FDB601-FEF7-48A3-2011-DE8414D1DC71
title: "Step Through Code"
brief: "This recipe shows how to step into, over, and out of functions."
article:
  - title: "Debugging" 
    url: https://developer.xamarin.com/guides/ios/deployment,_testing,_and_metrics/debugging_in_xamarin_ios
---

<a name="Recipe" class="injected"></a>


# Recipe

1. Put your IDE into Debug mode, and set a breakpoint at the point in the
code where you want to start stepping from:

 [ ![](Images/ios_step_02.png)](Images/ios_step_02.png)

2. On the top left, you will see four gray icons with arrows and dots:

 [ ![](Images/ios_step_01.png)](Images/ios_step_01.png)

In Visual Studio, the controls look the same except the arrows are blue:

 [ ![](Images/ios_step_00_vs.png)](Images/ios_step_00_vs.png)

 [ ![](Images/ios_step_01_vs.png)](Images/ios_step_01_vs.png)

You have four options here:

-  **Play** will begin running the code.
-  **Step over** and  **Step into** execute the next line of code. If the next line of code is a function call,  **Step over** will execute the entire function and stop at the next line of code outside it.  **Step into** will stop at the first line of code inside the function.
-  Use  **Step out** if you are inside a function, and want to move on to the next function.


3. The following screenshot illustrates stopping at the first breakpoint, and stepping into the next function:

 [ ![](Images/ios_step_03.png)](Images/ios_step_03.png)

 [ ![](Images/ios_step_03_vs.png)](Images/ios_step_03_vs.png)

