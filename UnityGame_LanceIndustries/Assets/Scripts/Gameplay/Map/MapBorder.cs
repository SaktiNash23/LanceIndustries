using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class MapBorder : MonoBehaviour
{
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] SpriteRenderer spriteRend;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col1;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col2;
    [BoxGroup("MAP BORDER REFERENCES")] [SerializeField] Collider2D col3;

    public void ToggleBorder(bool show)
    {
        if(show)
            spriteRend.enabled = col1.enabled = col2.enabled = col3.enabled = true;
        else
            spriteRend.enabled = col1.enabled = col2.enabled = col3.enabled = false;
    }
}
