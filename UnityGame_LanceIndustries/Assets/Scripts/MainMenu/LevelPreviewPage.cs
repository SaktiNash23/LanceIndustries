using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class LevelPreviewPage : MonoBehaviour
{
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtStageNumber;
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtHighScoreNumber;

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
    }
}
