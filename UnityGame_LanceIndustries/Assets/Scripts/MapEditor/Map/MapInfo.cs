using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapInfo : LibraryItemBase
{
    [BoxGroup("MAP INFO")] public string mapName;
    [BoxGroup("MAP INFO")] public TextAsset mapData;
}

