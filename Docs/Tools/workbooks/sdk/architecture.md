---
title: "Architecture Overview"
description: "This document describes the architecture of Xamarin Workbooks, examining how the interactive agent and interactive client work together."
ms.prod: xamarin
ms.assetid: 6C0226BE-A0C4-4108-B482-0A903696AB04
author: davidortinau
ms.author: daortin
ms.date: 03/30/2017
---

# Architecture Overview

Xamarin Workbooks features two main components which must work in conjunction
with each other: _Agent_ and _Client_.

## Interactive Agent

The Agent component is a small platform-specific assembly which runs in the
context of a .NET application.

Xamarin Workbooks provides pre-built "empty" applications for a number of
platforms, such as iOS, Android, Mac, and WPF. These applications explicitly
host the agent.

During live inspection (Xamarin Inspector), the agent is injected via the
IDE debugger into an existing application as part of the regular development &
debugging workflow.

## Interactive Client

The client is a native shell (Cocoa on Mac, WPF on Windows) that hosts a web
browser surface for presenting the workbook/REPL interface. From an SDK
perspective, all client integrations are implemented in JavaScript and CSS.

The client is responsible (via Roslyn) for compiling source code into small
assemblies and sending them over to the connected agent for execution. Results
of execution are sent back to the client for rendering. Each cell in a workbook
yields one assembly which references the assembly of the previous cell.

Because an agent can be running on any type of .NET platform and has access to
anything in the running application, care must be taken to serialize results in
a platform-agnostic manner.
