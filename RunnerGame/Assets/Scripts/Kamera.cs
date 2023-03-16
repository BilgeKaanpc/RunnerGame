using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.position + offset,.125f);
    }

}
