using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alt_Karakter : MonoBehaviour
{
    GameObject Target;
    NavMeshAgent _Navmesh;
    void Start()
    {
        _Navmesh = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {

        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().VarisNoktasi;
    }
    private void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("igneliKutu"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(yeniPoz);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Testere"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(yeniPoz);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Pervaneigneler"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(yeniPoz);
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Balyoz"))
        {
            Vector3 yeniPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().YokOlmaEfektiOlustur(yeniPoz,true);
            gameObject.SetActive(false);
        }
    }
}
