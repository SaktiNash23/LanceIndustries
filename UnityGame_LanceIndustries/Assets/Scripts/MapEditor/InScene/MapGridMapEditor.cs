using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapGridMapEditor : MonoBehaviour
{
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] int mapGridIndex;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBorder mapBorderRightSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBorder mapBorderBottomSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutPortalSnapper mapPortalUpSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutPortalSnapper mapPortalBottomSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutPortalSnapper mapPortalLeftSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutPortalSnapper mapPortalRightSnapper;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapLayoutBoxSnapper mapBoxSnapper;

    public int MapGridIndex
    {
        get
        {
            return mapGridIndex;
        }
    }

    public Vector3 GetBorderSnappingPosition(SNAPPING_DIR snappingDir)
    {
        switch (snappingDir)
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

    public Vector3 GetPortalSnappingPosition(SNAPPING_DIR snappingDir)
    {
        switch (snappingDir)
        {
            case SNAPPING_DIR.UP:
                if(!mapPortalUpSnapper)
                {
                    Debug.LogWarning("UP PORTAL SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapPortalUpSnapper.GetSnappingPosition();
            case SNAPPING_DIR.DOWN:
                if (!mapPortalBottomSnapper)
                {
                    Debug.LogWarning("BOTTOM PORTAL SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapPortalBottomSnapper.GetSnappingPosition();
            case SNAPPING_DIR.LEFT:
                if (!mapPortalLeftSnapper)
                {
                    Debug.LogWarning("LEFT PORTAL SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapPortalLeftSnapper.GetSnappingPosition();
            case SNAPPING_DIR.RIGHT:
                if (!mapPortalRightSnapper)
                {
                    Debug.LogWarning("RIGHT PORTAL SNAPPER IS NOT BEING ASSIGNED.", this);
                    return Vector3.zero;
                }
                return mapPortalRightSnapper.GetSnappingPosition();
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
