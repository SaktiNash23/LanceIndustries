using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;
    
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] bool debugMode;
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] TextAsset levelToLoad;

    [BoxGroup("GRID REFERENCES")] [SerializeField] Transform transformGrids;

    [BoxGroup("BORDER REFERENCES")] [SerializeField] Transform transformBorders;

    [BoxGroup("PREFABS")] [SerializeField] Proto_LaserOrigin originPointPrefab;
    [BoxGroup("PREFABS")] [SerializeField] EndPoint endPointPrefab;

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
        if(debugMode && levelToLoad != null)
        {
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
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                    Proto_LaserOrigin originPoint = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
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
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                    Proto_LaserOrigin originPoint = Instantiate(originPointPrefab, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorders.GetChild(inSceneObjData.mapGridIndex).transform);
                    transformGrids.GetChild(inSceneObjData.mapGridIndex).gameObject.SetActive(false);
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
            }
        }

        GameManager.gameManagerInstance.Initialization(mapDataHolder);
    }
}
