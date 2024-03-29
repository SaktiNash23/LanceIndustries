using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class MainMenuUIManager : MonoBehaviour
{
    [BoxGroup("CANVAS SCALER")] [SerializeField] CanvasScaler canvasScaler;

    [BoxGroup("REFERENCES")] [SerializeField] Button btnPlay;

    [BoxGroup("REFERENCES")] [SerializeField] RectTransform rtContentLevels;
    [BoxGroup("REFERENCES")] [SerializeField] RectTransform rtContentLevelPreview;

    [BoxGroup("REFERENCES")] [SerializeField] UIHelper uiHelperPanelLevelSelection;
    [BoxGroup("REFERENCES")] [SerializeField] UIHelper uiHelperPanelLevelPreview;

    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] Button btnBackToLevelSelection;
    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] Button btnSwitchLevelLeft;
    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] Button btnSwitchLevelRight;
    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] LevelPreviewPage levelPreviewPage0;
    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] LevelPreviewPage levelPreviewPage1;
    [BoxGroup("LEVEL PREVIEW REFERENCES")] [SerializeField] LevelPreviewPage levelPreviewPage2;

    [BoxGroup("PREFABS")] [SerializeField] LevelPage levelPagePrefab;
    [BoxGroup("PREFABS")] [SerializeField] LevelPreviewPage levelPreviewPagePrefab;

    private static MainMenuUIManager _instance;
    public static MainMenuUIManager Instance { get => _instance; }

    public List<MainMenuLevelUI> LevelUIs { get; set; } = new List<MainMenuLevelUI>();

    public bool SwitchingLevelPage { get; set; } = false;

    //------------------------------- MONOBEHAVIOUR FUNCTIONS -------------------------------//

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        uiHelperPanelLevelSelection.callbacksAfterPop.AddListener(() => btnPlay.interactable = true);
        uiHelperPanelLevelSelection.callbacksAfterUnPop.AddListener(() => btnPlay.interactable = true);
        InitializeLevelSelection();
        InitializeLevelPreviewPage();
    }

    private void OnEnable()
    {
        btnBackToLevelSelection.onClick.AddListener(() => ShowLevelPreview(false));

        #region OLD SCROLL SNAPPING SYSTEM

        //btnSwitchLevelLeft.onClick.AddListener(() => UIHelperManager.Instance.ScrollSnapping(uiHelperPanelLevelPreview, false, () => 
        //{ 
        //    btnSwitchLevelLeft.gameObject.SetActive(false);
        //    btnSwitchLevelRight.gameObject.SetActive(false);
        //    btnBackToLevelSelection.gameObject.SetActive(false);
        //    SwitchingLevelPage = true;
        //}, () =>
        //{
        //    btnSwitchLevelLeft.gameObject.SetActive(true);
        //    btnSwitchLevelRight.gameObject.SetActive(true);
        //    btnBackToLevelSelection.gameObject.SetActive(true);
        //    ShowLevelPreview(true, --selectedMapPreviewIndex);
        //    SwitchingLevelPage = false;
        //}));
        //btnSwitchLevelRight.onClick.AddListener(() => UIHelperManager.Instance.ScrollSnapping(uiHelperPanelLevelPreview, true, () =>
        //{
        //    btnSwitchLevelLeft.gameObject.SetActive(false);
        //    btnSwitchLevelRight.gameObject.SetActive(false);
        //    btnBackToLevelSelection.gameObject.SetActive(false);
        //    SwitchingLevelPage = true;
        //}, () =>
        //{
        //    btnSwitchLevelLeft.gameObject.SetActive(true);
        //    btnSwitchLevelRight.gameObject.SetActive(true);
        //    btnBackToLevelSelection.gameObject.SetActive(true);
        //    ShowLevelPreview(true, ++selectedMapPreviewIndex);
        //    SwitchingLevelPage = false;
        //}));

        #endregion

        #region NEW SCROLL SNAPPING SYSTEM
        btnSwitchLevelLeft.onClick.AddListener(() => UIHelperManager.Instance.ScrollSnapping(uiHelperPanelLevelPreview, false));
        btnSwitchLevelRight.onClick.AddListener(() => UIHelperManager.Instance.ScrollSnapping(uiHelperPanelLevelPreview, true));
        #endregion
    }

    private void OnDisable()
    {
        btnBackToLevelSelection.onClick.RemoveAllListeners();
        btnSwitchLevelLeft.onClick.RemoveAllListeners();
        btnSwitchLevelRight.onClick.RemoveAllListeners();
    }

    //------------------------------- LEVEL SELECTION  -------------------------------//

    public void ShowLevelSelection(bool show)
    {
        uiHelperPanelLevelSelection.ExecuteUIHandlingAction(show, () => btnPlay.interactable = false);
    }

    //------------------------------- LEVEL PREVIEW PAGE (NEW SCROLL SNAP SYSTEM) -------------------------------//

    private void InitializeLevelPreviewPage()
    {
        List<MapInfo> allMapInfos = new List<MapInfo>(Resources.LoadAll<MapInfo>("Map Infos"));
        foreach (var mapInfo in allMapInfos)
        {
            LevelPreviewPage levelPreviewPage = Instantiate(levelPreviewPagePrefab, rtContentLevelPreview, false);
            levelPreviewPage.TargetMapInfo = mapInfo;
            levelPreviewPage.PopularizeDisplay();
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(rtContentLevelPreview);
    }

    public void ShowLevelPreview(bool show, int mapIndex = -1)
    {
        uiHelperPanelLevelPreview.gameObject.SetActive(show);

        if (show)
        {
            //selectedMapPreviewIndex = mapIndex;
            //// If the selected level preview is the first one
            //if (mapIndex == 0)
            //{
            //    // Can be simplified, however to make it consistent with the below if statement, write it like this
            //    if (mapCount >= 3)
            //    {
            //        levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
            //        levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex + 1]);
            //        levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex + 2]);
            //    }
            //    else if(mapCount == 2)
            //    {
            //        levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
            //        levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex + 1]);
            //    }
            //    else if(mapCount == 1)
            //    {
            //        levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
            //    }

            //    rtContentLevelPreview.anchoredPosition = new Vector2(0f, 0f);
            //}
            //else if (mapIndex == mapCount - 1)       // If the selected level preview is the last one
            //{
            //    if (mapCount >= 3)
            //    {
            //        levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex]);
            //        levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex - 1]);
            //        levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 2]);
            //        rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x * 2f, 0f);
            //    }
            //    else if(mapCount == 2)
            //    {
            //        levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex]);
            //        levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 1]);
            //        rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
            //    }
            //}
            //else
            //{
            //    levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 1]);
            //    levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex]);
            //    levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex + 1]);

            //    rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
            //}

            float targetScrollRectNormalizedPosX = (float)mapIndex / (uiHelperPanelLevelPreview.content.childCount - 1);

            uiHelperPanelLevelPreview.scrollRect.horizontalNormalizedPosition = targetScrollRectNormalizedPosX;
        }
    }

    //------------------------------- LEVEL PREVIEW PAGE(OLD SCROLL SNAP SYSTEM) -------------------------------//

    private int mapCount;
    private int selectedMapPreviewIndex;
    public void IncrementSelectedMapPreviewIndex() => selectedMapPreviewIndex++;
    public void DecrementSelectedMapPreviewIndex() => selectedMapPreviewIndex--;

    private void InitializeLevelSelection()
    {
        Queue<MapInfo> allMapInfos = new Queue<MapInfo>(Resources.LoadAll<MapInfo>("Map Infos"));
        int pageCount = Mathf.CeilToInt((float)allMapInfos.Count / 15f);
        mapCount = allMapInfos.Count;
        rtContentLevels.sizeDelta = new Vector2(canvasScaler.referenceResolution.x * pageCount, canvasScaler.referenceResolution.y);

        for (int i = 0; i < pageCount; i++)
        {
            List<MapInfo> mapInfos = new List<MapInfo>();
            for (int j = 0; j < 15; j++)
            {
                mapInfos.Add(allMapInfos.Dequeue());
                if (allMapInfos.Count <= 0)
                    break;
            }

            LevelPage levelPage = Instantiate(levelPagePrefab, rtContentLevels, false);
            levelPage.PopularizeDisplay(mapInfos);
        }

        LevelPage.ResetCurrentPopularizedMapIndex();

        if (mapCount == 2)
        {
            Destroy(levelPreviewPage2.gameObject);
        }
        else if (mapCount == 1)
        {
            Destroy(levelPreviewPage1.gameObject);
            Destroy(levelPreviewPage2.gameObject);
        }
        else if (mapCount <= 0)
        {
            Destroy(levelPreviewPage0);
            Destroy(levelPreviewPage1);
            Destroy(levelPreviewPage2);
        }
    }

    //public void ShowLevelPreview(bool show, int mapIndex = -1)
    //{
    //    uiHelperPanelLevelPreview.gameObject.SetActive(show);

    //    if (show)
    //    {
    //        selectedMapPreviewIndex = mapIndex;
    //        // If the selected level preview is the first one
    //        if (mapIndex == 0)
    //        {
    //            // Can be simplified, however to make it consistent with the below if statement, write it like this
    //            if (mapCount >= 3)
    //            {
    //                levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
    //                levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex + 1]);
    //                levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex + 2]);
    //            }
    //            else if (mapCount == 2)
    //            {
    //                levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
    //                levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex + 1]);
    //            }
    //            else if (mapCount == 1)
    //            {
    //                levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex]);
    //            }

    //            rtContentLevelPreview.anchoredPosition = new Vector2(0f, 0f);
    //        }
    //        else if (mapIndex == mapCount - 1)       // If the selected level preview is the last one
    //        {
    //            if (mapCount >= 3)
    //            {
    //                levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex]);
    //                levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex - 1]);
    //                levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 2]);
    //                rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x * 2f, 0f);
    //            }
    //            else if (mapCount == 2)
    //            {
    //                levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex]);
    //                levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 1]);
    //                rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
    //            }
    //        }
    //        else
    //        {
    //            levelPreviewPage0.PopularizeDisplay(LevelUIs[mapIndex - 1]);
    //            levelPreviewPage1.PopularizeDisplay(LevelUIs[mapIndex]);
    //            levelPreviewPage2.PopularizeDisplay(LevelUIs[mapIndex + 1]);

    //            rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
    //        }
    //    }
    //}

    public void RefreshLevelPreview()
    {
        if (selectedMapPreviewIndex == 0)
        {
            // Can be simplified, however to make it consistent with the below if statement, write it like this
            if (mapCount >= 3)
            {
                levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
                levelPreviewPage1.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex + 1]);
                levelPreviewPage2.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex + 2]);
            }
            else if (mapCount == 2)
            {
                levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
                levelPreviewPage1.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex + 1]);
            }
            else if (mapCount == 1)
            {
                levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
            }

            rtContentLevelPreview.anchoredPosition = new Vector2(0f, 0f);
        }
        else if (selectedMapPreviewIndex == mapCount - 1)       // If the selected level preview is the last one
        {
            if (mapCount >= 3)
            {
                levelPreviewPage2.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
                levelPreviewPage1.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex - 1]);
                levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex - 2]);
                rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x * 2f, 0f);
            }
            else if (mapCount == 2)
            {
                levelPreviewPage1.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
                levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex - 1]);
                rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
            }
        }
        else
        {
            levelPreviewPage0.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex - 1]);
            levelPreviewPage1.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex]);
            levelPreviewPage2.PopularizeDisplay(LevelUIs[selectedMapPreviewIndex + 1]);

            rtContentLevelPreview.anchoredPosition = new Vector2(-canvasScaler.referenceResolution.x, 0f);
        }
    }

    //------------------------------- GENERAL -------------------------------//

    public void SetSwitchingLevelPage(bool switching) => SwitchingLevelPage = switching;
}
