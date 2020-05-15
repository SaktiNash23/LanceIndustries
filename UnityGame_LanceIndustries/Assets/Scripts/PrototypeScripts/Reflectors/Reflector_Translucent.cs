﻿using System.Collections;
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

        switch(transform.rotation.eulerAngles.z)
        {
            case 0.0f:
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

            case 90.0f:
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

            case 180.0f:
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

            case 270.0f:
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

        #region Old Code Translucent Reflector
        /*
        if (transform.rotation.eulerAngles.z == 0.0f)
        {
            if(base.referenceVector == Vector2.down)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
            }
        }

        if(transform.rotation.eulerAngles.z == 90.0f)
        {
            if (base.referenceVector == Vector2.right)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            }

            if (base.referenceVector == Vector2.down)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
            }
        }

        if(transform.rotation.eulerAngles.z == 180.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
            }

            if (base.referenceVector == Vector2.right)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            }
        }

        if(transform.rotation.eulerAngles.z == 270.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
            }
        }
        */
        #endregion

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();
        
    }

    public override void setReflectorLaserColor()
    {
        referenceProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;
        newProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;

        referenceProjectile.GetComponent<SpriteRenderer>().color = reflectorTranslucent_SO.laserColorToChange;
        newProjectile.GetComponent<SpriteRenderer>().color = reflectorTranslucent_SO.laserColorToChange;
    }

    public override void setReflectorHitFalseForProjectile()
    {
        base.setReflectorHitFalseForProjectile();
        newProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

}
