using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Bilge;

public class AyarlarManager : MonoBehaviour
{
    public AudioSource ButtonSound;
    public Slider MenuSes;
    public Slider MenuFx;
    public Slider OyunSes;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public List<DilVerileriMain> DilVerileriMain = new List<DilVerileriMain>();
    List<DilVerileriMain> DilOkunanVeriler = new List<DilVerileriMain>();
    public TMP_Text[] TextObjects;
    public TMP_Text DilText;
    public Button[] DilButtons;
    int activeIndex = 0;

    void Start()
    {

        _veriYonetim.Dil_Load();
        DilOkunanVeriler = _veriYonetim.ReturnDilList();
        DilVerileriMain.Add(DilOkunanVeriler[4]);
        LanguageControl();
        ButtonSound.volume = _BellekYonetim.VeriOku_float("MenuFx");

        MenuSes.value = _BellekYonetim.VeriOku_float("MenuSes");
        MenuFx.value = _BellekYonetim.VeriOku_float("MenuFx");
        OyunSes.value = _BellekYonetim.VeriOku_float("OyunSes");
        LanguageControl_Set();
    }
    void LanguageControl()
    {
        if (_BellekYonetim.VeriOku_string("Dil") == "TR")
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = DilVerileriMain[0].DilVerileri_TR[i].metin;
            }
        }
        else
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = DilVerileriMain[0].DilVerileri_EN[i].metin;
            }
        }
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

    void LanguageControl_Set()
    {
        if(_BellekYonetim.VeriOku_string("Dil") == "TR")
        {
            activeIndex = 0;
            DilText.text = "Turkce";
            DilButtons[0].interactable = false;
        }
        else
        {
            activeIndex = 1;
            DilText.text = "English";
            DilButtons[1].interactable = false;
        }
    }

    public void ChangeLanguage(string yon)
    {
        ButtonSound.Play();
        if (yon == "ileri")
        {
            activeIndex = 1;
            DilText.text = "English";
            DilButtons[1].interactable = false;
            DilButtons[0].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "EN");
        }
        else
        {
            activeIndex = 0;
            DilText.text = "Turkce";
            DilButtons[0].interactable = false;
            DilButtons[1].interactable = true;
            _BellekYonetim.VeriKaydet_string("Dil", "TR");
        }
        LanguageControl();


    }
}
