---
title: "Sample Integrations"
description: "This document links to samples that demonstrate Xamarin Workbooks integrations. Linked samples work with representation rendering and SkiaSharp."
ms.prod: xamarin
ms.assetid: 327DAD2E-1F76-4EB5-BCD0-9E7384D99E48
author: davidortinau
ms.author: daortin
ms.date: 03/30/2017
---

# Sample Integrations

See the [Kitchen Sink][KitchenSink] sample for a working example of an
integration. Simply build `KitchenSink.sln` in Visual Studio for Mac or
Visual Studio and then open `KitchenSink.workbook`.

[![Kitchen Sink Integration Screenshot](samples-images/kitchensinkintegrationscreenshot.png)](samples-images/kitchensinkintegrationscreenshot.png#lightbox)

The Kitchen Sink sample demonstrates both sets of concepts:

* The representation pieces demonstrate how to use `RepresentationManager` to
  enhance rendering by using the built-in representations.
* The `Person` object and its associated JavaScript renderer demonstrate using
  `ISerializableObject` without going through a representation provider.

Also see [SkiaSharp][skiasharp] for a real-world example of an integration
that uses the existing [representations](~/tools/workbooks/sdk/representations.md) provided by Xamarin Workbooks to render
its types.

[KitchenSink]: https://github.com/xamarin/Workbooks/tree/master/SDK/Samples/KitchenSink
[skiasharp]: https://github.com/mono/SkiaSharp/tree/master/source/SkiaSharp.Workbooks
