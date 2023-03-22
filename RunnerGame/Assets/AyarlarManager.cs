using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AyarlarManager : MonoBehaviour
{
    public AudioSource ButtonSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void ChangeLanguage()
    {
        ButtonSound.Play();
    }
}