using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject Kamera;
    public bool SonaGeldikmi;
    public GameObject GidecegiYer;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
        else
        {
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
        }else if (other.CompareTag("Sontetikleyici"))
        {
            Kamera.GetComponent<Kamera>().SonaGeldikmi = true;
            SonaGeldikmi = true;
            _GameManager.GetComponent<GameManager>().DusmanlariTetikle();
        }
    }
}
