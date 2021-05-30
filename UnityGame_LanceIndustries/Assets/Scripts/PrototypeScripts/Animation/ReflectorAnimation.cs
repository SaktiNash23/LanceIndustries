using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAnimation : MonoBehaviour
{
    private Animator anim;
    private Animator buildAnimator;
    private float storeReflectorFloatZ;

    void Awake()
    {
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        buildAnimator = transform.Find("BuildAnimator").GetComponent<Animator>();
    }

    public void PlayDeflectAnimation(float reflector_zRotation)
    {
        storeReflectorFloatZ = reflector_zRotation;

        if (Mathf.Approximately(reflector_zRotation, 0.0f))
        {
            //anim.SetBool("playDeflect_0", true);
            anim.Play("Reflector_Rotation0", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 90.0f))
        {
            //anim.SetBool("playDeflect_90", true);
            anim.Play("Reflector_Rotation90", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 180.0f))
        {
            //anim.SetBool("playDeflect_180", true);
            anim.Play("Reflector_Rotation180", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 270.0f))
        {
            //anim.SetBool("playDeflect_270", true);
            anim.Play("Reflector_Rotation270", 0, 0f);
            return;
        }
    }

    public void playInvalidAnimation(float reflector_zRotation)
    {
        storeReflectorFloatZ = reflector_zRotation;

        if (Mathf.Approximately(reflector_zRotation, 0.0f))
        {
            //anim.SetBool("playInvalid_0", true);
            anim.Play("Reflector_Invalid_0", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 90.0f))
        {
            //anim.SetBool("playInvalid_90", true);
            anim.Play("Reflector_Invalid_90", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 180.0f))
        {
            //anim.SetBool("playInvalid_180", true);
            anim.Play("Reflector_Invalid_180", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(reflector_zRotation, 270.0f))
        {
            //anim.SetBool("playInvalid_270", true);
            anim.Play("Reflector_Invalid_270", 0, 0f);
            return;
        }
    }

    public void resetDeflectAnimation()
    {
        anim.SetBool("playDeflect_0", false);
        anim.SetBool("playDeflect_90", false);
        anim.SetBool("playDeflect_180", false);
        anim.SetBool("playDeflect_270", false);

        anim.SetBool("playInvalid_0", false);
        anim.SetBool("playInvalid_90", false);
        anim.SetBool("playInvalid_180", false);
        anim.SetBool("playInvalid_270", false);

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, storeReflectorFloatZ);
    }

    public void ForceReferencePointStationary(float zRotation)
    {
        transform.Find("ReferencePoint").localRotation = Quaternion.Euler(0.0f, 0.0f, zRotation);
    }

    public void activateBuildAnimation(float zRotation)
    {    
        if (Mathf.Approximately(zRotation, 0.0f))
        {
            buildAnimator.Play("BuildAnimation", 0, 0f);
            anim.Play("BuildAnimation_Reflector", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(zRotation, 90.0f))
        {
            buildAnimator.Play("BuildAnimation_Hammer_90", 0, 0f);
            anim.Play("BuildAnimation_Reflector_90", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(zRotation, 180.0f))
        {
            buildAnimator.Play("BuildAnimation_Hammer_180", 0, 0f);
            anim.Play("BuildAnimation_Reflector_180", 0, 0f);
            return;
        }
        else if (Mathf.Approximately(zRotation, 270.0f))
        {
            buildAnimator.Play("BuildAnimation_Hammer_270", 0, 0f);
            anim.Play("BuildAnimation_Reflector_270", 0, 0f);
            return;
        }        
    }

}
