---
title: "Getting started with C"
description: "This document describes how to use .NET Embedding to embed .NET code in a C application. It discusses how to use .NET Embedding in both Visual Studio 2019 and Visual Studio for Mac."
ms.prod: xamarin
ms.assetid: 2A27BE0F-95FB-4C3A-8A43-72540179AA85
author: davidortinau
ms.author: daortin
ms.date: 04/19/2018
---

# Getting started with C

## Requirements

To use .NET Embedding with C, you'll need a Mac or Windows machine running:

### macOS

* macOS 10.12 (Sierra) or later
* Xcode 8.3.2 or later
* [Mono](https://www.mono-project.com/download/)

### Windows

* Windows 7, 8, 10 or later
* Visual Studio 2015 or later

## Installing .NET Embedding from NuGet

Follow these [instructions](~/tools/dotnet-embedding/get-started/install/install.md) to install and configure .NET Embedding for your project.

The command invocation you should configure will look like (possibly with different version numbers and paths):

### Visual Studio for Mac

```shell
mono {SolutionDir}/packages/Embeddinator-4000.0.4.0.0/tools/Embeddinator-4000.exe --gen=c --outdir=managed_c --platform=macos --compile managed.dll
```

### Visual Studio 2017

```shell
$(SolutionDir)\packages\Embeddinator-4000.0.2.0.80\tools\Embeddinator-4000.exe --gen=c --outdir=managed_c --platform=windows --compile managed.dll
```

## Generation

### Output files

If all goes well, you will be presented with the following output:

```shell
Parsing assemblies...
    Parsed 'managed.dll'
Processing assemblies...
Generating binding code...
    Generated: managed.h
    Generated: managed.c
    Generated: mscorlib.h
    Generated: mscorlib.c
    Generated: embeddinator.h
    Generated: glib.c
    Generated: glib.h
    Generated: mono-support.h
    Generated: mono_embeddinator.c
    Generated: mono_embeddinator.h
```

Since the `--compile` flag was passed to the tool, .NET Embedding should also have compiled the output files into a shared library, which you can find next to the generated files, a **libmanaged.dylib** file on macOS, and **managed.dll** on Windows.

To consume the shared library, you can include the **managed.h** C header file, which provides the C declarations corresponding to the respective managed library APIs and link with the previously mentioned compiled shared library.

## Further Reading

* [.NET Embedding Limitations](~/tools/dotnet-embedding/limitations.md)
* [Contributing to the open source project](https://github.com/mono/Embeddinator-4000/blob/master/Contributing.md)
* [Error codes and descriptions](~/tools/dotnet-embedding/errors.md)
