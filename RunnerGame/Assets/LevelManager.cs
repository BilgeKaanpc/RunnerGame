using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Start()
    {

        _BellekYonetim.VeriKaydet_int("SonLevel", Level);
        int mevcutLevel = _BellekYonetim.VeriOku_int("SonLevel") - 4;
        for(int i = 0; i < Buttons.Length; i++)
        {
            if(i + 1 <= mevcutLevel)
            {
                Buttons[i].GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
                int index = i + 1;
            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = Lock;
            }
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
