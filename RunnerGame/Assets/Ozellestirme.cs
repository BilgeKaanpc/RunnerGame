using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bilge;
using TMPro;

public class Ozellestirme : MonoBehaviour
{
    public TMP_Text puanText;
    public TMP_Text HatText;
    [Header("Sapkalar")]
    public GameObject[] Hats;
    public Button[] SapkaButtons;
    [Header("Sopalar")]
    public GameObject[] Weapons;
    [Header("Materialler")]
    public GameObject[] Materials;
    BellekYonetim _BellekYonetimi = new BellekYonetim();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    VeriYonetimi _veriYonetim = new VeriYonetimi();

    int SapkaIndex = -1;

    void Start()
    {
        _BellekYonetimi.VeriKaydet_int("AktifSapka",-1);

        if(_BellekYonetimi.VeriOku_int("AktifSapka") == -1)
        {
            foreach(var item in Hats)
            {
                item.SetActive(false);
            }
            SapkaIndex = -1;
            HatText.text = "Sapka Yok";
        }
        else
        {
            SapkaIndex = _BellekYonetimi.VeriOku_int("AktifSapka");
            Hats[SapkaIndex].SetActive(true);
        }
        _veriYonetim.Load();
        _ItemBilgileri = _veriYonetim.ReturnList();

    }

    

    public void SapkaYonButton(string islem)
    {
        if(islem == "ileri")
        {
            if(SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Hats[SapkaIndex].SetActive(true);
                HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
            }
            else
            {
                Hats[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Hats[SapkaIndex].SetActive(true);
                HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
            }

            if(SapkaIndex == Hats.Length - 1)
            {
                SapkaButtons[1].interactable = false;
            }
            else
            {
                SapkaButtons[1].interactable = true;
            }

            if (SapkaIndex != -1)
                SapkaButtons[0].interactable = true;
        }
        else
        {
            if(SapkaIndex != -1)
            {
                Hats[SapkaIndex].SetActive(false);
                SapkaIndex--;
                if(SapkaIndex != -1)
                {
                    Hats[SapkaIndex].SetActive(true);
                    SapkaButtons[0].interactable = true;
                    HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
                }
                else
                {
                    SapkaButtons[0].interactable = false;
                    HatText.text = "Sapka Yok";
                }

            }
            else
            {
                SapkaButtons[0].interactable = false;
                HatText.text = "Sapka Yok";
            }

            if (SapkaIndex != Hats.Length - 1)
            {
                SapkaButtons[1].interactable = true;
            }
        }
    }

}
