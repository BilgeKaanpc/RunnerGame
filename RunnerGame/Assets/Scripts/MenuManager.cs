using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Bilge;

public class MenuManager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public List<ItemBilgileri> _Varsayilan_ItemBilgileri = new List<ItemBilgileri>();
    public List<DilVerileriMain> _Varsayilan_DilVerileri = new List<DilVerileriMain>();
    public GameObject quitPanel;
    public AudioSource buttonSound;
    public List<DilVerileriMain> DilVerileriMain = new List<DilVerileriMain>();
    List<DilVerileriMain> DilOkunanVeriler = new List<DilVerileriMain>();
    public TMP_Text[] TextObjects;
    public GameObject loadPanel;
    public Slider LoadSlider;
    // Start is called before the first frame update
    void Start()
    {
        _BellekYonetim.ControlAndCreate();
        _veriYonetim.FileCreate(_Varsayilan_ItemBilgileri, _Varsayilan_DilVerileri);
        buttonSound.volume = PlayerPrefs.GetFloat("MenuFx");

       // PlayerPrefs.SetString("Dil", "TR");
        _veriYonetim.Dil_Load();
        DilOkunanVeriler = _veriYonetim.ReturnDilList();
        DilVerileriMain.Add(DilOkunanVeriler[0]);
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
        StartCoroutine(LoadAsync(_BellekYonetim.VeriOku_int("SonLevel")));
        
    }

    IEnumerator LoadAsync(int level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        loadPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadSlider.value = progress;
            yield return null;
        }
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
