using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static AudioClip customerEnter;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        customerEnter = Resources.Load<AudioClip>("Bell Walk 2");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "Bell Walk 2":
                audioSrc.PlayOneShot(customerEnter);
                break;
        }
    }
}
