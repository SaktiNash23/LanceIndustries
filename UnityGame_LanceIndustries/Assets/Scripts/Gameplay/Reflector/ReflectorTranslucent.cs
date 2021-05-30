using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorTranslucent : Reflector
{
    [Header("TRANSLUCENT")]
    [SerializeField] protected Transform laserBarrel;

    public override void CalculateLaser(Laser laser, RaycastHit2D hit)
    {
        ValidReflection();
        SpawnSpark(hit.point, normal.rotation);

        laser.transform.right = Vector3.Reflect(laser.transform.right, normal.right);
        laser.transform.position = referencePoint.position;
        laser.LaserColor = reflectorColor;
        laser.RefreshLaserMaterialColor();
        StartCoroutine(laser.SetReflectorHitFalse(0.02f));
        Laser spawnedLaser = ObjectPooler.Instance.PopOrCreate(laserPrefab, laserBarrel.position, laserBarrel.rotation);
        spawnedLaser.LaserColor = reflectorColor;
        spawnedLaser.RefreshLaserMaterialColor();
        StartCoroutine(spawnedLaser.SetReflectorHitFalse(0.02f));
    }

    // public void calculateLaser_Translucent(RaycastHit2D hitParam, GameObject projectile)
    // {
    //     base.RetrieveLaserProperties(hitParam, projectile);
    //     base.CalculateLaserBase();

    //     switch(/*transform.rotation.eulerAngles.z*/ Mathf.Round(transform.parent.Find("ReferencePoint").localEulerAngles.z))
    //     {
    //         case 0:
    //             if (base.referenceVector == Vector2.down)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;
    //             }
    //             else if (base.referenceVector == Vector2.left)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;
    //             }
    //             break;

    //         case 90:
    //             if (base.referenceVector == Vector2.right)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //             }
    //             else if (base.referenceVector == Vector2.down)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(180.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.DOWN;
    //             }
    //             break;

    //         case 180:
    //             if (base.referenceVector == Vector2.up)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;
    //             }
    //             else if (base.referenceVector == Vector2.right)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(-90.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.RIGHT;
    //             }
    //             break;

    //         case 270:
    //             if (base.referenceVector == Vector2.up)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner0").transform.position, Quaternion.AngleAxis(0.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.UP;
    //             }
    //             else if (base.referenceVector == Vector2.left)
    //             {
    //                 newProjectile = Instantiate(projectile, transform.parent.Find("ProjectileSpawner1").transform.position, Quaternion.AngleAxis(90.0f, Vector3.forward));
    //                 newProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
    //                 newProjectile.GetComponent<Proto_Projectile>().laserDir = LASER_DIRECTION.LEFT;
    //             }
    //             break;
    //     }

    //     SetLaserColor();
    //     SparkAnimation.PlayDeflectAnimation();

    //     if (ReflectorAnimation != null)
    //         ReflectorAnimation.PlayDeflectAnimation(/*transform.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);

    //     SetReflectorHitFalseForSpawnedProjectile();

    // }
}
