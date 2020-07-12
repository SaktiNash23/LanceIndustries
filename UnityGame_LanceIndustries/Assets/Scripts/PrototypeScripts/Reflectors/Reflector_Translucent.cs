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
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
                }
                break;

            case 90:
                if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                }
                else if (base.referenceVector == Vector2.down)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;
                }
                break;

            case 180:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;
                }
                else if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                }
                break;

            case 270:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
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
        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                break;

            case ReflectorColor_Enum.BLUE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                break;

            case ReflectorColor_Enum.YELLOW:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                break;

            case ReflectorColor_Enum.WHITE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
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
