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
    public int index = 0;
    //public GameObject outHilt;

    // Start is called before the first frame update
    void Start()
    {
        hilt = hiltObject.GetComponent<Image>();
        hilts = new Sprite[4] { hilt1, hilt2, hilt3, hilt4 };
        hilt.sprite = hilts[0];
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
        //SpriteRenderer outSprite = outHilt.GetComponent<SpriteRenderer>();
        //outSprite.sprite = hilts[index];
        GetComponent<Prompt>().promptingInteractable.closePrompt();
    }
}
