using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruzgar : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AltKarakterler"))
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-5,0,0),ForceMode.Impulse);
        }
    }
}
