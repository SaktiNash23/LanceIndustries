using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    private static GameplayUIManager instance;
    public static GameplayUIManager Instance { get => instance; }

    [Header("REFLECTOR")]
    public TextMeshProUGUI txtReflectorStockWhite;
    public TextMeshProUGUI txtReflectorStockRed;
    public TextMeshProUGUI txtReflectorStockBlue;
    public TextMeshProUGUI txtReflectorStockYellow;

    [Header("PAUSE MENU")]
    public Button btnReturnToMainMenu;
    public Button btnNextStage;
    public Button btnStageSelection;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        btnReturnToMainMenu.onClick.AddListener(() => PersistentDataManager.Instance.SetTimeScale(1f));
        btnReturnToMainMenu.onClick.AddListener(() => SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU));
        btnStageSelection.onClick.AddListener(() => PersistentDataManager.Instance.SetTimeScale(1f));
        btnStageSelection.onClick.AddListener(() => SceneLoader.Instance.LoadSceneWithLoadingScreen(SCENE_ENUM.MAIN_MENU));
        if (PersistentDataManager.Instance.GetSelectedMapIndex() < PersistentDataManager.Instance.MapDataHolderNamePairs.Count - 1)
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

    public void RefreshReflectorStockUIs(REFLECTOR_TYPE reflectorType)
    {
        switch (reflectorType)
        {
            case REFLECTOR_TYPE.BASIC:
                txtReflectorStockWhite.text = GameManager.Instance.BasicWhiteReflectorStock.ToString();
                txtReflectorStockRed.text = GameManager.Instance.BasicRedReflectorStock.ToString();
                txtReflectorStockBlue.text = GameManager.Instance.BasicBlueReflectorStock.ToString();
                txtReflectorStockYellow.text = GameManager.Instance.BasicYellowReflectorStock.ToString();
                break;
            case REFLECTOR_TYPE.TRANSLUCENT:
                txtReflectorStockWhite.text = GameManager.Instance.TranslucentWhiteReflectorStock.ToString();
                txtReflectorStockRed.text = GameManager.Instance.TranslucentRedReflectorStock.ToString();
                txtReflectorStockBlue.text = GameManager.Instance.TranslucentBlueReflectorStock.ToString();
                txtReflectorStockYellow.text = GameManager.Instance.TranslucentYellowReflectorStock.ToString();
                break;
            case REFLECTOR_TYPE.DOUBLE_WAY:
                txtReflectorStockWhite.text = GameManager.Instance.DoubleWayWhiteReflectorStock.ToString();
                txtReflectorStockRed.text = GameManager.Instance.DoubleWayRedReflectorStock.ToString();
                txtReflectorStockBlue.text = GameManager.Instance.DoubleWayBlueReflectorStock.ToString();
                txtReflectorStockYellow.text = GameManager.Instance.DoubleWayYellowReflectorStock.ToString();
                break;
            case REFLECTOR_TYPE.THREE_WAY:
                txtReflectorStockWhite.text = GameManager.Instance.ThreeWayWhiteReflectorStock.ToString();
                txtReflectorStockRed.text = GameManager.Instance.ThreeWayRedReflectorStock.ToString();
                txtReflectorStockBlue.text = GameManager.Instance.ThreeWayBlueReflectorStock.ToString();
                txtReflectorStockYellow.text = GameManager.Instance.ThreeWayYellowReflectorStock.ToString();
                break;
        }
    }
}
