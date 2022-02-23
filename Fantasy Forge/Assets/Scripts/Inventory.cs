using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventoryCapacity;

    private InventoryItem[] _items;
    private int _itemCount;

    // Start is called before the first frame update
    void Start()
    {
        _items = new InventoryItem[inventoryCapacity];
        _itemCount = 0;

        // DEBUG
        InventoryItem testItem1 = new InventoryItem();
        testItem1.itemState = ItemState.Ingot;
        testItem1.metalType = MetalType.Gold;
        InventoryItem testItem2 = new InventoryItem();
        testItem2.itemState = ItemState.Shape;
        testItem2.metalType = MetalType.Iron;

        addItem(testItem1);
        addItem(testItem2);
    }

    public bool addItem(InventoryItem item)
    {
        if (_itemCount == inventoryCapacity)
            return false;

        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (_items[i] == null)
            {
                _items[i] = item;
                _itemCount++;
                return true;
            }
        }

        return false;
    }

    public InventoryItem removeItem(int index)
    {
        if (_items[index] != null && 0 <= index && index < inventoryCapacity)
        {
            InventoryItem returnItem = _items[index];
            _items[index] = null;
            _itemCount--;
            return returnItem;
        }

        return null;
    }

    public InventoryItem removeItem(InventoryItem item)
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (_items[i] == item)
                return removeItem(i);
        }

        return null;
    }

    public bool hasItem(ItemState state)
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (_items[i] != null)
                if (_items[i].itemState == state)
                    return true;
        }

        return false;
    }

    // Return item in inventory with matching state. Currently selects first occurance. Change later to only choose held item, or start search at cursor position?
    public InventoryItem getItem(ItemState state)
    {
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (_items[i] != null)
                if (_items[i].itemState == state)
                    return _items[i];
        }

        return null;
    }

}
