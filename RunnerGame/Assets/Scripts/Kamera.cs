using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool SonaGeldikmi;
    public GameObject GidecegiYer;
    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (!SonaGeldikmi)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, .125f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, GidecegiYer.transform.position + offset, .05f);
        }
    }

}
