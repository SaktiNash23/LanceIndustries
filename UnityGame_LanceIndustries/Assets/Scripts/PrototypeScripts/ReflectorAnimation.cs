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
    }

    public void playInvalidAnimation(float reflector_zRotation)
    {
        storeReflectorFloatZ = reflector_zRotation;

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
        Debug.Log("Rotation : " + transform.rotation.eulerAngles.z);
    }
}
