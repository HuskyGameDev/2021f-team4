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
    public WoodType woodType;    // Only applicable after construction
    public HiltType hiltType;     //Only applicable after assembly

    public static Sprite[,] MetalSprites;
    public static Sprite[]  HiltSprites;


    public GameObject toGameObject()
    {
        GameObject itemObject = new GameObject("Item Object");  // Represents entire item
        GameObject metalObject = new GameObject("Metal Object"); // Represents metal portion (raw metal, ingot, etc.)

        metalObject.transform.parent = itemObject.transform;
        SpriteRenderer metalSprite = metalObject.AddComponent<SpriteRenderer>();
        metalSprite.sprite = MetalSprites[(int)metalType, (int)itemState];
        metalSprite.sortingOrder = 2;

        if (itemState == ItemState.Sword)
        {
            GameObject hiltObject = new GameObject("Hilt Object"); // Represents hilt

            hiltObject.transform.parent = itemObject.transform;
            SpriteRenderer hiltSprite = hiltObject.AddComponent<SpriteRenderer>();
            hiltSprite.sprite = HiltSprites[(int)hiltType];
            hiltSprite.sortingOrder = 3;
        }

        return itemObject;
    }

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
