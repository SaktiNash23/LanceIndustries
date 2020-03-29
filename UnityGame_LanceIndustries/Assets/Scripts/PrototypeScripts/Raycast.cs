using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    void OnMouseDown()
    {
        rotateReflector((transform.rotation.eulerAngles.z));
        Debug.Log("ADASDAS");
        Debug.Log("aswe");
        Debug.Log("wer");
        Debug.LogWarning("WARNINGASFDA");
        Debug.Log("TESTSTASAD");
    }


    public void rotateReflector(float zRotation)
    {
        Debug.Log(zRotation);

        if (zRotation != 270.0f)
            transform.Rotate(0.0f, 0.0f, 90.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public void TestFunc()
    {
        Debug.Log("COOL");
    }
}
