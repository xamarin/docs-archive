---
title: "Introduction to content pipelines"
description: "Content pipelines are applications, or parts of applications, that are used to convert files into a format that can be loaded by game projects. The MonoGame Content Pipeline is a specific content pipeline implementation for converting files for CocosSharp and MonoGame projects."
ms.prod: xamarin
ms.assetid: 40628B5F-FAF7-4FA7-A929-6C3FEA83F8EC
ms.date: 03/27/2017
---

# Introduction to content pipelines

_Content pipelines are applications, or parts of applications, that are used to convert files into a format that can be loaded by game projects. The MonoGame Content Pipeline is a specific content pipeline implementation for converting files for CocosSharp and MonoGame projects._

This article provides a conceptual understanding of content pipelines, primarily focusing on the *MonoGame Content Pipeline*, which is a content pipeline implementation used with CocosSharp and MonoGame.


## What is a content pipeline?

The term *content pipeline* is a general term for the process of converting a file from one format to another. The *input* of the content pipeline is typically a file outputted by an authoring tool, such as image files from Photoshop. The content pipeline creates the *output* file in a format that can be loaded directly by a game project. Typically the output files are optimized for fast loading and reduced disk size.

Content pipelines may be command-line executables, dedicated GUI-based applications, or plugins embedded in another application such as Visual Studio. Content pipelines typically run before the game executes. If the content pipeline is associated with some application like Visual Studio, then it may execute during compile-time. If the content pipeline is a standalone application, then it may run when explicitly told to do so by the user. The application or logic responsible for converting a specific input file (such as a **.png**) to an associated output file is referred to as a *builder*. 

We can visualize the path that a file takes from authoring to being loaded at runtime as follows:

![](introduction-images/image1.png "The path that a file takes from authoring to being loaded at runtime is visualized in this diagram")

## Why use a content pipeline?

Content pipelines introduce an extra step between the authoring application and the game, which can increase compile times and add complexity to the development process. Despite these considerations, content pipelines introduce a number of benefits to game development:


### Converting to a format understood by the game

CocosSharp and MonoGame provide methods for loading various types of content; however, the content must be formatted correctly before being loaded. Most types of content require some type of conversion before being loaded. For example, sound effects in the **.wav** format must be converted into an **.xnb** file to be loaded at runtime since CocosSharp and MonoGame do not support loading the **.wav** file format.


### Converting to a format native to the hardware

Different hardware may treat content differently at runtime. For example, CocosSharp games can load image files when creating a `CCSprite` instance. Although the same code can be used to load the files on both iOS and Android, each platform stores the loaded file differently. As a consequence, the MonoGame Content Pipeline formats texture **.xnb** files differently depending on the target platform.


### Reducing size on disk 

Content pipelines can be used to remove information, which is useful at author time but not necessary at runtime. The original (input) file can store all information which can help content creators maintain existing content, but the output file can be stripped-down to keep the overall game file small. This consideration is especially useful for mobile games that are downloaded rather than distributed on installation media.


### Reducing load time

Games may require modifications of content to improve runtime performance, to improve visuals, or to add new features. For example many 3D games calculate lighting one time, then use the result of this calculation when rendering complex scenes. Since performing these calculations when loading content can be prohibitively expensive the calculation can instead be performed when the game is built. The resulting calculations can be included in the content, enabling the content to be loaded much faster than would otherwise be possible. 


## xnb file extension

The **.xnb** file extension is the extension for all files outputted by the Monogame Content Pipeline. This matches the extension of files outputted by Microsoft XNA’s Content Pipeline.

The **.xnb** extension is used regardless of the original file type. In other words, image files (**.png**), audio files (**.wav**), and any custom file types will all be outputted as **.xnb** files when passed through the content pipeline. Since the extension cannot be used to distinguish between different file formats then both CocosSharp and MonoGame methods which load **.xnb** files do not expect extensions when loading the file.

CocosSharp and MonoGame .xnb files can be created using the Monogame Pipeline tool which is covered [in this walkthrough](~/graphics-games/cocossharp/content-pipeline/walkthrough.md).


## Summary

This article provided an overview and benefits of content pipelines in general, as well as an introduction to the MonoGame Content Pipeline.

## Related links

- [MonoGame Pipeline Documentation](http://www.monogame.net/documentation/?page=Pipeline)
