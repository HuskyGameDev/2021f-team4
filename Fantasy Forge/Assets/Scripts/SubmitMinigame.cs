using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitMinigame : MonoBehaviour
{
    private CustomerMovement _waitingCustomer = null; // Customer whose request is to be fulfilled. First in line?
    
    void Start()
    {
        Prompt prompt = GetComponentInParent<Prompt>();
        Inventory playerInventory = prompt.playerInventory;
        Interactable promptingInteractable = prompt.promptingInteractable;
        Debug.Log("SUBMIT MINIGAME OPENED");

        // Figure out which customer item is being given to. (what waitingCustomer is)
        CustomerMovement[] customers = FindObjectsOfType<CustomerMovement>();
        foreach (CustomerMovement c in customers)
        {
            if (c.inFront)
            {
                _waitingCustomer = c;
                break;
            }
        }

        if (_waitingCustomer != null)
        {
            if (prompt.inputItem.Equals(_waitingCustomer.desiredItem))
            {
                // Customer is happy
                Debug.Log("RIGHT ITEM");
            }
            else
            {
                // Customer is unhappy
                Debug.Log("WRONG ITEM");
            }

            playerInventory.removeItem(prompt.inputItem);

            _waitingCustomer.dismiss();
        }

        promptingInteractable.closePrompt();

    }

    // Update is called once per frame
    void Update()
    {

    }
}

