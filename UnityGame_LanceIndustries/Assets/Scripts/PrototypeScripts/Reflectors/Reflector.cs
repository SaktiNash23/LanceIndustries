using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

    public class Reflector : MonoBehaviour
    {
        private Reflector_SO reflectorBase_SO;

        protected Vector2 referenceVector;
        protected RaycastHit2D referenceHitParam;

        protected GameObject referenceProjectile;

        void Start()
        {
            reflectorBase_SO = GameManager.gameManagerInstance.allReflectorSO[0]; //Index 0: Base Reflector
        }

        public void retrieveLaserProperties(RaycastHit2D hitParam, GameObject projectile)
        {
            referenceVector = projectile.GetComponent<Proto_Projectile>().DirectionVector;
            referenceHitParam = hitParam;
            referenceProjectile = projectile;
        }

        public void calculateLaser_Base()
        {
            if (referenceProjectile.GetComponent<Proto_Projectile>().ReflectorHit == true)
            {
                referenceProjectile.transform.position = referenceHitParam.point;
            }

            if (transform.rotation.eulerAngles.z == 0.0f)
            {
                if (referenceVector == Vector2.down)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }

                if (referenceVector == Vector2.left)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector2.up;
                }
            }

            if (transform.rotation.eulerAngles.z == 90.0f)
            {
                if (referenceVector == Vector2.right)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
                }

                if (referenceVector == Vector2.down)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }
            }

            if (transform.rotation.eulerAngles.z == 180.0f)
            {
                if (referenceVector == Vector2.up)
                {
                    referenceProjectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector2.left;
                }

                if (referenceVector == Vector2.right)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
            }

            if (transform.rotation.eulerAngles.z == 270.0f)
            {
                if (referenceVector == Vector2.up)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.right;
                }
                else if (referenceVector == Vector2.left)
                {
                    referenceProjectile.GetComponent<Proto_Projectile>().DirectionVector = Vector2.down;
                }
            }

            //referenceProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        }


        public void calculateLaser_Basic(RaycastHit2D hitParam, GameObject projectile)
        {
            retrieveLaserProperties(hitParam, projectile);
            referenceProjectile.transform.position = hitParam.point;
            calculateLaser_Base();
            setReflectorLaserColor();
            setReflectorHitFalseForProjectile();
        }

        public virtual void setReflectorLaserColor()
        {
            referenceProjectile.GetComponent<TrailRenderer>().material = reflectorBase_SO.laserMaterialToChange;
        }

        public virtual void setReflectorHitFalseForProjectile()
        {
            referenceProjectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
        }
    }
