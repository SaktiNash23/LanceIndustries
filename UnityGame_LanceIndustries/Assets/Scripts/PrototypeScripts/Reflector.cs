using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public DIRECTION dir;

    void OnTriggerEnter(Collider col)
    {
      
        if(col.tag == "Laser")
        {
            if (dir == DIRECTION.RIGHT)
            {
                Vector3 tempVector = col.gameObject.GetComponent<Proto_Projectile>().DirectionVector;

                if (tempVector == Vector3.up)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
                }

                if (tempVector == Vector3.right)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
                }

                if (tempVector == Vector3.down)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
                }

                if (tempVector == Vector3.left)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
                }
            }

            if(dir == DIRECTION.LEFT)
            {
                Vector3 tempVector = col.gameObject.GetComponent<Proto_Projectile>().DirectionVector;

                if (tempVector == Vector3.up)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.left;
                }

                if (tempVector == Vector3.right)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.up;
                }

                if (tempVector == Vector3.down)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.right;
                }

                if (tempVector == Vector3.left)
                {
                    col.gameObject.GetComponent<Proto_Projectile>().DirectionVector = Vector3.down;
                }
            }   
        }
    }
}
