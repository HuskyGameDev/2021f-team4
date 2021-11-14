using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceScript : MonoBehaviour
{

    public Slider bellow;
    public Slider heat;
    public GameObject complete;
    private float timer;
    public float time;
    public Sprite bellow1;
    public Sprite bellow2;
    public Sprite bellow3;
    public Sprite bellow4;
    public Sprite bellow5;
    public Image bellowImage;
    public GameObject minigame;

    // Start is called before the first frame update
    void Start()
    {
        heat.value = 10;
        bellow.value = (float)22.5;
        bellowImage.sprite = bellow1;
    }

    // Update is called once per frame
    void Update()
    {
        if(heat.value >= 2 && heat.value <= 4)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer >= time)
        {
            GetComponent<Prompt>().promptingInteractable.closePrompt(); 
            //Destroy(minigame);
        }

        heat.value += (float)0.001;
    }

    public void pump()
    {
        if(bellow.value == 0)
        {
            heat.value--;
        }
        if(bellow.value > 16.9)
        {
            bellowImage.sprite = bellow1;
        }
        else if(bellow.value > 13.3)
        {
            bellowImage.sprite = bellow2;
        }
        else if(bellow.value > 9.0)
        {
            bellowImage.sprite = bellow3;
        }
        else if(bellow.value > 1.8)
        {
            bellowImage.sprite = bellow4;
        }
        else
        {
            bellowImage.sprite = bellow5;
        }
    }
}
