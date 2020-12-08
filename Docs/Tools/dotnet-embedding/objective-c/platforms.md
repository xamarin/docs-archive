---
title: "Objective-C Platforms"
description: "This document describes the various platforms that .NET Embedding can target when working with Objective-C code. It discusses macOS, iOS, tvOS, and watchOS."
ms.prod: xamarin
ms.assetid: 43253BE4-A03A-4646-9A14-32C05174E672
author: davidortinau
ms.author: daortin
ms.date: 11/14/2017
---

# Objective-C Platforms

.NET Embedding can target various platforms when generating Objective-C code:

* macOS
* iOS
* tvOS
* watchOS [not implemented yet]

The platform is selected by passing the `--platform=<platform>` command-line
argument to .NET Embedding.

When building for the iOS, tvOS and watchOS platforms, .NET Embedding will
always create a framework that embeds Xamarin.iOS, since Xamarin.iOS contains
a lot of runtime support code which is required on these platforms.

However, when building for the macOS platform, it's possible to choose whether
the generated framework should embed Xamarin.Mac or not. It's possible to not
embed Xamarin.Mac if the bound assembly does not reference Xamarin.Mac.dll
(either directly or indirectly), and this is selected by passing
`--platform=macOS` to the .NET Embedding tool.

If the bound assembly contains a reference to Xamarin.Mac.dll, it's necessary
to embed Xamarin.Mac, and additionally the embeddinator must know which target
framework to use.

There are three possible Xamarin.Mac target frameworks: `modern` (previously
called `mobile`), `full` and `system` (the difference between each is
described in Xamarin.Mac's [target framework][1] documentation), and each is
selected by passing `--platform=macOS-modern`, `--platform=macOS-full` or
`--platform=macOS-system` to the .NET Embedding tool.

[1]: ~/mac/platform/target-framework.md
