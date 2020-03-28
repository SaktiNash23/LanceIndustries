using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_Translucent : Reflector
{
    private Reflector_SO reflectorTranslucent_SO;
    private GameObject newProjectile;

    void Start()
    {
        reflectorTranslucent_SO = GameManager.gameManagerInstance.allReflectorSO[1]; //Index 1: Transulucent Reflector
    }

    public void calculateLaser_Translucent(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);
        base.calculateLaser_Base();

        if (transform.rotation.eulerAngles.z == 0.0f)
        {
            if(base.referenceVector == Vector2.down)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
            }
        }

        if(transform.rotation.eulerAngles.z == 90.0f)
        {
            if (base.referenceVector == Vector2.right)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
            }

            if (base.referenceVector == Vector2.down)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
            }
        }

        if(transform.rotation.eulerAngles.z == 180.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
            }

            if (base.referenceVector == Vector2.right)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
            }
        }

        if(transform.rotation.eulerAngles.z == 270.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
            }
        }
  
        setReflectorLaserColor();
        setReflectorHitFalseForProjectile();
        
    }

    public override void setReflectorLaserColor()
    {
        referenceProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;
        newProjectile.GetComponent<TrailRenderer>().material = reflectorTranslucent_SO.laserMaterialToChange;
    }

    public override void setReflectorHitFalseForProjectile()
    {
        base.setReflectorHitFalseForProjectile();
        newProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

}
