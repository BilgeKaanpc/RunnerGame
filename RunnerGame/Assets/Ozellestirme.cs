using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bilge;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        Save();
        Load();

    }

    public void Save()
    {
        _ItemBilgileri.Add(new ItemBilgileri());
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
        bf.Serialize(file, _ItemBilgileri);
        file.Close();

    }

    public void Load()
    {
        if (File.Exists((Application.persistentDataPath + "/ItemVerileri.gd")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd",FileMode.Open);
            _ItemBilgileri = (List<ItemBilgileri>)bf.Deserialize(file);
            file.Close();
            Debug.Log(_ItemBilgileri[1].ItemName);
        }
    }

    public void SapkaYonButton(string islem)
    {
        if(islem == "ileri")
        {
            if(SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Hats[SapkaIndex].SetActive(true);
            }
            else
            {
                Hats[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Hats[SapkaIndex].SetActive(true);
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
                }
                else
                {
                    SapkaButtons[0].interactable = false;
                }

            }
            else
            {
                SapkaButtons[0].interactable = false;
            }

            if (SapkaIndex != Hats.Length - 1)
            {
                SapkaButtons[1].interactable = true;
            }
        }
    }

}
