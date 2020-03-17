using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{

    public Material redMat;
    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Laser")
        {
            col.gameObject.transform.position = gameObject.transform.position;
            Vector3 tempVector = col.gameObject.GetComponent<Proto_Projectile>().DirectionVector;

            //Can deflect UP and LEFT lasers
            if (rend.flipX == false && rend.flipY == true)
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

            //Can deflect DOWN & RIGHT lasers
            if (rend.flipX == true && rend.flipY == false)
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

            //Can deflect UP and RIGHT lasers
            if(rend.flipX == true && rend.flipY == true)
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

            //Can deflect DOWN and LEFT lasers
            if(rend.flipX == false && rend.flipY == false)
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
