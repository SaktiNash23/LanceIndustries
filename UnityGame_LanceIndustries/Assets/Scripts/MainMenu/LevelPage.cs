using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelPage : MonoBehaviour
{
    [BoxGroup("PREFABS")] [SerializeField] MainMenuLevelUI levelUIPrefab;

    private static int currentPopularizedMapIndex = 0;

    public void PopularizeDisplay(List<MapInfo> mapInfos)
    {
        foreach(var mapInfo in mapInfos)
        {
            MainMenuLevelUI levelUI = Instantiate(levelUIPrefab, transform, false);
            MainMenuUIManager.Instance.LevelUIs.Add(levelUI);
            levelUI.PopularizeDisplay(mapInfo);
            levelUI.MapIndex = currentPopularizedMapIndex;
            currentPopularizedMapIndex++;
        }
    }
}
