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

    public Sprite StartingPointRed;
    public Sprite StartingPointBlue;
    public Sprite StartingPointYellow;
    public Sprite StartingPointWhite;

    private GameObject[] allActiveReflectors;

    void OnMouseUp()
    {
        if (GameManager.gameManagerInstance.gameIsPaused == false && !GameManager.gameManagerInstance.AllCorrectLasersHaveReached)
        {
            if (GameManager.gameManagerInstance.beginCountDown == false)
            {
                GameManager.gameManagerInstance.Reset();
                GameManager.gameManagerInstance.beginCountDown = true; //Disable this line if you want to test without timer

                GameObject[] allStartingPoints = GameObject.FindGameObjectsWithTag("StartingPoint");

                for (int i = 0; i < allStartingPoints.Length; ++i)
                {
                    allStartingPoints[i].GetComponent<Proto_LaserOrigin>().shootLaser();
                }

            }
        }
    }

    void OnMouseOver()
    {
        if (GameManager.gameManagerInstance.gameIsPaused == false)
        {
            if (Input.GetMouseButtonDown(1))
            {
                shootLaser();
            }
        }
    }

    private void activateAnimatorComponentsOnReflectors()
    {
        allActiveReflectors = System.Array.Empty<GameObject>();
        allActiveReflectors = GameObject.FindGameObjectsWithTag("ReflectorGM");

        foreach(GameObject activeReflector in allActiveReflectors)
        {
            activeReflector.GetComponent<Animator>().enabled = true;
        }
    }

    #region Naughty Attributes Functions

    [Button("Shoot Laser")]
    public void shootLaser()
    {
        Vector3 projectileTargetRot = transform.rotation.eulerAngles - new Vector3(0f, 0f, 90f);

        GameObject projectile = LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Dequeue();
        projectile.transform.SetPositionAndRotation(transform.position + (transform.right * 0.4f), Quaternion.Euler(projectileTargetRot));

        projectile.SetActive(true);

        projectile.GetComponent<Proto_Projectile>().setLaserDirectionEnum(transform.rotation.eulerAngles.z);
        //projectile.transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z, Vector3.forward);      

        #region Starting Point Laser Color

        switch (laserColor_StartingPoint.ToString())
        {
            case "RED":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.RED;
                tempLaserColor = Color.red;
                break;

            case "BLUE":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.BLUE;
                tempLaserColor = Color.blue;
                break;

            case "YELLOW":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.YELLOW;
                tempLaserColor = Color.yellow;
                break;

            case "WHITE":
                projectile.GetComponent<Proto_Projectile>().laserColor_Enum = Proto_Projectile.LaserColor_Enum.WHITE;
                tempLaserColor = Color.white;
                break;
        }

        //projectile.GetComponent<SpriteRenderer>().material.color = tempLaserColor * colorIntensity;
        projectile.GetComponent<Proto_Projectile>().ChangeLaserMaterialColor();

        #endregion
    }

    #endregion

    public void Initialization()
    {
        switch (laserColor_StartingPoint)
        {
            case LaserColor_StartingPoint.RED:
                tempLaserColor = Color.red;
                GetComponent<SpriteRenderer>().sprite = StartingPointRed;
                break;

            case LaserColor_StartingPoint.BLUE:
                tempLaserColor = Color.blue;
                GetComponent<SpriteRenderer>().sprite = StartingPointBlue;
                break;

            case LaserColor_StartingPoint.YELLOW:
                tempLaserColor = Color.yellow;
                GetComponent<SpriteRenderer>().sprite = StartingPointYellow;
                break;

            case LaserColor_StartingPoint.WHITE:
                tempLaserColor = Color.white;
                GetComponent<SpriteRenderer>().sprite = StartingPointWhite;
                break;
        }
    }  
}
