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
        Target = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().VarisNoktasý;
    }

    private void LateUpdate()
    {
        _Navmesh.SetDestination(Target.transform.position);
    }

}
