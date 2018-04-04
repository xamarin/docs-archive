---
id: 7AAA95E0-6B6C-27AE-A682-7CB7451D70AB
title: "Create an Android Project"
brief: "This recipe shows how to create a new Android project in Xamarin and Visual Studio."
article:
  - title: "Hello, Android" 
    url: https://developer.xamarin.com/guides/android/getting_started/hello,_world
---


# Recipe

## Visual Studio for Mac

1.  First, launch Visual Studio for Mac and click on <span class="UIItem">**New...**</span> in the top left corner: ![](Images/project_00.png)
2.  A window will pop up with a list of options for the types of projects available to you. To create an Android project, select the Android>App category to open up the available sub-categories, then choose Android App: ![](Images/android_project_01.png)
3.  Choose a sensible name for your app and choose development targets: ![](Images/app_name.png)
4.  Choose a project name, then click <span class="UIItem">**OK**</span>:  
    ![](Images/project_name.png)
5.  Visual Studio for Mac will create and populate a new project for you: ![](Images/android_project_02.png)

## Visual Studio

1.  First, launch Visual Studio and click on <span class="UIItem">**New Project...**</span> in the top left corner: ![](Images/project_00_vs.png)
2.  A window will pop up with a list of options for the types of projects available to you. To create an Android project, select the Android then <span class="UIItem">**Blank App (Android)**</span>. Enter a project name: ![](Images/android_project_01_vs.png)
3.  Visual Studio will create and populate a new project for you: ![](Images/android_project_02_vs.png)

</ide>

## Additional Information

If you are using periods in your project name, do not use *Android* in your project name, as this will result in namespace conflicts and build failures. For example, do not name your project "Hello.Android" - use "Hello.Droid" instead.

