using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;
    
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] bool debugMode;
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] TextAsset levelToLoad;

    [BoxGroup("GRID REFERENCES")] [SerializeField] Transform transformGrids;

    [BoxGroup("BORDER REFERENCES")] [SerializeField] Transform transformBorders;

    [BoxGroup("PREFABS")] [SerializeField] LaserOrigin originPointPrefab;
    [BoxGroup("PREFABS")] [SerializeField] LaserDestination endPointPrefab;
    [BoxGroup("PREFABS")] [SerializeField] Teleporter teleporterSetAPrefab;
    [BoxGroup("PREFABS")] [SerializeField] Teleporter teleporterSetBPrefab;

    private static LevelLoader _instance;
    public static LevelLoader Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            if(_instance != this)
                Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void Start()
    {
        if(PersistentDataManager.Instance?.SelectedMapDataHolderNamePair != null)
        {
            LoadLevel(PersistentDataManager.Instance.SelectedMapDataHolderNamePair.mapDataHolder);
            PersistentDataManager.Instance.SelectedMapDataHolderNamePair = null;
            return;
        }

        if(debugMode && levelToLoad != null)
        {
            PersistentDataManager.Instance.SelectedMapDataHolderNamePair = PersistentDataManager.Instance.GetMapDataHolderNamePair(levelToLoad.name);
            PersistentDataManager.Instance.UpdateSelectedMapIndex();
            LoadLevel(levelToLoad);
        }
    }

    public void LoadLevel(string levelName)
    {
        TextAsset loadedLevel = Resources.Load<TextAsset>("Map Datas/" + levelName);
        MapDataHolder mapDataHolder = JsonUtility.FromJson<MapDataHolder>(loadedLevel.text);
        foreach (var inSceneObjData in mapDataHolder.inSceneObjectDatas)
        {
            switch (inSceneObjData.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.DEFAULT, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointWhite.LaserColor = LASER_COLOR.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointRed.LaserColor = LASER_COLOR.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointBlue.LaserColor = LASER_COLOR.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    LaserDestination endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointWhite.LaserColor = LASER_COLOR.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    LaserDestination endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointRed.LaserColor = LASER_COLOR.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    LaserDestination endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    LaserDestination endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointBlue.LaserColor = LASER_COLOR.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.Initialization(inSceneObjData.borderDir, 1);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.Initialization(inSceneObjData.borderDir, 2);
                    break;
            }
        }

        GameManager.Instance.Initialization(mapDataHolder);
    }

    public void LoadLevel(TextAsset targetLevel)
    {
        MapDataHolder mapDataHolder = JsonUtility.FromJson<MapDataHolder>(targetLevel.text);
        foreach (var inSceneObjData in mapDataHolder.inSceneObjectDatas)
        {
            switch (inSceneObjData.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.DEFAULT, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointWhite.LaserColor = LASER_COLOR.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointRed.LaserColor = LASER_COLOR.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointBlue.LaserColor = LASER_COLOR.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    LaserDestination endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointWhite.LaserColor = LASER_COLOR.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    LaserDestination endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointRed.LaserColor = LASER_COLOR.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    LaserDestination endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    LaserDestination endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointBlue.LaserColor = LASER_COLOR.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.Initialization(inSceneObjData.borderDir, 1);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.Initialization(inSceneObjData.borderDir, 2);
                    break;
            }
        }

        GameManager.Instance.Initialization(mapDataHolder);
    }

    public void LoadLevel(MapDataHolder mapData)
    {
        foreach (var inSceneObjData in mapData.inSceneObjectDatas)
        {
            switch (inSceneObjData.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.DEFAULT, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(LASER_COLOR.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointWhite.LaserColor = LASER_COLOR.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointRed.LaserColor = LASER_COLOR.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    originPointBlue.LaserColor = LASER_COLOR.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    LaserDestination endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointWhite.LaserColor = LASER_COLOR.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    LaserDestination endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointRed.LaserColor = LASER_COLOR.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    LaserDestination endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointYellow.LaserColor = LASER_COLOR.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    LaserDestination endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).GetComponent<Proto_Grid>().IsOccupied = true;
                    endPointBlue.LaserColor = LASER_COLOR.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.Initialization(inSceneObjData.borderDir, 1);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.Initialization(inSceneObjData.borderDir, 2);
                    break;
            }
        }

        GameManager.Instance.Initialization(mapData);
    }
}
