﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;
    public Material whiteMaterial;


    public enum targetLaserColor
    {
        RED,
        BLUE,
        YELLOW,
        WHITE
    };

    public targetLaserColor targetLaser;
    private bool isHitByLaser = false;
    private bool isHitByCorrectLaser = false;

    void Awake()
    {
        switch (targetLaser)
        {
            case targetLaserColor.RED:
                gameObject.GetComponent<SpriteRenderer>().color = redMaterial.color;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = blueMaterial.color;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = yellowMaterial.color;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = whiteMaterial.color;
                break;
        }
    }


    public void checkIfCorrectLaserHit(GameObject projectile)
    {
        if (isHitByLaser == false)
        {
            if (projectile.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color)
            {
                Debug.Log("Correct Laser Color Hit");
                isHitByLaser = true;
                isHitByCorrectLaser = true;
                GameManager.gameManagerInstance.updateEndPointStatus(true);
            }
            else
            {
                Debug.Log("Wrong Laser Color Hit");
                Debug.LogWarning("Projectile Color : " + projectile.GetComponent<SpriteRenderer>().color.ToString());
                Debug.LogWarning("End Point Color : " + gameObject.GetComponent<SpriteRenderer>().color.ToString());
                isHitByLaser = true;
                GameManager.gameManagerInstance.updateEndPointStatus(false);
            }
        }
    }


    public bool isHitByLaser_Accessor
    {
        get
        { return isHitByLaser; }

        set
        { isHitByLaser = value; }
    }

    public bool isHitByCorrectLaser_Accessor
    {
        get { return isHitByCorrectLaser; }

        set
        { isHitByCorrectLaser = value; }
    }

    public void Initialization()
    {
        switch (targetLaser)
        {
            case targetLaserColor.RED:
                gameObject.GetComponent<SpriteRenderer>().color = redMaterial.color;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = blueMaterial.color;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = yellowMaterial.color;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = whiteMaterial.color;
                break;
        }
    }
}