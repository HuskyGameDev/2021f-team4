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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(heat.value >= 5 && heat.value <= 8)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if(timer >= time)
        {
            complete.SetActive(true);
        }

        heat.value -= (float)0.001;
    }

    public void pump()
    {
        if(bellow.value == 1)
        {
            heat.value++;
        }
    }
}
