using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyScript : MonoBehaviour
{

    private Image hilt;
    private static Sprite[] hilts;
    public Sprite hilt1;
    public Sprite hilt2;
    public Sprite hilt3;
    public Sprite hilt4;
    public GameObject hiltObject;
<<<<<<< Updated upstream
    public int index = 0;
    //public GameObject outHilt;
=======
    public GameObject swordObject;
    private int index = 0;

    private InventoryItem _inputItem;
    public Sprite ironSword;
    public Sprite goldSword;
    public Sprite silverSword;
    public Sprite emeraldSword;
    private int signal = 0;
    private CustomerMovement customerMovement;
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        customerMovement  = GameObject.FindObjectOfType<CustomerMovement>();
        hilt = hiltObject.GetComponent<Image>();
        hilts = new Sprite[4] { hilt1, hilt2, hilt3, hilt4 };
        hilt.sprite = hilts[0];
<<<<<<< Updated upstream
=======



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
>>>>>>> Stashed changes
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
            index = 3;
        }
        hilt.sprite = hilts[index];
    }

    public void rightButton()
    {
        index++;
        if(index > 3)
        {
            index = 0;
        }
        hilt.sprite = hilts[index];
    }

    public void choose()
    {
<<<<<<< Updated upstream
        //SpriteRenderer outSprite = outHilt.GetComponent<SpriteRenderer>();
        //outSprite.sprite = hilts[index];
=======
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
>>>>>>> Stashed changes
        GetComponent<Prompt>().promptingInteractable.closePrompt();
        signal = 1;
        customerMovement.complete = signal;
    }
}