---
title: "Using UrhoSharp To Build A 3D Game"
description: "This document provides an overview of UrhoSharp, describing scenes, components, shapes, cameras, actions, user input, sound, and more."
ms.prod: xamarin
ms.assetid: D9BEAD83-1D9E-41C3-AD4B-3D87E13674A0
author: conceptdev
ms.author: crdun
ms.date: 03/29/2017
---

# Using UrhoSharp to build a 3D game

Before you write your first game, you want to get familiarized with
the basics: how to setup your scene, how to load resources (this
contains your artwork) and how to create simple interactions for your
game.

<a name="scenenodescomponentsandcameras"></a>

## Scenes, nodes, components, and cameras

The scene model can be described as a component-based scene graph. The
Scene consists of a hierarchy of scene nodes, starting from the root
node, which also represents the whole scene. Each `Node` has a 3D
transform (position, rotation and scale), a name, an ID, plus an
arbitrary number of components.  Components bring a node to life, they
can make add a visual representation (`StaticModel`), they can emit
sound (`SoundSource`), they can provide a collision boundary and so on.

You can create your scenes and setup nodes using the [Urho Editor](#urhoeditor), or
you can do things from your C# code.  In this document we will explore
setting things up using code, as they illustrate the elements
necessary to get things to show up on your screen

In addition to setting up your scene, you need to setup a `Camera`, this is what determines what will get shown to the user.

### Setting up your scene

You would typically create this form your Start method:

```csharp
var scene = new Scene ();
// Create the Octree component to the scene. This is required before
// adding any drawable components, or else nothing will show up. The
// default octree volume will be from -1000, -1000, -1000) to
//(1000, 1000, 1000) in world coordinates; it is also legal to place
// objects outside the volume but their visibility can then not be
// checked in a hierarchically optimizing manner
scene.CreateComponent<Octree> ();
// Create a child scene node (at world origin) and a StaticModel
// component into it. Set the StaticModel to show a simple plane mesh
// with a "stone" material. Note that naming the scene nodes is
// optional. Scale the scene node larger (100 x 100 world units)
var planeNode = scene.CreateChild("Plane");
planeNode.Scale = new Vector3 (100, 1, 100);
var planeObject = planeNode.CreateComponent<StaticModel> ();
planeObject.Model = ResourceCache.GetModel ("Models/Plane.mdl");
planeObject.SetMaterial(ResourceCache.GetMaterial("Materials/StoneTiled.xml"));
```

### Components

Rendering 3D objects, sound playback, physics and scripted logic
updates are all enabled by creating different Components into the
nodes by calling `CreateComponent<T>()`.  For example, setup your node
and light component like this:

```csharp
// Create a directional light to the world so that we can see something. The
// light scene node's orientation controls the light direction; we will use
// the SetDirection() function which calculates the orientation from a forward
// direction vector.
// The light will use default settings (white light, no shadows)
var lightNode = scene.CreateChild("DirectionalLight");
lightNode.SetDirection (new Vector3(0.6f, -1.0f, 0.8f));
```

We have created above a node with the name "`DirectionalLight`" and
set a direction for it, but nothing else.  Now, we can turn the above
node into a light-emitting node, by attaching a `Light` component to it,
with `CreateComponent`:

```csharp
var light = lightNode.CreateComponent<Light>();
```

Components created into the `Scene` itself have a special role: to
implement scene-wide functionality. They should be created before all
other components, and include the following:

 `Octree`: implements spatial partitioning and accelerated visibility queries. Without this 3D objects can not be rendered.
 `PhysicsWorld`: implements physics simulation. Physics components such as `RigidBody` or `CollisionShape` can not function properly without this.
 `DebugRenderer`: implements debug geometry rendering.

Ordinary components like
`Light`,
`Camera` or
`StaticModel`
should not be created directly into the
`Scene`, but
rather into child nodes.

The library comes with a wide variety of components that you can
attach to your nodes to bring them to life: user-visible elements
(models), sounds, rigid bodies, collision shapes, cameras, light
sources, particle emitters and much more.

### Shapes

As a convenience, various shapes are available as simple nodes in the
Urho.Shapes namespace.  These include boxes, spheres, cones, cylinders
and planes.

### Camera and viewport

Just like the light, cameras are components, so you will need to
attach the component to a node, like this:

```csharp
var CameraNode = scene.CreateChild ("camera");
camera = CameraNode.CreateComponent<Camera>();
CameraNode.Position = new Vector3 (0, 5, 0);
```

With this, you have created a camera, and you have placed the camera
in the 3D world, the next step is to inform the `Application` that this
is the camera that you want to use, this is done with the following
code:

```csharp
Renderer.SetViewPort (0, new Viewport (Context, scene, camera, null))
```

And now you should be able to see the results of your creation.

### Identification and scene hierarchy

Unlike nodes, components do not have names; components inside the same
node are only identified by their type, and index in the node's
component list, which is filled in creation order, for example, you
can retrieve the
`Light` component
out of the `lightNode` object above like this:

```csharp
var myLight = lightNode.GetComponent<Light>();
```

You can also get a list of all the components by retrieving the
`Components` property which returns an `IList<Component>` that you can
use.

When created, both nodes and components get scene-global integer
IDs. They can be queried from the Scene by using the functions
`GetNode(uint id)` and `GetComponent(uint id)`. This is much faster than
for example doing recursive name-based scene node queries.

There is no built-in concept of an entity or a game object; rather it
is up to the programmer to decide the node hierarchy, and in which
nodes to place any scripted logic. Typically, free-moving objects in
the 3D world would be created as children of the root node. Nodes can
be created either with or without a name using
`CreateChild`. Uniqueness of node names is not enforced.

Whenever there is some hierarchical composition, it is recommended
(and in fact necessary, because components do not have their own 3D
transforms) to create a child node.

For example if a character was holding an object in his hand, the
object should have its own node, which would be parented to the
character's hand bone (also a `Node`).  The exception is the physics
`CollisionShape`, which can be offsetted and rotated individually in
relation to the node.

Note that `Scene`'s own transform is purposefully ignored as an
optimization when calculating world derived transforms of child nodes,
so changing it has no effect and it should be left as it is (position
at origin, no rotation, no scaling.)

`Scene` nodes can be freely reparented. In contrast components always
belong to the node they attached to, and can not be moved between
nodes. Both nodes and components provide a `Remove` function to
accomplish this without having to go through the parent. Once the node
is removed, no operations on the node or component in question are
safe after calling that function.

It is also possible to create a `Node` that does not belong to a
scene. This is useful for example with a camera moving in a scene that
may be loaded or saved, because then the camera will not be saved
along with the actual scene, and will not be destroyed when the scene
is loaded. However, note that creating geometry, physics or script
components to an unattached node, and then moving it into a scene
later will cause those components to not work correctly.

### Scene updates

A Scene whose updates are enabled (default) will be automatically
updated on each main loop iteration.  The application's `SceneUpdate`
event handler is invoked on it.

Nodes and components can be excluded from the scene update by
disabling them, see `Enabled`.  The behavior depends on
the specific component, but for example disabling a drawable component
also makes it invisible, while disabling a sound source component
mutes it. If a node is disabled, all of its components are treated as
disabled regardless of their own enable/disable state.

## Adding behavior to your components

The best way to structure your game is to make your own component that
encapsulate an actor or element on your game.  This makes the feature
self contained, from the assets used to display it, to its behavior.

The simplest way of adding behavior to a component is to use actions,
which are instructions that you can queue and combine that with C#
async programming.  This allows the behavior for your component to be
very high level and makes it simpler to understand what is happening.

Alternatively, you can control exactly what happens to your component
by updating your component properties on each frame (discussed in
Frame-based Behavior section).

### Actions

You can add behavior to nodes very easily using Actions.  Actions can
alter various node properties and execute them over a period of time,
or repeat them a number of times with a given animation curve.

For example, consider a "cloud" node on your scene, you can fade it like this:

```csharp
await cloud.RunActionsAsync (new FadeOut (duration: 3))
```

Actions are immutable objects, which allows you to reuse the action for driving different objects.

A common idiom is to create an action that performs the reverse
operation:

```csharp
var gotoExit = new MoveTo (duration: 3, position: exitLocation);
var return = gotoExit.Reverse ();
```

The following example would fade the object for you over a period of
three seconds.  You can also run one action after another, for
example, you could first move the cloud, and then hide it:

```csharp
await cloud.RunActionsAsync (
    new MoveBy  (duration: 1.5f, position: new Vector3(0, 0, 15),
    new FadeOut (duration: 3));
```

If you want both actions to take place at the same time, you can use
the Parallel action, and provide all the actions you want done in
parallel:

```csharp
  await cloud.RunActionsAsync (
    new Parallel (
      new MoveBy  (duration: 3, position: new Vector3(0, 0, 15),
      new FadeOut (duration: 3)));
```

In the above example the cloud will move and fade out at the same time.

You will notice that these are using C# `await`, which allows you to
think linearly about the behavior you want to achieve.

### Basic actions

These are the actions supported in UrhoSharp:

- Moving nodes: `MoveTo`, `MoveBy`, `Place`, `BezierTo`, `BezierBy`, `JumpTo`, `JumpBy`
- Rotating nodes: `RotateTo`, `RotateBy`
- Scaling nodes: `ScaleTo`, `ScaleBy`
- Fading nodes: `FadeIn`, `FadeTo`, `FadeOut`, `Hide`, `Blink`
- Tinting: `TintTo`, `TintBy`
- Instants: `Hide`, `Show`, `Place`, `RemoveSelf`, `ToggleVisibility`
- Looping: `Repeat`, `RepeatForever`, `ReverseTime`

Other advanced features include the combination of the `Spawn` and `Sequence` actions.

### Easing - controlling the speed of your actions

Easing is a way that directs the way that the animation will unfold,
and it can make your animations a lot more pleasant.  By default your
actions will have a linear behavior, for example a `MoveTo` action would
have a very robotic movement.  You can wrap your Actions into an
Easing action to change the behavior, for example, one that would
slowly start the movement, accelerate and slowly reach the end
(`EasyInOut`).

You do this by wrapping an existing Action into an easing action, for
example:

```csharp
await cloud.RunActionAsync (
   new EaseInOut (
     new MoveTo (duration: 3, position: new Vector (0,0,15)), rate:1))
```

There are many easing modes, the following chart shows the various
easing types and their behavior on the value of the object they are
controlling over the period of time, from start to finish:

![Easing Modes](using-images/easing.png "This chart shows the various
easing types and their behavior on the value of the object they are
controlling over the period of time")

### Using actions and async code

In your `Component` subclass, you should introduce an async method that
prepares your component behavior and drives the functionality for it.
Then you would invoke this method using the C# `await` keyword from
another part of your program, either your `Application.Start` method or
in response to a user or story point in your application.

For example:

```csharp
class Robot : Component {
    public bool IsAlive;
    async void Launch ()
    {
        // Dress up our robot
        var cache = Application.ResourceCache;
        var model = node.CreateComponent<StaticModel>();
        model.Model = cache.GetModel ("robot.mdl"));
        model.SetMaterial (cache.GetMaterial ("robot.xml"));
        Node.SetScale (1);

        // Bring the robot into our scene
        await Node.RunActionsAsync(
            new MoveBy(duration: 0.6f, position: new Vector3(0, -2, 0)));
        // Make him move around to avoid the user fire
        MoveRandomly(minX: 1, maxX: 2, minY: -3, maxY: 3, duration: 1.5f);
        // And simultaneously have him shoot at the user
        StartShooting();
    }

    protected async void MoveRandomly (float minX, float maxX,
                                       float minY, float maxY,
                       float duration)
    {
        while (IsAlive){
            var moveAction = new MoveBy(duration,
                new Vector3(RandomHelper.NextRandom(minX, maxX),
                            RandomHelper.NextRandom(minY, maxY), 0));
            await Node.RunActionsAsync(moveAction, moveAction.Reverse());
        }
    }
    protected async void StartShooting()
    {
        while (IsAlive && Node.Components.Count > 0){
            foreach (var weapon in Node.Components.OfType<Weapon>()){
                await weapon.FireAsync(false);
                if (!IsAlive)
                    return;
            }
            await Node.RunActionsAsync(new DelayTime(0.1f));
        }
    }
}
```

In the `Launch` method above three actions are started: the robot comes
into the scene, this action will alter the location of the node over a
period of 0.6 seconds.  Since this is an async option, this will
happen concurrently as the next instruction which is the call to
`MoveRandomly`.  This method will alter the position of the robot in
parallel to a random location.  This is achieved by performing two
compounded actions, the movement to a new location, and going back to
the original position and repeat this as long as the robot remains
alive.  And to make things more interesting, the robot will keep
shooting simultaneously.  The shooting will only start every 0.1
seconds.

### Frame-based behavior programming

If you want to control the behavior of your component on a
frame-by-frame basis instead of using actions, what you would do is to
override the `OnUpdate` method of your `Component` subclass.  This
method is invoked once per frame, and is invoked only if you set the
ReceiveSceneUpdates property to true.

The following shows how to create a `Rotator` component, that is then
attached to a Node, which causes the node to rotate:

```csharp
class Rotator : Component {
    public Rotator()
    {
        ReceiveSceneUpdates = true;
    }
    public Vector3 RotationSpeed { get; set; }
    protected override void OnUpdate(float timeStep)
    {
        Node.Rotate(new Quaternion(
            RotationSpeed.X  timeStep,
            RotationSpeed.Y  timeStep,
            RotationSpeed.Z  timeStep),
            TransformSpace.Local);
    }
}
```

And this is how you would attach this component to a node:

```csharp
Node boxNode = new Node();
var rotator = new Rotator() { RotationSpeed = rotationSpeed };
boxNode.AddComponent (rotator);
```

### Combining styles

You can use the async/action based model for programming much of the
behavior which is great for fire-and-forget style of programming, but
you can also fine tune your component’s behavior to also run some
update code on each frame.

For example, in the SamplyGame demo this is used in the `Enemy` class encodes
the basic behavior uses actions, but it also ensures that the
components point toward the user by setting direction of the node with
`Node.LookAt`:

```csharp
    protected override void OnUpdate(SceneUpdateEventArgs args)
    {
        Node.LookAt(
            new Vector3(0, -3, 0),
            new Vector3(0, 1, -1),
            TransformSpace.World);
        base.OnUpdate(args);
    }
```

## Loading and saving scenes

Scenes can be loaded and saved in XML format; see the functions
`LoadXml` and `SaveXML`. When a scene is loaded, all existing content
in it (child nodes and components) is removed first. Nodes and
components that are marked temporary with the `Temporary` property
will not be saved. The serializer handles all built-in components and
properties but it's not smart enough to handle custom properties and
fields defined in your Component subclasses. However it provides two
virtual methods for this:

 `OnSerialize` where you can register you custom states for the serialization

 `OnDeserialized` where you can obtain your saved custom states.

Typically, a custom component will look like the following:

```csharp
class MyComponent : Component {
    // Constructor needed for deserialization
    public MyComponent(IntPtr handle) : base(handle) { }
    public MyComponent() { }
    // user defined properties (managed state):
    public Quaternion MyRotation { get; set; }
    public string MyName { get; set; }

    public override void OnSerialize(IComponentSerializer ser)
    {
        // register our properties with their names as keys using nameof()
        ser.Serialize(nameof(MyRotation), MyRotation);
        ser.Serialize(nameof(MyName), MyName);
    }

    public override void OnDeserialize(IComponentDeserializer des)
    {
        MyRotation = des.Deserialize<Quaternion>(nameof(MyRotation));
        MyName = des.Deserialize<string>(nameof(MyName));
    }
    // called when the component is attached to some node
    public override void OnAttachedToNode()
    {
        var node = this.Node;
    }
}
```

### Object prefabs

Just loading or saving whole scenes is not flexible enough for games
where new objects need to be dynamically created. On the other hand,
creating complex objects and setting their properties in code will
also be tedious. For this reason, it is also possible to save a scene
node which will include its child nodes, components and
attributes. These can later conveniently be loaded as a group.  Such a
saved object is often referred to as a prefab. There are three ways to
do this:

- In code by calling `Node.SaveXml` on the Node
- In the editor, by selecting the node in the hierarchy window and choosing "Save node as" from the "File" menu.
- Using the "node" command in `AssetImporter`, which will save the scene node hierarchy and any models contained in the input asset (eg. a Collada file)

To instantiate the saved node into a scene, call `InstantiateXml`. The
node will be created as a child of the Scene but can be freely
reparented after that. Position and rotation for placing the node need
to be specified. The following code demonstrates how to instantiate a
prefab `Ninja.xm` to a scene with desired position and rotation:

```csharp
var prefabPath = Path.Combine (FileSystem.ProgramDir,"Data/Objects/Ninja.xml");
using (var file = new File(Context, prefabPath, FileMode.Read))
{
    scene.InstantiateXml(file, desiredPos, desiredRotation,
        CreateMode.Replicated);
}
```

## Events

UrhoObjects raise a number of events, these are surfaced as C# events
on the various classes that generate them.  In addition to the
C#-based event model, it is also possible to use a the `SubscribeToXXX`
methods that will allow you to subscribe and keep a subscription token
that you can later use to unsubscribe.  The difference is that the
former will allow many callers to subscribe, while the second one only
allows one, but allows for the nicer lambda-style approach to be used,
and yet, allow for easy removal of the subscription.  They are
mutually exclusive.

When you subscribe to an event, you must provide a method that takes
an argument with the appropriate event arguments.

For example, this is how you subscribe to a mouse button down event:

```csharp
public void override Start ()
{
    UI.MouseButtonDown += HandleMouseButtonDown;
}

void HandleMouseButtonDown(MouseButtonDownEventArgs args)
{
    Console.WriteLine ("button pressed");
}
```

With lambda style:

```csharp
public void override Start ()
{
    UI.MouseButtonDown += args => {
        Console.WriteLine ("button pressed");
    };
}
```

Sometimes you will want to stop receiving notifications for the event,
in those cases, save the return value from the call to `SubscribeTo`
method, and invoke the Unsubscribe method on it:

```csharp
Subscription mouseSub;

public void override Start ()
{
    mouseSub = UI.SubscribeToMouseButtonDown (args => {
    Console.WriteLine ("button pressed");
      mouseSub.Unsubscribe ();
    };
}
```

The parameter received by the event handler is a strongly typed
event arguments class that will be specific to each event and contains the
event payload.

## Responding to user input

You can subscribe to various events, like keystrokes down by
subscribing to the event, and responding to the input being delivered:

```csharp
Start ()
{
    UI.KeyDown += HandleKeyDown;
}

void HandleKeyDown (KeyDownEventArgs arg)
{
     switch (arg.Key){
     case Key.Esc: Engine.Exit (); return;
}
```

But in many scenarios, you want your scene update handlers to check
on the current status of the keys when they are being updated, and
update your code accordingly.  For example, the following can be used
to update the camera location based on the keyboard input:

```csharp
protected override void OnUpdate(float timeStep)
{
    Input input = Input;
    // Movement speed as world units per second
    const float moveSpeed = 4.0f;
    // Read WASD keys and move the camera scene node to the
    // corresponding direction if they are pressed
    if (input.GetKeyDown(Key.W))
        CameraNode.Translate(Vector3.UnitY  moveSpeed  timeStep, TransformSpace.Local);
    if (input.GetKeyDown(Key.S))
        CameraNode.Translate(new Vector3(0.0f, -1.0f, 0.0f)  moveSpeed  timeStep, TransformSpace.Local);
    if (input.GetKeyDown(Key.A))
        CameraNode.Translate(new Vector3(-1.0f, 0.0f, 0.0f)  moveSpeed  timeStep, TransformSpace.Local);
    if (input.GetKeyDown(Key.D))
        CameraNode.Translate(Vector3.UnitX  moveSpeed  timeStep, TransformSpace.Local);
}
```

## Resources (assets)

Resources include most things in UrhoSharp that are loaded from mass storage during initialization or runtime:

- `Animation` - used for skeletal animations
- `Image` - represents images stored in a variety of graphic formats
- `Model` - 3D Models
- `Material` - materials used to render Models.
- `ParticleEffect`- [describes](https://urho3d.github.io/documentation/1.4/_particles.html) how a particle emitter works, see "[Particles](#particles)" below.
- `Shader` - custom shaders
- `Sound` - sounds to playback, see "[Sound](#sound)" below.
- `Technique` - material rendering techniques
- `Texture2D` - 2D texture
- `Texture3D` - 3D texture
- `TextureCube` - Cube texture
- `XmlFile`

They are managed and loaded by the `ResourceCache` subsystem (available as `Application.ResourceCache`).

The resources themselves are identified by their file paths, relative
to the registered resource directories or package files. By default,
the engine registers the resource directories `Data` and `CoreData`, or
the packages `Data.pak` and `CoreData.pak` if they exist.

If loading a resource fails, an error will be logged and a null
reference is returned.

The following example shows a typical way of fetching a resource from
the resource cache.  In this case, a texture for a UI element, this
uses the `ResourceCache` property from the `Application` class.

```csharp
healthBar.SetTexture(ResourceCache.GetTexture2D("Textures/HealthBarBorder.png"));
```

Resources can also be created manually and stored to the resource
cache as if they had been loaded from disk.

Memory budgets can be set per resource type: if resources consume more
memory than allowed, the oldest resources will be removed from the
cache if not in use anymore. By default the memory budgets are set to
unlimited.

### Bringing 3D-models and images

Urho3D tries to use existing file formats whenever possible, and
define custom file formats only when absolutely necessary such as for
models (.mdl) and for animations (.ani). For these types of assets,
Urho provides a converter -
[AssetImporter](https://urho3d.github.io/documentation/1.4/_tools.html)
which can consume many popular 3D formats such as fbx, dae, 3ds, and
obj, etc.

There is also a handy add-in for Blender
[https://github.com/reattiva/Urho3D-Blender](https://github.com/reattiva/Urho3D-Blender)
that can export your Blender assets in the format that is suitable for
Urho3D.

### Background loading of resources

Normally, when requesting resources using one of the `ResourceCache`’s
`Get` method, they are loaded immediately in the main thread, which may
take several milliseconds for all the required steps (load file from
disk, parse data, upload to GPU if necessary) and can therefore result
in framerate drops.

If you know in advance what resources you need, you can request them
to be loaded in a background thread by calling
`BackgroundLoadResource`. You can subscribe to the Resource
Background Loaded event by using the
`SubscribeToResourceBackgroundLoaded` method. it will tell if the
loading actually was a success or a failure. Depending on the
resource, only a part of the loading process may be moved to a
background thread, for example the finishing GPU upload step always
needs to happen in the main thread. Note that if you call one of the
resource loading methods for a resource that is queued for background
loading, the main thread will stall until its loading is complete.

The asynchronous scene loading functionality `LoadAsync` and
`LoadAsyncXML` has the option to background load the resources first
before proceeding to load the scene content. It can also be used to
only load the resources without modifying the scene, by specifying the
`LoadMode.ResourcesOnly`. This allows to prepare a scene or object
prefab file for fast instantiation.

Finally the maximum time (in milliseconds) spent each frame on
finishing background loaded resources can be configured by setting the
`FinishBackgroundResourcesMs` property on the `ResourceCache`.

<a name="sound"></a>

## Sound

Sound is an important part of game play, and the UrhoSharp framework
provides a way of playing sounds in your game.  You play sounds by
attaching a
`SoundSource`
component to a
`Node` and then
playing a named file from your resources.

This is how it is done:

```csharp
var explosionNode = Scene.CreateChild();
var sound = explosionNode.CreateComponent<SoundSource>();
soundSource.Play(Application.ResourceCache.GetSound("Sounds/ding.wav"));
soundSource.Gain = 0.5f;
soundSource.AutoRemove = true;
```

<a name="particles"></a>

## Particles

Particles provide a simple way of adding some simple and inexpensive
effects to your application.  You can consume particles stored in PEX
format, using tools like
[http://onebyonedesign.com/flash/particleeditor/](http://onebyonedesign.com/flash/particleeditor/).

Particles are components that can be added to a node.  You need to
call the node’s `CreateComponent<ParticleEmitter2D>` method to create
the particle and then configure the particle by setting the Effect
property to a 2D effect that is loaded from the resource cache.

For example, you can invoke this method on your component to show some particles that are rendered as an explosion when it hits:

```csharp
public async void Explode (Component target)
{
    // show a small explosion when the missile reaches an aircraft.
    var cache = Application.ResourceCache;
    var explosionNode = Scene.CreateChild();
    explosionNode.Position = target.Node.WorldPosition;
    explosionNode.SetScale(1f);
    var particle = explosionNode.CreateComponent<ParticleEmitter2D>();
    particle.Effect = cache.GetParticleEffect2D("explosion.pex");
    var scaleAction = new ScaleTo(0.5f, 0f);
    await explosionNode.RunActionsAsync(
        scaleAction, new DelayTime(0.5f));
    explosionNode.Remove();
}
```

The above code will create an explosion node that is attached to your
current component, inside this explosion node we create a 2D particle
emitter and configure it by setting the Effect property.  We run two
actions, one that scales the node to be smaller, and one that leaves
it at that size for 0.5 seconds.  Then we remove the explosion, which
also removes the particle effect from the screen.

The above particle renders like this when using a sphere texture:

![Particles with a sphere texture](using-images/image-1.png "The above particle renders like this when using a sphere texture")

And this is what it looks if you use a blocky texture:

![Particles with a box texture](using-images/image-2.png "And this is what it looks if using a blocky texture")

## Multi-threading support

UrhoSharp is a single threaded library.  This means that you should
not attempt to invoke methods in UrhoSharp from a background thread,
or you risk corrupting the application state, and likely crash your
application.

If you want to run some code in the background and then update Urho
components on the main UI, you can use the
`Application.InvokeOnMain(Action)`
method.  Additionally, you can use C# await and the .NET task APIs to
ensure that the code is executed on the proper thread.

## UrhoEditor

You can download the Urho Editor for your platform from the [Urho
Website](http://urho3d.github.io/), go to Downloads and pick the
latest version.

## Copyrights

This documentation contains original content from Xamarin Inc, but
draws extensively from the open source documentation for the Urho3D
project and contains screenshots from the Cocos2D project.
