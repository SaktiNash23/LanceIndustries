using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Vector3 directionVector;
    private Rigidbody2D rb;

    void Awake()
    {
        directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();
       
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + directionVector * projectileSpeed * Time.fixedDeltaTime);
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
