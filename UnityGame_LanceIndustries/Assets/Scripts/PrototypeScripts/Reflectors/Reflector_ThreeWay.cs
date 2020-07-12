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

        #region Three Way Code v2.0
        /*
        switch (referenceVector.ToString()) //Using reference vector here instead of rotation cause Three Way Reflector can't be rotated by player
        {
            #region Vector2.up
            case "(0.0, 1.0)":   //Vector2.up
                //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));       
                newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

                //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;

                //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                break;
            #endregion

            #region Vector2.down
            case "(0.0, -1.0)":  //Vector2.down
                //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

                //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;

                //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                break;
            #endregion

            #region Vector2.left
            case "(-1.0, 0.0)":  //Vector2.left
                //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

                //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

                //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                break;
            #endregion

            #region Vector2.right
            case "(1.0, 0.0)":   //Vector2.right
                //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

                //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

                //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                break;
            #endregion      
        }
        */
        #endregion

        if (LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Count > 0)
        {
            switch (projectile.GetComponent<Proto_Projectile>().laserDirection)
            {
                #region INCOMING LASER DIRECTION: UP
                case Proto_Projectile.LaserDirection.UP:
                    newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;

                    newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;

                    newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile2.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                    break;
                #endregion

                #region INCOMING LASER DIRECTION: DOWN
                case Proto_Projectile.LaserDirection.DOWN:
                    newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;

                    newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;

                    newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile2.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                    break;
                #endregion

                #region INCOMING LASER DIRECTION: LEFT
                case Proto_Projectile.LaserDirection.LEFT:
                    newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;

                    newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;

                    newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
                    newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    newProjectile2.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
                    break;
                #endregion

                #region INCOMING LASER DIRECTION: RIGHT
                case Proto_Projectile.LaserDirection.RIGHT:
                    newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
                    newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    newProjectile0.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;

                    newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
                    newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    newProjectile1.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;

                    newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
                    newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
                    newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    newProjectile2.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                    break;
                #endregion

                default:
                    break;
            }

            newProjectile0.SetActive(true);
            newProjectile1.SetActive(true);
            newProjectile2.SetActive(true);

            setReflectorLaserColor();
            sparkAnimationScript.playDeflectAnimation();
            setReflectorHitFalseForProjectile();

            if (reflectorAnimationScript != null)
                reflectorAnimationScript.playDeflectAnimation(transform.rotation.eulerAngles.z);

            referenceProjectile.GetComponent<Proto_Projectile>().returnLaserToPool(referenceProjectile);

            referenceProjectile.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Laser Pool Out of Stock");
            GameManager.gameManagerInstance.currentWindowTime_Accessor = 0.0f;
            GameManager.gameManagerInstance.findAndReturnLasersToPool();
            //LaserPooler.instance_LaserPoolList.addLasersToPool(); //Readds lasers to the pool
        }
    }

    public override void setReflectorLaserColor()
    {
        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                break;
                
            case ReflectorColor_Enum.BLUE:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                break;

            case ReflectorColor_Enum.YELLOW:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                break;

            case ReflectorColor_Enum.WHITE:
                newProjectile0.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile1.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                newProjectile2.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                break;
                
            default:
                break;
        }

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
