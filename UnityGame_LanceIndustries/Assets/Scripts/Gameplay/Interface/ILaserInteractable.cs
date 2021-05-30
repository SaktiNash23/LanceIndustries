using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaserInteractable 
{
    void OnLaserOverlap(Laser laser, RaycastHit2D hit);
}
