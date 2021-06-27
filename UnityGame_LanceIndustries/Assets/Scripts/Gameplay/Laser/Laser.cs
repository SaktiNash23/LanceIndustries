using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum LASER_COLOR
{
    DEFAULT,
    RED,
    BLUE,
    YELLOW,
    WHITE
}

public enum LaserColor_Enum
{
    RED,
    BLUE,
    YELLOW,
    WHITE
};

public enum LASER_DIRECTION
{
    DEFAULT,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Laser : PoolObject
{
    [Header("REFERENCES")]
    [SerializeField] protected SpriteRenderer spriteRend;

    public float projectileSpeed;
    public float projectileRaycastLength;
    public Vector3 directionVector;
    public LayerMask layerMaskStruct; //Set the layers that you want the Laser to ignore in the script attached to Projectile2D prefab
    private bool destroyCheck = false; //Purpose of this variable is to ensure OnDestroy() contents are only called if the laser hits specific game objects

    public LASER_DIRECTION LaserDir { get; set; }
    public LASER_COLOR LaserColor { get; set; }

    public ILaserInteractable LastInteractable { get; set; }

    #region MonoBehaviour
    private void OnEnable()
    {
        directionVector = transform.up;
        LastInteractable = null;
        projectileSpeed = 7.0f;
    }

    private void OnDisable()
    {
        if (destroyCheck == true)
            GameManager.Instance.checkForAnyLasersInScene();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionVector, (directionVector.normalized * projectileSpeed * Time.fixedDeltaTime).magnitude, ~layerMaskStruct);
            if (hit)
            {
                ILaserInteractable laserInteractable = hit.transform.GetComponentInParent<ILaserInteractable>();

                if (laserInteractable != null && LastInteractable != laserInteractable)
                {
                    LastInteractable = laserInteractable;
                    ReduceSpeed();
                    laserInteractable.OnLaserOverlap(this, hit);
                    return;
                }

                // #region HIT: End Point
                // case "EndPoint":
                //     hitStore.collider.gameObject.GetComponent<EndPoint>().checkIfCorrectLaserHit(gameObject);
                //     projectileSpeed = 0.0f;
                //     destroyCheck = true;

                //     //Destroy(gameObject, 0.1f);

                //     LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Enqueue(this.gameObject);
                //     gameObject.transform.position = GameObject.FindGameObjectWithTag("InactivePooledLasers").transform.position;
                //     gameObject.transform.rotation = Quaternion.identity;
                //     gameObject.SetActive(false);
                //     projectileSpeed = 7.0f;
                //     break;
                // #endregion

                // #region HIT: Teleporters

                // case "TeleporterSetA":
                // case "TeleporterSetB":
                // case "TeleporterSetC":
                //     Debug.Log("HIT TELEPORTER SET");
                //     hitStore.collider.gameObject.GetComponentInParent<Teleporter>().teleportLaser(gameObject, hitStore.collider.gameObject);
                //     break;

                // #endregion

                // #region HIT: Colored Border
                //     case "ColoredBorder":
                //         hitStore.collider.gameObject.GetComponent<ColoredBorder>().checkIfCorrectLaserHit(gameObject);
                //         break;

                //         #endregion
                // }
            }

            transform.position += transform.right * projectileSpeed * Time.fixedDeltaTime;
        }
    }
    #endregion

    public void RefreshLaserMaterialColor()
    {
        Color targetColor = Color.white;

        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                targetColor = Color.red;
                break;
            case LASER_COLOR.BLUE:
                targetColor = Color.blue;
                break;
            case LASER_COLOR.YELLOW:
                targetColor = Color.yellow;
                break;
            case LASER_COLOR.WHITE:
                targetColor = Color.white;
                break;
        }

        spriteRend.color = targetColor * 3.0f;
    }

    public void ReduceSpeed()
    {
        if (GameManager.Instance.gimmick_LaserSpeedDecrease == true)
        {
            projectileSpeed = Mathf.Clamp(--projectileSpeed, 2.0f, 7.0f);
        }
    }

    public void returnLaserToPool(GameObject laserToReturn)
    {
        // LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Enqueue(laserToReturn);
        // laserToReturn.transform.position = GameObject.FindGameObjectWithTag("InactivePooledLasers").transform.position;
        // laserToReturn.transform.rotation = Quaternion.identity;
    }

    public void setLaserDirectionEnum(float laserOrigin_zRotation)
    {
        switch (Mathf.RoundToInt(laserOrigin_zRotation))
        {
            case 0:
                LaserDir = LASER_DIRECTION.RIGHT;
                break;

            case 90:
                LaserDir = LASER_DIRECTION.UP;
                break;

            case 180:
                LaserDir = LASER_DIRECTION.LEFT;
                break;

            case 270:
                LaserDir = LASER_DIRECTION.DOWN;
                break;
        }
    }
}
