using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterSpriteMaskTrigger : MonoBehaviour
{
    [SerializeField] protected SpriteMask spriteMask;

    public Laser targetLaser { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
    }
}
