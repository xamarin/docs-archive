---
title: "Introduction to Game Development with MonoGame"
description: "This multi-part walkthrough shows how to create a simple 2D application using MonoGame.  It covers common game programming concepts, such as graphics, input, game entities, and physics."
ms.prod: xamarin
ms.assetid: D781401F-7A96-4098-9645-5F98AEAF7F71
author: conceptdev
ms.author: crdun
ms.date: 03/28/2017
---
# Introduction to Game Development with MonoGame

_This multi-part walkthrough shows how to create a simple 2D application using MonoGame.  It covers common game programming concepts, such as graphics, input, game entities, and physics._

This article describes MonoGame API technology for making cross-platform games. For a full list of platforms, see the [MonoGame website](http://www.monogame.net/). This tutorial will use C# for code samples, although MonoGame is fully functional with F# as well.

MonoGame is a cross-platform, hardware accelerated API providing graphics, audio, game state management, input, and a content pipeline for importing assets. Unlike most game engines, MonoGame does not provide or impose any pattern or project structure.  While this means that developers are free to organize their code as they like, it also means that a bit of setup code is needed when first starting a new project.

The first section of this walkthrough focuses on setting up an empty project. The last section covers writing all of our game logic and content – most of which will be cross platform.

By the end of this walkthrough, we will have created a simple game where the player can control an animated character with touch input.  Although this is not technically a full game (since it has no win or lose conditions), it demonstrates numerous game development concepts and can be used as the foundation for many types of games.

The following shows the result of this walkthrough:

![Animation of sample game character following the mouse](images/image1.gif)

## MonoGame and XNA

The MonoGame library is intended to mimic Microsoft’s XNA library in both syntax and functionality.  All MonoGame objects exist under the Microsoft.Xna namespace – allowing most XNA code to be used in MonoGame with no modification.

Developers familiar with XNA will already be familiar with MonoGame’s syntax, and developers looking for additional information on working with MonoGame will be able to reference existing online XNA walkthroughs, API documentation, and discussions.

## Walkthrough Parts

- [Part 1 – Creating a Cross Platform MonoGame Project](~/graphics-games/monogame/introduction/part1.md)
- [Part 2 – Implementing the WalkingGame](~/graphics-games/monogame/introduction/part2.md)

## Related Links

- [WalkingGame MonoGame Project (sample)](/samples/xamarin/mobile-samples/walkinggamemg/)
- [MonoGame Android on NuGet](https://www.nuget.org/packages/MonoGame.Framework.Android/)
- [MonoGame iOS on NuGet](https://www.nuget.org/packages/MonoGame.Framework.iOS/)
- [MonoGame API Documentation](http://www.monogame.net/documentation/?page=main)