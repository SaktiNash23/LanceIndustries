using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class MainMenuUIManager : MonoBehaviour
{
    [BoxGroup("REFERENCES")] [SerializeField] RectTransform rtContentLevels;

    [BoxGroup("PREFABS")] [SerializeField] MainMenuLevelUI mainMenuLevelUIPrefab;

    public void Start()
    {
        InitializeLevelSelection();
    }

    public void InitializeLevelSelection()
    {
        foreach(var mapInfo in LibraryLinker.Instance.MapInfoLib.mapInfos)
        {
            MainMenuLevelUI levelUI = Instantiate(mainMenuLevelUIPrefab, rtContentLevels, false);
            levelUI.PopularizeDisplay(mapInfo);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
