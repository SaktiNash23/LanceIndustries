using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Vector3 directionVector;
    private Rigidbody2D rb;
    private bool reflectorHit = false;
    public RaycastHit2D hitStore;

    void Awake()
    {
        directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();  
    }
    
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + directionVector.normalized * projectileSpeed * Time.fixedDeltaTime);

        if (reflectorHit == false)
        {
            RaycastHit2D hitStore = Physics2D.Raycast(transform.position, directionVector, 0.2f);
            Debug.DrawRay(transform.position, directionVector * 0.2f);

            if (hitStore)
            {
                if (hitStore.collider.gameObject.tag == "Reflector")
                {
                    Debug.Log("Reflector HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector>().calculateLaser(hitStore, gameObject);
                    }
                }
                else if(hitStore.collider.gameObject.tag == "InvalidBounds")
                {
                    Destroy(gameObject);
                }
            }

        }     
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

    public bool ReflectorHit
    {
        get
        {
            return reflectorHit;
        }

        set
        {
            reflectorHit = value;
        }

    }

    public void reflectorHitFalse()
    {
        reflectorHit = false;
    }
 
}
