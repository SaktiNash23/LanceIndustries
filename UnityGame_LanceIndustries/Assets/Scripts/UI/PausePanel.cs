using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button btnResume;
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
        btnResume.onClick.AddListener(Resume);
        btnMainMenu.onClick.AddListener(LoadMainMenu);
    }

    private void OnDisable()
    {
        btnResume.onClick.RemoveListener(Resume);
        btnMainMenu.onClick.RemoveListener(LoadMainMenu);
    }

    private void Resume()
    {
        GameManager.Instance.Pause(false);
        UIHelper.ExecuteUIHandlingAction(false);
    }

    private void LoadMainMenu()
    {
        SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU);
    }
}
