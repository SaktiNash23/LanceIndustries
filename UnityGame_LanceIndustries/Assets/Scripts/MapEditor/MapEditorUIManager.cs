using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using NaughtyAttributes;

public class MapEditorUIManager : MonoBehaviour
{
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtContentMapList;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtPanelMapList;

    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] TMP_InputField ifMapName;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] Button btnCreateMap;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] GameObject gameObjSameMapNameWarning;

    [BoxGroup("PREFABS")] [SerializeField] MapButton mapButtonPrefab;

    private float contentMapListMinDeltaY;

    private List<MapButton> mapButtons;

    //-------------------------- MONOBEHAVIOUR FUNCTIONS --------------------------//

    private void Awake()
    {
        mapButtons = new List<MapButton>();

        contentMapListMinDeltaY = rtContentMapList.sizeDelta.y;
    }

    //-------------------------- PANEL MENU SCREEN FUNCTIONS --------------------------//

    public void LoadMap()
    {
        MapInfoLibrary mapInfoLibrary = (MapInfoLibrary)LibraryLinker.Instance.MapInfoLib;
        if(mapButtons.Count > 0)
        {
            foreach (var mapButton in mapButtons)
                Destroy(mapButton.gameObject);
             
            mapButtons.Clear();
        }
        foreach(var mapInfo in mapInfoLibrary.mapInfos)
        {
            MapButton mapButton = Instantiate<MapButton>(mapButtonPrefab, rtPanelMapList.transform, false);
            mapButton.PopularizeDisplay(mapInfo);
            mapButtons.Add(mapButton);
        }

        float heightMapButton = mapButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        float minDeltaYToFitMap = heightMapButton * mapInfoLibrary.mapInfos.Count + rtPanelMapList.GetComponent<VerticalLayoutGroup>().spacing * mapInfoLibrary.mapInfos.Count + 20;
        float targetDeltaY = contentMapListMinDeltaY > minDeltaYToFitMap ? contentMapListMinDeltaY : minDeltaYToFitMap;
        rtContentMapList.sizeDelta = new Vector2(rtContentMapList.sizeDelta.x, targetDeltaY);
    }

    //-------------------------- PANEL CREATE MAP FUNCTIONS --------------------------//

    public void ResetIFMapName()
    {
        ifMapName.text = "";
        gameObjSameMapNameWarning.SetActive(false);
    }

    public void IFMapNameOnEndEditCallback(string mapName)
    {
        MapInfoLibrary mapInfoLib = LibraryLinker.Instance.MapInfoLib;

        bool sameMapName = false;

        foreach (var mapInfo in mapInfoLib.mapInfos)
        {
            if (mapInfo.mapName == mapName)
            {
                sameMapName = true;
                gameObjSameMapNameWarning.SetActive(true);
                break;
            }
        }

        if(mapName == "")
        {
            btnCreateMap.interactable = false;
        }
        else if(sameMapName)
        {
            btnCreateMap.interactable = false;
            gameObjSameMapNameWarning.SetActive(true);
        }
        else
        {
            btnCreateMap.interactable = true;
            gameObjSameMapNameWarning.SetActive(false);
        }
    }

    public void CreateMap()
    {
        MapData newMapData = new MapData();
        string mapDataJson = JsonUtility.ToJson(newMapData, true);
        string mapDataSavePath = "Assets/Data Library/Map Data/" + ifMapName.text + ".json";
        FileStream file = new FileStream(mapDataSavePath, FileMode.Create);
        StreamWriter writer = new StreamWriter(file);
        writer.Write(mapDataJson);
        writer.Close();
        file.Close();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        string mapInfoSavePath = "Assets/Data Library/Map Info/" + ifMapName.text + " Map Info.asset";
        TextAsset targetTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(mapDataSavePath, typeof(TextAsset));
        MapInfo mapInfo = ScriptableObject.CreateInstance<MapInfo>();
        mapInfo.mapData = targetTextAsset;
        mapInfo.mapName = ifMapName.text;

        MapInfoLibrary mapInfoLib = LibraryLinker.Instance.MapInfoLib;

        mapInfo.idNo = (mapInfoLib.mapInfos.Count + 1).ToString("D2");

        AssetDatabase.CreateAsset(mapInfo, mapInfoSavePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        mapInfoLib.mapInfos.Add(mapInfo);

    }

}
