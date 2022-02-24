using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventoryCapacity;
    public GameObject uiPanel;
    public Sprite itemSlotSprite;
    public float panelWidth;        // Shortcut to calculating size from camera and canvas. In units
    public float slotSize;          // Size of each inventory slot in canvas pixels

    private InventoryItem[] _items;
    private GameObject[]    _itemPanels;
    private int _itemCount;

    // Start is called before the first frame update
    void Start()
    {
        _items = new InventoryItem[inventoryCapacity];
        _itemPanels = new GameObject[inventoryCapacity];
        _itemCount = 0;

        float panelSpacing = panelWidth / (inventoryCapacity + 1);
        int x = 0;

        for (float i = -panelWidth / 2 + panelSpacing; i < panelWidth / 2; i += panelSpacing)
        {
            GameObject itemPanel = new GameObject();
            SpriteRenderer itemPanelSprite = itemPanel.AddComponent<SpriteRenderer>();
            itemPanelSprite.sprite = itemSlotSprite;
            itemPanelSprite.sortingOrder = 1;

            itemPanel.transform.parent = uiPanel.transform;
            itemPanel.transform.position = uiPanel.transform.position + new Vector3(i, 0, 0);
            itemPanel.transform.localScale = new Vector3(slotSize, slotSize, 1);

            _itemPanels[x++] = itemPanel;

        }

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
