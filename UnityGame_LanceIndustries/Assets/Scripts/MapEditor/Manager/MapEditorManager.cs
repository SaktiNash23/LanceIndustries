using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using NaughtyAttributes;
using System;

public class MapEditorManager : MonoBehaviour
{
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] CanvasGroup cgPanelMenuScreen;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtContentMapList;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtPanelMapList;
    [BoxGroup("PANEL MENU SCREEN PREFABS")] [SerializeField] MapButton mapButtonPrefab;

    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] TMP_InputField ifMapName;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] Button btnCreateMap;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] GameObject gameObjSameMapNameWarning;

    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] CanvasGroup cgPanelMapEditing;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] CanvasGroup cgPanelMapEditingTopLayer;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelEndPoints;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBasicReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifTranslucentReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifDoubleWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifSplitReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifThreeWayReflectorAmount;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject verticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject horizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointWhitePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointRedPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointYellowPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointBluePrefab;

    [BoxGroup("MAP EDITOR IN SCENE REFERENCES")] [SerializeField] GameObject mapLayoutGameObj;

    [BoxGroup("GIZMO REFERENCES")] public MoveGizmo moveGizmoPrefab;
    [BoxGroup("GIZMO REFERENCES")] public RotationGizmo rotationGizmoPrefab;

    private float contentMapListMinDeltaY;

    private List<MapButton> mapButtons = new List<MapButton>();

    [HideInInspector] public string LoadedMapDataPath;

    private static MapEditorManager _instance;
    public static MapEditorManager Instance
    {
        get { return _instance; }
    }

    private int basicReflectorAmount;
    private int translucentReflectorAmount;
    private int doubleWayReflectorAmount;
    private int splitReflectorAmount;
    private int threeWayReflectorAmount;

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

    private void Start()
    {
        InitializeMapSelectionButtons();
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
        StartCoroutine(LoadMapCoroutine(0.25f));
    }

    public IEnumerator LoadMapCoroutine(float delay)
    {
#if UNITY_EDITOR
        yield return new WaitForSeconds(delay);

        cgPanelMenuScreen.alpha = 0f;
        cgPanelMenuScreen.interactable = false;
        cgPanelMenuScreen.blocksRaycasts = false;
        cgPanelMapEditing.alpha = 1f;
        cgPanelMapEditing.interactable = true;
        cgPanelMapEditing.blocksRaycasts = true;
        cgPanelMapEditingTopLayer.alpha = 1f;
        cgPanelMapEditingTopLayer.interactable = true;
        cgPanelMapEditingTopLayer.blocksRaycasts = true;

        mapLayoutGameObj.SetActive(true);

        TextAsset mapData = AssetDatabase.LoadAssetAtPath<TextAsset>(LoadedMapDataPath);
        string jsonData = mapData.text;
        MapDataHolder mapDataHolder = JsonUtility.FromJson<MapDataHolder>(jsonData);
        if(mapDataHolder != null)
        {
            foreach(var inSceneObjectData in mapDataHolder.inSceneObjectDatas)
            {
                MapEditorInSceneObject inSceneObject = null;

                switch (inSceneObjectData.inSceneObjectType)
                {
                    case IN_SCENE_OBJECT_TYPES.VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(verticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(horizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(originPointPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(destinationPointWhitePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(destinationPointRedPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(destinationPointYellowPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(destinationPointBluePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                }
            }
        }

        MapLayoutBorder[] mapLayoutBorders = FindObjectsOfType<MapLayoutBorder>();
        foreach(var mapLayoutBorder in mapLayoutBorders)
            mapLayoutBorder.Initialization();

        MapLayoutBoxSnapper[] mapLayoutBoxSnappers = FindObjectsOfType<MapLayoutBoxSnapper>();
        foreach (var mapLayoutBoxSnapper in mapLayoutBoxSnappers)
            mapLayoutBoxSnapper.Initialization();

        basicReflectorAmount = mapDataHolder.basicReflectorAmount;
        translucentReflectorAmount = mapDataHolder.translucentReflectorAmount;
        doubleWayReflectorAmount = mapDataHolder.doubleWayReflectorAmount;
        splitReflectorAmount = mapDataHolder.splitReflectorAmount;
        threeWayReflectorAmount = mapDataHolder.threeWayReflectorAmount;

        UpdateIFReflectorAmount();

        MapEditorInputManager.Instance.MapEditing = true;
#endif
        yield return null;
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
#if UNITY_EDITOR
        MapData newMapData = new MapData();
        string mapDataJson = JsonUtility.ToJson(newMapData, true);
        string mapDataSavePath = "Assets/Data Library/Map Datas/" + ifMapName.text + ".json";
        FileStream file = new FileStream(mapDataSavePath, FileMode.Create);
        StreamWriter writer = new StreamWriter(file);
        writer.Write(mapDataJson);
        writer.Close();
        file.Close();
        AssetDatabase.Refresh();

        string mapInfoSavePath = "Assets/Data Library/Map Infos/" + ifMapName.text + " Map Info.asset";
        TextAsset targetTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(mapDataSavePath, typeof(TextAsset));
        MapInfo mapInfo = ScriptableObject.CreateInstance<MapInfo>();
        mapInfo.mapData = targetTextAsset;
        mapInfo.mapName = ifMapName.text;
        mapInfo.idNo = (LibraryLinker.Instance.MapInfoLib.mapInfos.Count + 1).ToString("D2");

        AssetDatabase.CreateAsset(mapInfo, mapInfoSavePath);
        AssetDatabase.Refresh();

        LibraryLinker.Instance.MapInfoLib.mapInfos.Add(mapInfo);
        AssetDatabase.Refresh();

        LoadedMapDataPath = mapDataSavePath;
        InitializeMapSelectionButtons();
#endif
    }

    //--------------------------- IN SCENE MAP EDITING FUNCTIONS ---------------------------//

    public void SaveMap()
    {
#if UNITY_EDITOR
        MapEditorInSceneObject[] inSceneObjects = FindObjectsOfType<MapEditorInSceneObject>();
        InSceneObjectData[] inSceneObjectDatas = new InSceneObjectData[inSceneObjects.Length];
        MapDataHolder mapDataHolder = new MapDataHolder();
        mapDataHolder.inSceneObjectDatas = new List<InSceneObjectData>();
        for(int i = 0; i < inSceneObjects.Length; i++)
        {
            InSceneObjectData objectData = inSceneObjects[i].InSceneObjData;
            mapDataHolder.inSceneObjectDatas.Add(objectData);
        }

        mapDataHolder.basicReflectorAmount = int.Parse(ifBasicReflectorAmount.text);
        mapDataHolder.translucentReflectorAmount = int.Parse(ifTranslucentReflectorAmount.text);
        mapDataHolder.doubleWayReflectorAmount = int.Parse(ifDoubleWayReflectorAmount.text);
        mapDataHolder.splitReflectorAmount = int.Parse(ifSplitReflectorAmount.text);
        mapDataHolder.threeWayReflectorAmount = int.Parse(ifThreeWayReflectorAmount.text);

        string jsonData = JsonUtility.ToJson(mapDataHolder, true);

        StreamWriter streamWriter = new StreamWriter(LoadedMapDataPath, false);
        streamWriter.WriteLine(jsonData);
        streamWriter.Close();
        AssetDatabase.Refresh();
#endif
    }

    public void BackToMenu()
    {
        cgPanelMenuScreen.alpha = 1f;
        cgPanelMenuScreen.interactable = true;
        cgPanelMenuScreen.blocksRaycasts = true;
        cgPanelMapEditing.alpha = 0f;
        cgPanelMapEditing.interactable = false;
        cgPanelMapEditing.blocksRaycasts = false;
        cgPanelMapEditingTopLayer.alpha = 0f;
        cgPanelMapEditingTopLayer.interactable = false;
        cgPanelMapEditingTopLayer.blocksRaycasts = false;

        mapLayoutGameObj.SetActive(false);
        rtPanelEndPoints.gameObject.SetActive(false);

        MapEditorInSceneObject[] inSceneObjects = FindObjectsOfType<MapEditorInSceneObject>();
        foreach (var inSceneObject in inSceneObjects)
            Destroy(inSceneObject.gameObject);

        LoadedMapDataPath = "";
        MapEditorInputManager.Instance.MapEditing = false;
    }

    public void TogglePanelEndPoints()
    {
        rtPanelEndPoints.gameObject.SetActive(!rtPanelEndPoints.gameObject.activeSelf);
    }

    private void UpdateIFReflectorAmount()
    {
        ifBasicReflectorAmount.text = basicReflectorAmount.ToString();
        ifTranslucentReflectorAmount.text = translucentReflectorAmount.ToString();
        ifDoubleWayReflectorAmount.text = doubleWayReflectorAmount.ToString();
        ifSplitReflectorAmount.text = splitReflectorAmount.ToString();
        ifThreeWayReflectorAmount.text = threeWayReflectorAmount.ToString();
    }

    public MapEditorInSceneObject CreateInSceneObj(IN_SCENE_OBJECT_TYPES inSceneObjType)
    {
        MapEditorInSceneObject inSceneObj = null;
        switch (inSceneObjType)
        {
            case IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(horizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), horizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(verticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), verticalLinePrefab.transform.rotation);
                break;
        }

        return inSceneObj;
    }
}
