using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapButton : MonoBehaviour
{
    public TextMeshProUGUI txtMapNo;
    public TextMeshProUGUI txtMapName;

    MapInfo mapInfo; 

    public void PopularizeDisplay(MapInfo targetMapInfo)
    {
        mapInfo = targetMapInfo;
        txtMapNo.text = targetMapInfo.idNo.ToString();
        txtMapName.text = targetMapInfo.mapName;
    }
}
