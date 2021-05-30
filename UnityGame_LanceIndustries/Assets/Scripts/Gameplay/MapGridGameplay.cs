using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapGridGameplay : MonoBehaviour
{
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] int mapGridIndex;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] Wall leftBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] Wall rightBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] Wall topBorder;
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] Wall bottomBorder;

    public int MapGridIndex
    {
        get
        {
            return mapGridIndex;
        }
    }

    public void ToggleBorder(LASER_COLOR color, SNAPPING_DIR borderDir, bool show)
    {
        switch (borderDir)
        {
            case SNAPPING_DIR.LEFT:
                leftBorder.Initialization(color);
                leftBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.RIGHT:
                rightBorder.Initialization(color);
                rightBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.UP:
                topBorder.Initialization(color);
                topBorder.ToggleBorder(show);
                break;
            case SNAPPING_DIR.DOWN:
                bottomBorder.Initialization(color);
                bottomBorder.ToggleBorder(show);
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
