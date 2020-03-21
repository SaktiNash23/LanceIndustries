using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public Reflector_SO reflector_SO;
    public Material yellowMat;
    RaycastHit hit;

    private void Start()
    {
        //Initializes the reflector angle based on data stored in ScriptableObject
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, reflector_SO.zRotation);
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray mouseDownRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseDownRay, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.name.Contains("Reflector"))
                {
                    Debug.Log("HTI");
                }
            }
        }
    }

    public void calculateLaser(RaycastHit2D hitParam, GameObject projectile)
    {
        Vector3 tempVector = projectile.GetComponent<Proto_Projectile>().DirectionVector;

        if (projectile.GetComponent<Proto_Projectile>().ReflectorHit == true)
        {
            Debug.Log("REFLECTOR HIT");
            projectile.transform.position = hitParam.point;
        }

        if(reflector_SO.reflectDirTag == "DownRight")
        {
            if (tempVector == Vector3.up)
            {
                projectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
            }
            else if (tempVector == Vector3.left)
            {
                projectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
            }
        }

        if(reflector_SO.reflectDirTag == "UpLeft")
        {
            if (tempVector == Vector3.right)
            {
                projectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
            }

            if (tempVector == Vector3.down)
            {
                projectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
            }
        }

        if (reflector_SO.reflectDirTag == "DownLeft")
        {
            if (tempVector == Vector3.up)
            {
                projectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
            }

            if (tempVector == Vector3.right)
            {
                projectile.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
            }
        }

        if(reflector_SO.reflectDirTag == "UpRight")
        {
            if (tempVector == Vector3.down)
            {
                projectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
            }

            if (tempVector == Vector3.left)
            {
                projectile.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
            }
        }
        
        projectile.GetComponent<TrailRenderer>().material = yellowMat;
        projectile.GetComponent<Proto_Projectile>().Invoke("reflectorHitFalse", 0.02f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        #region Laser OnTriggerEnter2D code storage
        /*
        if (col.tag == "Laser")
        {
            
            //col.gameObject.transform.position = gameObject.transform.position;
            Vector3 tempVector = col.gameObject.GetComponent<Proto_Projectile>().DirectionVector;

            //Can deflect laser towards DOWN or RIGHT direction
            if (reflector_SO.reflectDirTag == "DownRight")
            {      
                if (tempVector == Vector3.up)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
                }

                if (tempVector == Vector3.left)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
                }
            }

            //Can deflect towards UP or LEFT direction
            if (reflector_SO.reflectDirTag == "UpLeft")
            {
                if (tempVector == Vector3.right)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
                }

                if (tempVector == Vector3.down)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
                }
            }

            //Can deflect laser towards DOWN or LEFT direction
            if(reflector_SO.reflectDirTag == "DownLeft")
            {
                if (tempVector == Vector3.up)
                {
                    if (col.gameObject.GetComponent<Proto_Projectile>().ReflectorHit == true)
                    {
                        Debug.Log("TRUE");
                        col.gameObject.transform.position = col.gameObject.GetComponent<Proto_Projectile>().hitStore.point;
                    }

                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
                    col.gameObject.GetComponent<TrailRenderer>().material = yellowMat;
                    col.gameObject.GetComponent<Proto_Projectile>().ReflectorHit = false;
                }

                if (tempVector == Vector3.right)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
                }
            }

            //Can deflect laser towards UP or RIGHT direction
            if(reflector_SO.reflectDirTag == "UpRight")
            {
                if (tempVector == Vector3.down)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
                }

                if (tempVector == Vector3.left)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
                }
            }

        }     
        */
        #endregion
    }
}
