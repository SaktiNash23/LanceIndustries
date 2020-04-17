using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileRaycastLength;
    public float projectile2ndRaycastLength;
    private Vector3 directionVector;
    private Rigidbody2D rb;
    private bool reflectorHit = false;
    public RaycastHit2D hitStore;
    private int layerMask;
    private float lifeTime = 3.0f;


    void Awake()
    {
        directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();

        layerMask = 1 << 8;
        layerMask = ~layerMask; //Ask raycast to ignore only Layer 8 which is the IgnoredByProjectile layer
    }
    
    void Update()
    {
        if (lifeTime > 0.0f)
            lifeTime -= Time.deltaTime;
        else
        {         
            Destroy(gameObject);
            Debug.Log("Projectile lifetime expired");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + directionVector.normalized * projectileSpeed * Time.fixedDeltaTime);

        if (reflectorHit == false)
        {
            RaycastHit2D hitStore = Physics2D.Raycast(transform.position, directionVector, projectileRaycastLength, layerMask);
            Debug.DrawRay(transform.position, directionVector * projectileRaycastLength);

            if (hitStore)
            {
                if (hitStore.collider.gameObject.tag == "Reflector")
                {
                    Debug.Log("Reflector HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector>().calculateLaser_Basic(hitStore, gameObject);

                    }
                }

                if(hitStore.collider.gameObject.tag == "ReflectorTranslucent")
                {
                    Debug.Log("Reflector Translucent HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Translucent>().calculateLaser_Translucent(hitStore, gameObject);
                    }
                }

                if(hitStore.collider.gameObject.tag == "ReflectorDoubleWay")
                {
                    Debug.Log("Reflector Translucent HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_DoubleWay>().calculateLaser_DoubleWay(hitStore, gameObject);
                    }
                }

                if(hitStore.collider.gameObject.tag == "ReflectorSplit")
                {
                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Split>().calculateLaser_Split(hitStore, gameObject);
                    }
                }

                if(hitStore.collider.gameObject.tag == "ReflectorThreeWay")
                {
                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_ThreeWay>().calculateLaser_ThreeWay(hitStore, gameObject);
                    }
                }

                if(hitStore.collider.gameObject.tag == "InvalidBounds")
                {
                    projectileSpeed = 0.0f;
                    Destroy(gameObject, 1.0f);
                }

                if(hitStore.collider.gameObject.tag == "Border")
                {
                    projectileSpeed = 0.0f;
                    Destroy(gameObject, 1.0f);
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
