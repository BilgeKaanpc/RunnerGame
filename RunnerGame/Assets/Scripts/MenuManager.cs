using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Bilge;

public class MenuManager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public GameObject quitPanel;
    public AudioSource buttonSound;
    public List<DilVerileriMain> DilVerileriMain = new List<DilVerileriMain>();
    public TMP_Text[] TextObjects;
    // Start is called before the first frame update
    void Start()
    {
        _BellekYonetim.ControlAndCreate();
        _veriYonetim.FileCreate(_ItemBilgileri);
        buttonSound.volume = PlayerPrefs.GetFloat("MenuFx");

        PlayerPrefs.SetString("Dil", "TR");
        LanguageControl();
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

    public void LoadScene(int index)
    {
        buttonSound.Play();
        SceneManager.LoadScene(index);
    }
    public void Oyna()
    {
        buttonSound.Play();
        SceneManager.LoadScene(_BellekYonetim.VeriOku_int("SonLevel"));
        
    }
    public void QuitButtonChoose(string durum)
    {
        buttonSound.Play();
        if (durum == "Evet")
            Application.Quit();
        else if (durum == "cikis")
            quitPanel.SetActive(true);
        else
            quitPanel.SetActive(false);
    }
}
