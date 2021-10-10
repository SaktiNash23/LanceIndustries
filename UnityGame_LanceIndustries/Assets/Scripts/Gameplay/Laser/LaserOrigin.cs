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
        if (GameManager.Instance.CanStartGame)
        {
            GameManager.Instance.Reset();
            GameManager.Instance.StartGame();
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

    public void Fire()
    {
        Laser laser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.position, barrel.rotation);
        laser.LaserColor = laserColor;
        laser.RefreshLaserMaterialColor();
        GameManager.Instance.AddLaser(laser);
    }
}
