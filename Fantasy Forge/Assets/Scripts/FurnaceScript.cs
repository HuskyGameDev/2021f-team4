using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FurnaceScript : MonoBehaviour
{

    public Slider bellow;
    public Slider heat;
    private float timer;
    public float time;
    public Sprite bellow1;
    public Sprite bellow2;
    public Sprite bellow3;
    public Sprite bellow4;
    public Sprite bellow5;
    public Image bellowImage;
    public Animator animator;
    public GameObject drop;
    public float dropDuration;
    private bool pumpFlag = true;
    public float minigameDuration;
    private bool timingFlag = false;
    private float minigameTiming;
    public TMP_Text timerText1;
    public TMP_Text timerText2;
    public GameObject visualTimer1;
    public GameObject visualTimer2;
    public GameObject instructions;
    public GameObject hand;
    public Animator dropAnim;

    private InventoryItem _inputItem;
    private Inventory _playerInventory; // Inventory attached to palyer object

    // Start is called before the first frame update
    void Start()
    {
        heat.value = 20;
        bellow.value = (float)22.5;
        bellowImage.sprite = bellow1;

        _inputItem = GetComponent<Prompt>().inputItem;

        if (_inputItem.metalType == MetalType.Iron)
        {
            dropAnim.SetInteger("MetalType", 0);
        }
        else if (_inputItem.metalType == MetalType.Gold)
        {
            dropAnim.SetInteger("MetalType", 1);
        }
        else if (_inputItem.metalType == MetalType.Silver)
        {
            dropAnim.SetInteger("MetalType", 2);
        }
        else if (_inputItem.metalType == MetalType.Emerald)
        {
            dropAnim.SetInteger("MetalType", 3);
        }

        StartCoroutine(IngotDrop(dropDuration));

        minigameTiming = minigameDuration;
        hand.SetActive(false);
        instructions.SetActive(false);
        visualTimer1.SetActive(false);
        visualTimer2.SetActive(false);

        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        Debug.Log("Input to furnace is " + _inputItem.itemState + " " + _inputItem.metalType);
    }

    // Update is called once per frame
    void Update()
    {
        if(heat.value >= 6 && heat.value <= 10)
        {
            timer += Time.deltaTime;
            animator.SetInteger("fire", 2);
            timerText2.text = timer.ToString("0.0");
        }
        else if(heat.value <= 1)
        {
            _playerInventory.removeItem(_inputItem);
            GetComponent<Prompt>().promptingInteractable.closePrompt();
        }
        else if(heat.value > 10)
        {
            timer = 0;
            animator.SetInteger("fire", 1);
            timerText2.text = timer.ToString("0.0");
        }
        else
        {
            timer = 0;
            animator.SetInteger("fire", 3);
            timerText2.text = timer.ToString("0.0");
        }

        if(timer >= time)
        {
            // Convert input item from raw to ingot
            _inputItem.itemState = ItemState.Ingot;
            Debug.Log("Output from furnace is " + _inputItem.itemState + " " + _inputItem.metalType);

            GetComponent<Prompt>().promptingInteractable.closePrompt(); 
        }

        heat.value += (float)0.005;

        if(timingFlag)
        {
            minigameTiming -= Time.deltaTime;
            timerText1.text = minigameTiming.ToString("0.0");
        }
        if (minigameTiming <= 0)
        {
            _playerInventory.removeItem(_inputItem);
            GetComponent<Prompt>().promptingInteractable.closePrompt();
        }
    }

    public void pump()
    {
        if(bellow.value > 16.9)
        {
            bellowImage.sprite = bellow1;
            pumpFlag = true;
        }
        else if(bellow.value > 13.3)
        {
            bellowImage.sprite = bellow2;
            pumpFlag = true;
        }
        else if(bellow.value > 9.0)
        {
            bellowImage.sprite = bellow3;
            pumpFlag = true;
        }
        else if(bellow.value > 1.8)
        {
            bellowImage.sprite = bellow4;
            pumpFlag = true;
        }
        else
        {
            bellowImage.sprite = bellow5;
            if (pumpFlag)
            {
                heat.value-=2;
                pumpFlag = false;
            }
        }
    }

    IEnumerator IngotDrop(float duration)
    {
        yield return new WaitForSeconds(duration);

        drop.SetActive(false);
        timingFlag = true;
        visualTimer1.SetActive(true);
        visualTimer2.SetActive(true);
        hand.SetActive(true);
        instructions.SetActive(true);
    }
}
