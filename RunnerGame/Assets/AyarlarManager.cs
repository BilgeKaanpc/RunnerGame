using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bilge;

public class AyarlarManager : MonoBehaviour
{
    public AudioSource ButtonSound;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    void Start()
    {

        ButtonSound.volume = _BellekYonetim.VeriOku_float("MenuFx");

        MenuSes.value = _BellekYonetim.VeriOku_float("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_float("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_float("OyunSes");
    }

    public void SesAyarla(string which)
    {
        switch (which)
        {
            case "menuses":
                _BellekYonetim.VeriKaydet_float("MenuSes", MenuSes.value);

                break;

            case "menufx":
                _BellekYonetim.VeriKaydet_float("MenuFx", MenuFx.value);
                break;

            case "oyunses":
                _BellekYonetim.VeriKaydet_float("OyunSes", OyunSes.value);
                break;

            default:
                break;
        }
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
