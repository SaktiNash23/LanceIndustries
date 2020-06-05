using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
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

    public void checkIfCorrectLaserHit(GameObject projectile)
    {
        if (isHitByLaser == false)
        {
            if(projectile.GetComponent<Proto_Projectile>().laserColor_Enum.ToString() == targetLaser.ToString())
            {
                Debug.Log("SAME COLOR TAG");
                isHitByLaser = true;
                isHitByCorrectLaser = true;
                GameManager.gameManagerInstance.updateEndPointStatus(true);

            }
            else
            {
                Debug.Log("Wrong LASER COLOR TAG");
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
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }
}
