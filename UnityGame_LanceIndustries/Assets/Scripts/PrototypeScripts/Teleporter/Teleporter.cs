﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private bool isPairValid = false;
    private GameObject[] teleporterPair;

    public void teleportLaser(GameObject projectileToTeleport, GameObject hitTeleporter)
    {
        //Check which set of teleporter is it
        //Find if there is a pair of teleporter of the same set
        //If above conditions valid, find the other teleporter in the set that was not hit
        //Adjust the properties of the laser such as direction and move it to the other teleporter

        isPairValid = checkForTeleporterPair(hitTeleporter.gameObject.tag);

        if(isPairValid == true)
        {
            for (int i = 0; i < teleporterPair.Length; ++i)
            {
                if(teleporterPair[i].GetInstanceID() != hitTeleporter.GetInstanceID())
                {
                    //Debug.Log("Found the other teleporter");

                    switch(teleporterPair[i].transform.rotation.eulerAngles.z)
                    {
                        case 0.0f://Laser Direction: RIGHT
                            projectileToTeleport.transform.position = teleporterPair[i].transform.position + new Vector3(0.1f, 0.0f, 0.0f);
                            projectileToTeleport.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.right;
                            projectileToTeleport.transform.rotation = Quaternion.AngleAxis(-90.0f, Vector3.forward);
                            break;

                        case 90.0f://Laser Direction: UP
                            projectileToTeleport.transform.position = teleporterPair[i].transform.position + new Vector3(0.0f, 0.1f, 0.0f);
                            projectileToTeleport.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.up;
                            projectileToTeleport.transform.rotation = Quaternion.AngleAxis(0.0f, Vector3.forward);
                            break;

                        case 180.0f://Laser Direction: LEFT
                            projectileToTeleport.transform.position = teleporterPair[i].transform.position + new Vector3(-0.1f, 0.0f, 0.0f);
                            projectileToTeleport.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.left;
                            projectileToTeleport.transform.rotation = Quaternion.AngleAxis(90.0f, Vector3.forward);
                            break;

                        case 270.0f://Laser Direction: DOWN
                            projectileToTeleport.transform.position = teleporterPair[i].transform.position + new Vector3(0.0f, -0.1f, 0.0f);
                            projectileToTeleport.gameObject.GetComponent<Proto_Projectile>().directionVector = Vector2.down;
                            projectileToTeleport.transform.rotation = Quaternion.AngleAxis(180.0f, Vector3.forward);
                            break;
                    }
                }
            }   
        }
    }

    private bool checkForTeleporterPair(string teleporterSetTag)
    {
        teleporterPair = GameObject.FindGameObjectsWithTag(teleporterSetTag);

        if(teleporterPair.Length == 2)
        {
            //Debug.Log("There is a pair");
            return true;
        }
        else
        {
            Debug.LogError("No paired teleporter. Make sure a set of teleporter only has 2 in a set");
            return false;
        }
    }

    public void SetRotation(SNAPPING_DIR dir)
    {
        switch (dir)
        {
            case SNAPPING_DIR.LEFT:
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.position += new Vector3(0.08f, 0.0f, 0.0f);
                break;
            case SNAPPING_DIR.RIGHT:
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                transform.position += new Vector3(-0.10f, 0.02f, 0.0f);
                break;
            case SNAPPING_DIR.UP:
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                transform.position += new Vector3(0.0f, -0.10f, 0.0f);
                break;
            case SNAPPING_DIR.DOWN:
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                transform.position += new Vector3(0.0f, 0.12f, 0.0f);
                break;
        }
    }
}