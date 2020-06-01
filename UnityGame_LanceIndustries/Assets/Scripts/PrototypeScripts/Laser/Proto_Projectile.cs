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
    public LayerMask layerMaskStruct; //Set the layers that you want the Laser to ignore in the script attached to Projectile2D prefab
    private bool destroyCheck = false; //Purpose of this variable is to ensure OnDestroy() contents are only called if the laser hits specific game objects

    public enum LaserColor_Enum
    {
        RED,
        BLUE,
        YELLOW,
        WHITE
    };

    public LaserColor_Enum laserColor_Enum;

    Color tempColor;
    public float ColorIntensity;

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
            RaycastHit2D hitStore = Physics2D.Raycast(transform.position, directionVector, projectileRaycastLength, ~layerMaskStruct); //LayerMask is inversed so it ignores the layers that is set
            Debug.DrawRay(transform.position, directionVector * projectileRaycastLength);

            if (hitStore)
            {
                switch(hitStore.collider.gameObject.tag)
                {
                    #region HIT: Basic Reflector

                    case "Reflector":
                        reflectorHit = true;
                        reduceLaserSpeed();
                        hitStore.collider.gameObject.GetComponent<Reflector>().calculateLaser_Basic(hitStore, gameObject);
                        break;

                    #endregion

                    #region HIT: Translucent Reflector
                    case "ReflectorTranslucent":
                        reflectorHit = true;
                        reduceLaserSpeed();
                        hitStore.collider.gameObject.GetComponent<Reflector_Translucent>().calculateLaser_Translucent(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Double Way Reflector
                    case "ReflectorDoubleWay":
                        reflectorHit = true;
                        reduceLaserSpeed();
                        hitStore.collider.gameObject.GetComponent<Reflector_DoubleWay>().calculateLaser_DoubleWay(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Split Reflector
                    case "ReflectorSplit":
                        reflectorHit = true;
                        reduceLaserSpeed();
                        hitStore.collider.gameObject.GetComponent<Reflector_Split>().calculateLaser_Split(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Three Way Reflector
                    case "ReflectorThreeWay":
                        reflectorHit = true;
                        reduceLaserSpeed();
                        hitStore.collider.gameObject.GetComponent<Reflector_ThreeWay>().calculateLaser_ThreeWay(hitStore, gameObject);
                        break;
                    #endregion

                    #region HIT: Invalid Bounds
                    case "InvalidBounds":
                        projectileSpeed = 0.0f;
                        destroyCheck = true; //If true, it means the projectile was destroyed by hitting invalid bounds or border
                        Destroy(gameObject, 0.0f);                  
                        break;
                    #endregion

                    #region HIT: Border
                    case "Border":
                        projectileSpeed = 0.0f;
                        destroyCheck = true; //If true, it means the projectile was destroyed by hitting invalid bounds or border
                        Destroy(gameObject, 0.0f);
                        break;
                    #endregion

                    #region HIT: End Point
                    case "EndPoint":
                        hitStore.collider.gameObject.GetComponent<EndPoint>().checkIfCorrectLaserHit(gameObject);
                        projectileSpeed = 0.0f;
                        destroyCheck = true;
                        Destroy(gameObject, 0.1f);
                        break;
                    #endregion

                    #region HIT: Teleporters

                    case "TeleporterSetA":
                    case "TeleporterSetB":
                    case "TeleporterSetC":
                        Debug.Log("HIT TELEPORTER SET");
                        hitStore.collider.gameObject.GetComponentInParent<Teleporter>().teleportLaser(gameObject, hitStore.collider.gameObject);
                        break;

                    #endregion

                    #region HIT: Colored Border
                    case "ColoredBorder":
                        hitStore.collider.gameObject.GetComponent<ColoredBorder>().checkIfCorrectLaserHit(gameObject);
                        break;

                        //If hit object with tag: "BorderColored"
                        //Use same script concept as EndPoint script. Each border have enum that you set the color.
                        //Then check the laser color enum with colored border enum, then perform the appropriate response
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

    public bool DestroyCheck
    {
        get
        {
            return destroyCheck;
        }

        set
        {
            destroyCheck = value;
        }
    }

    #endregion

    public void reflectorHitFalse()
    {
        reflectorHit = false;
    }

    public void ChangeLaserMaterialColor()
    {
        switch (laserColor_Enum.ToString())
        {
            case "RED":
                tempColor = Color.red;       
                break;

            case "BLUE":
                tempColor = Color.blue;
                break;

            case "YELLOW":
                tempColor = Color.yellow;          
                break;

            case "WHITE":
                tempColor = Color.white;
                break;
        }

        tempColor = tempColor * ColorIntensity;
        GetComponent<SpriteRenderer>().material.color = tempColor;
    }

    public void reduceLaserSpeed()
    {
        if(GameManager.gameManagerInstance.gimmick_LaserSpeedDecrease == true)
        {
            projectileSpeed = Mathf.Clamp(--projectileSpeed, 2.0f, 7.0f);
        }
    }

    void OnDestroy()
    {
        if (destroyCheck == true)
        {
            Debug.Log("Destroy Check True");
            GameManager.gameManagerInstance.checkForAnyLasersInScene();
        }
    }
 
}
