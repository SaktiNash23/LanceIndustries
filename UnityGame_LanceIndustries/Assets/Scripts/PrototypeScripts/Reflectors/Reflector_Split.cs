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
        reflectorSplit_SO = base.reflectorBase_SO;
    }

    public void calculateLaser_Split(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);

        switch(transform.rotation.eulerAngles.z)
        {
            case 0.0f:
                if (base.referenceVector == Vector2.down)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
                break;

            case 90.0f:
                if (base.referenceVector == Vector2.right)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                else if (base.referenceVector == Vector2.down)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                break;

            case 180.0f:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                else if (base.referenceVector == Vector2.right)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
                break;

            case 270.0f:
                if (base.referenceVector == Vector2.up)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                else if (base.referenceVector == Vector2.left)
                {
                    newProjectile0 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                    newProjectile1 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
                break;
        }


        #region Old Split Reflector Code
        /*
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
        */
        #endregion

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();

        //Destroy the original projectile at the end here
        Destroy(referenceProjectile);
    }

    public override void setReflectorLaserColor()
    {
        newProjectile0.GetComponent<TrailRenderer>().material = reflectorSplit_SO.laserMaterialToChange;
        newProjectile1.GetComponent<TrailRenderer>().material = reflectorSplit_SO.laserMaterialToChange;

        newProjectile0.GetComponent<SpriteRenderer>().color = reflectorSplit_SO.laserColorToChange;
        newProjectile1.GetComponent<SpriteRenderer>().color = reflectorSplit_SO.laserColorToChange;

        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.BLUE:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.YELLOW:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.WHITE:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                //Maybe in the future, change glow of laser here
                break;
        }
    }

    public override void setReflectorHitFalseForProjectile()
    {
        newProjectile0.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile1.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }
}
