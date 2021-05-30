using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorThreeWay : Reflector
{
    [Header("THREE WAY")]
    [SerializeField] protected Collider2D reflector;
    [SerializeField] protected Collider2D reflector2;
    [SerializeField] protected Collider2D reflector3;
    [SerializeField] protected Collider2D reflector4;
    [SerializeField] protected Transform barrel;
    [SerializeField] protected Transform barrel2;
    [SerializeField] protected Transform barrel3;
    [SerializeField] protected Transform barrel4;


    public override void CalculateLaser(Laser laser, RaycastHit2D hit)
    {
        Laser spawnedLaser = null;
        Laser spawnedLaser2 = null;
        Laser spawnedLaser3 = null;

        Collider2D hitReflector = hit.collider;

        if (hitReflector == reflector)
        {
            spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel2.transform.position, barrel2.transform.rotation);
            spawnedLaser2 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel3.transform.position, barrel3.transform.rotation);
            spawnedLaser3 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel4.transform.position, barrel4.transform.rotation);
        }
        else if (hitReflector == reflector2)
        {
            spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.transform.position, barrel.transform.rotation);
            spawnedLaser2 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel3.transform.position, barrel3.transform.rotation);
            spawnedLaser3 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel4.transform.position, barrel4.transform.rotation);
        }
        else if (hitReflector == reflector3)
        {
            spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.transform.position, barrel.transform.rotation);
            spawnedLaser2 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel2.transform.position, barrel2.transform.rotation);
            spawnedLaser3 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel4.transform.position, barrel4.transform.rotation);
        }
        else
        {
            spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel.transform.position, barrel.transform.rotation);
            spawnedLaser2 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel2.transform.position, barrel2.transform.rotation);
            spawnedLaser3 = ObjectPooler.Instance.PopOrCreate(laserPrefab, barrel3.transform.position, barrel3.transform.rotation);
        }

        ValidReflection();
        SpawnSpark(hit.point, normal.rotation);

        spawnedLaser.LaserColor = reflectorColor;
        spawnedLaser.RefreshLaserMaterialColor();
        StartCoroutine(spawnedLaser.SetReflectorHitFalse(0.02f));
        spawnedLaser2.LaserColor = reflectorColor;
        spawnedLaser2.RefreshLaserMaterialColor();
        StartCoroutine(spawnedLaser2.SetReflectorHitFalse(0.02f));
        spawnedLaser3.LaserColor = reflectorColor;
        spawnedLaser3.RefreshLaserMaterialColor();
        StartCoroutine(spawnedLaser3.SetReflectorHitFalse(0.02f));

        laser.Push();
    }

    // public void calculateLaser_ThreeWay(RaycastHit2D hitParam, GameObject projectile)
    // {
    //     base.RetrieveLaserProperties(hitParam, projectile);

    //     #region Three Way Code v2.0
    //     /*
    //     switch (referenceVector.ToString()) //Using reference vector here instead of rotation cause Three Way Reflector can't be rotated by player
    //     {
    //         #region Vector2.up
    //         case "(0.0, 1.0)":   //Vector2.up
    //             //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));       
    //             newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //             newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

    //             //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;

    //             //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //             break;
    //         #endregion

    //         #region Vector2.down
    //         case "(0.0, -1.0)":  //Vector2.down
    //             //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

    //             //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;

    //             //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //             break;
    //         #endregion

    //         #region Vector2.left
    //         case "(-1.0, 0.0)":  //Vector2.left
    //             //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //             newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //             newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

    //             //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

    //             //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //             newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //             break;
    //         #endregion

    //         #region Vector2.right
    //         case "(1.0, 0.0)":   //Vector2.right
    //             //newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //             newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //             newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;

    //             //newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //             newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;

    //             //newProjectile2 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //             newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //             newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //             break;
    //         #endregion      
    //     }
    //     */
    //     #endregion

    //     if (LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Count > 0)
    //     {
    //         switch (projectile.GetComponent<Proto_Projectile>().laserDir)
    //         {
    //             #region INCOMING LASER DIRECTION: UP
    //             case LASER_DIRECTION.UP:
    //                 newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;

    //                 newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;

    //                 newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile2.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //                 break;
    //             #endregion

    //             #region INCOMING LASER DIRECTION: DOWN
    //             case LASER_DIRECTION.DOWN:
    //                 newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;

    //                 newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;

    //                 newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile2.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //                 break;
    //             #endregion

    //             #region INCOMING LASER DIRECTION: LEFT
    //             case LASER_DIRECTION.LEFT:
    //                 newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;

    //                 newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;

    //                 newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner2").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile2.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;
    //                 break;
    //             #endregion

    //             #region INCOMING LASER DIRECTION: RIGHT
    //             case LASER_DIRECTION.RIGHT:
    //                 newProjectile0 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile0.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;

    //                 newProjectile1 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile1.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;

    //                 newProjectile2 = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
    //                 newProjectile2.transform.SetPositionAndRotation(transform.parent.Find("ProjectileSpawner3").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile2.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile2.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //                 break;
    //             #endregion

    //             default:
    //                 break;
    //         }

    //         newProjectile0.SetActive(true);
    //         newProjectile1.SetActive(true);
    //         newProjectile2.SetActive(true);

    //         SetLaserColor();
    //         SparkAnimation.PlayDeflectAnimation();
    //         SetReflectorHitFalseForSpawnedProjectile();

    //         if (ReflectorAnimation != null)
    //             ReflectorAnimation.PlayDeflectAnimation(transform.rotation.eulerAngles.z);

    //         hitProjectile.GetComponent<Proto_Projectile>().returnLaserToPool(hitProjectile);

    //         hitProjectile.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("Laser Pool Out of Stock");
    //         GameManager.gameManagerInstance.currentWindowTime_Accessor = 0.0f;
    //         GameManager.gameManagerInstance.findAndReturnLasersToPool();
    //         //LaserPooler.instance_LaserPoolList.addLasersToPool(); //Readds lasers to the pool
    //     }
    // }
}
