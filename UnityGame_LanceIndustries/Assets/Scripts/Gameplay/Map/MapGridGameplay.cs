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
}
