using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_Split : Reflector
{
    private Reflector_SO reflectorSplit_SO;
    private GameObject newProjectile0;
    private GameObject newProjectile1;

    // Start is called before the first frame update
    void Start()
    {
        reflectorSplit_SO = GameManager.gameManagerInstance.allReflectorSO[3]; //Index 3: Split Reflector
    }

    public void calculateLaser_Split(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);

        if (transform.rotation.eulerAngles.z == 0.0f)
        {
            if (base.referenceVector == Vector2.down)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
            }
        }

        if (transform.rotation.eulerAngles.z == 90.0f)
        {
            if (base.referenceVector == Vector2.right)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
            }

            if (base.referenceVector == Vector2.down)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
            }
        }

        if (transform.rotation.eulerAngles.z == 180.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
            }

            if (base.referenceVector == Vector2.right)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
            }
        }

        if (transform.rotation.eulerAngles.z == 270.0f)
        {
            if (base.referenceVector == Vector2.up)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
            }
            else if (base.referenceVector == Vector2.left)
            {
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.identity);
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.identity);
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
            }
        }

        setReflectorLaserColor();
        setReflectorHitFalseForProjectile();

        //Destroy the original projectile at the end here
        Destroy(referenceProjectile);
    }

    public override void setReflectorLaserColor()
    {
        newProjectile0.GetComponent<TrailRenderer>().material = reflectorSplit_SO.laserMaterialToChange;
        newProjectile1.GetComponent<TrailRenderer>().material = reflectorSplit_SO.laserMaterialToChange;
    }

    public override void setReflectorHitFalseForProjectile()
    {
        newProjectile0.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile1.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }
}
