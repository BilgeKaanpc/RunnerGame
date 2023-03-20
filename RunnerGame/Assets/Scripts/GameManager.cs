using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bilge;

public class GameManager : MonoBehaviour
{
    public static int AnlikKarakterSayisi = 1;

    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesi;

    [Header("Level Verileri")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject anaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;
    void Start()
    {
        DusmanlariOlustur();
    }

    void Update()
    {
    }

    public void DusmanlariOlustur()
    {
        for(int i = 0; i < KacDusmanOlsun; i++)
        {
            Dusmanlar[i].SetActive(true);
        }
    }

    public void DusmanlariTetikle()
    {
        foreach(var item in Dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Dusman>().AnimasyonTetikle();
            }
        }
        SonaGeldikmi = true;
        SavasDurumu();
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

    void SavasDurumu()
    {

        if (SonaGeldikmi)
        {

            if (AnlikKarakterSayisi == 1 || KacDusmanOlsun == 0)
            {
                OyunBittimi = true;
                foreach (var item in Dusmanlar)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }
                foreach (var item in Karakterler)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Saldir", false);
                    }
                }

                anaKarakter.GetComponent<Animator>().SetBool("Saldir", false);

                if (AnlikKarakterSayisi < KacDusmanOlsun || AnlikKarakterSayisi == KacDusmanOlsun)
                {
                    Debug.Log("Lose");
                }
                else
                {
                    Debug.Log("Win");

                }
            }
        }

    }

    public void YokOlmaEfektiOlustur(Vector3 Pozisyon,bool Balyoz = false,bool Durum = false)
    {
        foreach (var item in YokOlmaEfektleri)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!Durum)
                {
                    AnlikKarakterSayisi--;
                }
                else
                {
                    KacDusmanOlsun--;
                }
                if (!OyunBittimi)
                {
                    SavasDurumu();
                }
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
