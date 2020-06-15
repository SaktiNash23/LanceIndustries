using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector_ThreeWay : Reflector
{
    private Reflector_SO reflectorThreeWay_SO;
    private GameObject newProjectile0;
    private GameObject newProjectile1;
    private GameObject newProjectile2;

    // Start is called before the first frame update
    void Start()
    {
        reflectorThreeWay_SO = base.reflectorBase_SO;
    }

    public void calculateLaser_ThreeWay(RaycastHit2D hitParam, GameObject projectile)
    {
        base.retrieveLaserProperties(hitParam, projectile);

        #region Three Way Code v1.0
        /*
        base.calculateLaser_Base();

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

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();
        */
        #endregion

        Debug.Log(referenceVector.ToString());
      
        switch (referenceVector.ToString())
        {
            case "(0.0, 1.0)":   //Vector2.up
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(1).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                newProjectile2 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                break;

            case "(0.0, -1.0)":  //Vector2.down
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(2).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;

                newProjectile2 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                break;

            case "(-1.0, 0.0)":  //Vector2.left
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(1).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(2).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                newProjectile2 = Instantiate(projectile, transform.parent.GetChild(3).transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                break;

            case "(1.0, 0.0)":   //Vector2.right
                newProjectile0 = Instantiate(projectile, transform.parent.GetChild(1).transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;

                newProjectile1 = Instantiate(projectile, transform.parent.GetChild(2).transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;

                newProjectile2 = Instantiate(projectile, transform.parent.GetChild(4).transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                break;
        }

        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();
        setReflectorHitFalseForProjectile();

        Destroy(referenceProjectile);

    }

    public override void setReflectorLaserColor()
    {
        //referenceProjectile.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;
        //newProjectile0.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;
        //newProjectile1.GetComponent<TrailRenderer>().material = reflectorThreeWay_SO.laserMaterialToChange;

        //referenceProjectile.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;
        //newProjectile0.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;
        //newProjectile1.GetComponent<SpriteRenderer>().color = reflectorThreeWay_SO.laserColorToChange;

        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.BLUE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.YELLOW:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                //Maybe in the future, change glow of laser here
                break;

            case ReflectorColor_Enum.WHITE:
                //referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                //Maybe in the future, change glow of laser here
                break;
        }

        //referenceProjectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile0.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile1.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
        newProjectile2.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
    }

    public override void setReflectorHitFalseForProjectile()
    {
        //base.setReflectorHitFalseForProjectile();
        newProjectile0.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile1.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        newProjectile2.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);

    }
}
