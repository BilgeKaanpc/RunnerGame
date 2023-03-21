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
    void Start()
    {

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

    public void SahneYukle(int index)
    {
        SceneManager.LoadScene(index);
        //SceneManager.LoadScene(int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text) + 4);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
