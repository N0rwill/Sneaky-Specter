using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyMove : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    public void SitUp()
    {
        anim.enabled = true;
    }
}
