using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MapButton : MonoBehaviour
{
    public Button btnMap;
    public TextMeshProUGUI txtMapNo;
    public TextMeshProUGUI txtMapName;

    public string MapDataPath { get; set; }

    MapInfo mapInfo;

    public void Start()
    {
        AddCallback(OnLoadAction);
    }

    public void PopularizeDisplay(MapInfo targetMapInfo)
    {
        mapInfo = targetMapInfo;
        txtMapNo.text = targetMapInfo.idNo.ToString();
        txtMapName.text = targetMapInfo.mapName;
    }

    public void InitializeMapDataPath()
    {
        string mapDataDirectory = "Assets/Data Library/Map Data/";
        string targetPath = mapDataDirectory + txtMapName.text + ".json";
        MapDataPath = targetPath;
    }

    private void OnLoadAction()
    {
        MapEditorManager.Instance.LoadedMapDataPath = MapDataPath;
        MapEditorManager.Instance.LoadMap();
    }

    public void AddCallback(UnityAction action)
    {
        btnMap.onClick.AddListener(action);
    }

    public void RemoveCallback(UnityAction action)
    {
        btnMap.onClick.RemoveListener(action);
    }
}
