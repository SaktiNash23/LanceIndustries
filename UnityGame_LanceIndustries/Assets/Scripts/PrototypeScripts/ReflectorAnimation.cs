using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorAnimation : MonoBehaviour
{
    private Animator anim;
    private float storeReflectorFloatZ;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void playDeflectAnimation(float reflector_zRotation)
    {
        storeReflectorFloatZ = reflector_zRotation;

        #region Old Anim Code
        /*
        switch (reflector_zRotation)
        {
            case 0.0f:
                anim.SetBool("playDeflect_0", true);
                break;

            case 90.0f:
                anim.SetBool("playDeflect_90", true);
                break;

            case 180.0f:
                anim.SetBool("playDeflect_180", true);
                break;

            case 270.0f:
                anim.SetBool("playDeflect_270", true);
                break;
        }
        */
        #endregion

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

        #region Old Anim Code
        /*
        switch (reflector_zRotation)
        {
            case 0.0f:
                anim.SetBool("playInvalid_0", true);
                break;

            case 90.0f:
                anim.SetBool("playInvalid_90", true);
                break;

            case 180.0f:
                anim.SetBool("playInvalid_180", true);
                break;

            case 270.0f:
                anim.SetBool("playInvalid_270", true);
                break;
        }
        */
        #endregion

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
}
