using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static AudioClip customerEnter;
    static AudioSource audioSrc;

    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        customerEnter = Resources.Load<AudioClip>("Bell Walk 2");
        audioSrc = GetComponent<AudioSource>();

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
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

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
