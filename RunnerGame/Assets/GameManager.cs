using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Hedef;
    public GameObject PrefabKarakter;
    public GameObject DogmaNoktas�;
    public GameObject VarisNoktas�;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(PrefabKarakter, DogmaNoktas�.transform.position, Quaternion.identity);
        }
    }
}
