using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryPanel;

    private ArrayList _inventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        _inventorySlots = new ArrayList(InventoryPanel.GetComponentsInChildren<Transform>().Length - 1);

        foreach (Transform t in InventoryPanel.GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != InventoryPanel)
                _inventorySlots.Add(t.transform);
        }
        Debug.Log("Inventory slots: " + _inventorySlots.Count);
    }

}
