using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour
{

    private Image hilt;
    private Image ghost1;
    private Image ghost2;
    private Image sword;
    private static Sprite[] hilts;
    public Sprite hilt1;
    public Sprite hilt2;
    public Sprite hilt3;
    public GameObject hiltObject;
    public GameObject phantom1;
    public GameObject phantom2;
    public GameObject swordObject;
    private int index = 0;

    private InventoryItem _inputItem;
    public Sprite ironSword;
    public Sprite goldSword;
    public Sprite silverSword;
    public Sprite emeraldSword;
    //private int signal = 0;  //Line caused NullReference
    //CustomerMovement customerMovement; //Line caused NullReference

    // Start is called before the first frame update
    void Start()
    {
        hilt = hiltObject.GetComponent<Image>();
        ghost1 = phantom1.GetComponent<Image>(); 
        ghost2 = phantom2.GetComponent<Image>();
        sword = swordObject.GetComponent<Image>();
        hilts = new Sprite[3] {hilt1, hilt2, hilt3};
        hilt.sprite = hilts[0];

        _inputItem = GetComponent<Prompt>().inputItem;
        Debug.Log("Input to assembly is " + _inputItem.itemState + " " + _inputItem.metalType);

        if (_inputItem.metalType == MetalType.Iron)
        {
            sword.sprite = ironSword;
        }
        else if (_inputItem.metalType == MetalType.Gold)
        {
            sword.sprite = goldSword;
        }
        else if (_inputItem.metalType == MetalType.Silver)
        {
            sword.sprite = silverSword;
        }
        else if (_inputItem.metalType == MetalType.Emerald)
        {
            sword.sprite = emeraldSword;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leftButton()
    {
        index--;
        if(index < 0 )
        {
            index = 2;
        }
        hilt.sprite = hilts[index];
        
        if(index == 0)
        {
            ghost1.sprite = hilts[2];
            ghost2.sprite = hilts[index + 1];
        }
        else if(index == 2)
        {
            ghost1.sprite = hilts[index - 1];
            ghost2.sprite = hilts[0];
        }
        else
        {
            ghost1.sprite = hilts[index - 1];
            ghost2.sprite = hilts[index + 1];
        }
    }

    public void rightButton()
    {
        index++;
        if(index > 2)
        {
            index = 0;
        }
        hilt.sprite = hilts[index];

        if (index == 0)
        {
            ghost1.sprite = hilts[2];
            ghost2.sprite = hilts[index + 1];
        }
        else if (index == 2)
        {
            ghost1.sprite = hilts[index - 1];
            ghost2.sprite = hilts[0];
        }
        else
        {
            ghost1.sprite = hilts[index - 1];
            ghost2.sprite = hilts[index + 1];
        }
    }

    public void choose()
    {
        _inputItem.itemState = ItemState.Sword;
        if(index == 0)
        {
            _inputItem.hiltType = HiltType.Hilt1;
        }
        else if (index == 1)
        {
            _inputItem.hiltType = HiltType.Hilt2;
        }
        else if (index == 2)
        {
            _inputItem.hiltType = HiltType.Hilt3;
        }
        Debug.Log("Output from assembly is " + _inputItem.itemState + " " + _inputItem.metalType + " " + _inputItem.hiltType);
        GetComponent<Prompt>().promptingInteractable.closePrompt();
        //signal = 1;  //Line caused NullReference
        //customerMovement.complete = signal;  //Line Caused NullReference
    }
}
