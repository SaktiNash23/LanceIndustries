using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public Reflector_SO reflector_SO;

    private void Start()
    {
        //Initializes the reflector angle based on data stored in ScriptableObject
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, reflector_SO.zRotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {    
        if (col.tag == "Laser")
        {
            col.gameObject.transform.position = gameObject.transform.position;
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
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
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
    }
}
