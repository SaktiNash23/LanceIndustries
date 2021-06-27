using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class Wall : MonoBehaviour, ILaserInteractable
{
    [BoxGroup("WALL REFERENCES")] [SerializeField] SpriteRenderer spriteRend;
    [BoxGroup("WALL REFERENCES")] [SerializeField] Collider2D col;

    [BoxGroup("WALL SETTINGS")] [SerializeField] Sprite defaultWallSprite;
    [BoxGroup("WALL SETTINGS")] [SerializeField] Sprite redWallSprite;
    [BoxGroup("WALL SETTINGS")] [SerializeField] Sprite blueWallSprite;
    [BoxGroup("WALL SETTINGS")] [SerializeField] Sprite yellowWallSprite;
    [BoxGroup("WALL SETTINGS")] [SerializeField] Sprite whiteWallSprite;

    protected LASER_COLOR wallColor;

    public void Initialization(LASER_COLOR color)
    {
        wallColor = color;
        switch (wallColor)
        {
            case LASER_COLOR.DEFAULT:
                spriteRend.sprite = defaultWallSprite;
                break;
            case LASER_COLOR.RED:
                spriteRend.sprite = redWallSprite;
                break;
            case LASER_COLOR.BLUE:
                spriteRend.sprite = blueWallSprite;
                break;
            case LASER_COLOR.YELLOW:
                spriteRend.sprite = yellowWallSprite;
                break;
            case LASER_COLOR.WHITE:
                spriteRend.sprite = whiteWallSprite;
                break;
        }
    }

    public void ToggleBorder(bool show)
    {
        spriteRend.enabled = col.enabled = show;
    }

    public void OnLaserOverlap(Laser laser, RaycastHit2D hit)
    {
        if (laser.LaserColor != wallColor)
        {
            laser.Push();
        }
        else
        {
            Debug.Log("WALL COLOR: " + wallColor);
        }
    }
}
