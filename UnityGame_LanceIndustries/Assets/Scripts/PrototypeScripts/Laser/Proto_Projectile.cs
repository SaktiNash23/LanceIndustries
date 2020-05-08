using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileRaycastLength;
    private Vector3 directionVector;
    private Rigidbody2D rb;
    [SerializeField]
    private bool reflectorHit = false;
    public RaycastHit2D hitStore;
    private int layerMask;
    private float lifeTime = 5.0f;


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
                //REMINDER: Refactor these if statements into a switch statement in the future
                #region HIT: Basic Reflector
                if(hitStore.collider.gameObject.tag == "Reflector")
                {
                    Debug.Log("Reflector HIT");
                    
                    if(reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector>().calculateLaser_Basic(hitStore, gameObject);
                    }
                }
                #endregion

                #region HIT: Translucent Reflector
                if (hitStore.collider.gameObject.tag == "ReflectorTranslucent")
                {
                    Debug.Log("Reflector Translucent HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Translucent>().calculateLaser_Translucent(hitStore, gameObject);
                    }
                }
                #endregion

                #region HIT: Double Way Reflector
                if (hitStore.collider.gameObject.tag == "ReflectorDoubleWay")
                {
                    Debug.Log("Reflector DoubleWay HIT");

                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_DoubleWay>().calculateLaser_DoubleWay(hitStore, gameObject);
                    }
                }
                #endregion

                #region HIT: Split Reflector
                if (hitStore.collider.gameObject.tag == "ReflectorSplit")
                {
                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Split>().calculateLaser_Split(hitStore, gameObject);
                    }
                }
                #endregion

                #region HIT: Three Way Reflector
                if (hitStore.collider.gameObject.tag == "ReflectorThreeWay")
                {
                    if (reflectorHit == false)
                    {
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_ThreeWay>().calculateLaser_ThreeWay(hitStore, gameObject);
                    }
                }
                #endregion

                #region HIT: Invalid Bounds
                if (hitStore.collider.gameObject.tag == "InvalidBounds")
                {
                    projectileSpeed = 0.0f;
                    Destroy(gameObject, 0.1f);
                }
                #endregion

                #region HIT: Border
                if (hitStore.collider.gameObject.tag == "Border")
                {
                    projectileSpeed = 0.0f;
                    Destroy(gameObject, 0.1f);
                }
                #endregion

                #region HIT: End Point
                if (hitStore.collider.gameObject.tag == "EndPoint")
                {                 
                    hitStore.collider.gameObject.GetComponent<EndPoint>().checkIfCorrectLaserHit(gameObject);
                    projectileSpeed = 0.0f;
                    Destroy(gameObject, 0.1f);
                }
                #endregion
            }

        }     
    }

    #region Accessor Functions

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

    #endregion

    public void reflectorHitFalse()
    {
        reflectorHit = false;
    }
 
}
