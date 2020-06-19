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

        #region Double Way Code v1.0
        /*
        base.calculateLaser_Base();

        switch(transform.rotation.eulerAngles.z)
        {
            case 0.0f:
                if (base.referenceVector == Vector2.down)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                break;

            case 90.0f:
                if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                else if (base.referenceVector == Vector2.down)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                break;

            case 180.0f:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                else if (base.referenceVector == Vector2.right)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                break;

            case 270.0f:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                break;
        }
        */
        #endregion

        switch(transform.rotation.eulerAngles.z)
        {
            case 0.0f:
                if(base.referenceVector == Vector2.left)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                break;

            case 90.0f:
                if(base.referenceVector == Vector2.down)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                break;

            case 180.0f:
                if(base.referenceVector == Vector2.right)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                break;

            case 270.0f:
                if(base.referenceVector == Vector2.up)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                break;
        }

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();

        if (reflectorAnimationScript != null)
            reflectorAnimationScript.playDeflectAnimation(transform.rotation.eulerAngles.z);

        Destroy(referenceProjectile);
    }

    public override void setReflectorLaserColor()
    {
        //referenceProjectile.GetComponent<TrailRenderer>().material = reflectorDoubleWay_SO.laserMaterialToChange;
        //newProjectile.GetComponent<TrailRenderer>().material = reflectorDoubleWay_SO.laserMaterialToChange;

        //referenceProjectile.GetComponent<SpriteRenderer>().color = reflectorDoubleWay_SO.laserColorToChange;
        //newProjectile.GetComponent<SpriteRenderer>().color = reflectorDoubleWay_SO.laserColorToChange;

        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.BLUE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.YELLOW:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.WHITE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                //Maybe in the future, change glow of laser here
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
