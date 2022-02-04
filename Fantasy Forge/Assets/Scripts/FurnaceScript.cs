using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text timerText;
    public GameObject visualTimer;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        heat.value = 20;
        bellow.value = (float)22.5;
        bellowImage.sprite = bellow1;
        StartCoroutine(IngotDrop(dropDuration));
        minigameTiming = minigameDuration;
        timerText = visualTimer.GetComponent<Text>();
        hand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(heat.value >= 6 && heat.value <= 10)
        {
            timer += Time.deltaTime;
            animator.SetInteger("fire", 2);
        }
        else if(heat.value <= 1)
        {
            GetComponent<Prompt>().promptingInteractable.closePrompt();
        }
        else if(heat.value > 10)
        {
            timer = 0;
            animator.SetInteger("fire", 1);
        }
        else
        {
            timer = 0;
            animator.SetInteger("fire", 3);
        }

        if(timer >= time)
        {
            GetComponent<Prompt>().promptingInteractable.closePrompt(); 
        }

        heat.value += (float)0.005;

        if(timingFlag)
        {
            minigameTiming -= Time.deltaTime;
            timerText.text = minigameTiming.ToString("0.0");
        }
        if (minigameTiming <= 0)
        {
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
        visualTimer.SetActive(true);
        hand.SetActive(true);
    }
}
