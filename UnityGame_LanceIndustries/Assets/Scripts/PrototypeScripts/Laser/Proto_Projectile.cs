using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileRaycastLength;
    private Vector3 directionVector;
    private Rigidbody2D rb;
    private bool reflectorHit = false;
    public RaycastHit2D hitStore;
    private int layerMask;
    private float lifeTime = 5.0f;

    void Awake()
    {
        directionVector = new Vector3(0.0f, 1.0f, 0.0f);
        rb = GetComponent<Rigidbody2D>();

        layerMask = 1 << 8;
        layerMask = ~layerMask; //Make raycast to ignore only Layer 8 which is the IgnoredByProjectile layer

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
                switch(hitStore.collider.gameObject.tag)
                {
                    #region HIT: Basic Reflector

                    case "Reflector":
                        Debug.Log("Reflector HIT");
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector>().calculateLaser_Basic(hitStore, gameObject);
                        break;

                    #endregion

                    #region HIT: Translucent Reflector
                    case "ReflectorTranslucent":
                        Debug.Log("Reflector Translucent HIT");
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Translucent>().calculateLaser_Translucent(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Double Way Reflector
                    case "ReflectorDoubleWay":
                        Debug.Log("Reflector DoubleWay HIT");
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_DoubleWay>().calculateLaser_DoubleWay(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Split Reflector
                    case "ReflectorSplit":
                        Debug.Log("Reflector Split HIT");
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_Split>().calculateLaser_Split(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Three Way Reflector
                    case "ReflectorThreeWay":
                        Debug.Log("Reflector Three Way HIT");
                        reflectorHit = true;
                        hitStore.collider.gameObject.GetComponent<Reflector_ThreeWay>().calculateLaser_ThreeWay(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Invalid Bounds
                    case "InvalidBounds":
                        projectileSpeed = 0.0f;
                        Destroy(gameObject, 0.0f);
                        break;
                    #endregion

                    #region HIT: Border
                    case "Border":
                        projectileSpeed = 0.0f;
                        Destroy(gameObject, 0.0f);
                        break;
                    #endregion

                    #region HIT: End Point
                    case "EndPoint":
                        hitStore.collider.gameObject.GetComponent<EndPoint>().checkIfCorrectLaserHit(gameObject);
                        projectileSpeed = 0.0f;
                        Destroy(gameObject, 0.1f);
                        break;
                    #endregion
                }
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
