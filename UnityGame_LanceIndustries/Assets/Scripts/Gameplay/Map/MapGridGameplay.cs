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

    public void ToggleBorder(BORDER_DIRECTION borderDir, bool show)
    {
        switch (borderDir)
        {
            case BORDER_DIRECTION.LEFT:
                leftBorder.ToggleBorder(show);
                break;
            case BORDER_DIRECTION.RIGHT:
                rightBorder.ToggleBorder(show);
                break;
            case BORDER_DIRECTION.UP:
                topBorder.ToggleBorder(show);
                break;
            case BORDER_DIRECTION.DOWN:
                bottomBorder.ToggleBorder(show);
                break;
        }
    }
}
