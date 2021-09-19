using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Proto_Grid : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRend;

    [SerializeField] protected Sprite spriteHoverValid;
    [SerializeField] protected Sprite spriteHoverInvalid;

    public bool IsOccupied { get; set; }
    public Reflector OccupiedReflector { get; private set; }

    #region MonoBehaviour
    private void OnTriggerEnter(Collider col)
    {
        if (GameplayInputManager.Instance.SelectingReflector && col.GetComponentInParent<Reflector>())
        {
            ShowGrid(true, !IsOccupied);
            GameplayInputManager.Instance.HighlightGridOutline(this);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.GetComponentInParent<Reflector>())
        {
            ShowGrid(false);
            GameplayInputManager.Instance.HighlightGridOutline(null);
        }
    }
    #endregion

    public void ShowGrid(bool show, bool valid = false)
    {
        spriteRend.color = new Color(1f, 1f, 1f, show ? 1f : 0f);
        spriteRend.sprite = valid ? spriteHoverValid : spriteHoverInvalid;
    }

    public void OccupyReflector(Reflector reflector)
    {
        IsOccupied = reflector != null ? true : false;
        OccupiedReflector = reflector;
    }
}
