---
title: ".NET Embedding"
description: ".NET Embedding allows your existing .NET Code (C#, F#, and others) to be consumed by code written in other programming languages."
ms.prod: xamarin
ms.assetid: 617C38CA-B921-4A76-8DFC-B0A3DF90E48A
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# .NET Embedding

![Preview](~/media/shared/preview.png)

.NET Embedding allows your existing .NET Code (C#,
F#, and others) to be consumed from other programming languages and in
various different environments.

This means that if you have a .NET library that you want to use from
your existing iOS app, you can do that.   Or if you want to link it
with a native C++ library, you can also do that.   Or consume .NET
code from Java.

.NET Embedding is based on the [Embeddinator-4000](https://github.com/mono/Embeddinator-4000) 
open source project.

## Environments and Languages

The tool is both aware of the environment it will use, as well as the
language that will consume it.   For example, the iOS platform does
not allow just-in-time (JIT) compilation, so .NET Embedding will
statically compile your .NET code into native code that can be used in
iOS.  Other environments do allow JIT compilation, and in those
environments, we opt to JIT compile.

It supports various language consumers, so it surfaces .NET code as
idiomatic code in the target language.   This is the list of supported
languages at present:

- [**Objective-C**](objective-c/index.md) – mapping .NET to idiomatic Objective-C APIs
- [**Java**](android/index.md) – mapping .NET to idiomatic Java APIs
- [**C**](get-started/c.md) – mapping .NET to object-oriented like C APIs

More languages will come later.

## Getting Started

To get started, check one of our guides for each of the currently
supported languages:

- [**Objective-C**](get-started/objective-c/index.md) – covers macOS and iOS
- [**Java**](get-started/java/index.md) – covers macOS and Android
- [**C**](get-started/c.md) – covers C language on desktop platforms

## Related Links

- [Embeddinator-4000 on GitHub](https://github.com/mono/Embeddinator-4000)
