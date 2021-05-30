using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutermostBorder : MonoBehaviour, ILaserInteractable
{
    public void OnLaserOverlap(Laser laser, RaycastHit2D hit)
    {
        FxManager.Instance.SpawnFx<FxSpark>(hit.point, Quaternion.identity);
        laser.Push();
    }
}
