id:{60CB8D6D-3E26-BD8D-88F0-3B63FE1BC8F7}  
title:Create an iOS Project  
brief:This recipe shows how to create a new iOS project.  
article:[Hello, iPhone](/guides/ios/getting_started/hello,_world)  

<a name="Recipe" class="injected"></a>


# Recipe
<ide name="xs">
<h2>Create an iOS Project in Xamarin Studio</h2>
<ol>
  <li>First, launch Xamarin Studio and click <span class="UIItem">+ New Solution...</span> in the upper left corner: <img src="Images/ios_project_00.png" /></li>
  <li>A window will pop up asking you to choose a template for the project. Select <span class="UIItem">App</span> under <span class="UIItem">iOS</span> in the panel on the left. Then select the desired app type from the list of options and click <span class="UIItem">Next</span>. If you are new to Xamarin.iOS and don't know which to choose, select <span class="UIItem">Single View Application</span>. <img src="Images/choose_template.png" /></li>
  <li>The window will move to the <span class="UIItem">Configure your iOS app</span> screen. Enter the name of your app, select the devices you'd like to support and choose a minimum target OS. <img src="Images/choose_device.png" /></li>
  <li>The window will move to the <span class="UIItem">Configure your new project</span> screen. Choose the name of the project (avoiding spaces and special characters) and the location on disk to store the project, then click <span class="UIItem">Create</span><img src="Images/choose_name.png" /></li>
  <li>Xamarin Studio will create your new iOS app. <img src="Images/xam_result.png" /></li>
</ol>
</ide>
<ide name="vs">
<h2>Create an iOS Project in Visual Studio</h2>
<ol>
  <li><a href="/guides/ios/getting_started/introduction_to_xamarin_ios_for_visual_studio">Make sure Xamarin.iOS for Visual Studio is configured correctly</a>. Launch Visual Studio and choose <span class="UIItem">File > New > Project</span>: <im src="Images/ios_project_00_vs.png" /></li>
  <li>A window will pop up with a list of options for the types of projects available to you. To create an iOS project, choose the <span class="UIItem">Templates</span> and then the <span class="UIItem">Visual C#</span> category to open up the available sub-categories, then select <span class="UIItem">iOS</span>:<img src="Images/ios_project_01_vs.png" /></li>
  <li>Choose your device from the menu on the left. If you are new to Xamarin.iOS and unsure about what template to use, select <span class="UIItem">Single View Application</span> from the panel on the right. This will create an application with one view and one View Controller.</li>

  <li>Choose a sensible name for your project, avoiding spaces and special characters, and click <span class="UIItem">OK</span>.
  <li>Visual Studio will create and populate a new project for you:<img src="Images/ios_project_02_vs.png"</li>
</ol>
 </ide>
