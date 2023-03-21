using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bilge;

public class MenuManager : MonoBehaviour
{
    BellekYonetim _BellekYonetim = new BellekYonetim();
    public GameObject quitPanel;
    // Start is called before the first frame update
    void Start()
    {
        _BellekYonetim.ControlAndCreate();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Oyna()
    {
        SceneManager.LoadScene(_BellekYonetim.VeriOku_int("SonLevel"));
        
    }
    public void QuitButtonChoose(string durum)
    {
        if (durum == "Evet")
            Application.Quit();
        else if (durum == "cikis")
            quitPanel.SetActive(true);
        else
            quitPanel.SetActive(false);
    }
}
