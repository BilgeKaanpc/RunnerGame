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
    bool Temasvar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AltKarakterler") || other.CompareTag("Player"))
        {
            MaterialDegistirveAnimation();
            Temasvar = true;
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
