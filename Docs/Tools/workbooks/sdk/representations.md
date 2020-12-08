---
title: "Representations in Xamarin Workbooks"
description: "This document describes the Xamarin Workbooks representation pipeline, which enables the rendering of rich results for any code that returns a value."
ms.prod: xamarin
ms.assetid: 5C7A60E3-1427-47C9-A022-720F25ECB031
author: davidortinau
ms.author: daortin
ms.date: 03/30/2017
---

# Representations in Xamarin Workbooks

## Representations

Within a workbook or inspector session, code that is executed and yields a
result (e.g. a method returning a value or the result of an expression) is
processed through the representation pipeline in the agent. All objects, with
the exception of primitives such as integers, will be reflected to produce
interactive member graphs and will go through a process to provide alternate
representations that the client can render more richly. Objects of any size and
depth are safely supported (including cycles and infinite enumerables) due to
lazy and interactive reflection and remoting.

Xamarin Workbooks provides a few types common to all agents and clients that
allow for rich rendering of results. `Color` is one example of such a type,
where for example on iOS, the agent is responsible for converting `CGColor` or
`UIColor` objects into a `Xamarin.Interactive.Representations.Color` object.

In addition to common representations, the integration SDK provides APIs for
serializing custom representations in the agent and rendering representations
in the client.

## External Representations

`Xamarin.Interactive.IAgent.RepresentationManager` provides the ability to
register a `RepresentationProvider`, which an integration must implement to
convert from an arbitrary object to an agnostic form to render. These agnostic
forms must implement the `ISerializableObject` interface.

Implementing the `ISerializableObject` interface adds a Serialize method
that precisely controls how objects are serialized. The `Serialize`
method expects that a developer will exactly specify which properties
are to be serialized, and what the final name will be. Looking at the
`Person` object in our [`KitchenSink` sample][sample], we can see how
this works:

```csharp
public sealed class Person : ISerializableObject
{
  public string Name { get; }

  // Rest of the code is omitted…

  void ISerializableObject.Serialize (ObjectSerializer serializer)
    => serializer.Property (nameof (Name), Name);
}
```

If we wanted to provide a superset or subset of properties from the
original object, we can do that with `Serialize`. For example, we might
do something like this to provide a pre-computed `Age` property on `Person`:

```csharp
public sealed class Person : ISerializableObject
{
  public string Name { get; set; }
  public DateTime DateOfBirth { get; set; }

  // <snip>

  void ISerializableObject.Serialize (ObjectSerializer serializer)
  {
    serializer.Property (nameof (Name), Name);
    serializer.Property (nameof (DateOfBirth), DateOfBirth);

    // Let's pre-compute an Age property that's the person's age in years,
    // so we don't have to compute it in the renderer.
    var age = (DateTime.MinValue + (DateTime.Now - DateOfBirth)).Year - 1;
    serializer.Property ("Age", age)
  }
}
```

> [!NOTE]
> APIs that produce `ISerializableObject` objects directly do
> not need to be handled by a `RepresentationProvider`. If the object you
> want to display is **not** an `ISerializableObject`, you will want to
> handle wrapping it in your `RepresentationProvider`.

### Rendering a Representation

Renderers are implemented in JavaScript and will have access to a JavaScript
version of the object represented via `ISerializableObject`. The JavaScript
copy will also have a `$type` string property that indicates the .NET type
name.

We recommend using TypeScript for client integration code, which of course
compiles to vanilla JavaScript. Either way, the SDK provides [typings][typings]
which can be referenced directly by TypeScript or simply referred to manually
if writing vanilla JavaScript is preferred.

The main integration point for rendering is
`xamarin.interactive.RendererRegistry`:

```js
xamarin.interactive.RendererRegistry.registerRenderer(
  function (source) {
    if (source.$type === "SampleExternalIntegration.Person")
      return new PersonRenderer;
    return undefined;
  }
);
```

Here, `PersonRenderer` implements the `Renderer` interface. See the [typings][typings] for more details.

[typings]: https://github.com/xamarin/Workbooks/blob/master/SDK/typings/xamarin-interactive.d.ts
