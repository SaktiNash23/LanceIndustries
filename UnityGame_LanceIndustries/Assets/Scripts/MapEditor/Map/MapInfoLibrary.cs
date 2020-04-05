using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapInfoLibrary : LibraryBase
{
    [BoxGroup("MAP INFO LIBRARY INFO")] public List<MapInfo> mapInfos;

    public MapInfo GetMapInfo(string targetId)
    {
        foreach (var mapInfo in mapInfos)
            if (mapInfo.idNo == targetId)
                return mapInfo;

        Debug.LogError("Map Info with " + "<color=red>" + targetId + "</color> couldn't be found.");
        return null;
    }
}
