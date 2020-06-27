using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_Translucent : Reflector
{
    private Reflector_SO reflectorTranslucent_SO;
    private GameObject newProjectile;

    void Start()
    {
        reflectorTranslucent_SO = base.reflectorBase_SO;
    }

    public void calculateLaser_Translucent(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);
        base.calculateLaser_Base();

        switch(/*transform.rotation.eulerAngles.z*/ Mathf.Round(transform.parent.Find("ReferencePoint").localEulerAngles.z))
        {
            case 0:
                if (base.referenceVector == Vector2.down)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                break;

            case 90:
                if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                else if (base.referenceVector == Vector2.down)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                break;

            case 180:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                else if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                break;

            case 270:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                break;
        }

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();

        if (reflectorAnimationScript != null)
            reflectorAnimationScript.playDeflectAnimation(/*transform.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);

        setReflectorHitFalseForProjectile();
        
    }

    public override void setReflectorLaserColor()
    {
        referenceProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;
        newProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;

        //referenceProjectile.GetComponent<SpriteRenderer>().color = reflectorTranslucent_SO.laserColorToChange;
        //newProjectile.GetComponent<SpriteRenderer>().color = reflectorTranslucent_SO.laserColorToChange;

        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.BLUE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.YELLOW:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.WHITE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                //Maybe in the future, change glow of laser here
                break;
        }

        referenceProjectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
    }

    public override void setReflectorHitFalseForProjectile()
    {
        base.setReflectorHitFalseForProjectile();
        newProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

}
