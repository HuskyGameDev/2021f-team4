using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public MetalType metalType;
    public Sprite[] basketSpriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = basketSpriteSheet[(int)metalType];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
