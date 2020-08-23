using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class LevelPreviewPage : MonoBehaviour
{
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtStageNumber;
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtHighScoreNumber;
    [BoxGroup("REFERENCES")] [SerializeField] Button btnSelectStage;

    [BoxGroup("REFERENCES")] [SerializeField] GameObject levelLayout;

    private MapDataHolder mapData = null;

    public void PopularizeDisplay(MainMenuLevelUI mainMenuLevelUI)
    {
        string targetStageStr = "";

        if(mainMenuLevelUI.TargetMapInfo.mapName.Contains("("))
        {
            int startIndex = mainMenuLevelUI.TargetMapInfo.mapName.IndexOf('(') + 1;
            int endIndex = mainMenuLevelUI.TargetMapInfo.mapName.IndexOf(')');

            for(int i = startIndex; i < endIndex; i++)
            {
                targetStageStr += mainMenuLevelUI.TargetMapInfo.mapName[i];
            }
        }
        else
        {
            targetStageStr = (mainMenuLevelUI.MapIndex + 1).ToString();
        }

        txtStageNumber.text = targetStageStr;

        mapData = PersistentDataManager.Instance.GetMapDataHolder(mainMenuLevelUI.TargetMapInfo.mapName);

        btnSelectStage.onClick.RemoveAllListeners();

        btnSelectStage.onClick.AddListener(() =>
        {
            if (!MainMenuUIManager.Instance.SwitchingLevelPage)
            {
                PersistentDataManager.Instance.SelectedMapDataHolderNamePair = PersistentDataManager.Instance.GetMapDataHolderNamePair(mainMenuLevelUI.TargetMapInfo.mapName);
                PersistentDataManager.Instance.UpdateSelectedMapIndex();
                btnSelectStage.interactable = false;
                SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.GAMEPLAY_SCENE);
            }
        });

        // Toggle all to off
        for (int i = 0; i < levelLayout.transform.childCount; i++)
        {
            levelLayout.transform.GetChild(i).GetComponent<MapGridUI>().ToggleAllWalls(false);
            levelLayout.transform.GetChild(i).GetComponent<MapGridUI>().ToggleOriginPoint(default, Quaternion.identity, false);
            levelLayout.transform.GetChild(i).GetComponent<MapGridUI>().ToggleDestinationPoint(default, Quaternion.identity, false);
            levelLayout.transform.GetChild(i).GetComponent<MapGridUI>().ToggleAllPortals(false);
        }

        // Toggle those right one
        foreach(var inSceneObj in mapData.inSceneObjectDatas)
        {
            switch (inSceneObj.inSceneObjectType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleWall(BorderColor.NONE, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleWall(BorderColor.WHITE, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleWall(BorderColor.RED, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleWall(BorderColor.YELLOW, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleWall(BorderColor.BLUE, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_WHITE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleOriginPoint(BorderColor.WHITE, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_RED:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleOriginPoint(BorderColor.RED, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_YELLOW:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleOriginPoint(BorderColor.YELLOW, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT_BLUE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleOriginPoint(BorderColor.RED, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleDestinationPoint(BorderColor.WHITE, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleDestinationPoint(BorderColor.RED, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleDestinationPoint(BorderColor.YELLOW, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().ToggleDestinationPoint(BorderColor.BLUE, inSceneObj.rotation, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().TogglePortal(true, inSceneObj.borderDir, true);
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    levelLayout.transform.GetChild(inSceneObj.mapGridIndex).GetComponent<MapGridUI>().TogglePortal(false, inSceneObj.borderDir, true);
                    break;
            }
        }
    }
}
