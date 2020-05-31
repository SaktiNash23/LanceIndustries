using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapGridGameplay : MonoBehaviour
{
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] int mapGridIndex;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapBorder leftBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapBorder rightBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapBorder topBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] MapBorder bottomBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] ColoredBorder coloredLeftBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] ColoredBorder coloredRightBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] ColoredBorder coloredTopBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] ColoredBorder coloredBottomBorder;

    public int MapGridIndex
    {
        get
        {
            return mapGridIndex;
        }
    }

    public void ToggleBorder(SNAPPING_DIR borderDir, bool show)
    {
        switch (borderDir)
        {
            case SNAPPING_DIR.LEFT:
                leftBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.RIGHT:
                rightBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.UP:
                topBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.DOWN:
                bottomBorder.ToggleBorder(show);
                break;
        }
    }

    public void ToggleColoredBorder(BorderColor targetBorderColor, SNAPPING_DIR borderDir, bool show)
    {
        switch (borderDir)
        {
            case SNAPPING_DIR.LEFT:
                coloredLeftBorder.borderColor = targetBorderColor;
                coloredLeftBorder.Initialization();
                coloredLeftBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.RIGHT:
                coloredRightBorder.borderColor = targetBorderColor;
                coloredRightBorder.Initialization();
                coloredRightBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.UP:
                coloredTopBorder.borderColor = targetBorderColor;
                coloredTopBorder.Initialization();
                coloredTopBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.DOWN:
                coloredBottomBorder.borderColor = targetBorderColor;
                coloredBottomBorder.Initialization();
                coloredBottomBorder.ToggleBorder(show);
                break;
        }
    }

    public Transform GetBorderTransform(SNAPPING_DIR dir)
    {
        switch (dir)
        {
            case SNAPPING_DIR.LEFT:
                return leftBorder.transform;
            case SNAPPING_DIR.RIGHT:
                return rightBorder.transform;
            case SNAPPING_DIR.UP:
                return topBorder.transform;
            case SNAPPING_DIR.DOWN:
                return bottomBorder.transform;
            default:
                return default;
        }
    }
}
