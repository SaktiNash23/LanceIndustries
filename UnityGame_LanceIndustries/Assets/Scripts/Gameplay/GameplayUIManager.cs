using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public Button btnReturnToMainMenu;
    public Button btnNextStage;
    public Button btnStageSelection;

    private void OnEnable()
    {
        btnReturnToMainMenu.onClick.AddListener(() => PersistentDataManager.Instance.SetTimeScale(1f));
        btnReturnToMainMenu.onClick.AddListener(() => SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU));
        btnStageSelection.onClick.AddListener(() => PersistentDataManager.Instance.SetTimeScale(1f));
        btnStageSelection.onClick.AddListener(() => SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU));
        if(PersistentDataManager.Instance.GetSelectedMapIndex() < PersistentDataManager.Instance.MapDataHolderNamePairs.Count - 1)
        {
            int targetMapIndex = PersistentDataManager.Instance.GetSelectedMapIndex() + 1;
            btnNextStage.onClick.AddListener(() => PersistentDataManager.Instance.SetTimeScale(1f));
            btnNextStage.onClick.AddListener(() => PersistentDataManager.Instance.SelectedMapDataHolderNamePair = PersistentDataManager.Instance.MapDataHolderNamePairs[targetMapIndex]);
            btnNextStage.onClick.AddListener(() => PersistentDataManager.Instance.UpdateSelectedMapIndex());
            btnNextStage.onClick.AddListener(() => SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.GAMEPLAY_SCENE));
        }
        else
        {
            btnNextStage.interactable = false;
        }
    }

    private void OnDisable()
    {
        btnReturnToMainMenu.onClick.RemoveAllListeners();
        btnStageSelection.onClick.RemoveAllListeners();
        btnNextStage.onClick.RemoveAllListeners();
    }
}
