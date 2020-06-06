using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapInfo : LibraryItemBase
{
    [BoxGroup("MAP INFO")] public string mapName;
    [BoxGroup("MAP INFO")] public TextAsset mapData;
}

[System.Serializable]
public class MapDataHolder
{
    public List<InSceneObjectData> inSceneObjectDatas;
    public float timeLimit;
    public int basicReflectorAmount;
    public int redBasicReflectorAmount;
    public int yellowBasicReflectorAmount;
    public int blueBasicReflectorAmount;
    public int translucentReflectorAmount;
    public int redTranslucentReflectorAmount;
    public int yellowTranslucentReflectorAmount;
    public int blueTranslucentReflectorAmount;
    public int doubleWayReflectorAmount;
    public int redDoubleWayReflectorAmount;
    public int yellowDoubleWayReflectorAmount;
    public int blueDoubleWayReflectorAmount;
    public int splitReflectorAmount;
    public int redSplitReflectorAmount;
    public int yellowSplitReflectorAmount;
    public int blueSplitReflectorAmount;
    public int threeWayReflectorAmount;
    public int redThreeWayReflectorAmount;
    public int yellowThreeWayReflectorAmount;
    public int blueThreeWayReflectorAmount;
}

