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

public enum HiltType
{
    Hilt1,
    Hilt2,
    Hilt3
}

public class InventoryItem
{
    public ItemState itemState;
    public MetalType metalType;   // Always applicable
    public WoodType  woodType;    // Only applicable after construction
    public HiltType hiltType;     //Only applicable after assembly

    public string toString()
    {
        string itemString;
        if (itemState == ItemState.Raw)
        {
            itemString = itemState.ToString() + " " + metalType.ToString();
        }
        else
        {
            itemString = metalType.ToString() + " " + itemState.ToString();
        }
        
        return itemString;
    }
}
