---
title: "Advanced Integration Topics"
description: "This document describes advanced topics related to Xamarin Workbooks integrations. It discusses the Xamarin.Workbook.Integrations NuGet package and API exposure within a Xamarin Workbook."
ms.prod: xamarin
ms.assetid: 002CE0B1-96CC-4AD7-97B7-43B233EF57A6
author: davidortinau
ms.author: daortin
ms.date: 03/30/2017
---

# Advanced Integration Topics

Integration assemblies should reference
the [`Xamarin.Workbooks.Integrations` NuGet][nuget]. Check out
our [quick-start documentation](~/tools/workbooks/sdk/index.md) for more information about getting
started with the NuGet package.

Client integrations are also supported, and are initiated by placing JavaScript
or CSS files with the same name as the agent integration assembly in the same
directory. For example, if the agent integration assembly (which references the
NuGet) is named `SampleExternalIntegration.dll`, then `SampleExternalIntegration.js`
and `SampleExternalIntegration.css` will be integrated into the client as well if
they exist. Client integrations are optional.

The external integration itself can be packaged as a NuGet, provided and
referenced directly inside the application that is hosting the agent, or simply
placed alongside a `.workbook` file that wishes to consume it.

External integrations (agent and client) in NuGet packages will be automatically
loaded when the package is referenced, as per the quick-start documentation,
while integration assemblies shipped alongside the workbook will need to reference
it as so:

```csharp
#r "SampleExternalIntegration.dll"
```

When referencing an integration this way, it will not be loaded by the client
right away&mdash;you'll need to call some code from the integration to have it
load. We'll be addressing this bug in the future.

The `Xamarin.Interactive` PCL provides a few important integration APIs. Every
integration must at least provide an integration entry point:

```csharp
using Xamarin.Interactive;

[assembly: AgentIntegration (typeof (AgentIntegration))]

class AgentIntegration : IAgentIntegration
{
    const string TAG = nameof (AgentIntegration);

    public void IntegrateWith (IAgent agent)
    {
        // hook into IAgent APIs
    }
}
```

At this point, once the integration assembly is referenced, the client will
implicitly load JavaScript and CSS integration files.

## APIs

As with any assembly that is referenced by a workbook or live inspect session,
any of its public APIs are accessible to the session. Therefore it is
important to have a safe and sensible API surface for users to explore.

The integration assembly is effectively a bridge between an application or
SDK of interest and the session. It can provide new APIs that make sense
specifically in the context of a workbook or live inspect session, or provide
no public APIs and simply perform "behind the scenes" tasks like yielding
object [representations](~/tools/workbooks/sdk/representations.md).

> [!NOTE]
> APIs which must be public but should not be surfaced via IntelliSense
> can be marked with the usual `[EditorBrowsable (EditorBrowsableState.Never)]`
> attribute.

[nuget]: https://nuget.org/packages/Xamarin.Workbooks.Integration
