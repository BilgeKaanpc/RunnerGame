using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bilge;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    BellekYonetim _BellekYonetim = new BellekYonetim();
    public Sprite Lock;

    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public AudioSource ButtonSound;
    public List<DilVerileriMain> DilVerileriMain = new List<DilVerileriMain>();
    List<DilVerileriMain> DilOkunanVeriler = new List<DilVerileriMain>();
    public TMP_Text TextObjects;
    void Start()
    {

        _veriYonetim.Dil_Load();
        DilOkunanVeriler = _veriYonetim.ReturnDilList();
        DilVerileriMain.Add(DilOkunanVeriler[2]);
        LanguageControl();
        ButtonSound.volume = PlayerPrefs.GetFloat("MenuFx");
        int mevcutLevel = _BellekYonetim.VeriOku_int("SonLevel") - 4;
        int index = 1;
        for(int i = 0; i < Buttons.Length; i++)
        {
            if(i + 1 <= mevcutLevel)
            {
                Buttons[i].GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
                int SceneIndex = index + 4;
                Buttons[i].onClick.AddListener(delegate { SahneYukle(SceneIndex); });
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = Lock;
                Buttons[i].enabled = false;
            }
            index++;
        }
    }
    void LanguageControl()
    {
        if (_BellekYonetim.VeriOku_string("Dil") == "TR")
        {
            TextObjects.text = DilVerileriMain[0].DilVerileri_TR[0].metin;
        }
        else
        {
            TextObjects.text = DilVerileriMain[0].DilVerileri_EN[0].metin;
        }
    }
    public void SahneYukle(int index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(index);
        //SceneManager.LoadScene(int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text) + 4);
    }
    public void BackToMenu()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }
}
