using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using NaughtyAttributes;
using System.Linq;
using UnityEngine.Experimental.AI;
using System;

public class MapEditorManager : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;

    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] CanvasGroup cgPanelMenuScreen;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtContentMapList;
    [BoxGroup("PANEL MENU SCREEN REFERENCES")] [SerializeField] RectTransform rtPanelMapList;
    [BoxGroup("PANEL MENU SCREEN PREFABS")] [SerializeField] MapButton mapButtonPrefab;

    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] TMP_InputField ifMapName;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] Button btnCreateMap;
    [BoxGroup("PANEL CREATE MAP REFERENCES")] [SerializeField] GameObject gameObjSameMapNameWarning;

    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] CanvasGroup cgPanelMapEditing;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] CanvasGroup cgPanelMapEditingTopLayer;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelHorizontalLines;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelVerticalLines;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelOriginPoints;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelDestinationPoints;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelPortals;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelPortals1stSet;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] RectTransform rtPanelPortals2ndSet;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] List<MapEditorObjectCreatorButton> mapEditorObjectCreatorParentButtons;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] List<MapEditorObjectCreatorButton> mapEditorObjectCreatorPortalSubMenuButtons;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifTimeLimit;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifWhiteBasicReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifRedBasicReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifYellowBasicReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBlueBasicReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifWhiteTranslucentReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifRedTranslucentReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifYellowTranslucentReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBlueTranslucentReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifWhiteDoubleWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifRedDoubleWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifYellowDoubleWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBlueDoubleWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifWhiteSplitReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifRedSplitReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifYellowSplitReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBlueSplitReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifWhiteThreeWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifRedThreeWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifYellowThreeWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] TMP_InputField ifBlueThreeWayReflectorAmount;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] UIHelper uiHelperOptionMenu;
    [BoxGroup("PANEL MAP EDITING REFERENCES")] [SerializeField] UIHelper uiHelperLevelSettingsMenu;

    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject normalVerticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject whiteVerticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject redVerticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject yellowVerticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject blueVerticalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject normalHorizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject whiteHorizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject redHorizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject yellowHorizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject blueHorizontalLinePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointWhitePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointRedPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointYellowPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject originPointBluePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointWhitePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointRedPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointYellowPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject destinationPointBluePrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject firstSetPortalHorizontalPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject firstSetPortalVerticalPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject secondSetPortalHorizontalPrefab;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT PREFABS")] [SerializeField] MapEditorInSceneObject secondSetPortalVerticalPrefab;

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

    private float timeLimit;
    private int whiteBasicReflectorAmount;
    private int redBasicReflectorAmount;
    private int yellowBasicReflectorAmount;
    private int blueBasicReflectorAmount;
    private int whiteTranslucentReflectorAmount;
    private int redTranslucentReflectorAmount;
    private int yellowTranslucentReflectorAmount;
    private int blueTranslucentReflectorAmount;
    private int whiteDoubleWayReflectorAmount;
    private int redDoubleWayReflectorAmount;
    private int yellowDoubleWayReflectorAmount;
    private int blueDoubleWayReflectorAmount;
    private int whiteSplitReflectorAmount;
    private int redSplitReflectorAmount;
    private int yellowSplitReflectorAmount;
    private int blueSplitReflectorAmount;
    private int whiteThreeWayReflectorAmount;
    private int redThreeWayReflectorAmount;
    private int yellowThreeWayReflectorAmount;
    private int blueThreeWayReflectorAmount;

    public bool EditingIF { get; set; } = false;

    //-------------------------- MONOBEHAVIOUR FUNCTIONS --------------------------//

    private void Awake()
    {
        contentMapListMinDeltaY = rtContentMapList.sizeDelta.y;

        if (_instance != null)
        {
            if (_instance != this)
                Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        InitializeMapSelectionButtons();
    }

    //-------------------------- PANEL MENU SCREEN FUNCTIONS --------------------------//

    public void InitializeMapSelectionButtons()
    {
        List<MapInfo> mapInfos = new List<MapInfo>(Resources.LoadAll<MapInfo>("Map Infos"));

        if(mapButtons.Count > 0)
        {
            foreach (var mapButton in mapButtons)
                Destroy(mapButton.gameObject);
             
            mapButtons.Clear();
        }

        foreach(var mapInfo in mapInfos)
        {
            MapButton mapButton = Instantiate<MapButton>(mapButtonPrefab, rtPanelMapList.transform, false);
            mapButton.PopularizeDisplay(mapInfo);
            mapButton.InitializeMapDataPath();
            mapButtons.Add(mapButton);
        }

        float heightMapButton = mapButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        float minDeltaYToFitMap = heightMapButton * mapInfos.Count + rtPanelMapList.GetComponent<VerticalLayoutGroup>().spacing * mapInfos.Count + 20;
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
        if (mapDataHolder != null)
        {
            foreach (var inSceneObjectData in mapDataHolder.inSceneObjectDatas)
            {
                MapEditorInSceneObject inSceneObject = null;

                switch (inSceneObjectData.inSceneObjectType)
                {
                    case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(normalVerticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(whiteVerticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(redVerticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(yellowVerticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(blueVerticalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(normalHorizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(whiteHorizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(redHorizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(yellowHorizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(blueHorizontalLinePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(originPointWhitePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(originPointRedPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(originPointYellowPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(originPointBluePrefab, inSceneObjectData.position, inSceneObjectData.rotation);
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
                    case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(firstSetPortalHorizontalPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(firstSetPortalVerticalPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(secondSetPortalHorizontalPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                    case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                        inSceneObject = Instantiate<MapEditorInSceneObject>(secondSetPortalVerticalPrefab, inSceneObjectData.position, inSceneObjectData.rotation);
                        break;
                }
            }
        }

        yield return new WaitForSeconds(1.0f);

        MapSnappersInitialization();

        timeLimit = mapDataHolder.timeLimit;
        whiteBasicReflectorAmount = mapDataHolder.basicReflectorAmount;
        redBasicReflectorAmount = mapDataHolder.redBasicReflectorAmount;
        yellowBasicReflectorAmount = mapDataHolder.yellowBasicReflectorAmount;
        blueBasicReflectorAmount = mapDataHolder.blueBasicReflectorAmount;
        whiteTranslucentReflectorAmount = mapDataHolder.translucentReflectorAmount;
        redTranslucentReflectorAmount = mapDataHolder.redTranslucentReflectorAmount;
        yellowTranslucentReflectorAmount = mapDataHolder.yellowTranslucentReflectorAmount;
        blueTranslucentReflectorAmount = mapDataHolder.blueTranslucentReflectorAmount;
        whiteDoubleWayReflectorAmount = mapDataHolder.doubleWayReflectorAmount;
        redDoubleWayReflectorAmount = mapDataHolder.redDoubleWayReflectorAmount;
        yellowDoubleWayReflectorAmount = mapDataHolder.yellowDoubleWayReflectorAmount;
        blueDoubleWayReflectorAmount = mapDataHolder.blueDoubleWayReflectorAmount;
        whiteSplitReflectorAmount = mapDataHolder.splitReflectorAmount;
        redSplitReflectorAmount = mapDataHolder.redSplitReflectorAmount;
        yellowSplitReflectorAmount = mapDataHolder.yellowSplitReflectorAmount;
        blueSplitReflectorAmount = mapDataHolder.blueSplitReflectorAmount;
        whiteThreeWayReflectorAmount = mapDataHolder.threeWayReflectorAmount;
        redThreeWayReflectorAmount = mapDataHolder.redThreeWayReflectorAmount;
        yellowThreeWayReflectorAmount = mapDataHolder.yellowThreeWayReflectorAmount;
        blueThreeWayReflectorAmount = mapDataHolder.blueThreeWayReflectorAmount;

        UpdateIFLevelSettings();

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
        List<MapInfo> mapInfos = new List<MapInfo>(Resources.LoadAll<MapInfo>("Map Infos"));

        bool sameMapName = false;

        foreach (var mapInfo in mapInfos)
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
        string mapDataSavePath = "Assets/Resources/Map Datas/" + ifMapName.text + ".json";
        using (FileStream file = new FileStream(mapDataSavePath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(file))
                writer.Write(mapDataJson);
        }
        AssetDatabase.Refresh();

        string mapInfoSavePath = "Assets/Resources/Map Infos/" + ifMapName.text + " Map Info.asset";
        TextAsset targetTextAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(mapDataSavePath, typeof(TextAsset));
        MapInfo mapInfo = ScriptableObject.CreateInstance<MapInfo>();
        mapInfo.mapData = targetTextAsset;
        mapInfo.mapName = ifMapName.text;
        //mapInfo.idNo = (LibraryLinker.Instance.MapInfoLib.mapInfos.Count + 1).ToString("D2");

        AssetDatabase.CreateAsset(mapInfo, mapInfoSavePath);
        AssetDatabase.Refresh();

        //LibraryLinker.Instance.MapInfoLib.mapInfos.Add(mapInfo);
        //EditorUtility.SetDirty(LibraryLinker.Instance.MapInfoLib);
        AssetDatabase.SaveAssets();
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

        mapDataHolder.timeLimit = float.Parse(ifTimeLimit.text);
        mapDataHolder.basicReflectorAmount = int.Parse(ifWhiteBasicReflectorAmount.text);
        mapDataHolder.redBasicReflectorAmount = int.Parse(ifRedBasicReflectorAmount.text);
        mapDataHolder.yellowBasicReflectorAmount = int.Parse(ifYellowBasicReflectorAmount.text);
        mapDataHolder.blueBasicReflectorAmount = int.Parse(ifBlueBasicReflectorAmount.text);
        mapDataHolder.translucentReflectorAmount = int.Parse(ifWhiteTranslucentReflectorAmount.text);
        mapDataHolder.redTranslucentReflectorAmount = int.Parse(ifRedTranslucentReflectorAmount.text);
        mapDataHolder.yellowTranslucentReflectorAmount = int.Parse(ifYellowTranslucentReflectorAmount.text);
        mapDataHolder.blueTranslucentReflectorAmount = int.Parse(ifBlueTranslucentReflectorAmount.text);
        mapDataHolder.doubleWayReflectorAmount = int.Parse(ifWhiteDoubleWayReflectorAmount.text);
        mapDataHolder.redDoubleWayReflectorAmount = int.Parse(ifRedDoubleWayReflectorAmount.text);
        mapDataHolder.yellowDoubleWayReflectorAmount = int.Parse(ifYellowDoubleWayReflectorAmount.text);
        mapDataHolder.blueDoubleWayReflectorAmount = int.Parse(ifBlueDoubleWayReflectorAmount.text);
        mapDataHolder.splitReflectorAmount = int.Parse(ifWhiteSplitReflectorAmount.text);
        mapDataHolder.redSplitReflectorAmount = int.Parse(ifRedSplitReflectorAmount.text);
        mapDataHolder.yellowSplitReflectorAmount = int.Parse(ifYellowSplitReflectorAmount.text);
        mapDataHolder.blueSplitReflectorAmount = int.Parse(ifBlueSplitReflectorAmount.text);
        mapDataHolder.threeWayReflectorAmount = int.Parse(ifWhiteThreeWayReflectorAmount.text);
        mapDataHolder.redThreeWayReflectorAmount = int.Parse(ifRedThreeWayReflectorAmount.text);
        mapDataHolder.yellowThreeWayReflectorAmount = int.Parse(ifYellowThreeWayReflectorAmount.text);
        mapDataHolder.blueThreeWayReflectorAmount = int.Parse(ifBlueThreeWayReflectorAmount.text);

        string jsonData = JsonUtility.ToJson(mapDataHolder, true);

        using (FileStream file = new FileStream(LoadedMapDataPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(file))
                writer.Write(jsonData);
        }

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

        ResetAllSnappers();
        mapLayoutGameObj.SetActive(false);

        MapEditorInputManager.Instance.SwitchSelectedPanel(MAP_EDITING_PANEL.NONE);

        MapEditorInSceneObject[] inSceneObjects = FindObjectsOfType<MapEditorInSceneObject>();
        foreach (var inSceneObject in inSceneObjects)
            Destroy(inSceneObject.gameObject);

        LoadedMapDataPath = "";
        MapEditorInputManager.Instance.MapEditing = false;

        if(MapEditorInputManager.Instance.OptionMenuVisibility)
            ToggleOptionMenu();
        if (MapEditorInputManager.Instance.LevelSettingsVisibility)
            ToggleLevelSettingsMenu();
    }

    private void MapSnappersInitialization()
    {
        MapLayoutBorder[] mapLayoutBorders = FindObjectsOfType<MapLayoutBorder>();
        foreach (var mapLayoutBorder in mapLayoutBorders)
            mapLayoutBorder.Initialization();

        MapLayoutBoxSnapper[] mapLayoutBoxSnappers = FindObjectsOfType<MapLayoutBoxSnapper>();
        foreach (var mapLayoutBoxSnapper in mapLayoutBoxSnappers)
            mapLayoutBoxSnapper.Initialization();

        MapLayoutPortalSnapper[] mapLayoutPortalSnappers = FindObjectsOfType<MapLayoutPortalSnapper>();
        foreach (var mapLayoutPortalSnapper in mapLayoutPortalSnappers)
            mapLayoutPortalSnapper.Initialization();
    }

    private void ResetAllSnappers()
    {
        MapLayoutBorder[] allBorderSnappers = FindObjectsOfType<MapLayoutBorder>();
        MapLayoutBoxSnapper[] allBoxSnappers = FindObjectsOfType<MapLayoutBoxSnapper>();
        MapLayoutPortalSnapper[] allPortalSnappers = FindObjectsOfType<MapLayoutPortalSnapper>();
        foreach (var borderSnapper in allBorderSnappers)
            borderSnapper.GotSnappedObject = false;
        foreach (var boxSnapper in allBoxSnappers)
            boxSnapper.GotSnappedObject = false;
        foreach (var portalSnapper in allPortalSnappers)
            portalSnapper.GotSnappedObject = false;
    }

    public void MasterFix()
    {
        List<MapEditorInSceneObject> allInSceneObjs = new List<MapEditorInSceneObject>(FindObjectsOfType<MapEditorInSceneObject>());
        List<MapEditorInSceneObject> inSceneObjsToBeRemoved = new List<MapEditorInSceneObject>();

        List<MapEditorInSceneObject> allOriginEndPoints = new List<MapEditorInSceneObject>();
        List<MapEditorInSceneObject> allHorizontalLines = new List<MapEditorInSceneObject>();
        List<MapEditorInSceneObject> allVerticalLines = new List<MapEditorInSceneObject>();
        List<MapEditorInSceneObject> allHorizontalPortals = new List<MapEditorInSceneObject>();
        List<MapEditorInSceneObject> allVerticalPortals = new List<MapEditorInSceneObject>();

        foreach(var inSceneObj in allInSceneObjs)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            if (objType == IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE || objType == IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED || objType == IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW || objType == IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE || 
                objType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE || objType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED || objType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW || objType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE)
            {
                allOriginEndPoints.Add(inSceneObj);
                inSceneObjsToBeRemoved.Add(inSceneObj);
            }
        }

        foreach(var inSceneObj in inSceneObjsToBeRemoved)
        {
            allInSceneObjs.Remove(inSceneObj);
        }

        inSceneObjsToBeRemoved.Clear();

        foreach (var inSceneObj in allInSceneObjs)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            if (objType == IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE ||
                objType == IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE)
            {
                allHorizontalLines.Add(inSceneObj);
                inSceneObjsToBeRemoved.Add(inSceneObj);
            }
        }

        foreach (var inSceneObj in inSceneObjsToBeRemoved)
        {
            allInSceneObjs.Remove(inSceneObj);
        }

        inSceneObjsToBeRemoved.Clear();

        foreach (var inSceneObj in allInSceneObjs)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            if (objType == IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE ||
                objType == IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE)
            {
                allVerticalLines.Add(inSceneObj);
                inSceneObjsToBeRemoved.Add(inSceneObj);
            }
        }

        foreach (var inSceneObj in inSceneObjsToBeRemoved)
        {
            allInSceneObjs.Remove(inSceneObj);
        }

        inSceneObjsToBeRemoved.Clear();

        foreach(var inSceneObj in allInSceneObjs)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            if(objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL)
            {
                allHorizontalPortals.Add(inSceneObj);
                inSceneObjsToBeRemoved.Add(inSceneObj);
            }
        }

        foreach (var inSceneObj in inSceneObjsToBeRemoved)
        {
            allInSceneObjs.Remove(inSceneObj);
        }

        inSceneObjsToBeRemoved.Clear();

        foreach (var inSceneObj in allInSceneObjs)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            if (objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL)
            {
                allVerticalPortals.Add(inSceneObj);
                inSceneObjsToBeRemoved.Add(inSceneObj);
            }
        }

        foreach (var inSceneObj in inSceneObjsToBeRemoved)
        {
            allInSceneObjs.Remove(inSceneObj);
        }

        inSceneObjsToBeRemoved.Clear();

        DuplicatesFix(allOriginEndPoints);
        DuplicatesFix(allHorizontalLines);
        DuplicatesFix(allVerticalLines);
        DuplicatesFix(allHorizontalPortals);
        DuplicatesFix(allVerticalPortals);

        List<MapGridMapEditor> allMapGrids = new List<MapGridMapEditor>(FindObjectsOfType<MapGridMapEditor>());

        PositionsFix(allMapGrids, allOriginEndPoints);
        PositionsFix(allMapGrids, allHorizontalLines);
        PositionsFix(allMapGrids, allVerticalLines);

        MapSnappersInitialization();
    }

    private void DuplicatesFix(List<MapEditorInSceneObject> inSceneObjs)
    {
        if (inSceneObjs.Count <= 0)
            return;

        IN_SCENE_OBJECT_TYPES objType = inSceneObjs[0].inSceneObjectType;

        switch (objType)
        {
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                List<int> duplicatedPointObjIndexes = new List<int>();
                for(int i = 0; i < inSceneObjs.Count - 1; i++)
                {
                    if (!duplicatedPointObjIndexes.Contains(i))
                    {
                        for (int j = i + 1; j < inSceneObjs.Count; j++)
                        {
                            if (inSceneObjs[i].InSceneObjData.mapGridIndex == inSceneObjs[j].InSceneObjData.mapGridIndex)
                            {
                                duplicatedPointObjIndexes.Add(j);
                            }
                        }
                    }
                }

                foreach (var duplicatedPointObjIndex in duplicatedPointObjIndexes)
                {
                    if (inSceneObjs[duplicatedPointObjIndex].SnappedTargetBox)
                    {
                        inSceneObjs[duplicatedPointObjIndex].SnappedTargetBox.GotSnappedObject = false;
                    }
                    Destroy(inSceneObjs[duplicatedPointObjIndex].gameObject);
                }

                inSceneObjs.RemoveAll(obj => obj == null);
                break;

            case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                List<int> duplicatedHorizontalLineIndexes = new List<int>();
                List<MapEditorInSceneObject> notValidHorizontalLines = new List<MapEditorInSceneObject>();

                foreach(var horizontalLine in inSceneObjs)
                {
                    if (horizontalLine.InSceneObjData.borderDir == SNAPPING_DIR.NONE)
                        notValidHorizontalLines.Add(horizontalLine);
                }

                foreach(var horizontalLine in notValidHorizontalLines)
                {
                    inSceneObjs.Remove(horizontalLine);
                    Destroy(horizontalLine.gameObject);
                }

                notValidHorizontalLines.Clear();

                for(int i = 0; i < inSceneObjs.Count - 1; i++)
                {
                    if (!duplicatedHorizontalLineIndexes.Contains(i))
                    {
                        for (int j = i + 1; j < inSceneObjs.Count; j++)
                        {
                            if (inSceneObjs[i].InSceneObjData.mapGridIndex == inSceneObjs[j].InSceneObjData.mapGridIndex && inSceneObjs[i].InSceneObjData.borderDir == inSceneObjs[j].InSceneObjData.borderDir)
                            {
                                duplicatedHorizontalLineIndexes.Add(j);
                            }
                        }
                    }
                }

                foreach (var duplicatedHorizontalLineIndex in duplicatedHorizontalLineIndexes)
                {
                    if (inSceneObjs[duplicatedHorizontalLineIndex].SnappedTargetBox)
                    {
                        inSceneObjs[duplicatedHorizontalLineIndex].SnappedTargetBox.GotSnappedObject = false;
                    }
                    Destroy(inSceneObjs[duplicatedHorizontalLineIndex].gameObject);
                }

                inSceneObjs.RemoveAll(obj => obj == null);
                break;

            case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                List<int> duplicatedVerticalLineIndexes = new List<int>();
                List<MapEditorInSceneObject> notValidVerticalLines = new List<MapEditorInSceneObject>();

                foreach (var verticalLine in inSceneObjs)
                {
                    if (verticalLine.InSceneObjData.borderDir == SNAPPING_DIR.NONE)
                        notValidVerticalLines.Add(verticalLine);
                }

                foreach (var verticalLine in notValidVerticalLines)
                {
                    inSceneObjs.Remove(verticalLine);
                    Destroy(verticalLine.gameObject);
                }

                notValidVerticalLines.Clear();

                for (int i = 0; i < inSceneObjs.Count - 1; i++)
                {
                    if (!duplicatedVerticalLineIndexes.Contains(i))
                    {
                        for (int j = i + 1; j < inSceneObjs.Count; j++)
                        {
                            if (inSceneObjs[i].InSceneObjData.mapGridIndex == inSceneObjs[j].InSceneObjData.mapGridIndex && inSceneObjs[i].InSceneObjData.borderDir == inSceneObjs[j].InSceneObjData.borderDir)
                            {
                                duplicatedVerticalLineIndexes.Add(j);
                            }
                        }
                    }
                }

                foreach (var duplicatedVerticalLineIndex in duplicatedVerticalLineIndexes)
                {
                    if (inSceneObjs[duplicatedVerticalLineIndex].SnappedTargetBox)
                    {
                        inSceneObjs[duplicatedVerticalLineIndex].SnappedTargetBox.GotSnappedObject = false;
                    }
                    Destroy(inSceneObjs[duplicatedVerticalLineIndex].gameObject);
                }

                inSceneObjs.RemoveAll(obj => obj == null);
                break;

            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                List<int> duplicatedHorizontalPortalIndexes = new List<int>();
                List<MapEditorInSceneObject> notValidHorizontalPortals = new List<MapEditorInSceneObject>();

                foreach (var horizontalPortal in inSceneObjs)
                {
                    if (horizontalPortal.InSceneObjData.borderDir == SNAPPING_DIR.NONE)
                        notValidHorizontalPortals.Add(horizontalPortal);
                }

                foreach (var horizontalPortal in notValidHorizontalPortals)
                {
                    inSceneObjs.Remove(horizontalPortal);
                    Destroy(horizontalPortal.gameObject);
                }

                notValidHorizontalPortals.Clear();

                for (int i = 0; i < inSceneObjs.Count - 1; i++)
                {
                    if (!duplicatedHorizontalPortalIndexes.Contains(i))
                    {
                        for (int j = i + 1; j < inSceneObjs.Count; j++)
                        {
                            if (inSceneObjs[i].InSceneObjData.mapGridIndex == inSceneObjs[j].InSceneObjData.mapGridIndex && inSceneObjs[i].InSceneObjData.borderDir == inSceneObjs[j].InSceneObjData.borderDir)
                            {
                                duplicatedHorizontalPortalIndexes.Add(j);
                            }
                        }
                    }
                }

                foreach (var duplicatedHorizontalPortalIndex in duplicatedHorizontalPortalIndexes)
                {
                    if (inSceneObjs[duplicatedHorizontalPortalIndex].SnappedTargetBox)
                    {
                        inSceneObjs[duplicatedHorizontalPortalIndex].SnappedTargetBox.GotSnappedObject = false;
                    }
                    Destroy(inSceneObjs[duplicatedHorizontalPortalIndex].gameObject);
                }

                inSceneObjs.RemoveAll(obj => obj == null);
                break;

            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                List<int> duplicatedVerticalPortalIndexes = new List<int>();
                List<MapEditorInSceneObject> notValidVerticalPortals = new List<MapEditorInSceneObject>();

                foreach (var verticalPortal in inSceneObjs)
                {
                    if (verticalPortal.InSceneObjData.borderDir == SNAPPING_DIR.NONE)
                        notValidVerticalPortals.Add(verticalPortal);
                }

                foreach (var verticalPortal in notValidVerticalPortals)
                {
                    inSceneObjs.Remove(verticalPortal);
                    Destroy(verticalPortal.gameObject);
                }

                notValidVerticalPortals.Clear();

                for (int i = 0; i < inSceneObjs.Count - 1; i++)
                {
                    if (!duplicatedVerticalPortalIndexes.Contains(i))
                    {
                        for (int j = i + 1; j < inSceneObjs.Count; j++)
                        {
                            if (inSceneObjs[i].InSceneObjData.mapGridIndex == inSceneObjs[j].InSceneObjData.mapGridIndex && inSceneObjs[i].InSceneObjData.borderDir == inSceneObjs[j].InSceneObjData.borderDir)
                            {
                                duplicatedVerticalPortalIndexes.Add(j);
                            }
                        }
                    }
                }

                foreach (var duplicatedVerticalPortalIndex in duplicatedVerticalPortalIndexes)
                {
                    if (inSceneObjs[duplicatedVerticalPortalIndex].SnappedTargetBox)
                    {
                        inSceneObjs[duplicatedVerticalPortalIndex].SnappedTargetBox.GotSnappedObject = false;
                    }
                    Destroy(inSceneObjs[duplicatedVerticalPortalIndex].gameObject);
                }

                inSceneObjs.RemoveAll(obj => obj == null);
                break;
        }
    }

    private void PositionsFix(List<MapGridMapEditor> allMapGrids, List<MapEditorInSceneObject> inSceneObjs)
    {
        if (inSceneObjs.Count <= 0)
            return;

        IN_SCENE_OBJECT_TYPES objType = inSceneObjs[0].inSceneObjectType;

        switch (objType)
        {
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                foreach(var inSceneObj in inSceneObjs)
                {
                    foreach(var mapGrid in allMapGrids)
                    {
                        if(inSceneObj.InSceneObjData.mapGridIndex == mapGrid.MapGridIndex)
                        {
                            inSceneObj.transform.position = mapGrid.GetBoxSnappingPosition();
                            inSceneObj.UpdateInSceneObjectData();
                        }
                    }
                }
                break;
            case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
            case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
            case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                foreach (var inSceneObj in inSceneObjs)
                {
                    foreach (var mapGrid in allMapGrids)
                    {
                        if (inSceneObj.InSceneObjData.mapGridIndex == mapGrid.MapGridIndex)
                        {
                            inSceneObj.transform.position = mapGrid.GetBorderSnappingPosition(inSceneObj.InSceneObjData.borderDir);
                            inSceneObj.UpdateInSceneObjectData();
                        }
                    }
                }
                break;
            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                foreach (var inSceneObj in inSceneObjs)
                {
                    foreach (var mapGrid in allMapGrids)
                    {
                        if (inSceneObj.InSceneObjData.mapGridIndex == mapGrid.MapGridIndex)
                        {
                            inSceneObj.transform.position = mapGrid.GetPortalSnappingPosition(inSceneObj.InSceneObjData.borderDir);
                            inSceneObj.UpdateInSceneObjectData();
                        }
                    }
                }
                break;
        }
    }

    private void UpdateIFLevelSettings()
    {
        ifTimeLimit.text = timeLimit.ToString();
        ifWhiteBasicReflectorAmount.text = whiteBasicReflectorAmount.ToString();
        ifRedBasicReflectorAmount.text = redBasicReflectorAmount.ToString();
        ifYellowBasicReflectorAmount.text = yellowBasicReflectorAmount.ToString();
        ifBlueBasicReflectorAmount.text = blueBasicReflectorAmount.ToString();
        ifWhiteTranslucentReflectorAmount.text = whiteTranslucentReflectorAmount.ToString();
        ifRedTranslucentReflectorAmount.text = redTranslucentReflectorAmount.ToString();
        ifYellowTranslucentReflectorAmount.text = yellowTranslucentReflectorAmount.ToString();
        ifBlueTranslucentReflectorAmount.text = blueTranslucentReflectorAmount.ToString();
        ifWhiteDoubleWayReflectorAmount.text = whiteDoubleWayReflectorAmount.ToString();
        ifRedDoubleWayReflectorAmount.text = redDoubleWayReflectorAmount.ToString();
        ifYellowDoubleWayReflectorAmount.text = yellowDoubleWayReflectorAmount.ToString();
        ifBlueDoubleWayReflectorAmount.text = blueDoubleWayReflectorAmount.ToString();
        ifWhiteSplitReflectorAmount.text = whiteSplitReflectorAmount.ToString();
        ifRedSplitReflectorAmount.text = redSplitReflectorAmount.ToString();
        ifYellowSplitReflectorAmount.text = yellowSplitReflectorAmount.ToString();
        ifBlueSplitReflectorAmount.text = blueSplitReflectorAmount.ToString();
        ifWhiteThreeWayReflectorAmount.text = whiteThreeWayReflectorAmount.ToString();
        ifRedThreeWayReflectorAmount.text = redThreeWayReflectorAmount.ToString();
        ifYellowThreeWayReflectorAmount.text = yellowThreeWayReflectorAmount.ToString();
        ifBlueThreeWayReflectorAmount.text = blueThreeWayReflectorAmount.ToString();
    }

    public void OnIFSelect()
    {
        EditingIF = true;
    }

    public void OnIFDeselect()
    {
        EditingIF = false;
    }

    public void OnIFEndEdit()
    {
        EditingIF = false;
    }

    public MapEditorInSceneObject CreateInSceneObj(IN_SCENE_OBJECT_TYPES inSceneObjType)
    {
        MapEditorInSceneObject inSceneObj = null;
        switch (inSceneObjType)
        {
            case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(normalHorizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), normalHorizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(whiteHorizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), whiteHorizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(redHorizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), redHorizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(yellowHorizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), yellowHorizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(blueHorizontalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), blueHorizontalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(normalVerticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), normalVerticalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(whiteVerticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), whiteVerticalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(redVerticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), redVerticalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(yellowVerticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), yellowVerticalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(blueVerticalLinePrefab, new Vector3(-0.5f, -3.7f, 0.0f), blueVerticalLinePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(originPointWhitePrefab, new Vector3(-0.5f, -3.7f, 0.0f), originPointWhitePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                inSceneObj = Instantiate<MapEditorInSceneObject>(originPointRedPrefab, new Vector3(-0.5f, -3.7f, 0.0f), originPointRedPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                inSceneObj = Instantiate<MapEditorInSceneObject>(originPointYellowPrefab, new Vector3(-0.5f, -3.7f, 0.0f), originPointYellowPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(originPointBluePrefab, new Vector3(-0.5f, -3.7f, 0.0f), originPointBluePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(destinationPointWhitePrefab, new Vector3(-0.5f, -3.7f, 0.0f), destinationPointWhitePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                inSceneObj = Instantiate<MapEditorInSceneObject>(destinationPointRedPrefab, new Vector3(-0.5f, -3.7f, 0.0f), destinationPointRedPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                inSceneObj = Instantiate<MapEditorInSceneObject>(destinationPointYellowPrefab, new Vector3(-0.5f, -3.7f, 0.0f), destinationPointYellowPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                inSceneObj = Instantiate<MapEditorInSceneObject>(destinationPointBluePrefab, new Vector3(-0.5f, -3.7f, 0.0f), destinationPointBluePrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                inSceneObj = Instantiate<MapEditorInSceneObject>(firstSetPortalHorizontalPrefab, new Vector3(-0.5f, -3.7f, 0.0f), firstSetPortalHorizontalPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                inSceneObj = Instantiate<MapEditorInSceneObject>(firstSetPortalVerticalPrefab, new Vector3(-0.5f, -3.7f, 0.0f), firstSetPortalVerticalPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                inSceneObj = Instantiate<MapEditorInSceneObject>(secondSetPortalHorizontalPrefab, new Vector3(-0.5f, -3.7f, 0.0f), secondSetPortalHorizontalPrefab.transform.rotation);
                break;
            case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                inSceneObj = Instantiate<MapEditorInSceneObject>(secondSetPortalVerticalPrefab, new Vector3(-0.5f, -3.7f, 0.0f), secondSetPortalVerticalPrefab.transform.rotation);
                break;
        }

        return inSceneObj;
    }
    
    public void ToggleOptionMenu()
    {
        if (!uiHelperOptionMenu.InTransition)
        {
            uiHelperOptionMenu.ExecuteUIHandlingAction();
            MapEditorInputManager.Instance.ToggleOptionMenuVisibility();
            cgPanelMapEditing.interactable = !(MapEditorInputManager.Instance.OptionMenuVisibility || MapEditorInputManager.Instance.LevelSettingsVisibility);
        }
    }

    public void ToggleLevelSettingsMenu()
    {
        if(!uiHelperLevelSettingsMenu.InTransition)
        {
            uiHelperLevelSettingsMenu.ExecuteUIHandlingAction();
            MapEditorInputManager.Instance.ToggleLevelSettingsMenuVisibility();
            cgPanelMapEditing.interactable = !(MapEditorInputManager.Instance.OptionMenuVisibility || MapEditorInputManager.Instance.LevelSettingsVisibility);
        }
    }

    public void ShowSubPanel(MAP_EDITING_PANEL selectedPanel, bool toggle = false)
    {
        switch (selectedPanel)
        {
            case MAP_EDITING_PANEL.NONE:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelPortals1stSet.gameObject.SetActive(false);
                rtPanelPortals2ndSet.gameObject.SetActive(false);
                foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                    creatorBtn.ToggleHotKeyText(true);
                break;
            case MAP_EDITING_PANEL.HORIZONTAL_LINE:
                if (toggle)
                {
                    rtPanelHorizontalLines.gameObject.SetActive(!rtPanelHorizontalLines.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelHorizontalLines.gameObject.activeSelf);
                }
                else
                {
                    rtPanelHorizontalLines.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelPortals1stSet.gameObject.SetActive(false);
                rtPanelPortals2ndSet.gameObject.SetActive(false);
                break;
            case MAP_EDITING_PANEL.VERTICAL_LINE:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelVerticalLines.gameObject.SetActive(!rtPanelVerticalLines.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelVerticalLines.gameObject.activeSelf);
                }
                else
                {
                    rtPanelVerticalLines.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelPortals1stSet.gameObject.SetActive(false);
                rtPanelPortals2ndSet.gameObject.SetActive(false);
                break;
            case MAP_EDITING_PANEL.ORIGIN_POINT:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelOriginPoints.gameObject.SetActive(!rtPanelOriginPoints.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelOriginPoints.gameObject.activeSelf);
                }
                else
                {
                    rtPanelOriginPoints.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                break;
            case MAP_EDITING_PANEL.DESTINATION_POINT:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelDestinationPoints.gameObject.SetActive(!rtPanelDestinationPoints.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelDestinationPoints.gameObject.activeSelf);
                }
                else
                {
                    rtPanelDestinationPoints.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelPortals1stSet.gameObject.SetActive(false);
                rtPanelPortals2ndSet.gameObject.SetActive(false);
                break;
            case MAP_EDITING_PANEL.PORTAL:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelPortals.gameObject.SetActive(!rtPanelPortals.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelPortals.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorPortalSubMenuButtons)
                        creatorBtn.ToggleHotKeyText(rtPanelPortals.gameObject.activeSelf);
                }
                else
                {
                    rtPanelPortals.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                    foreach (var creatorBtn in mapEditorObjectCreatorPortalSubMenuButtons)
                        creatorBtn.ToggleHotKeyText(true);
                }
                rtPanelPortals1stSet.gameObject.SetActive(false);
                rtPanelPortals2ndSet.gameObject.SetActive(false);

                break;
            case MAP_EDITING_PANEL.PORTAL_1ST_SET:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelPortals1stSet.gameObject.SetActive(!rtPanelPortals1stSet.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelPortals1stSet.gameObject.activeSelf);
                }
                else
                {
                    rtPanelPortals1stSet.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                rtPanelPortals2ndSet.gameObject.SetActive(false);
                break;
            case MAP_EDITING_PANEL.PORTAL_2ND_SET:
                rtPanelHorizontalLines.gameObject.SetActive(false);
                rtPanelVerticalLines.gameObject.SetActive(false);
                rtPanelOriginPoints.gameObject.SetActive(false);
                rtPanelDestinationPoints.gameObject.SetActive(false);
                rtPanelPortals.gameObject.SetActive(false);
                rtPanelPortals1stSet.gameObject.SetActive(false);
                if (toggle)
                {
                    rtPanelPortals2ndSet.gameObject.SetActive(!rtPanelPortals2ndSet.gameObject.activeSelf);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(!rtPanelPortals2ndSet.gameObject.activeSelf);
                }
                else
                {
                    rtPanelPortals2ndSet.gameObject.SetActive(true);
                    foreach (var creatorBtn in mapEditorObjectCreatorParentButtons)
                        creatorBtn.ToggleHotKeyText(false);
                }
                break;
        }
    }
}
