﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_ThreeWay : Reflector
{
    private Reflector_SO reflectorThreeWay_SO;
    private GameObject newProjectile0;
    private GameObject newProjectile1;

    // Start is called before the first frame update
    void Start()
    {
        reflectorThreeWay_SO = base.reflectorBase_SO;
    }

    public void calculateLaser_ThreeWay(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);
        base.calculateLaser_Base();

        if (transform.rotation.eulerAngles.z == 0.0f)
        {
            if (base.referenceVector == Vector2.down)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
            }
        }

        if (transform.rotation.eulerAngles.z == 90.0f)
        {
            if (base.referenceVector == Vector2.right)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            }

            if (base.referenceVector == Vector2.down)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
            }
        }

        if (transform.rotation.eulerAngles.z == 180.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
            }

            if (base.referenceVector == Vector2.right)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
            }
        }

        if (transform.rotation.eulerAngles.z == 270.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                newProjectile0.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                newProjectile1.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
            }
        }

        setReflectorLaserColor();
        setReflectorHitFalseForProjectile();
    }


    public override void setReflectorLaserColor()
    {
        referenceProjectile.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;
        newProjectile0.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;
        newProjectile1.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;

        referenceProjectile.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;
        newProjectile0.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;
        newProjectile1.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;
    }

    public override void setReflectorHitFalseForProjectile()
    {
        base.setReflectorHitFalseForProjectile();
        newProjectile0.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile1.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }
}
