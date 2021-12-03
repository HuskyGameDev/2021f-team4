using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitKey : MonoBehaviour
{
    public KeyCode quitKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(quitKey))
        {
            Application.Quit();
        }
    }
}
