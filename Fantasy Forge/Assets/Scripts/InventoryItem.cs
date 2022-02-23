using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState
{
    Raw,    // Before furnace
    Ingot,  // After furnace, before hammer
    Shape,  // After hammer, before grindstone
    Blade,  // After grindstone, before contruction
    Sword   // After construction, final product
}

public enum MetalType
{
    Iron,
    Gold,
    Silver,
    Emerald
}

public enum WoodType
{
    Oak,
    Mahogany,
    Birch
}

public class InventoryItem
{
    public ItemState itemState;
    public MetalType metalType;   // Always applicable
    public WoodType  woodType;    // Only applicable after construction
}
