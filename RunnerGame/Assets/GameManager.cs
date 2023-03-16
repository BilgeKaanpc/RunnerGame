using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Hedef;
    public GameObject PrefabKarakter;
    public GameObject DogmaNoktasý;
    public GameObject VarisNoktasý;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(PrefabKarakter, DogmaNoktasý.transform.position, Quaternion.identity);
        }
    }
}
