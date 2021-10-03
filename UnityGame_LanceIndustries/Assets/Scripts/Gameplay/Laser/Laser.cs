using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public enum LASER_COLOR
{
    DEFAULT,
    RED,
    BLUE,
    YELLOW,
    WHITE
}

public enum LASER_DIRECTION
{
    DEFAULT,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public class Laser : PoolObject
{
    [Header("Settings")]
    [SerializeField] protected float initialProjectileSpeed;
    [SerializeField] protected float minProjectileSpeed;
    [SerializeField] protected float speedReductionUponReflected;

    [Header("References")]
    [SerializeField] protected SpriteRenderer spriteRend;

    public LASER_COLOR LaserColor { get; set; }
    public ILaserInteractable LastInteractable { get; set; }

    private float currentProjectileSpeed;

    #region MonoBehaviour
    private void OnEnable()
    {
        LastInteractable = null;
        currentProjectileSpeed = initialProjectileSpeed;
    }

    private void OnDisable()
    {
        GameManager.Instance.Lasers.Remove(this);
        GameManager.Instance.EndGameCheck();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, (transform.right * currentProjectileSpeed * Time.fixedDeltaTime).magnitude);
            if (hit)
            {
                ILaserInteractable laserInteractable = hit.transform.GetComponentInParent<ILaserInteractable>();

                if (laserInteractable != null && LastInteractable != laserInteractable)
                {
                    LastInteractable = laserInteractable;
                    currentProjectileSpeed = Mathf.Clamp(currentProjectileSpeed - speedReductionUponReflected, minProjectileSpeed, initialProjectileSpeed);
                    laserInteractable.OnLaserOverlap(this, hit);
                    return;
                }
            }

            transform.position += transform.right * currentProjectileSpeed * Time.fixedDeltaTime;
        }
    }
    #endregion

    public void RefreshLaserMaterialColor()
    {
        Color targetColor = Color.white;

        switch (LaserColor)
        {
            case LASER_COLOR.RED:
                targetColor = Color.red;
                break;
            case LASER_COLOR.BLUE:
                targetColor = Color.blue;
                break;
            case LASER_COLOR.YELLOW:
                targetColor = Color.yellow;
                break;
            case LASER_COLOR.WHITE:
                targetColor = Color.white;
                break;
        }

        spriteRend.color = targetColor * 3.0f;
    }

    public void ReduceLaserSpeed()
    {
        currentProjectileSpeed = Mathf.Clamp(currentProjectileSpeed - speedReductionUponReflected, minProjectileSpeed, initialProjectileSpeed);
    }
}
