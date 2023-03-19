using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BosKarakter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material AtanacakOlanMaterial;
    public NavMeshAgent _Navmesh;
    public Animator _Animator;
    public GameObject target;
    public GameManager _GameManager;
    bool Temasvar;

    Vector3 PozisyonVer()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakterler") || other.CompareTag("Player"))
        {
            MaterialDegistirveAnimation();
            Temasvar = true;
        }
        else if (other.CompareTag("igneliKutu"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Testere"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Pervaneigneler"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer());
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Balyoz"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), true);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Dusman"))
        {
            _GameManager.YokOlmaEfektiOlustur(PozisyonVer(), false, false);
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (Temasvar)
        {
            _Navmesh.SetDestination(target.transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void MaterialDegistirveAnimation()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = AtanacakOlanMaterial;
        _Renderer.materials = mats;
        _Animator.SetBool("Saldir", true);
    }

}
