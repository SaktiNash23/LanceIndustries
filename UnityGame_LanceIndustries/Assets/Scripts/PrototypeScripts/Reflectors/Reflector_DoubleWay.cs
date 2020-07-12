using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_DoubleWay : Reflector
{
    private Reflector_SO reflectorDoubleWay_SO;
    private GameObject newProjectile0;
    private GameObject newProjectile1;

    private void Start()
    {
        reflectorDoubleWay_SO = base.reflectorBase_SO;
    }

    public void calculateLaser_DoubleWay(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);

        switch(/*transform.rotation.eulerAngles.z*/ Mathf.Round(transform.parent.Find("ReferencePoint").localEulerAngles.z))
        {
            case 0:
                if(base.referenceVector == Vector2.left)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;

                    newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;
                }
                break;

            case 90:
                if(base.referenceVector == Vector2.down)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;

                    newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                }
                break;

            case 180:
                if(base.referenceVector == Vector2.right)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;

                    newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;
                }
                break;

            case 270:
                if(base.referenceVector == Vector2.up)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;

                    newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
                }
                break;
        }

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();

        if (reflectorAnimationScript != null)
            reflectorAnimationScript.playDeflectAnimation(/*transform.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);

        Destroy(referenceProjectile);
    }

    public override void setReflectorLaserColor()
    {
        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                break;

            case ReflectorColor_Enum.BLUE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                break;

            case ReflectorColor_Enum.YELLOW:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                break;

            case ReflectorColor_Enum.WHITE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                break;
        }

        //referenceProjectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile0.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile1.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
    }

    public override void setReflectorHitFalseForProjectile()
    {
        //base.setReflectorHitFalseForProjectile();
        newProjectile0.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile1.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

}
