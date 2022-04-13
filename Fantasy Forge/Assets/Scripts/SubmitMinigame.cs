using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitMinigame : MonoBehaviour
{
    public CustomerMovement waitingCustomer; // Customer whose request is to be fulfilled. First in line?
    
    void Start()
    {
        Prompt prompt = GetComponentInParent<Prompt>();
        Inventory playerInventory = prompt.playerInventory;
        Interactable promptingInteractable = prompt.promptingInteractable;
        Debug.Log("SUBMIT MINIGAME OPENED");

        // Figure out which customer item is being given to. (what waitingCustomer is)

        if (prompt.inputItem.Equals(waitingCustomer.desiredItem))
        {
            // Customer is happy
        }
        else
        {
            // Customer is unhappy
        }

        playerInventory.removeItem(prompt.inputItem);

        promptingInteractable.closePrompt();

    }

    // Update is called once per frame
    void Update()
    {

    }
}

