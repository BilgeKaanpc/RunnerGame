using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public Kamera Kamera;
    public bool SonaGeldikmi;
    public GameObject GidecegiYer;
    public Slider _Slider;
    public GameObject GecisNoktasi;
    // Start is called before the first frame update
    void Start()
    {

        float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
        _Slider.maxValue = fark;
    }
    private void FixedUpdate()
    {
        if (!SonaGeldikmi)
        {
            transform.Translate(Vector3.forward * .5f * Time.deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {



        if (SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, GidecegiYer.transform.position, .01f);
            if(_Slider.value != 0)
            {
                _Slider.value -= .01f;
            }
        }
        else
        {

            float fark = Vector3.Distance(transform.position, GecisNoktasi.transform.position);
            _Slider.value = fark;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Input.GetAxis("Mouse X") < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y, transform.position.z), .3f);
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y, transform.position.z), .3f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Carpma")|| other.CompareTag("Toplama")|| other.CompareTag("Cikartma")|| other.CompareTag("Bolme"))
        {
            int sayi = int.Parse(other.name);
            _GameManager.AdamYonetim(other.tag,sayi,other.transform);
        }
        else if (other.CompareTag("Sontetikleyici"))
        {
            Kamera.SonaGeldikmi = true;
            SonaGeldikmi = true;
            _GameManager.GetComponent<GameManager>().DusmanlariTetikle();
        }
        else if (other.CompareTag("BosKarakter"))
        {
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direk")|| collision.gameObject.CompareTag("igneliKutu")|| collision.gameObject.CompareTag("Pervaneigneler"))
        {
            if (transform.position.x > 0)
            {

                transform.position = new Vector3(transform.position.x - .2f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
            }
            
        
        }
    }
}
