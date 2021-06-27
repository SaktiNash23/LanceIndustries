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

    private void OnMouseOver()
    {
#if UNITY_EDITOR
        if (!GameManager.Instance.IsGamePaused)
            if (Input.GetMouseButtonDown(1))
                Fire();
#endif
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.IsGamePaused == false && !GameManager.Instance.AllCorrectLasersHaveReached)
        {
            if (GameManager.Instance.beginCountDown == false)
            {
                GameManager.Instance.Reset();
                GameManager.Instance.beginCountDown = true; //Disable this line if you want to test without timer

                GameObject[] allStartingPoints = GameObject.FindGameObjectsWithTag("StartingPoint");

                for (int i = 0; i < allStartingPoints.Length; ++i)
                {
                    allStartingPoints[i].GetComponent<LaserOrigin>().Fire();
                }

                GameplayInputManager.Instance.EnableInput = false;
            }
        }
    }

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

    protected void Fire()
    {
        Laser laser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.position, barrel.rotation);
        laser.LaserColor = laserColor;
        laser.RefreshLaserMaterialColor();
    }
}
