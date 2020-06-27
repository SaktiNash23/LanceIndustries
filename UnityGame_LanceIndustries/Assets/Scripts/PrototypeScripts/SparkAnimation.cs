﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void playDeflectAnimation()
    {
        anim.Play("DeflectSpark", 0 , 0f);
    }

    public void resetDeflectAnimation()
    {
        anim.SetBool("playSpark", false);
    }

}
