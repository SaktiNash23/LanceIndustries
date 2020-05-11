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
    public int translucentReflectorAmount;
    public int doubleWayReflectorAmount;
    public int splitReflectorAmount;
    public int threeWayReflectorAmount;
}

