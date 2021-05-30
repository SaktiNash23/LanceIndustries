using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum REFLECTOR_TYPE
{
    DEFAULT = 0,
    BASIC = 1,
    TRANSLUCENT = 2,
    DOUBLE_WAY = 3,
    THREE_WAY = 4
}

public class Reflector : PoolObject, ILaserInteractable
{
    [Header("VISUAL SETTINGS")]
    [SerializeField] protected float validReflectionAnimationAngleChange = 15.0f;
    [SerializeField] protected float validReflectionAnimationDuration = 0.1f;

    [Header("SPRITES")]
    [SerializeField] protected Sprite redColorSprite;
    [SerializeField] protected Sprite blueColorSprite;
    [SerializeField] protected Sprite yellowColorSprite;
    [SerializeField] protected Sprite whiteColorSprite;

    [Header("REFERENCES")]
    [SerializeField] protected SpriteRenderer spriteRend;
    [SerializeField] protected Transform referencePoint;
    [SerializeField] protected Collider2D laserDetectorCol;
    [SerializeField] protected Transform normal;

    [Header("PREFABS")]
    [SerializeField] protected Laser laserPrefab;

    protected LASER_COLOR reflectorColor;
    protected Vector2 referenceVector;

    public bool Interactable { get; private set; } = true;
    public Collider2D LaserDetectorCol { get { return laserDetectorCol; } }

    public Proto_Grid OccupiedGridOutline { get; private set; }

    private Laser hitProjectile;

    public void Initialization(LASER_COLOR reflectorColor)
    {
        this.reflectorColor = reflectorColor;
    }

    public void RefreshReflectorColor()
    {
        switch (reflectorColor)
        {
            case LASER_COLOR.RED:
                spriteRend.sprite = redColorSprite;
                break;
            case LASER_COLOR.BLUE:
                spriteRend.sprite = blueColorSprite;
                break;
            case LASER_COLOR.YELLOW:
                spriteRend.sprite = yellowColorSprite;
                break;
            case LASER_COLOR.WHITE:
                spriteRend.sprite = whiteColorSprite;
                break;
            default:
                break;
        }
    }

    public virtual void CalculateLaser(Laser laser, RaycastHit2D hit)
    {
        ValidReflection();
        SpawnSpark(hit.point, normal.rotation);

        laser.transform.right = Vector3.Reflect(laser.transform.right, normal.right);
        laser.transform.position = referencePoint.position;
        laser.LaserColor = reflectorColor;
        laser.RefreshLaserMaterialColor();
        StartCoroutine(laser.SetReflectorHitFalse(0.02f));
    }

    public virtual void SetLaserColor()
    {
        switch (reflectorColor)
        {
            case LASER_COLOR.RED:
                hitProjectile.GetComponent<Laser>().LaserColor = LASER_COLOR.RED;
                break;
            case LASER_COLOR.BLUE:
                hitProjectile.GetComponent<Laser>().LaserColor = LASER_COLOR.BLUE;
                break;
            case LASER_COLOR.YELLOW:
                hitProjectile.GetComponent<Laser>().LaserColor = LASER_COLOR.YELLOW;
                break;
            case LASER_COLOR.WHITE:
                hitProjectile.GetComponent<Laser>().LaserColor = LASER_COLOR.WHITE;
                break;
        }

        hitProjectile.GetComponent<Laser>().RefreshLaserMaterialColor();
    }

    public virtual void SpawnSpark(Vector3 position, Quaternion rotation)
    {
        FxSpark fxSpark = FxManager.Instance.SpawnFx<FxSpark>(position, rotation);
    }

    public void OccupyGridOutline(Proto_Grid gridOutline)
    {
        OccupiedGridOutline = gridOutline;
    }

    public void Build()
    {
        Interactable = false;
        FxBuildHammer fxBuildHammer = FxManager.Instance.SpawnFx<FxBuildHammer>(transform.position, Quaternion.identity);
        StopAllCoroutines();
        StartCoroutine(BuildCoroutine(fxBuildHammer.GetCurrentAnimationClipLength));
    }

    public void Rotate90()
    {
        Interactable = false;
        StopAllCoroutines();
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f) * transform.rotation;
        StartCoroutine(RotateCoroutine(startRotation, targetRotation));
    }

    public void ValidReflection()
    {
        StopAllCoroutines();
        StartCoroutine(ValidReflectionCoroutine(transform.rotation, validReflectionAnimationDuration));
    }

    private IEnumerator BuildCoroutine(float duration)
    {
        Color color = Color.white;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            spriteRend.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0f, 1f, elapsed / duration));
            yield return null;
        }

        spriteRend.color = color;
        Interactable = true;
    }

    private IEnumerator RotateCoroutine(Quaternion startRotation, Quaternion targetRotation, float duration = 0.1f)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        Interactable = true;
    }

    private IEnumerator ValidReflectionCoroutine(Quaternion startRotation, float duration = 0.1f)
    {
        Quaternion initialRotation = startRotation;
        Quaternion firstTargetRotation = Quaternion.Euler(0.0f, 0.0f, validReflectionAnimationAngleChange) * startRotation;
        Quaternion secondTargetRotation = Quaternion.Euler(0.0f, 0.0f, -validReflectionAnimationAngleChange) * startRotation;
        float durationForEachTargetRotation = duration / 3.0f;
        float elapsed = 0.0f;
        while (elapsed < durationForEachTargetRotation)
        {
            elapsed += Time.deltaTime;
            spriteRend.transform.rotation = Quaternion.Lerp(startRotation, firstTargetRotation, elapsed / durationForEachTargetRotation);
            yield return null;
        }

        spriteRend.transform.rotation = firstTargetRotation;
        startRotation = firstTargetRotation;
        elapsed = 0.0f;

        while (elapsed < durationForEachTargetRotation)
        {
            elapsed += Time.deltaTime;
            spriteRend.transform.rotation = Quaternion.Lerp(startRotation, secondTargetRotation, elapsed / durationForEachTargetRotation);
            yield return null;
        }

        spriteRend.transform.rotation = secondTargetRotation;
        startRotation = secondTargetRotation;
        elapsed = 0.0f;

        while (elapsed < durationForEachTargetRotation)
        {
            elapsed += Time.deltaTime;
            spriteRend.transform.rotation = Quaternion.Lerp(startRotation, initialRotation, elapsed / durationForEachTargetRotation);
            yield return null;
        }

        spriteRend.transform.rotation = initialRotation;
    }

    public virtual void OnLaserOverlap(Laser laser, RaycastHit2D hit)
    {
        CalculateLaser(laser, hit);
    }
}
