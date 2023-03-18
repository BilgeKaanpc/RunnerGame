using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bilge;

public class GameManager : MonoBehaviour
{
    public GameObject VarisNoktas�;
    public static int AnlikKarakterSayisi = 1;

    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesi;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void AdamYonetim(string islemTuru,int gelenSayi, Transform Pozisyon)
    {
        switch (islemTuru)
        {
            case "Carpma":
                Matematiksel_islemler.Carpma(gelenSayi,OlusmaEfektleri,Karakterler,Pozisyon);
                break;
            case "Toplama":
                Matematiksel_islemler.Toplama(gelenSayi, OlusmaEfektleri, Karakterler, Pozisyon);
                break;
            case "Cikartma":
                Matematiksel_islemler.Cikartma(gelenSayi,YokOlmaEfektleri, Karakterler);
                break;
            case "Bolme":
                Matematiksel_islemler.Bolme(gelenSayi, YokOlmaEfektleri, Karakterler);
                break;
        }
    }

    public void YokOlmaEfektiOlustur(Vector3 Pozisyon,bool Balyoz = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                AnlikKarakterSayisi--;
                break;
            }
            if (Balyoz)
            {
                Vector3 yeniPoz = new Vector3(Pozisyon.x, 0.005f, Pozisyon.z);
                foreach (var item2 in AdamLekesi)
                {
                    if (!item2.activeInHierarchy)
                    {
                        item2.SetActive(true);
                        item2.transform.position = yeniPoz;
                        break;
                    }
                }
            }
        }
    }
}
