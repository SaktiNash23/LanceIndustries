using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredBorder : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material whiteMaterial;

    public enum BorderColor
    {
        RED,
        BLUE,
        YELLOW,
        WHITE
    };

    public BorderColor borderColor;


    void Awake()
    {
        switch (borderColor)
        {
            case BorderColor.RED:
                gameObject.GetComponent<SpriteRenderer>().color = redMaterial.color;
                break;

            case BorderColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = blueMaterial.color;
                break;

            case BorderColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = yellowMaterial.color;
                break;

            case BorderColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = whiteMaterial.color;
                break;
        }
    }

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
                gameObject.GetComponent<SpriteRenderer>().color = redMaterial.color;
                break;

            case BorderColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = blueMaterial.color;
                break;

            case BorderColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = yellowMaterial.color;
                break;

            case BorderColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = whiteMaterial.color;
                break;
        }
    }

}
