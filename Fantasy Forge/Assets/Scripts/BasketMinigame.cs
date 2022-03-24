using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMinigame : MonoBehaviour
{
    public SpriteRenderer basketSprite;
    public MetalType metalType;
    void Start()
    {
        Inventory playerInventory = GetComponentInParent<Prompt>().playerInventory;
        InventoryItem testItem3 = new InventoryItem();
        testItem3.itemState = ItemState.Raw;
        testItem3.metalType = metalType;
        playerInventory.addItem(testItem3);
        GetComponentInParent<Prompt>().promptingInteractable.closePrompt();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
