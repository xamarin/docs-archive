---
title: ".NET Embedding best practices for Objective-C"
description: "This document describes various best practices for using .NET Embedding with Objective-C. It discusses exposing a subset of the managed code, exposing a chunkier API, naming, and more."
ms.prod: xamarin
ms.assetid: 63C7F5D2-8933-4D4A-8348-E9CBDA45C472
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# .NET Embedding best practices for Objective-C

This is a draft and might not be in-sync with the features presently supported by the tool. We hope that this document will evolve separately and eventually match the final tool, i.e. we'll suggest long term best approaches - not immediate workarounds.

A large part of this document also applies to other supported languages. However all provided examples are in C# and Objective-C.

## Exposing a subset of the managed code

The generated native library/framework contains Objective-C code to call each of the managed APIs that is exposed. The more API you surface (make public) then larger the native _glue_ library will become.

It might be a good idea to create a different, smaller assembly, to expose only the required APIs to the native developer. That facade will also allow you more control over the visibility, naming, error checking... of the generated code.

## Exposing a chunkier API

There is a price to pay to transition from native to managed (and back). As such, it's better to expose _chunky instead of chatty_ APIs to the native developers, e.g.

**Chatty**

```csharp
public class Person {
  public string FirstName { get; set; }
  public string LastName { get; set; }
}
```

```objc
// this requires 3 calls / transitions to initialize the instance
Person *p = [[Person alloc] init];
p.firstName = @"Sebastien";
p.lastName = @"Pouliot";
```

**Chunky**

```csharp
public class Person {
  public Person (string firstName, string lastName) {}
}
```

```objc
// a single call / transition will perform better
Person *p = [[Person alloc] initWithFirstName:@"Sebastien" lastName:@"Pouliot"];
```

Since the number of transitions is smaller the performance will be better. It also requires less code to be generated, so this will produce a smaller native library as well.

## Naming

Naming things is one of two hardest problems in computer science, the others being cache invalidation and off-by-1 errors. Hopefully .NET Embedding can shield you from all but naming.

### Types

Objective-C does not support namespaces. In general, its types are prefixed with a 2 (for Apple) or 3 (for 3rd parties) character prefix, like `UIView` for UIKit's View, which denotes the framework.

For .NET types skipping the namespace is not possible as it can introduce duplicated, or confusing, names. This makes existing .NET types very long, e.g.

```csharp
namespace Xamarin.Xml.Configuration {
  public class Reader {}
}
```

would be used like:

```objc
id reader = [[Xamarin_Xml_Configuration_Reader alloc] init];
```

However you can re-expose the type as:

```csharp
public class XAMXmlConfigReader : Xamarin.Xml.Configuration.Reader {}
```

making it more Objective-C friendly to use, e.g.:

```objc
id reader = [[XAMXmlConfigReader alloc] init];
```

### Methods

Even good .NET names might not be ideal for an Objective-C API.

Naming conventions in Objective-C are different than .NET (camel case instead of pascal case, more verbose).
Please read the [coding guidelines for Cocoa](https://developer.apple.com/library/content/documentation/Cocoa/Conceptual/CodingGuidelines/Articles/NamingMethods.html#//apple_ref/doc/uid/20001282-BCIGIJJF).

From an Objective-C developer's point of view, a method with a `Get` prefix implies you do not own the instance, i.e. the [get rule](https://developer.apple.com/library/content/documentation/CoreFoundation/Conceptual/CFMemoryMgmt/Concepts/Ownership.html#//apple_ref/doc/uid/20001148-SW1).

This naming rule has no match in the .NET GC world; a .NET method with a `Create` prefix will behave identically in .NET. However, for Objective-C developers, it normally means you own the returned instance, i.e. the [create rule](https://developer.apple.com/library/content/documentation/CoreFoundation/Conceptual/CFMemoryMgmt/Concepts/Ownership.html#//apple_ref/doc/uid/20001148-103029).

## Exceptions

It's quite common in .NET to use exceptions extensively to report errors. However, they are slow and not quite identical in Objective-C. Whenever possible you should hide them from the Objective-C developer.

For example, the .NET `Try` pattern will be much easier to consume from Objective-C code:

```csharp
public int Parse (string number)
{
  return Int32.Parse (number);
}
```

versus

```csharp
public bool TryParse (string number, out int value)
{
  return Int32.TryParse (number, out value);
}
```

### Exceptions inside `init*`

In .NET a constructor must either succeed and return a (_hopefully_) valid instance or throw an exception.

In contrast, Objective-C allows `init*` to return `nil` when an instance cannot be created. This is a common, but not general, pattern used in many of Apple's frameworks. In some other cases an `assert` can happen (and kill the current process).

The generator follow the same `return nil` pattern for generated `init*` methods. If a managed exception is thrown, then it will be printed (using `NSLog`) and `nil` will be returned to the caller.

## Operators

Objective-C does not allow operators to be overloaded as C# does, so these are converted to class selectors.

["Friendly"](/dotnet/standard/design-guidelines/operator-overloads) named methods are generated in preference to the operator overloads when found, and can produce an easier to consume API.

Classes that override the operators `==` and\or `!=` should override the standard Equals (Object) method as well.