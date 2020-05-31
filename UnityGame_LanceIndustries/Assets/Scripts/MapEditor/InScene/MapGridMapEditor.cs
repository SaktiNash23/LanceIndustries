using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapGridMapEditor : MonoBehaviour
{
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] int mapGridIndex;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBorder mapBorderRightSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBorder mapBorderBottomSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBoxSnapper mapBoxSnapper;

    public int MapGridIndex
    {
        get
        {
            return mapGridIndex;
        }
    }

    public Vector3 GetBorderSnappingPosition(SNAPPING_DIR borderDir)
    {
        switch (borderDir)
        {
            case SNAPPING_DIR.RIGHT:
                if (!mapBorderRightSnapper)
                {
                    Debug.LogWarning("RIGHT BORDER SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapBorderRightSnapper.GetSnappingPosition();
            case SNAPPING_DIR.DOWN:
                if (!mapBorderBottomSnapper)
                {
                    Debug.LogWarning("BOTTOM BORDER SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapBorderBottomSnapper.GetSnappingPosition();
            default:
                Debug.LogWarning("NO SUCH DIR OF SNAPPER EXISTS IN MAP GRID");
                return Vector3.zero;
        }

    }

    public Vector3 GetBoxSnappingPosition()
    {
        if(!mapBoxSnapper)
        {
            Debug.LogWarning("MAP BOX SNAPPER IS NOT BEING ASSIGNED.", this);
            return Vector3.zero;
        }

        return mapBoxSnapper.GetSnappingPosition();
    }
}
