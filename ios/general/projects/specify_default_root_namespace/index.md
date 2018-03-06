---
id: F218D026-D3CC-C8C2-B726-C01B8E0E93A9
title: "Specify Default Root Namespace"
brief: "This recipe shows where to set the default namespace for a project."
---

<a name="Recipe" class="injected"></a>


# Recipe
<ide name="xs">
<h2>Set Default Namespace in Visual Studio for Mac</h2>
<ol>
  <li>Double click on the project in the <span class="UIItem">Solution Pad</span>, or right-click on the project and select <span class="UIItem">Options</span>: <br /> <img src="Images/DefaultNamespace1.png" /></li>
  <li>Choose <span class="uiitem">Main Settings</span> to view the <span class="uiitem">Default Namespace</span> field:<img src="Images/DefaultNamespace2.png" /></li>
  <li>This value will be automatically set as the root namespace in each new code file you create in your project, including the <code>.designer.cs</code> partial classes created when you add a XIB or Storyboard file.</li>
</ol>
</ide>
<ide name="vs">
<h2>Set Default Namespace in Visual Studio</h2>
<ol>
  <li>Open the project <span class="uiitem">Properties</span>, and then choose the <span class="uiitem">Application</span> panel: <img src="Images/DefaultNamespace3.png" /></li>
</ol>
</ide>
## Additional Information

If you edit the default namespace value in an existing project,
it will *NOT* update existing files, so beware that editing this value will result in two different root namespaces in your project.

