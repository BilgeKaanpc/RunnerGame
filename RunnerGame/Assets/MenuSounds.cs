using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    private static GameObject instance;
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.volume = PlayerPrefs.GetFloat("MenuSes");
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("MenuSes");
    }
}
