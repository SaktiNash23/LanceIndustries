using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum BorderColor
{
    RED,
    BLUE,
    YELLOW,
    WHITE
};

public class ColoredBorder : MonoBehaviour
{
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] SpriteRenderer spriteRend;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col1;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col2;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col3;

    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Sprite redBorder;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Sprite blueBorder;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Sprite yellowBorder;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Sprite whiteBorder;

    public BorderColor borderColor;

    public void checkIfCorrectLaserHit(GameObject projectile)
    {
        if (projectile.GetComponent<Proto_Projectile>().laserColor_Enum.ToString() == borderColor.ToString())
        {
            //Do nothing
        }
        else
        {
            projectile.GetComponent<Proto_Projectile>().DestroyCheck = true;
            projectile.GetComponent<Proto_Projectile>().projectileSpeed = 0.0f;
            Destroy(projectile);
        }
    }

    public void Initialization()
    {
        switch (borderColor)
        {
            case BorderColor.RED:
                gameObject.GetComponent<SpriteRenderer>().sprite = redBorder;
                break;

            case BorderColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().sprite = blueBorder;
                break;

            case BorderColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().sprite = yellowBorder;
                break;

            case BorderColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().sprite = whiteBorder;
                break;
        }
    }

    public void ToggleBorder(bool show)
    {
        if (show)
            spriteRend.enabled = col1.enabled = col2.enabled = col3.enabled = true;
        else
            spriteRend.enabled = col1.enabled = col2.enabled = col3.enabled = false;
    } 
}
