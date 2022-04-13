using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemState
{
    Raw,    // Before furnace
    Ingot,  // After furnace, before hammer
    Shape,  // After hammer, before grindstone
    Blade,  // After grindstone, before contruction
    Sword,  // After construction, final product
    None    // Used for basket/no requirement   
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
    public WoodType woodType;    // Only applicable after construction
    public HiltType hiltType;     //Only applicable after assembly

    public static Sprite[,] MetalSprites;
    public static Sprite[,] HiltSprites;


    public GameObject toGameObject()
    {
        GameObject itemObject = new GameObject("Item Object");  // Represents entire item
        
        SpriteRenderer itemSprite = itemObject.AddComponent<SpriteRenderer>();
        
        if(itemState == ItemState.Sword)
        {
            itemSprite.sprite = HiltSprites[(int)metalType, (int)hiltType];
        }
        else
        {
            itemSprite.sprite = MetalSprites[(int)metalType, (int)itemState];
        }
        
        itemSprite.sortingOrder = 2;

        return itemObject;
    }

    public string toString()
    {
        string itemString;
        if (itemState == ItemState.Raw)
        {
            itemString = itemState.ToString() + " " + metalType.ToString();
        }
        else if(itemState == ItemState.Sword)
        {
            itemString = metalType.ToString() + " " + itemState.ToString() + " with " + hiltType.ToString();
        }
        else
        {
            itemString = metalType.ToString() + " " + itemState.ToString();
        }

        return itemString;
    }

    public bool Equals(InventoryItem other)
    {
        return itemState == other.itemState &&
               metalType == other.metalType &&
               hiltType == other.hiltType;
    }
}
