using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    private static GameplayUIManager instance;
    public static GameplayUIManager Instance { get => instance; }

    [Header("References")]
    public ReflectorColorPanel reflectorColorPanel;
    public GameClearPanel gameClearPanel;
    public PausePanel pausePanel;

    [Header("Texts")]
    public TextMeshProUGUI txtReflectorStockWhite;
    public TextMeshProUGUI txtReflectorStockRed;
    public TextMeshProUGUI txtReflectorStockBlue;
    public TextMeshProUGUI txtReflectorStockYellow;

    [Header("Buttons")]
    public Button btnPause;

    private void Awake()
    {
        if (instance != null && instance != this)
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
        btnPause.onClick.AddListener(ShowPausePanel);
    }

    private void OnDisable()
    {
        btnPause.onClick.RemoveListener(ShowPausePanel);
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

    public void ShowGameClearPanel()
    {
        gameClearPanel.UIHelper.ExecuteUIHandlingAction(true, gameClearPanel.RefreshButtonInteractable);
    }

    public void ShowPausePanel()
    {
        GameManager.Instance.Pause(true);
        pausePanel.UIHelper.ExecuteUIHandlingAction(true);
    }
}
