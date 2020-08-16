using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Proto_Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileRaycastLength;
    public Vector3 directionVector;
    public bool reflectorHit = false;
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

    public Sprite laserSprite_Red;
    public Sprite laserSprite_Blue;
    public Sprite laserSprite_Yellow;
    public Sprite laserSprite_White;

    public enum LaserDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public LaserDirection laserDirection;

    void Awake()
    {
    }

    void FixedUpdate()
    {
        if (GameManager.gameManagerInstance.gameIsPaused == false)
        {
            if (reflectorHit == false)
            {
                hitStore = Physics2D.Raycast(transform.position, directionVector, (directionVector.normalized * projectileSpeed * Time.fixedDeltaTime).magnitude, ~layerMaskStruct); //LayerMask is inversed so it ignores the layers that is set
                                                                                                                                                                                     //Debug.DrawRay(transform.position, transform.position + (directionVector.normalized * projectileSpeed * Time.fixedDeltaTime));
                if (hitStore)
                {
                    switch (hitStore.collider.gameObject.tag)
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

                            if (hitStore.collider.gameObject.transform.parent.name.Contains("Basic"))
                            {
                                hitStore.collider.gameObject.transform.parent.GetChild(0).GetComponent<Reflector>().playInvalidHitAnimation();
                            }
                            else if (hitStore.collider.gameObject.transform.parent.name.Contains("Translucent"))
                            {
                                hitStore.collider.gameObject.transform.parent.GetChild(0).GetComponent<Reflector_Translucent>().playInvalidHitAnimation();
                            }
                            else if (hitStore.collider.gameObject.transform.parent.name.Contains("DoubleWay"))
                            {
                                hitStore.collider.gameObject.transform.parent.GetChild(0).GetComponent<Reflector_DoubleWay>().playInvalidHitAnimation();
                            }
                            //Three Way does not have Invalid Bounds

                            returnLaserToPool(this.gameObject);
                            gameObject.SetActive(false);
                            break;
                        #endregion

                        #region HIT: Border
                        case "Border":
                            projectileSpeed = 0.0f;
                            destroyCheck = true; //If true, it means the projectile was destroyed by hitting invalid bounds or border
                            returnLaserToPool(this.gameObject);
                            gameObject.SetActive(false);
                            break;
                        #endregion

                        #region HIT: End Point
                        case "EndPoint":
                            hitStore.collider.gameObject.GetComponent<EndPoint>().checkIfCorrectLaserHit(gameObject);
                            projectileSpeed = 0.0f;
                            destroyCheck = true;

                            //Destroy(gameObject, 0.1f);

                            LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Enqueue(this.gameObject);
                            gameObject.transform.position = GameObject.FindGameObjectWithTag("InactivePooledLasers").transform.position;
                            gameObject.transform.rotation = Quaternion.identity;
                            gameObject.SetActive(false);
                            projectileSpeed = 7.0f;
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

                            #endregion
                    }
                }
            }

            transform.position += directionVector.normalized * projectileSpeed * Time.fixedDeltaTime;
        }
    }

    #region Accessor Functions

    /*
    public Vector3 directionVector
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
    */
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
                //GetComponent<SpriteRenderer>().sprite = laserSprite_Red;
                break;
                
            case "BLUE":
                tempColor = Color.blue;
                //GetComponent<SpriteRenderer>().sprite = laserSprite_Blue;
                break;

            case "YELLOW":
                tempColor = Color.yellow;
                //GetComponent<SpriteRenderer>().sprite = laserSprite_Yellow;
                break;
                
            case "WHITE":
                tempColor = Color.white;
                //GetComponent<SpriteRenderer>().sprite = laserSprite_White;
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

    private void OnEnable()
    {
        directionVector = transform.up;
        reflectorHit = false;
        projectileSpeed = 7.0f;
    }

    private void OnDisable()
    {
        reflectorHit = false;
        projectileSpeed = 0.0f;

        if (destroyCheck == true)
        {
            GameManager.gameManagerInstance.checkForAnyLasersInScene();
        }
    }

    public void returnLaserToPool(GameObject laserToReturn)
    {
        LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Enqueue(laserToReturn);
        laserToReturn.transform.position = GameObject.FindGameObjectWithTag("InactivePooledLasers").transform.position;
        laserToReturn.transform.rotation = Quaternion.identity;
    }

    public void setLaserDirectionEnum(float laserOrigin_zRotation)
    {
        switch (Mathf.RoundToInt(laserOrigin_zRotation))
        {
            case 0:
                laserDirection = LaserDirection.RIGHT;
                break;

            case 90:
                laserDirection = LaserDirection.UP;
                break;

            case 180:
                laserDirection = LaserDirection.LEFT;
                break;

            case 270:
                laserDirection = LaserDirection.DOWN;
                break;
        }
    }
}
