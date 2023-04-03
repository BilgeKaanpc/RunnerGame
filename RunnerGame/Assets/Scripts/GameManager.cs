using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Bilge;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int AnlikKarakterSayisi = 1;

    public List<GameObject> Karakterler;
    public List<GameObject> OlusmaEfektleri;
    public List<GameObject> YokOlmaEfektleri;
    public List<GameObject> AdamLekesi;

    public GameObject[] Sapkalar;
    public GameObject[] Sopalar;
    public Material[] Materials;

    [Header("Level Verileri")]
    public List<GameObject> Dusmanlar;
    public int KacDusmanOlsun;
    public GameObject anaKarakter;
    public bool OyunBittimi;
    bool SonaGeldikmi;
    Matematiksel_islemler _Matematiksel_islemler = new Matematiksel_islemler();
    BellekYonetim _BellekYonetim = new BellekYonetim();

    VeriYonetimi _veriYonetim = new VeriYonetimi();
    public SkinnedMeshRenderer _Renderer;
    public Material DefaultTema;


    Scene _Scene;
    public AudioSource[] sesler;

    public GameObject[] islemPanelleri;

    public AudioSource GameSound;
    public Slider soundSlider;

    public List<DilVerileriMain> DilVerileriMain = new List<DilVerileriMain>();
    List<DilVerileriMain> DilOkunanVeriler = new List<DilVerileriMain>();
    public TMP_Text[] TextObjects;
    public GameObject loadPanel;
    public Slider LoadSlider;
    void Start()
    {
        DusmanlariOlustur();
        _Scene = SceneManager.GetActiveScene();
    }
    private void Awake()
    {
        _veriYonetim.Dil_Load();
        DilOkunanVeriler = _veriYonetim.ReturnDilList();
        DilVerileriMain.Add(DilOkunanVeriler[5]);
        LanguageControl();
        sesler[0].volume = _BellekYonetim.VeriOku_float("OyunSes");
        sesler[1].volume = _BellekYonetim.VeriOku_float("MenuFx");
        soundSlider.value = _BellekYonetim.VeriOku_float("OyunSes");
        Destroy(GameObject.FindWithTag("MenuMusic"));
        ItemControl();
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

    void Update()
    {
    }

    public void ItemControl()
    {

        if (_BellekYonetim.VeriOku_int("AktifSapka") != -1)
        {
            Sapkalar[_BellekYonetim.VeriOku_int("AktifSapka")].SetActive(true);
        }
        if (_BellekYonetim.VeriOku_int("AktifSopa") != -1)
        {
            Sopalar[_BellekYonetim.VeriOku_int("AktifSopa")].SetActive(true);
        }
        if (_BellekYonetim.VeriOku_int("AktifTema") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materials[_BellekYonetim.VeriOku_int("AktifTema")];
            _Renderer.materials = mats;
        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefaultTema;
            _Renderer.materials = mats;
        }
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
                _Matematiksel_islemler.Carpma(gelenSayi,OlusmaEfektleri,Karakterler,Pozisyon);
                break;
            case "Toplama":
                _Matematiksel_islemler.Toplama(gelenSayi, OlusmaEfektleri, Karakterler, Pozisyon);
                break;
            case "Cikartma":
                _Matematiksel_islemler.Cikartma(gelenSayi,YokOlmaEfektleri, Karakterler);
                break;
            case "Bolme":
                _Matematiksel_islemler.Bolme(gelenSayi, YokOlmaEfektleri, Karakterler);
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
                    islemPanelleri[3].SetActive(true);
                }
                else
                {
                    if(_Scene.buildIndex == _BellekYonetim.VeriOku_int("SonLevel"))
                    {
                        _BellekYonetim.VeriKaydet_int("SonLevel", _BellekYonetim.VeriOku_int("SonLevel") + 1);
                        _BellekYonetim.VeriKaydet_int("Puan", _BellekYonetim.VeriOku_int("Puan") + 300);
                    }


                    islemPanelleri[2].SetActive(true);
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

    public void CikisButton(string durum)
    {
        sesler[1].Play();
        Time.timeScale = 0;
        if(durum == "durdur")
        {
            islemPanelleri[0].SetActive(true);
        }
        else if (durum == "devamet")
        {
            islemPanelleri[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (durum == "tekrar")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_Scene.buildIndex);
        }
        else if (durum == "menu")
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    public void Ayarlar(string durum)
    {
        if(durum == "ayarla")
        {
            islemPanelleri[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islemPanelleri[1].SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void SesAyarla()
    {
        _BellekYonetim.VeriKaydet_float("OyunSes", soundSlider.value);
        sesler[0].volume = soundSlider.value;
    }

    public void NextLevel()
    {
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
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
}
