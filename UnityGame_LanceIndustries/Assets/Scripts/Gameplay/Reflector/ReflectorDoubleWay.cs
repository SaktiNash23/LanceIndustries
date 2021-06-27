using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorDoubleWay : Reflector
{
    [Header("DOUBLE WAY")]
    [SerializeField] protected Transform laserBarrel;
    [SerializeField] protected Transform laserBarrel2;

    public override void CalculateLaser(Laser laser, RaycastHit2D hit)
    {
        ValidReflection();
        SpawnSpark(hit.point, normal.rotation);

        Laser spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, laserBarrel.position, laserBarrel.rotation);
        spawnedLaser.LaserColor = reflectorColor;
        spawnedLaser.RefreshLaserMaterialColor();
        Laser spawnedLaser2 = ObjectPooler.Instance.PopOrCreate(laserPrefab, laserBarrel2.position, laserBarrel2.rotation);
        spawnedLaser2.LaserColor = reflectorColor;
        spawnedLaser2.RefreshLaserMaterialColor();
        laser.Push();
    }

    // public void CalculateLaserDoubleWay(RaycastHit2D hitParam, Proto_Projectile projectile)
    // {
    //     base.RetrieveLaserProperties(hitParam, projectile);

    //     switch(/*transform.rotation.eulerAngles.z*/ Mathf.Round(transform.parent.Find("ReferencePoint").localEulerAngles.z))
    //     {
    //         case 0:
    //             if(base.referenceVector == Vector2.left)
    //             {
    //                 newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;

    //                 newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;
    //             }
    //             break;

    //         case 90:
    //             if(base.referenceVector == Vector2.down)
    //             {
    //                 newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;

    //                 newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //             }
    //             break;

    //         case 180:
    //             if(base.referenceVector == Vector2.right)
    //             {
    //                 newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;

    //                 newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;
    //             }
    //             break;

    //         case 270:
    //             if(base.referenceVector == Vector2.up)
    //             {
    //                 newProjectile0 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile0.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile0.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;

    //                 newProjectile1 = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile1.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile1.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;
    //             }
    //             break;
    //     }

    //     SetLaserColor();
    //     SparkAnimation.playDeflectAnimation();
    //     SetReflectorHitFalseForSpawnedProjectile();

    //     if (ReflectorAnimation != null)
    //         ReflectorAnimation.playDeflectAnimation(/*transform.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);

    //     Destroy(spawnedProjectile);
    // }
}
