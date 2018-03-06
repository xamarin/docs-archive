---
id: A73F899C-42AC-49B2-93C4-A317FF38EB73
title: "Coloring a CCSprite"
brief: "How to use the CCSprite Color value to color sprites at runtime."
---


# Recipe

The `CCSprite` class contains a `Color` property which can be used to color or *tint* a sprite dynamically.

## Code Example

The following code creates a `CCSprite` grid, as might be used in a block breaking game:

```
CCColor3B[] rowColors = new CCColor3B[]
{
    new CCColor3B(100, 100, 255), // blue
    new CCColor3B(150, 150, 255), // light blue
    new CCColor3B(150, 255, 150), // light green
    new CCColor3B(70, 255, 70), // green
    new CCColor3B(255, 255, 0), // yellow
    new CCColor3B(200, 150, 0), // orange
    new CCColor3B(255, 70, 70), // red
};

for(int row = 0; row < rowColors.Length; row++)
{
    for (int column = 0; column < 10; column++)
    {
        var sprite = new CCSprite("Images/grayscale.png");
        sprite.Color = rowColors[row];
        sprite.PositionY = 170 - row * sprite.ContentSize.Height;
        sprite.PositionX = 20  + column * sprite.ContentSize.Width;

        this.AddChild(sprite);
    }
}
```

The code above produces the following result:

![](Images/coloredgrid.png)

The code above uses the grayscale.png image:

![](Images/grayscale.png)

## The Math Beind Coloring

The `Color` value on a `CCSprite` is applied by multiplying the `CCSprite.Texture` by the `Color` value. For example, assigning a `CCColor` of `new CCColor3B(255, 0, 0)` will remove all green and blue from the `CCSprite`, leaving behind only the red component of each pixel. Since colors cannot be added using the Color property, grayscale images provide the most flexibility for recoloring.
