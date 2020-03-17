using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_LaserOrigin : MonoBehaviour
{
    public GameObject projectileSphere;

    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        DIAGONAL
    }

    public DIRECTION dir;

    private Vector3 laserStartDir = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {

        switch(dir)
        {
            case DIRECTION.UP:
                transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                laserStartDir = Vector3.up;
                break;

            case DIRECTION.DOWN:
                transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                laserStartDir = Vector3.down;
                break;

            case DIRECTION.LEFT:
                transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                laserStartDir = Vector3.left;
                break;

            case DIRECTION.RIGHT:
                transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                laserStartDir = Vector3.right;
                break;

            case DIRECTION.DIAGONAL:
                transform.rotation = Quaternion.AngleAxis(-45.0f, Vector3.forward);
                laserStartDir = transform.rotation * Vector3.up;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(transform.rotation.eulerAngles);
            GameObject projectile = Instantiate(projectileSphere, transform.position, Quaternion.identity);
            projectile.GetComponent<Proto_Projectile>().DirectionVector = laserStartDir;
        }
    }
}
