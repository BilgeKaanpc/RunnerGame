using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bilge
{

    public class Matematiksel_islemler
    {

        public void Carpma(int gelenSayi, List<GameObject> OlusturmaEfektleri, List<GameObject> Karakterler, Transform Pozisyon)
        {
            int donguSayisi = (GameManager.AnlikKarakterSayisi * gelenSayi) - GameManager.AnlikKarakterSayisi;
            int sayi = 0;
            foreach (var item in Karakterler)
            {
                if (sayi < donguSayisi)
                {

                    if (!item.activeInHierarchy)
                    {
                        foreach (var efekt in OlusturmaEfektleri)
                        {
                            if (!efekt.activeInHierarchy)
                            {
                                efekt.SetActive(true);
                                efekt.transform.position = Pozisyon.position;
                                efekt.GetComponent<ParticleSystem>().Play();
                                efekt.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi++;
                    }
                }
                else
                {
                    sayi = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi *= gelenSayi;
        }
        public void Toplama(int gelenSayi, List<GameObject> OlusturmaEfektleri, List<GameObject> Karakterler, Transform Pozisyon)
        {
            int sayi2 = 0;
            foreach (var item in Karakterler)
            {
                if (sayi2 < gelenSayi)
                {

                    if (!item.activeInHierarchy)
                    {
                        foreach (var efekt in OlusturmaEfektleri)
                        {
                            if (!efekt.activeInHierarchy)
                            {
                                efekt.SetActive(true);
                                efekt.transform.position = Pozisyon.position;
                                efekt.GetComponent<ParticleSystem>().Play();
                                efekt.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Pozisyon.position;
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.AnlikKarakterSayisi += gelenSayi;
        }
        public void Cikartma(int gelenSayi, List<GameObject> YokOlmaEfektler, List<GameObject> Karakterler)
        {
            if (GameManager.AnlikKarakterSayisi < gelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    foreach (var efekt in YokOlmaEfektler)
                    {
                        if (!efekt.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                            efekt.SetActive(true);
                            efekt.transform.position = yeniPoz;
                            efekt.GetComponent<ParticleSystem>().Play();
                            efekt.GetComponent<AudioSource>().Play();
                            break;    
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi3 != gelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var efekt in YokOlmaEfektler)
                            {
                                if (!efekt.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                    efekt.SetActive(true);
                                    efekt.transform.position = yeniPoz;
                                    efekt.GetComponent<ParticleSystem>().Play();
                                    efekt.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.AnlikKarakterSayisi -= gelenSayi;
            }


        }
        public void Bolme(int gelenSayi, List<GameObject> YokOlmaEfektler, List<GameObject> Karakterler)
        {
            if (GameManager.AnlikKarakterSayisi <= gelenSayi)
            {
                foreach (var item in Karakterler)
                {
                    foreach (var efekt in YokOlmaEfektler)
                    {
                        if (!efekt.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                            efekt.SetActive(true);
                            efekt.transform.position = yeniPoz;
                            efekt.GetComponent<ParticleSystem>().Play();
                            efekt.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.AnlikKarakterSayisi = 1;
            }
            else
            {
                int bolen = GameManager.AnlikKarakterSayisi / gelenSayi;

                int sayi4 = 0;
                foreach (var item in Karakterler)
                {
                    if (sayi4 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var efekt in YokOlmaEfektler)
                            {
                                if (!efekt.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                    efekt.SetActive(true);
                                    efekt.transform.position = yeniPoz;
                                    efekt.GetComponent<ParticleSystem>().Play();
                                    efekt.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi4++;
                        }
                    }
                    else
                    {
                        sayi4 = 0;
                        break;
                    }
                }
                if (GameManager.AnlikKarakterSayisi % gelenSayi == 0)
                {

                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                }
                else if (GameManager.AnlikKarakterSayisi % gelenSayi == 1)
                {

                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                    GameManager.AnlikKarakterSayisi++;
                }
                else if (GameManager.AnlikKarakterSayisi % gelenSayi == 2)
                {

                    GameManager.AnlikKarakterSayisi /= gelenSayi;
                    GameManager.AnlikKarakterSayisi +=2;
                }
            }

        }


    }

    public class BellekYonetim
    {
        public void VeriKaydet_string(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_int(string Key, int value)
        {

            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
        public void VeriKaydet_float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string VeriOku_string(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int VeriOku_int(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float VeriOku_float(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        
        public void ControlAndCreate()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel",5);
                PlayerPrefs.SetInt("Puan", 100);
                PlayerPrefs.SetInt("AktifSapka", -1);
                PlayerPrefs.SetInt("AktifSopa", -1);
                PlayerPrefs.SetInt("AktifTema", -1);
                PlayerPrefs.SetFloat("MenuSes", 1);
                PlayerPrefs.SetFloat("MenuFx", 1);
                PlayerPrefs.SetFloat("OyunSes", 1);
                PlayerPrefs.SetString("Dil", "TR");
            }
        }
    }

    public class Verilerimiz
    {
        public static List<ItemBilgileri> _ItemBilgileri = new List<ItemBilgileri>();
    }
    [Serializable]
    public class ItemBilgileri
    {
        public int GroupIndex;
        public int ItemIndex;
        public string ItemName;
        public int Point;
        public bool bought;
    }

    public class VeriYonetimi
    {
        public void Save(List<ItemBilgileri> _ItemBilgileri)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, _ItemBilgileri);
            file.Close();

        }
        public void FileCreate(List<ItemBilgileri> _ItemBilgileri)
        {
            if(!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, _ItemBilgileri);
                file.Close();
            }

        } 
        List<ItemBilgileri> _ItemicListe;
        public void Load()
        {
            if (File.Exists((Application.persistentDataPath + "/ItemVerileri.gd")))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                _ItemicListe = (List<ItemBilgileri>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<ItemBilgileri> ReturnList()
        {
            return _ItemicListe;
        }
    }

    [Serializable]
    public class DilVerileriMain
    {
        public int BolumIndex;
        public List<DilVerileri_TR> DilVerileri_TR = new List<DilVerileri_TR>();
        public List<DilVerileri_TR> DilVerileri_EN = new List<DilVerileri_TR>();


    }


    [Serializable]
    public class DilVerileri_TR
    {
        public string metin;
    }

}