using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Proto_Grid : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRend;

    public GameObject reflectorStored_Grid;

    public bool IsOccupied { get; set; }
    public Reflector OccupiedReflector { get; private set; }

    #region MonoBehaviour
    private void OnTriggerStay(Collider col)
    {
        if (!IsOccupied && col.GetComponentInParent<Reflector>())
        {
            ShowGrid(true);
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

    public void ShowGrid(bool show)
    {
        spriteRend.color = new Color(1f, 1f, 1f, show ? 1f : 0f);
    }

    public void OccupyReflector(Reflector reflector)
    {
        IsOccupied = reflector != null ? true : false;
        OccupiedReflector = reflector;
    }
}
