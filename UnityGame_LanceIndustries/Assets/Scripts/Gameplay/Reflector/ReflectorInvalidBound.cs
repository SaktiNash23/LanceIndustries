using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReflectorInvalidBound : MonoBehaviour, ILaserInteractable
{
    public void OnLaserOverlap(Laser laser, RaycastHit2D hit)
    {
        FxSpark fxSpark = FxManager.Instance.SpawnFx<FxSpark>(hit.point, Quaternion.identity);
        fxSpark.transform.right = hit.normal;

        laser.Push();
    }
}
