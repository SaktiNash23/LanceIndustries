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

    public Sprite endPoint_Red;
    public Sprite endPoint_Red_ON;
    public Sprite endPoint_Blue;
    public Sprite endPoint_Blue_ON;
    public Sprite endPoint_Yellow;
    public Sprite endPoint_Yellow_ON;
    public Sprite endPoint_White;
    public Sprite endPoint_White_ON;

    private Animator endPointAnim;

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
                endPointAnim.SetBool("EndPointOn", true);
                //turnOnEndPoint();//Change the sprite to the ON version

            }
            else
            {
                Debug.Log("Wrong LASER COLOR TAG");

                #region End Point Behaviour

                isHitByLaser = true;
                GameManager.gameManagerInstance.updateEndPointStatus(false);

                #endregion

                //Enable / Uncomment the code in End Point Behaviour if you want the End Point to detect any laser only once, then stop detecting any future lasers that might hit
                //Disable / Comment the code in End Point Behaviour if you want the End Point to continue detecting any laser, even if the laser that hits it is wrong

            }
        }
    }

    //Change sprite back to OFF version
    public void resetEndPoint()
    {
        endPointAnim.SetBool("EndPointOn", false);

        switch(targetLaser)
        {
            case targetLaserColor.RED:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Red;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Blue;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Yellow;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_White;
                break;
        }
    }

    //Change the sprite to the ON version
    public void turnOnEndPoint()
    {
        switch (targetLaser)
        {
            case targetLaserColor.RED:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Red_ON;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Blue_ON;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Yellow_ON;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_White_ON;
                break;
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
        endPointAnim = GetComponent<Animator>();

        switch (targetLaser)
        {
            case targetLaserColor.RED:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Red;
                break;

            case targetLaserColor.BLUE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Blue;
                break;

            case targetLaserColor.YELLOW:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_Yellow;
                break;

            case targetLaserColor.WHITE:
                gameObject.GetComponent<SpriteRenderer>().sprite = endPoint_White;
                break;
        }
    }
}
