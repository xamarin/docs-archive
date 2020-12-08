---
title: "Xamarin Workbooks Editor Keyboard Shortcuts"
description: "This document describes keyboard shortcuts available for use in the Xamarin Workbooks editor. In particular, it looks at various ways the Return key is used."
ms.prod: xamarin
ms.assetid: 6375A371-3215-4A7C-B97B-A19E58BE96D6
author: davidortinau
ms.author: daortin
ms.date: 03/30/2017
---

# Xamarin Workbooks Editor Keyboard Shortcuts

## The Return Key, and its nuances

The following table describes the various key bindings for executing code
and authoring markdown. We have taken great care to choose sensible and
consistent key bindings that are both familiar and fluid.

|Key Binding|Code Cell|Markdown Cell|
|--- |--- |--- |
|<kbd>Return</kbd>|<p>If the caret is at the end of the cell buffer and the cell can be successfully parsed, it will be executed and results will be displayed below the buffer, and a new code cell will be inserted and focused cell after the executed cell.</p><p>If parsing is not successful, a new line will be inserted into the buffer. Compiler diagnostics will not be produced if parsing is not successful.</p>|<p><kbd>Return</kbd> exhibits different behavior depending on the Markdown context at the caret.</p><ul><li>If the caret is in a Markdown code block, a literal new line is inserted.</li><li>If the caret is in a Markdown list block, create a new list item or split the current list item.</li><li>If the caret is in any other type of Markdown block, create a new paragraph block or split the current block.</li></ul>|
|<dl><dt>Mac</dt><dd><kbd>Command‑Return</kbd></dd><dt>Win</dt><dd><kbd>Control‑Return</kbd></dd></dl>|<p>Always attempts to parse and execute the cell contents. If compilation is successful, results (including execution exceptions) will be displayed below the buffer, and if there are no subsequent cells, a new one will be created and focused.</p><p>If there are any compilation errors, diagnostics will be displayed and the buffer will remain focused with the caret position unchanged.</p>|Inserts and focuses a new code cell after the current markdown cell.|
|<dl><dt>Mac</dt><dd><kbd>Command‑Shift‑Return</kbd><dd><dt>Win</dt><dd><kbd>Control‑Shift‑Return</kbd></dd></dl>|Inserts and focuses a new markdown cell after the current cell.|Same behavior as <kbd>Return</kbd>|
|<kbd>Shift‑Return</kbd>|Always inserts a new line, regardless of caret location or content.|Inserts a hard line break within the current Markdown block.|
