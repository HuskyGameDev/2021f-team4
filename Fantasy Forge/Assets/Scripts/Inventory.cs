using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int inventoryCapacity;
    public GameObject uiPanel;
    public Sprite itemSlotSprite;
    public float panelWidth;        // Shortcut to calculating size from camera and canvas. In units
    public float slotSize;          // Size of each inventory slot in canvas pixels

    private Sprite[]  _itemSpriteSheet;

    private InventoryItem[] _items;
    private GameObject[]    _itemPanels;
    private int _itemCount;

    // Start is called before the first frame update
    void Start()
    {
        _itemSpriteSheet = Resources.LoadAll<Sprite>("Sprites/item_spritesheet");

        InventoryItem.MetalSprites = new Sprite[4, 5]
        {
            {   _itemSpriteSheet[0],
                _itemSpriteSheet[13],
                _itemSpriteSheet[36],
                _itemSpriteSheet[27],
                _itemSpriteSheet[27]},
            {   _itemSpriteSheet[1],
                _itemSpriteSheet[14],
                _itemSpriteSheet[37],
                _itemSpriteSheet[28],
                _itemSpriteSheet[28]},
            {   _itemSpriteSheet[2],
                _itemSpriteSheet[16],
                _itemSpriteSheet[38],
                _itemSpriteSheet[29],
                _itemSpriteSheet[29]},
            {   _itemSpriteSheet[3],
                _itemSpriteSheet[16],
                _itemSpriteSheet[39],
                _itemSpriteSheet[30],
                _itemSpriteSheet[30]}
        };

        InventoryItem.HiltSprites = new Sprite[3]
        {
            _itemSpriteSheet[22], _itemSpriteSheet[23], _itemSpriteSheet[26]
        };

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

        /*
        // DEBUG
        InventoryItem testItem1 = new InventoryItem();
        testItem1.itemState = ItemState.Raw;
        testItem1.metalType = MetalType.Gold;
        InventoryItem testItem2 = new InventoryItem();
        testItem2.itemState = ItemState.Blade;
        testItem2.metalType = MetalType.Iron;
        InventoryItem testItem3 = new InventoryItem();
        testItem3.itemState = ItemState.Blade;
        testItem3.metalType = MetalType.Emerald;

        addItem(testItem1);
        addItem(testItem2);
        addItem(testItem3);
        */
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
                updateUI();
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
            updateUI();
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

    public void updateUI()
    {
        // Fill inventory slots with proper items
        for (int i = 0; i < inventoryCapacity; i++)
        {
            if (_items[i] != null)
            {
                // Remove old item object from slot
                for (int j = 0; j < _itemPanels[i].transform.childCount; j++)
                    Destroy(_itemPanels[i].transform.GetChild(j).gameObject);

                // Recreate item object and position properlty
                GameObject itemObject = _items[i].toGameObject();
                itemObject.transform.parent     = _itemPanels[i].transform;
                itemObject.transform.position   = _itemPanels[i].transform.position;
                itemObject.transform.localScale = Vector3.one;
            }
        }
    }
    
    public bool isFull()
    {
        return _itemCount == inventoryCapacity;
    }

    /*
    public GameObject toGameObject(InventoryItem i)
    {
        GameObject itemObject  = new GameObject("Item Object");  // Represents entire item
        GameObject metalObject = new GameObject("Metal Object"); // Represents metal portion (raw metal, ingot, etc.)

        metalObject.transform.parent = itemObject.transform;
        SpriteRenderer metalSprite = metalObject.AddComponent<SpriteRenderer>();
        metalSprite.sprite = _metalSprites[(int)i.metalType,(int)i.itemState];
        metalSprite.sortingOrder = 2;

        if (i.itemState == ItemState.Sword)
        {
            GameObject hiltObject = new GameObject("Hilt Object"); // Represents hilt

            hiltObject.transform.parent = itemObject.transform;
            SpriteRenderer hiltSprite = hiltObject.AddComponent<SpriteRenderer>();
            hiltSprite.sprite = _hiltSprites[(int)i.hiltType];
            hiltSprite.sortingOrder = 3;
        }

        return itemObject;
    }*/
    
}
