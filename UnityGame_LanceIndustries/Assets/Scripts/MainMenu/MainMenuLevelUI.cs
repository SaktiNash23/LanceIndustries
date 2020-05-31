using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class MainMenuLevelUI : MonoBehaviour
{
    [BoxGroup("REFERENCES")] [SerializeField] Button btnLevel;
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtLevel;
    [BoxGroup("REFERENCES")] [SerializeField] TMP_Text txtHighscore;

    public void PopularizeDisplay(MapInfo mapInfo)
    {
        int openBracketIndex = mapInfo.mapName.IndexOf('(');
        int closeBracketIndex = mapInfo.mapName.IndexOf(')');
        
        string targetName = "";

        for(int i = openBracketIndex + 1; i < closeBracketIndex; i++)
        {
            targetName += mapInfo.mapName[i];
        }

        txtLevel.text = targetName;
        
        //txtHighscore
    }
}
