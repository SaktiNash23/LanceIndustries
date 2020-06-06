using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Proto_LaserOrigin : MonoBehaviour
{
    [ShowAssetPreview]
    public GameObject projectileSphere;

    public enum DIRECTION
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        DIAGONAL
    }
    public enum LaserColor_StartingPoint
    {
        RED,
        BLUE,
        YELLOW,
        WHITE
    };

    [InfoBox("Use this to set the direction that the origin will fire the laser in.", EInfoBoxType.Normal)]
    public DIRECTION dir;

    public LaserColor_StartingPoint laserColor_StartingPoint;

    private Vector3 laserStartDir = Vector3.zero;

    private Color tempLaserColor;
    public float colorIntensity;

    public Sprite RedIcon;
    public Sprite BlueIcon;
    public Sprite YellowIcon;
    public Sprite WhiteIcon;

    // Disable/Remove Awake() once Initialization function is already implemented

    void OnMouseUp()
    {
        if (GameManager.gameManagerInstance.beginCountDown == false)
        {
            GameManager.gameManagerInstance.Reset();
            GameManager.gameManagerInstance.beginCountDown = true;

            //shootLaser();
            GameObject[] allStartingPoints = GameObject.FindGameObjectsWithTag("StartingPoint");

            for (int i = 0; i < allStartingPoints.Length; ++i)
            {
                allStartingPoints[i].GetComponent<Proto_LaserOrigin>().shootLaser();
            }

        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            shootLaser();
        }
    }

    #region Naughty Attributes Functions

    [Button("Shoot Laser")]
    public void shootLaser()
    {
        Vector3 projectileTargetRot = transform.rotation.eulerAngles - new Vector3(0f, 0f, 90f);
        GameObject projectile = Instantiate(projectileSphere, transform.position, Quaternion.Euler(projectileTargetRot));
        projectile.GetComponent<Proto_Projectile>().DirectionVector = projectile.transform.up;
        //projectile.transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward);      

        #region Starting Point Laser Color

        switch (laserColor_StartingPoint.ToString())
        {
            case "RED":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                break;

            case "BLUE":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                break;

            case "YELLOW":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                break;

            case "WHITE":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                break;
        }

        projectile.GetComponent<SpriteRenderer>().material.color = tempLaserColor * colorIntensity;

        #endregion
    }

    [Button("Change Direction of Origin Laser")]
    private void changeLaserDirection()
    {
        switch (dir)
        {
            case DIRECTION.UP:
                laserStartDir = Vector2.right;
                dir = DIRECTION.RIGHT;
                //transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                break;

            case DIRECTION.RIGHT:
                laserStartDir = Vector2.down;
                dir = DIRECTION.DOWN;
                //transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                break;

            case DIRECTION.DOWN:
                laserStartDir = Vector2.left;
                dir = DIRECTION.LEFT;
                //transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                break;

            case DIRECTION.LEFT:
                laserStartDir = Vector2.up;
                dir = DIRECTION.UP;
                //transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                break;

            default:
                break;
        }

    }

    #endregion


    public void Initialization()
    {
        switch (laserColor_StartingPoint)
        {
            case LaserColor_StartingPoint.RED:
                tempLaserColor = Color.red;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = RedIcon;
                break;

            case LaserColor_StartingPoint.BLUE:
                tempLaserColor = Color.blue;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = BlueIcon;
                break;

            case LaserColor_StartingPoint.YELLOW:
                tempLaserColor = Color.yellow;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = YellowIcon;
                break;

            case LaserColor_StartingPoint.WHITE:
                tempLaserColor = Color.white;
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = WhiteIcon;
                break;
        }
    }
}
