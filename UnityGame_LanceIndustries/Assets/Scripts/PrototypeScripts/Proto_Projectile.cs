using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Vector3 directionVector;

    void Awake()
    {
        directionVector = new Vector3(0.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += directionVector * projectileSpeed * Time.deltaTime;     
    }

    public Vector3 DirectionVector
    {
        get
        {
            return directionVector;
        }

        set
        {
            directionVector = value;
        }
    }
}
