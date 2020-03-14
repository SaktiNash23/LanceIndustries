using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapInfo : ScriptableObject
{
    [BoxGroup("MAP INFO")] public int mapNo;
    [BoxGroup("MAP INFO")] public string mapName;
    [BoxGroup("MAP DATA")] public TextAsset mapData;
}

