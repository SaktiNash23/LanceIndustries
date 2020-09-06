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

    public int MapIndex { get; set; }

    public MapInfo TargetMapInfo { get; set; }

    //------------------------------- MONOBEHAVIOUR FUNCTIONS -------------------------------//

    private void OnEnable()
    {
        btnLevel.onClick.AddListener(() => MainMenuUIManager.Instance.ShowLevelPreview(true, MapIndex));
    }

    private void OnDisable()
    {
        btnLevel.onClick.RemoveAllListeners();
    }

    //------------------------------- POPULARIZATION -------------------------------//

    public void PopularizeDisplay(MapInfo mapInfo)
    {
        TargetMapInfo = mapInfo;

        int openBracketIndex = mapInfo.mapName.IndexOf('(');
        int closeBracketIndex = mapInfo.mapName.IndexOf(')');
        
        string targetName = "";

        for(int i = openBracketIndex + 1; i < closeBracketIndex; i++)
        {
            targetName += mapInfo.mapName[i];
        }

        txtLevel.text = targetName;
    }
}
