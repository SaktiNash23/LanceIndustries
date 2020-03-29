using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    void OnMouseDown()
    {
        rotateReflector((transform.rotation.eulerAngles.z));
    }


    public void rotateReflector(float zRotation)
    {
        Debug.Log(zRotation);

        if (zRotation != 270.0f)
            transform.Rotate(0.0f, 0.0f, 90.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

}
