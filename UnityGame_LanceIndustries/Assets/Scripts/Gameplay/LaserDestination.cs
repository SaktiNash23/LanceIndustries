using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestination : MonoBehaviour, ILaserInteractable
{
    [Header("REFERENCES")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("VISUAL")]
    [SerializeField] protected Sprite whiteOffSprite;
    [SerializeField] protected Sprite whiteOnSprite;
    [SerializeField] protected Sprite redOffSprite;
    [SerializeField] protected Sprite redOnSprite;
    [SerializeField] protected Sprite blueOffSprite;
    [SerializeField] protected Sprite blueOnSprite;
    [SerializeField] protected Sprite yellowOffSprite;
    [SerializeField] protected Sprite yellowOnSprite;

    public LASER_COLOR LaserColor { get; set; }
    public bool IsHitByLaser { get; set; }
    public bool IsHitByCorrectLaser { get; set; }

    private int onAnimationHash;

    protected void Awake()
    {
        onAnimationHash = Animator.StringToHash("On");
    }

    public void Initialization()
    {
        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                spriteRenderer.sprite = redOffSprite;
                break;

            case LASER_COLOR.BLUE:
                spriteRenderer.sprite = blueOffSprite;
                break;

            case LASER_COLOR.YELLOW:
                spriteRenderer.sprite = yellowOffSprite;
                break;

            case LASER_COLOR.WHITE:
                spriteRenderer.sprite = whiteOffSprite;
                break;
        }
    }

    public void Reset()
    {
        IsHitByLaser = IsHitByCorrectLaser = false;
        animator.SetBool(onAnimationHash, false);

        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                spriteRenderer.sprite = redOffSprite;
                break;

            case LASER_COLOR.BLUE:
                spriteRenderer.sprite = blueOffSprite;
                break;

            case LASER_COLOR.YELLOW:
                spriteRenderer.sprite = yellowOffSprite;
                break;

            case LASER_COLOR.WHITE:
                spriteRenderer.sprite = whiteOffSprite;
                break;
        }
    }

    public void On()
    {
        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                spriteRenderer.sprite = redOnSprite;
                break;

            case LASER_COLOR.BLUE:
                spriteRenderer.sprite = blueOnSprite;
                break;

            case LASER_COLOR.YELLOW:
                spriteRenderer.sprite = yellowOnSprite;
                break;

            case LASER_COLOR.WHITE:
                spriteRenderer.sprite = whiteOnSprite;
                break;
        }
    }

    public void checkIfCorrectLaserHit(GameObject projectile)
    {
        if (IsHitByLaser == false)
        {
            if (projectile.GetComponent<Laser>().LaserColor.ToString() == LaserColor.ToString())
            {
                IsHitByLaser = true;
                IsHitByCorrectLaser = true;
                GameManager.Instance.UpdateEndPointStatus(true);
            }
            else
            {
                IsHitByLaser = true;
                GameManager.Instance.UpdateEndPointStatus(false);
            }
        }
    }

    public void OnLaserOverlap(Laser laser, RaycastHit2D hit)
    {
        if(!IsHitByLaser)
        {
            if(laser.LaserColor == LaserColor)
            {
                animator.SetBool(onAnimationHash, true);
                IsHitByLaser = IsHitByCorrectLaser = true;
                GameManager.Instance.UpdateEndPointStatus(true);
            }
            else
            {
                IsHitByLaser = true;
                GameManager.Instance.UpdateEndPointStatus(false);
            }
        }

        laser.Push();
    }
}
