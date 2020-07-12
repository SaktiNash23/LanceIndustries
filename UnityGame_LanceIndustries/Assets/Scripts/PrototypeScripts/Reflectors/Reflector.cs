using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Reflector : MonoBehaviour
{
    public Reflector_SO reflectorBase_SO;//ATTN: Assign the correct ScriptableObject in the inspector according to the type of reflector
    public SparkAnimation sparkAnimationScript;//ATTN: Assign the correct sparkAnimator gameobject in the inspector according to the type of reflector / spark
    public ReflectorAnimation reflectorAnimationScript; //ATTN: Assign the correct reflectorAnimator gameobject in the inspector to the type of reflector

    public enum ReflectorColor_Enum
    {
        RED,
        BLUE,
        YELLOW,
        WHITE
    };

    public ReflectorColor_Enum reflectorColor_Enum;
    
    protected Vector2 referenceVector;
    protected RaycastHit2D referenceHitParam;
    protected GameObject referenceProjectile;
        
    void Start()
    {
        if(reflectorBase_SO == null)
        {
            Debug.LogWarning("No Scriptable Object has been assigned to this reflector. Please set the SO in the editor");
        }

        if(sparkAnimationScript == null)
        {
            Debug.LogWarning("Spark Animation script is not inserted. Please insert Spark Animation script in the editor");
        }

        if(reflectorAnimationScript == null)
        {
            Debug.LogWarning("Reflector Animation script is not inserted. Please insert Reflector Animation script in the editor");
        }

    }

    public void retrieveLaserProperties(RaycastHit2D hitParam, GameObject projectile)
    {
        referenceVector = projectile.GetComponent<Proto_Projectile>().directionVector;
        referenceHitParam = hitParam;
        referenceProjectile = projectile;
    }

    public void calculateLaser_Base()
    {
        if (referenceProjectile.GetComponent<Proto_Projectile>().ReflectorHit == true)
        {
            referenceProjectile.transform.position = referenceHitParam.point;
        }

        switch (/*Mathf.Round(transform.parent.rotation.eulerAngles.z)*/ Mathf.Round(transform.parent.Find("ReferencePoint").localEulerAngles.z))
        {
            case 0:
                if (referenceVector == Vector2.down)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                }
                else if (referenceVector == Vector2.left)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;
                }
                break;

            case 90:
                if (referenceVector == Vector2.right)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().directionVector = Vector3.up;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.UP;
                }
                else if (referenceVector == Vector2.down)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
                }
                break;

            case 180:
                if (referenceVector == Vector2.up)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.LEFT;
                }
                else if (referenceVector == Vector2.right)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;
                }
                break;

            case 270:
                if (referenceVector == Vector2.up)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.RIGHT;
                }
                else if (referenceVector == Vector2.left)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                    referenceProjectile.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                    referenceProjectile.GetComponent<Proto_Projectile>().laserDirection = Proto_Projectile.LaserDirection.DOWN;
                }
                break;

            default:
                break;
        }

        referenceProjectile.transform.position = transform.parent.Find("ReferencePoint").transform.position; //Aligns the position of the laser so that it is always in the center
    }

    public void calculateLaser_Basic(RaycastHit2D hitParam, GameObject projectile)
    {
        retrieveLaserProperties(hitParam, projectile);
        referenceProjectile.transform.position = hitParam.point;
        calculateLaser_Base();
        setReflectorLaserColor();
        sparkAnimationScript.playDeflectAnimation();

        if (reflectorAnimationScript != null)
            reflectorAnimationScript.playDeflectAnimation(/*transform.parent.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);

        setReflectorHitFalseForProjectile();
    }

    public virtual void setReflectorLaserColor()
    {
        switch (reflectorColor_Enum)
        {
            case ReflectorColor_Enum.RED:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                        break;

            case ReflectorColor_Enum.BLUE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                break;

            case ReflectorColor_Enum.YELLOW:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                break;

            case ReflectorColor_Enum.WHITE:
                referenceProjectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                break;
        }

        referenceProjectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();
    }

    public virtual void setReflectorHitFalseForProjectile()
    {
        referenceProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

    public void playInvalidHitAnimation()
    {
        reflectorAnimationScript.playInvalidAnimation(/*transform.parent.rotation.eulerAngles.z*/ transform.parent.Find("ReferencePoint").localEulerAngles.z);
        Debug.Log("INVALID");
    }

}
