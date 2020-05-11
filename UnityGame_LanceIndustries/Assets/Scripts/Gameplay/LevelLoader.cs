using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelLoader : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;
    
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] bool debugMode;
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] TextAsset levelToLoad;

    [BoxGroup("BORDER REFERENCES")] [SerializeField] Transform transformBorder;

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

    public void LoadLevel()
    {
    }

    public void LoadLevel(TextAsset targetLevel)
    {
        MapDataHolder mapDataHolder = JsonUtility.FromJson<MapDataHolder>(targetLevel.text);
        foreach (var inSceneObjData in mapDataHolder.inSceneObjectDatas)
        {
            switch (inSceneObjData.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.VERTICAL_LINE:
                    transformBorder.GetChild(inSceneObjData.mapGridIndex).GetComponent<MapGridGameplay>().ToggleBorder(inSceneObjData.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                    Proto_LaserOrigin originPoint = Instantiate(originPointPrefab, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    EndPoint endPointWhite = Instantiate(endPointPrefab, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform);
                    endPointWhite.targetLaser = EndPoint.targetLaserColor.WHITE;
                    endPointWhite.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    EndPoint endPointRed = Instantiate(endPointPrefab, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform);
                    endPointRed.targetLaser = EndPoint.targetLaserColor.RED;
                    endPointRed.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    EndPoint endPointYellow = Instantiate(endPointPrefab, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform);
                    endPointYellow.targetLaser = EndPoint.targetLaserColor.YELLOW;
                    endPointYellow.Initialization();
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    EndPoint endPointBlue = Instantiate(endPointPrefab, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform.position, inSceneObjData.rotation, transformBorder.GetChild(inSceneObjData.mapGridIndex).transform);
                    endPointBlue.targetLaser = EndPoint.targetLaserColor.BLUE;
                    endPointBlue.Initialization();
                    break;
            }
        }
    }
}
