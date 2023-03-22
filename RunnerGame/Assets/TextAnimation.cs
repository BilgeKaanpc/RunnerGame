using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public Animator _Anim;
  
    public void bePassive()
    {
        _Anim.SetBool("ok", false);
    }
}
