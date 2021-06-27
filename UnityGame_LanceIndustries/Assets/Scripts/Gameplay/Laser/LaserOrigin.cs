using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LaserOrigin : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] protected Transform barrel;
    [SerializeField] protected SpriteRenderer spriteRend;

    [Header("SPRITES")]
    public Sprite redLaserOriginSprite;
    public Sprite blueLaserOriginSprite;
    public Sprite yellowLaserOriginSprite;
    public Sprite whiteLaserOriginSprite;

    [Header("PREFABS")]
    [SerializeField] protected Laser laserPrefab;

    private LASER_COLOR laserColor;
    public LASER_COLOR LaserColor
    {
        get
        {
            return laserColor;
        }
        set
        {
            laserColor = value;
        }
    }

    private GameObject[] allActiveReflectors;

    #region MonoBehaviour
    private void OnMouseOver()
    {
        if (!GameManager.Instance.isGamePaused)
            if (Input.GetMouseButtonDown(1))
                ShootLaser();
    }

    private void OnMouseUp()
    {
        if (GameManager.Instance.isGamePaused == false && !GameManager.Instance.AllCorrectLasersHaveReached)
        {
            if (GameManager.Instance.beginCountDown == false)
            {
                GameManager.Instance.Reset();
                GameManager.Instance.beginCountDown = true; //Disable this line if you want to test without timer

                GameObject[] allStartingPoints = GameObject.FindGameObjectsWithTag("StartingPoint");

                for (int i = 0; i < allStartingPoints.Length; ++i)
                {
                    allStartingPoints[i].GetComponent<LaserOrigin>().ShootLaser();
                }

                GameplayInputManager.Instance.EnableInput = false;
            }
        }
    }
    #endregion

    #region Laser Origin
    public void Initialization()
    {
        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                spriteRend.sprite = redLaserOriginSprite;
                break;

            case LASER_COLOR.BLUE:
                spriteRend.sprite = blueLaserOriginSprite;
                break;

            case LASER_COLOR.YELLOW:
                spriteRend.sprite = yellowLaserOriginSprite;
                break;

            case LASER_COLOR.WHITE:
                spriteRend.sprite = whiteLaserOriginSprite;
                break;
        }
    }

    [Button("SHOOT")]
    public void ShootLaser()
    {
        Quaternion laserRotation = barrel.rotation;

        Laser laser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.position, laserRotation);

        // laser.GetComponent<Proto_Projectile>().setLaserDirectionEnum(transform.rotation.eulerAngles.z);

        switch (LaserColor.ToString())
        {
            case "RED":
                laser.GetComponent<Laser>().LaserColor = LASER_COLOR.RED;
                break;

            case "BLUE":
                laser.GetComponent<Laser>().LaserColor = LASER_COLOR.BLUE;
                break;

            case "YELLOW":
                laser.GetComponent<Laser>().LaserColor = LASER_COLOR.YELLOW;
                break;

            case "WHITE":
                laser.GetComponent<Laser>().LaserColor = LASER_COLOR.WHITE;
                break;
        }

        laser.RefreshLaserMaterialColor();
    }

    private void activateAnimatorComponentsOnReflectors()
    {
        allActiveReflectors = System.Array.Empty<GameObject>();
        allActiveReflectors = GameObject.FindGameObjectsWithTag("ReflectorGM");

        foreach (GameObject activeReflector in allActiveReflectors)
        {
            activeReflector.GetComponent<Animator>().enabled = true;
        }
    }
    #endregion
}
