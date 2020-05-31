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

    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Color redBorderColor;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Color blueBorderColor;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Color yellowBorderColor;
    [BoxGroup("MAP BORDER SETTINGS")] [SerializeField] Color whiteBorderColor;

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
                gameObject.GetComponent<SpriteRenderer>().color = redBorderColor;
                break;

            case BorderColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = blueBorderColor;
                break;

            case BorderColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = yellowBorderColor;
                break;

            case BorderColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = whiteBorderColor;
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
