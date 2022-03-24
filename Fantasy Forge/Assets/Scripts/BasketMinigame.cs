using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketMinigame : MonoBehaviour
{
    public  SpriteRenderer basketSprite;

    private MetalType      _metalType;

    void Start()
    {
        Inventory    playerInventory       = GetComponentInParent<Prompt>().playerInventory;
        Interactable promptingInteractable = GetComponentInParent<Prompt>().promptingInteractable;

        if (!playerInventory.isFull())
        {
            _metalType = promptingInteractable.gameObject.GetComponent<Basket>().metalType;

            InventoryItem newItem = new InventoryItem();
            newItem.itemState = ItemState.Raw;
            newItem.metalType = _metalType;
            playerInventory.addItem(newItem);
        }

        promptingInteractable.closePrompt();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
