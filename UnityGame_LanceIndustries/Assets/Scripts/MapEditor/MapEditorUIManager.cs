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
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] CanvasGroup cgPanelMenuScreen;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtContentMapList;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtPanelMapList;
    [BoxGroup("PANEL MENU SCREEN PREFABS")] [SerializeField] MapButton mapButtonPrefab;

    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] TMP_InputField ifMapName;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] Button btnCreateMap;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] GameObject gameObjSameMapNameWarning;

    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] CanvasGroup cgPanelMapEditing;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject verticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject horizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointPrefab;

    private float contentMapListMinDeltaY;

    private List<MapButton> mapButtons = new List<MapButton>();

    public string LoadedMapDataPath;

    private static MapEditorUIManager _instance;
    public static MapEditorUIManager Instance
    {
        get { return _instance; }
    }

    //-------------------------- MONOBEHAVIOUR FUNCTIONS --------------------------//

    private void Awake()
    {
        contentMapListMinDeltaY = rtContentMapList.sizeDelta.y;

        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //-------------------------- PANEL MENU SCREEN FUNCTIONS --------------------------//

    public void InitializeMapSelectionButtons()
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
            mapButton.InitializeMapDataPath();
            mapButtons.Add(mapButton);
        }

        float heightMapButton = mapButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        float minDeltaYToFitMap = heightMapButton * mapInfoLibrary.mapInfos.Count + rtPanelMapList.GetComponent<VerticalLayoutGroup>().spacing * mapInfoLibrary.mapInfos.Count + 20;
        float targetDeltaY = contentMapListMinDeltaY > minDeltaYToFitMap ? contentMapListMinDeltaY : minDeltaYToFitMap;
        rtContentMapList.sizeDelta = new Vector2(rtContentMapList.sizeDelta.x, targetDeltaY);
    }

    //-------------------------- BUTTONS FUNCTIONS --------------------------//

    public void LoadMap()
    {
        cgPanelMenuScreen.alpha = 0f;
        cgPanelMenuScreen.interactable = false;
        cgPanelMenuScreen.blocksRaycasts = false;
        cgPanelMapEditing.alpha = 1f;
        cgPanelMapEditing.interactable = true;
        cgPanelMapEditing.blocksRaycasts = true;

        StartCoroutine(LoadInSceneObjects(0.25f));
    }

    public IEnumerator LoadInSceneObjects(float delay)
    {
        yield return new WaitForSeconds(delay);
        TextAsset mapData = AssetDatabase.LoadAssetAtPath<TextAsset>(LoadedMapDataPath);
        string jsonData = mapData.text;
        InSceneObjectData[] inSceneObjectDatas = JsonHelper.FromJson<InSceneObjectData>(jsonData);
        for(int i = 0; i < inSceneObjectDatas.Length; i++)
        {
            MapEditorInSceneObject inSceneObject = null;

            switch (inSceneObjectDatas[i].inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.VERTICAL_LINE:
                    inSceneObject = Instantiate<MapEditorInSceneObject>(verticalLinePrefab, inSceneObjectDatas[i].position, inSceneObjectDatas[i].rotation);
                    break;
                case IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE:
                    inSceneObject = Instantiate<MapEditorInSceneObject>(horizontalLinePrefab, inSceneObjectDatas[i].position, inSceneObjectDatas[i].rotation);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                    inSceneObject = Instantiate<MapEditorInSceneObject>(originPointPrefab, inSceneObjectDatas[i].position, inSceneObjectDatas[i].rotation);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT:
                    inSceneObject = Instantiate<MapEditorInSceneObject>(destinationPointPrefab, inSceneObjectDatas[i].position, inSceneObjectDatas[i].rotation);
                    break;
            }

            //inSceneObject.transform.localScale = inSceneObjectDatas[i].scale;
        }
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
        AssetDatabase.Refresh();

        string mapInfoSavePath = "Assets/Data Library/Map Info/" + ifMapName.text + " Map Info.asset";
        TextAsset targetTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(mapDataSavePath, typeof(TextAsset));
        MapInfo mapInfo = ScriptableObject.CreateInstance<MapInfo>();
        mapInfo.mapData = targetTextAsset;
        mapInfo.mapName = ifMapName.text;
        mapInfo.idNo = (LibraryLinker.Instance.MapInfoLib.mapInfos.Count + 1).ToString("D2");

        AssetDatabase.CreateAsset(mapInfo, mapInfoSavePath);
        AssetDatabase.Refresh();

        LibraryLinker.Instance.MapInfoLib.mapInfos.Add(mapInfo);
        AssetDatabase.Refresh();
    }

    //--------------------------- IN SCENE MAP EDITING FUNCTIONS ---------------------------//

    public void SaveMap()
    {
        MapEditorInSceneObject[] inSceneObjects = FindObjectsOfType<MapEditorInSceneObject>();
        InSceneObjectData[] inSceneObjectDatas = new InSceneObjectData[inSceneObjects.Length];
        for(int i = 0; i < inSceneObjects.Length; i++)
        {
            InSceneObjectData objectData = inSceneObjects[i].inSceneObjectData;
            inSceneObjectDatas[i] = objectData;
        }

        string jsonData = JsonHelper.ToJson(inSceneObjectDatas, true);

        StreamWriter streamWriter = new StreamWriter(LoadedMapDataPath, false);
        streamWriter.WriteLine(jsonData);
        streamWriter.Close();
        AssetDatabase.Refresh();
    }
}
