using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Bilge;
using TMPro;

public class Ozellestirme : MonoBehaviour
{
    public TMP_Text puanText;
    public TMP_Text HatText;
    public TMP_Text SopaText;
    public TMP_Text MaterialText;
    public GameObject[] Panels;
    public GameObject islemCanvas;
    public GameObject[] GenelPaneller;
    public Button[] IslemButtonlari;
    public TextMeshProUGUI buyText; 
    int ActiveButtonIndex;

    public Animator saveAnim;


    [Header("Sapkalar")]
    public GameObject[] Hats;
    public Button[] SapkaButtons;
    [Header("Sopalar")]
    public GameObject[] Weapons;
    public Button[] SopaButtons;
    [Header("Materialler")]
    public Material[] Materials;
    public Material DefaultMaterial;
    public Button[] MaterialButtons;
    public SkinnedMeshRenderer _Renderer;

    public AudioSource[] Sounds;

    BellekYonetim _BellekYonetimi = new BellekYonetim();
    public List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    VeriYonetimi _veriYonetim = new VeriYonetimi();

    int SapkaIndex = -1;
    int SopaIndex = -1;
    int MaterialIndex = -1;

    void Start()
    {
       // _BellekYonetimi.VeriKaydet_int("Puan", 1250);
        puanText.text = _BellekYonetimi.VeriOku_int("Puan").ToString();

        _veriYonetim.Load();
        _ItemBilgileri = _veriYonetim.ReturnList();
        DurumKontrol(0,true);
        DurumKontrol(1,true);
        DurumKontrol(2,true);

    }

    void DurumKontrol(int Bolum, bool islem = false)
    {

        if(Bolum == 0)
        {

            if (_BellekYonetimi.VeriOku_int("AktifSapka") == -1)
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                buyText.text = "Satýn AL";
                IslemButtonlari[0].interactable = false;
                IslemButtonlari[1].interactable = false;
                if (!islem)
                {
                    SapkaIndex = -1;
                    HatText.text = "Sapka Yok";
                }
            }
            else
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                SapkaIndex = _BellekYonetimi.VeriOku_int("AktifSapka");
                Hats[SapkaIndex].SetActive(true);
                HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
                IslemButtonlari[1].interactable = true;

            }
        }else if (Bolum == 1)
        {
            if (_BellekYonetimi.VeriOku_int("AktifSopa") == -1)
            {
                foreach (var item in Weapons)
                {
                    item.SetActive(false);
                }
                buyText.text = "Satýn AL";
                IslemButtonlari[0].interactable = false;
                IslemButtonlari[1].interactable = false;
                if (!islem)
                {
                    SopaIndex = -1;
                    SopaText.text = "Sopa Yok";
                }
            }
            else
            {
                foreach (var item in Weapons)
                {
                    item.SetActive(false);
                }
                SopaIndex = _BellekYonetimi.VeriOku_int("AktifSopa");
                Weapons[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex +3].ItemName;
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
                IslemButtonlari[1].interactable = true;
            }

        }
        else
        {
            if (_BellekYonetimi.VeriOku_int("AktifTema") == -1)
            {

                buyText.text = "Satýn AL";
                if (!islem)
                {
                    MaterialIndex = -1;
                    MaterialText.text = "Tema Yok";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = false;
                }
                else
                {

                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultMaterial;
                    _Renderer.materials = mats;
                }
            }
            else
            {
                MaterialIndex = _BellekYonetimi.VeriOku_int("AktifTema");
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex+6].ItemName;
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
                IslemButtonlari[1].interactable = true;
            }
        }

    }

    void SatinAlim(int Index)
    {

        _ItemBilgileri[Index].bought = true;
        _BellekYonetimi.VeriKaydet_int("Puan", _BellekYonetimi.VeriOku_int("Puan") - _ItemBilgileri[Index].Point);
        buyText.text = "Satýn Alýndý";
        IslemButtonlari[0].interactable = false;
        IslemButtonlari[1].interactable = true;
        puanText.text = _BellekYonetimi.VeriOku_int("Puan").ToString();
    }

    public void SatinAl()
    {
        Sounds[1].Play();
        if (ActiveButtonIndex != -1)
        {
            switch (ActiveButtonIndex)
            {
                case 0:
                    SatinAlim(SapkaIndex);
                    break;
                case 1:
                    int Index = SopaIndex + 3;
                    SatinAlim(Index);
                    break;
                case 2:
                    int Index2 = MaterialIndex + 6;
                    SatinAlim(Index2);
                    break;
                default:
                    break;
            }
        }
    }

    void KaydetIslem(string Key, int Index)
    {

        _BellekYonetimi.VeriKaydet_int(Key, Index);
        IslemButtonlari[1].interactable = false;
        if (!saveAnim.GetBool("ok"))
        {
            saveAnim.SetBool("ok", true);
        }
    }

    public void Kaydet()
    {
        Sounds[2].Play();
        if (ActiveButtonIndex != -1)
        {
            switch (ActiveButtonIndex)
            {
                case 0:
                    KaydetIslem("AktifSapka", SapkaIndex);
                    break;
                case 1:
                    KaydetIslem("AktifSopa", SopaIndex);
                    break;
                case 2:
                    KaydetIslem("AktifTema", MaterialIndex);
                    break;
                default:
                    break;
            }
        }
    }

    public void SapkaYonButton(string islem)
    {
        Sounds[0].Play();
        if (islem == "ileri")
        {
            if (SapkaIndex == -1)
            {
                SapkaIndex = 0;
                Hats[SapkaIndex].SetActive(true);
                HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
                if (!_ItemBilgileri[SapkaIndex].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[SapkaIndex].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }
            else
            {
                Hats[SapkaIndex].SetActive(false);
                SapkaIndex++;
                Hats[SapkaIndex].SetActive(true);
                HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
                if (!_ItemBilgileri[SapkaIndex].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[SapkaIndex].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }

            if (SapkaIndex == Hats.Length - 1)
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
            if (SapkaIndex != -1)
            {
                Hats[SapkaIndex].SetActive(false);
                SapkaIndex--;
                if (SapkaIndex != -1)
                {
                    Hats[SapkaIndex].SetActive(true);
                    SapkaButtons[0].interactable = true;
                    HatText.text = _ItemBilgileri[SapkaIndex].ItemName;
                    if (!_ItemBilgileri[SapkaIndex].bought)
                    {
                        IslemButtonlari[1].interactable = false;
                        buyText.text = _ItemBilgileri[SapkaIndex].Point + " - Satýn Al";
                        if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SapkaIndex].Point)
                        {
                            IslemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            IslemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        buyText.text = "Satýn Alýndý";
                        IslemButtonlari[0].interactable = false;
                        IslemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    SapkaButtons[0].interactable = false;
                    HatText.text = "Sapka Yok";
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false; 
                }

            }
            else
            {
                SapkaButtons[0].interactable = false;
                HatText.text = "Sapka Yok";
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
            }

            if (SapkaIndex != Hats.Length - 1)
            {
                SapkaButtons[1].interactable = true;
            }
        }
    }

    public void SopaYonButton(string islem)
    {
        Sounds[0].Play();
        if (islem == "ileri")
        {
            if (SopaIndex == -1)
            {
                SopaIndex = 0;
                Weapons[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex+3].ItemName;
                if (!_ItemBilgileri[SopaIndex + 3].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[SopaIndex + 3].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }
            else
            {
                Weapons[SopaIndex].SetActive(false);
                SopaIndex++;
                Weapons[SopaIndex].SetActive(true);
                SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemName;
                if (!_ItemBilgileri[SopaIndex + 3].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[SopaIndex + 3].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }

            if (SopaIndex == Weapons.Length - 1)
            {
                SopaButtons[1].interactable = false;
            }
            else
            {
                SopaButtons[1].interactable = true;
            }

            if (SopaIndex != -1)
                SopaButtons[0].interactable = true;
        }
        else
        {
            if (SopaIndex != -1)
            {
                Weapons[SopaIndex].SetActive(false);
                SopaIndex--;
                if (SopaIndex != -1)
                {
                    Weapons[SopaIndex].SetActive(true);
                    SopaButtons[0].interactable = true;
                    SopaText.text = _ItemBilgileri[SopaIndex + 3].ItemName;
                    if (!_ItemBilgileri[SopaIndex + 3].bought)
                    {
                        IslemButtonlari[1].interactable = false;
                        buyText.text = _ItemBilgileri[SopaIndex + 3].Point + " - Satýn Al";
                        if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[SopaIndex + 3].Point)
                        {
                            IslemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            IslemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        buyText.text = "Satýn Alýndý";
                        IslemButtonlari[0].interactable = false;
                        IslemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    SopaButtons[0].interactable = false;
                    SopaText.text = "Sopa Yok";
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                }

            }
            else
            {
                SopaButtons[0].interactable = false;
                SopaText.text = "Sopa Yok";
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
            }

            if (SopaIndex != Weapons.Length - 1)
            {
                SopaButtons[1].interactable = true;
            }
        }
    }

    public void MaterialButton(string islem)
    {
        Sounds[0].Play();
        if (islem == "ileri")
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0; 
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemName;
                if (!_ItemBilgileri[MaterialIndex + 6].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[MaterialIndex + 6].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }
            else
            {
                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemName;

                if (!_ItemBilgileri[MaterialIndex + 6].bought)
                {
                    IslemButtonlari[1].interactable = false;
                    buyText.text = _ItemBilgileri[MaterialIndex + 6].Point + " - Satýn Al";
                    if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Point)
                    {
                        IslemButtonlari[0].interactable = false;
                    }
                    else
                    {
                        IslemButtonlari[0].interactable = true;
                    }
                }
                else
                {
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                    IslemButtonlari[1].interactable = true;
                }
            }

            if (MaterialIndex == Materials.Length - 1)
            {
                MaterialButtons[1].interactable = false;
            }
            else
            {
                MaterialButtons[1].interactable = true;
            }

            if (MaterialIndex != -1)
                MaterialButtons[0].interactable = true;
        }
        else
        {
            if (MaterialIndex != -1)
            {
                MaterialIndex--;
                if (MaterialIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materials[MaterialIndex];
                    _Renderer.materials = mats;
                    MaterialButtons[0].interactable = true;
                    MaterialText.text = _ItemBilgileri[MaterialIndex + 6].ItemName;

                    if (!_ItemBilgileri[MaterialIndex + 6].bought)
                    {
                        IslemButtonlari[1].interactable = false;
                        buyText.text = _ItemBilgileri[MaterialIndex + 6].Point + " - Satýn Al";
                        if (_BellekYonetimi.VeriOku_int("Puan") < _ItemBilgileri[MaterialIndex + 6].Point)
                        {
                            IslemButtonlari[0].interactable = false;
                        }
                        else
                        {
                            IslemButtonlari[0].interactable = true;
                        }
                    }
                    else
                    {
                        buyText.text = "Satýn Alýndý";
                        IslemButtonlari[0].interactable = false;
                        IslemButtonlari[1].interactable = true;
                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultMaterial;
                    _Renderer.materials = mats;
                    MaterialButtons[0].interactable = false;
                    MaterialText.text = "Tema Yok";
                    buyText.text = "Satýn Alýndý";
                    IslemButtonlari[0].interactable = false;
                }

            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = DefaultMaterial;
                _Renderer.materials = mats;
                MaterialButtons[0].interactable = false;
                MaterialText.text = "Tema Yok";
                buyText.text = "Satýn Alýndý";
                IslemButtonlari[0].interactable = false;
            }

            if (MaterialIndex != Materials.Length - 1)
            {
                MaterialButtons[1].interactable = true;
            }
        }


    }

    public void IslemPanelleri(int Index)
    {
        Sounds[0].Play();
        DurumKontrol(Index);
        ActiveButtonIndex = Index;
        GenelPaneller[0].SetActive(true);
        GenelPaneller[1].SetActive(true);
        Panels[Index].SetActive(true);
        islemCanvas.SetActive(false);
    }
    public void Back()
    {
        Sounds[0].Play();
        GenelPaneller[0].SetActive(false);
        GenelPaneller[1].SetActive(false);
        islemCanvas.SetActive(true);
        Panels[ActiveButtonIndex].SetActive(false);
        DurumKontrol(ActiveButtonIndex,true);
        ActiveButtonIndex = -1;
    }

    public void MainMenu()
    {
        Sounds[0].Play();
        _veriYonetim.Save(_ItemBilgileri);
        SceneManager.LoadScene(0);
    }

}
