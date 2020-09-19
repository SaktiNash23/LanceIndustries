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

    [BoxGroup("PREFABS")] [SerializeField] Proto_LaserOrigin originPointPrefab;
    [BoxGroup("PREFABS")] [SerializeField] EndPoint endPointPrefab;
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
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    Proto_LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointWhite.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    Proto_LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointRed.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    Proto_LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointYellow.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    Proto_LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointBlue.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    EndPoint endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointWhite.targetLaser = EndPoint.targetLaserColor.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    EndPoint endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointRed.targetLaser = EndPoint.targetLaserColor.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    EndPoint endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointYellow.targetLaser = EndPoint.targetLaserColor.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    EndPoint endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointBlue.targetLaser = EndPoint.targetLaserColor.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.SetRotation(inSceneObjData.borderDir);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.SetRotation(inSceneObjData.borderDir);
                    break;
            }
        }

        GameManager.gameManagerInstance.Initialization(mapDataHolder);
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
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    Proto_LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointWhite.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    Proto_LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointRed.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    Proto_LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointYellow.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    Proto_LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointBlue.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    EndPoint endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointWhite.targetLaser = EndPoint.targetLaserColor.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    EndPoint endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointRed.targetLaser = EndPoint.targetLaserColor.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    EndPoint endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointYellow.targetLaser = EndPoint.targetLaserColor.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    EndPoint endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointBlue.targetLaser = EndPoint.targetLaserColor.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.SetRotation(inSceneObjData.borderDir);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.SetRotation(inSceneObjData.borderDir);
                    break;
            }
        }

        GameManager.gameManagerInstance.Initialization(mapDataHolder);
    }

    public void LoadLevel(MapDataHolder mapData)
    {
        foreach (var inSceneObjData in mapData.inSceneObjectDatas)
        {
            switch (inSceneObjData.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.WHITE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.RED, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.YELLOW, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleColoredBorder(BorderColor.BLUE, inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    Proto_LaserOrigin originPointWhite = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointWhite.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.WHITE;
                    originPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    Proto_LaserOrigin originPointRed = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointRed.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.RED;
                    originPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    Proto_LaserOrigin originPointYellow = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointYellow.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.YELLOW;
                    originPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    Proto_LaserOrigin originPointBlue = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    originPointBlue.laserColor_StartingPoint = Proto_LaserOrigin.LaserColor_StartingPoint.BLUE;
                    originPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    EndPoint endPointWhite = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointWhite.targetLaser = EndPoint.targetLaserColor.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    EndPoint endPointRed = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointRed.targetLaser = EndPoint.targetLaserColor.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    EndPoint endPointYellow = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointYellow.targetLaser = EndPoint.targetLaserColor.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    EndPoint endPointBlue = Instantiate(endPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
                    endPointBlue.targetLaser = EndPoint.targetLaserColor.BLUE;
                    endPointBlue.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    Teleporter teleporter1stSet = Instantiate(teleporterSetAPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter1stSet.SetRotation(inSceneObjData.borderDir);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    Teleporter teleporter2ndSet = Instantiate(teleporterSetBPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().GetBorderTransform(inSceneObjData.borderDir));
                    teleporter2ndSet.SetRotation(inSceneObjData.borderDir);
                    break;
            }
        }

        GameManager.gameManagerInstance.Initialization(mapData);
    }
}
