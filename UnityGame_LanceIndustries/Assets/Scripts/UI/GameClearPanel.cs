using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnNextStage;
    [SerializeField] private Button btnMainMenu;

    private UIHelper uiHelper;
    public UIHelper UIHelper
    {
        get
        {
            if (!uiHelper)
                uiHelper = GetComponent<UIHelper>();
            return uiHelper;
        }
    }

    private void OnEnable()
    {
        btnNextStage.onClick.AddListener(LoadNextStage);
        btnMainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        btnNextStage.onClick.RemoveListener(LoadNextStage);
        btnMainMenu.onClick.RemoveListener(LoadMainMenu);
    }

    private void LoadNextStage()
    {
        PersistentDataManager.Instance.SelectNextMap();
        SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.GAMEPLAY_SCENE);
    }

    private void LoadMainMenu()
    {
        SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU);
    }

    public void RefreshButtonInteractable()
    {
        btnNextStage.interactable = PersistentDataManager.Instance.HasNextMap();
    }
}
