---
title: "Introduction to UrhoSharp"
description: "This document describes the basic structure of an UrhoSharp application and links to various guides and sample applications that demonstrate how to use UrhoSharp."
ms.prod: xamarin
ms.assetid: 18041443-5093-4AF7-8B20-03E00478EF35
author: conceptdev
ms.author: crdun
ms.date: 03/29/2017
---
# Introduction to UrhoSharp

![UrhoSharp logo](introduction-images/urhosharp-icon.png)

UrhoSharp is a powerful 3D Game Engine for Xamarin and .NET
developers.  It is similar in spirit to Apple’s SceneKit and SpriteKit
and include physics, navigation, networking and much more while still
being cross platform.

It is a .NET binding to the [Urho3D](https://urho3d.github.io/) engine
and allows developers to write cross platform code that can target
Android, iOS, Windows and Mac with the same codebase and can render to
both OpenGL and Direct3D systems.

UrhoSharp is a game engine with a lot of functionality out of the box:

- Powerful 3D graphic rendering
- Physics simulation (using the Bullet library)
- Scene handling
- Await/Async support
- Friendly Actions API
- 2D integration into 3D scenes
- Font rendering with FreeType
- Client and server networking capabilities
- Import a wide range of assets (with Open Assets Library)
- Navigation mesh and pathfinding (using Recast/Detour)
- Convex hull generation for collision detection (using StanHull)
- Audio playback (with **libvorbis**)

## Get started

UrhoSharp is conveniently distributed as a [NuGet package](https://www.nuget.org/) and it can be added to
your C# or F# projects that target Windows, Mac, Android or iOS.  The
NuGet comes with both the libraries required to run your program, as
well as the basic assets (CoreData) used by the engine.

### Urho as a Portable Class Library

The Urho package can be consumed either from a platform-specific
project, or from a Portable Class Library project, allowing you to
reuse all of your code across all platforms.  This means that all you
would have to do on each platform is to write your platform specific
entry point, and then transfer control to your shared game code.

### Samples

You can get a taste for the capabilities of Urho by opening in either
Visual Studio for Mac or Visual Studio the Sample solution from:

[https://github.com/xamarin/urho-samples](https://github.com/xamarin/urho-samples)

The default solution contains projects for Android, iOS, Windows and
Mac.  We have structured that solution so that we have a tiny platform
specific launcher, and all of the sample code and game code lives in a
portable class library, illustrating how to maximize code reuse across
all platforms.

Consult the [Urho and Your
Platform](~/graphics-games/urhosharp/platform/index.md) page for more
information on how to create your own solutions.

Since all of the samples share a common set of user interface
elements, the samples have abstracted the basic setup in this file:

[https://github.com/xamarin/urho-samples/blob/master/FeatureSamples/Core/Sample.cs](https://github.com/xamarin/urho-samples/blob/master/FeatureSamples/Core/Sample.cs)

This provides a Sample base class that handles some basic keystrokes
and touch events, setups a camera, provides basic user interface
elements, and this allows each sample to focus on the specific
functionality that is being showcased.

The following sample shows what the engine is capable of doing:

- [Samply Game](https://github.com/xamarin/urho-samples/tree/master/SamplyGame) a simple clone of ShootySkies.

While the other samples show individual properties of each sample.

## Basic structure

Your game should subclass the `Application` class, this is where you will setup your game
(on the `Setup` method)
and start your game (in the `Start` method).  Then you construct your
main user interface.  We are going to walk through a small sample that
shows the APIs to setup a 3D scene, some UI elements and attaching a
simple behavior to it.

```csharp
class MySample : Application {
    protected override void Start ()
    {
        CreateScene ();
        Input.KeyDown += (args) => {
            if (args.Key == Key.Esc) Exit ();
        };
    }

    async void CreateScene()
    {
        // UI text
        var helloText = new Text()
        {
            Value = "Hello World from MySample",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        helloText.SetColor(new Color(0f, 1f, 1f));
        helloText.SetFont(
            font: ResourceCache.GetFont("Fonts/Font.ttf"),
            size: 30);
        UI.Root.AddChild(helloText);

        // Create a top-level scene, must add the Octree
        // to visualize any 3D content.
        var scene = new Scene();
        scene.CreateComponent<Octree>();
        // Box
        Node boxNode = scene.CreateChild();
        boxNode.Position = new Vector3(0, 0, 5);
        boxNode.Rotation = new Quaternion(60, 0, 30);
        boxNode.SetScale(0f);
        StaticModel modelObject = boxNode.CreateComponent<StaticModel>();
        modelObject.Model = ResourceCache.GetModel("Models/Box.mdl");
        // Light
        Node lightNode = scene.CreateChild(name: "light");
        lightNode.SetDirection(new Vector3(0.6f, -1.0f, 0.8f));
        lightNode.CreateComponent<Light>();
        // Camera
        Node cameraNode = scene.CreateChild(name: "camera");
        Camera camera = cameraNode.CreateComponent<Camera>();
        // Viewport
        Renderer.SetViewport(0, new Viewport(scene, camera, null));
        // Perform some actions
        await boxNode.RunActionsAsync(
            new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1)));
        await boxNode.RunActionsAsync(
            new RepeatForever(new RotateBy(duration: 1,
                deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
     }
}
```

If you run this application, you will quickly discover that the
runtime is complaining about your assets are not there.  What you need
to do is create a hierarchy in your project that starts with the
special directory name "Data", and inside this, you would place the
assets that you reference in your program.  You must then set in the
item properties for each asset the “Copy to Output Directory” to “Copy
if Newer”, that will ensure that your data is there.

Let us explain what is going on here.

To launch your application you call the engine initialization
function, followed by creating a new instance of your Application
class, like this:

```csharp
new MySample().Run();
```

The runtime will invoke the `Setup` and `Start` methods for you.  If you
override `Setup` you can configure the engine parameters (not show in
this sample).

You must override `Start` as this will launch your game.  In this method
you will load your assets, connect event handlers, setup your scene
and start any actions that you desire.  In our sample, we both create
a little bit of UI to show to the user as well as setting up a 3D
scene.

The following piece of code uses the UI framework to create a text
element and add it to your application:

```csharp
// UI text
var helloText = new Text()
{
    Value = "Hello World from UrhoSharp",
    HorizontalAlignment = HorizontalAlignment.Center,
    VerticalAlignment = VerticalAlignment.Center
};
helloText.SetColor(new Color(0f, 1f, 1f));
helloText.SetFont(
    font: ResourceCache.GetFont("Fonts/Font.ttf"),
    size: 30);
UI.Root.AddChild(helloText);
```

The UI framework is there to provide a very simple in-game user
interface, and it works by adding new nodes to the `UI.Root` node.

The second part of our sample setups the main scene.  This involves a
number of steps, creating a 3D Scene, creating a 3D box in the screen,
adding a light, a camera and a viewport.  These are explored in more
detail in the section [Scene, Nodes, Components and Cameras](~/graphics-games/urhosharp/using.md#scenenodescomponentsandcameras).

The third part of our sample triggers a couple of actions.  Actions
are recipes that describe a particular effect, and once created they
can be executed by a node on demand by calling the `RunActionAsync`
method on a `Node`.

The first action scales the box with a bouncing effect and the second
one rotates the box forever:

```csharp
await boxNode.RunActionsAsync(
    new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1)));
```

The above shows how the first action that we create is a `ScaleTo`
action, this is merely a recipe that indicates that you want to scale
for a second towards the value one the scale property of a node.  This
action in turn is wrapped around an easing action, the `EaseBounceOut`
action.  The easing actions distort the linear execution of an action
and apply an effect, in this case it provides the bouncing-out effect.
So our recipe could be written as:

```csharp
var recipe = new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1));
```

Once the recipe has been created, we execute the recipe:

```csharp
await boxNode.RunActionsAsync (recipe)
```

The await indicates that the will want to resume execution after this
line when the action completes.  Once the action completes we trigger
the second animation.

The [Using UrhoSharp](~/graphics-games/urhosharp/using.md) document
explores in more depth the concepts behind Urho and how to structure
your code to build a game.

## Copyrights

This documentation contains original content from Xamarin Inc, but
draws extensively from the open source documentation for the Urho3D
project and contains screenshots from the Cocos2D project.
