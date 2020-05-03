using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapGridMapEditor : MonoBehaviour
{
    [BoxGroup("MAP GRID SETTINGS")] [SerializeField] int mapGridIndex;

    public int MapGridIndex
    {
        get
        {
            return mapGridIndex;
        }
    }
}
