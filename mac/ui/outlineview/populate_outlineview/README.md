---
id: 166287C4-9CAA-4082-853A-B66C3AA09704
title: "Populate an Outline View"
brief: "This recipe illustrates how to populate an Outline View using a custom NSOutlineViewDataSource from a hierarchal object graph."
dateupdated: 2016-05-31
related:
  - title: "NSOutlineView Class Reference" 
    url: https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Classes/NSOutlineView_Class/Reference/Reference.html
  - title: "NSOutlineViewDataSource Class Reference" 
    url: https://developer.apple.com/library/mac/#documentation/Cocoa/Reference/ApplicationKit/Protocols/NSOutlineViewDataSource_Protocol/Reference/Reference.html#//apple_ref/occ/intf/NSOutlineViewDataSource
  - title: "Outline View Programming Topics" 
    url: https://developer.apple.com/library/mac/#documentation/Cocoa/Conceptual/OutlineView/OutlineView.html#//apple_ref/doc/uid/10000023i
---

# Recipe

Typically, populating an Outline View consists of either sub-classing `NSOutlineViewDataSource`, or manually implementing it's protocol methods. In this recipe, we'll subclass it and utilize a hierarch of Animal objects to populate the outline. The sample code can be found in the Samples link above.

## Animal Class

First, we need to create a backing class for our items. In this example, let's create an Animal class that inherits from `NSObject`, and has a Children property that contains `IList`:

```
public class Animal : NSObject
{
	public string Name { get; set; }

	public Animal () { this.Name = String.Empty; }

	public IList Children
	{
		get { return this.children; }
		set { this.children = value; }
	}
	protected IList children = new List();

	public bool HasChildren
	{
		get { return (this.children.Count > 0); }
	}

	public override string ToString ()
	{
		return this.Name.ToString ();
	}
}
```

## Custom NSOutlineViewDataSource

Next, we need to sub-class the `NSOutlineViewDataSource` class and implement the following methods:

* `GetChildrenCount` - Called when the outline view needs to know how many children an item has. The item is passed as the item parameter and is of type of `NSObject`, so it should be cast to to the type necessary (in this case Animal) and the number of children should be returned.
* `ItemExpandable` - Specifies whether or not the item should be expandable, i.e., whether or not it has children.
* `GetObjectValue` - Called to get the text of the node. In this case, it should return the name of the _Animal_.
* `GetChild` - Called to get the child at the specified index of the passed item. For example, if an Animal had 3 child nodes, it would be called three times, passing 0, 1, and 2 as the index.

Therefore, our custom `NSOutlineViewDataSource` should look something like this:

```
public class AnimalsOutlineDataSource : NSOutlineViewDataSource
{
	// declarations
	protected Animal animalsTree;

	public AnimalsOutlineDataSource (Animal animalsTree) : base()
	{
		this.animalsTree = animalsTree;
	}

	public override int GetChildrenCount (NSOutlineView outlineView, NSObject item)
	{
		Console.Write ("GetChildrenCount called on " + animalsTree.ToString() + ", ");

		// if null, it's asking about the root element
		if (item == null) {
			Console.WriteLine ("root. Returning " + animalsTree.Children.Count);
			return animalsTree.Children.Count;
		} else {
			// get the number of children from the element passed
			Animal passedNode = item as Animal;
			if (passedNode != null) {
				Console.WriteLine (passedNode.ToString() + ". returning " + passedNode.Children.Count);
				return passedNode.Children.Count;
			} else {
				Console.WriteLine ("could not cast, there is a problem here");
				return 0;
			}
		}
	}

	public override bool ItemExpandable (NSOutlineView outlineView, NSObject item)
	{
		Console.WriteLine ("ItemExpandable called on " + animalsTree.ToString());
		// get the number of children from the element passed
		if (item != null) {
			Animal passedNode = item as Animal;
			if (passedNode != null)
				return passedNode.HasChildren;
			else
				return false;
		} else {
			// if null, it's asking about the root element
			return animalsTree.HasChildren;
		}
	}

	public override NSObject GetObjectValue (NSOutlineView outlineView, NSTableColumn forTableColumn, NSObject byItem)
	{
		Console.Write ("GetObjectValue called on " + animalsTree.ToString () + ", ");

		// get the number of children from the element passed
		if (byItem == null) {
			Console.WriteLine ("passed null, returning " + animalsTree.Name);
			return (NSString)animalsTree.Name;
			//return new NSString();
		} else {
			Animal passedNode = byItem as Animal;
			if (passedNode != null) {
				Console.WriteLine ("returning " + passedNode.Name);
				return (NSString)passedNode.Name;
			} else {
				Console.WriteLine ("returning an empty string, cast failed.");
				return new NSString();
			}
		}
	}

	public override NSObject GetChild (NSOutlineView outlineView, int childIndex, NSObject ofItem)
	{
		Console.Write ("GetChild called on " + animalsTree.ToString () + ", ");
		// null means it's asking for the root
		if (ofItem == null) {
			Console.WriteLine ("asked for root, returning " + animalsTree.Children [childIndex].ToString ());
			return animalsTree.Children [childIndex];
		} else {
			Console.WriteLine ("asked for child, returning " + ((ofItem as Animal).Children [childIndex]).ToString() );
			return (NSObject)((ofItem as Animal).Children [childIndex]);
		}
	}
}
```

## Initializing the Data Hierarchy

Next, we need to new up our data:

```
animalTree = new Animal() { Name = "My Animals", Children =
	new List () {
		new Animal () { Name = "Amphibians", Children =
			new List () {
				new Animal () { Name = "Frog" },
				new Animal () { Name = "Amphibious Snake" }
			}
		},
		new Animal () { Name = "Birds", Children =
			new List () {
				new Animal () { Name = "Parrots", Children =
					new List () {
						new Animal () { Name = "Maccaw" },
						new Animal () { Name = "African Gray" }
					}
				},
				new Animal () { Name = "Song Birds", Children =
					new List () {
						new Animal () { Name = "Blue Jay" },
						new Animal () { Name = "Western Goldfinch" }
					}
				},
			}
		},
		new Animal () { Name = "Mammals", Children =
			new List () {
				new Animal () { Name = "Human" },
				new Animal () { Name = "Opossum" },
				new Animal () { Name = "Kangaroo" },
				new Animal () { Name = "Rat" },
				new Animal () { Name = "Gorrila" }
			}
		},
		new Animal () { Name = "Fish", Children =
			new List () {
				new Animal () { Name = "Sea Bass" },
				new Animal () { Name = "Lake Trout" },
				new Animal () { Name = "Bluefin Tuna" },
				new Animal () { Name = "Amberjack Tuna" },
				new Animal () { Name = "Steelhead" },
				new Animal () { Name = "Salmon" },
				new Animal () { Name = "Minnow" }
			}
		}
	}
};
```

## Instantiate and Assign DataSource

Finally, once we've got an object hierarchy, we create our custom datasource and apply it to the Outline View. This would typically be done in `AwakeFromNib`:

```
public override void AwakeFromNib ()
{
	base.AwakeFromNib ();

	this.outlineDataSource = new AnimalsOutlineDataSource (this.animalTree);
	this.MainOutlineView.DataSource = this.outlineDataSource;

}
```

That's it! We should now have a working Outline View:

[ ![](Images/Expanded_OutlineView.png)](Images/Expanded_OutlineView.png)
