using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bilge;

public class MenuManager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    public GameObject quitPanel;
    public AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        _BellekYonetim.ControlAndCreate();
        _veriYonetim.FileCreate(_ItemBilgileri);

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
