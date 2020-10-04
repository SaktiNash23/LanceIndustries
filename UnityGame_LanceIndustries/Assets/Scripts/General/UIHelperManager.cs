using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Events;

public class UIHelperManager : MonoBehaviour
{
    [BoxGroup("CANVAS SCALER")] public CanvasScaler canvasScaler;

    [BoxGroup("PAGE SCROLLING SETTINGS")] public float requiredXOffset;
    [BoxGroup("PAGE SCROLLING SETTINGS")] public float scrollDuration = 0.25f;

    [BoxGroup("DEBUGGING")] [ReadOnly] public Vector2 pointerStartDragPos;
    [BoxGroup("DEBUGGING")] [ReadOnly] public Vector2 pointerEndDragPos;

    [BoxGroup("DEBUGGING")] [ReadOnly] public int touchId = -1;

    private static UIHelperManager _instance;
    public static UIHelperManager Instance { get => _instance; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;
    }

    public void SnappingOnBeginDrag(BaseEventData eventData)
    {
#if UNITY_EDITOR
        pointerStartDragPos = ((PointerEventData)eventData).position;
#elif UNITY_IOS || UNITY_ANDROID
        if (touchId == -1)
        {
            pointerStartDragPos = Input.touches[0].position;
            touchId = Input.touches[0].fingerId;
        }
#endif

        UIHelper uiHelper = eventData.selectedObject.GetComponentInParent<UIHelper>();

        uiHelper.onBeginDrag?.Invoke();
    }

    public void SnappingOnEndDrag(BaseEventData eventData)
    {
#if UNITY_EDITOR
        pointerEndDragPos = ((PointerEventData)eventData).position;
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.GetTouch(touchId).phase == TouchPhase.Ended)
        {
            pointerEndDragPos = Input.GetTouch(touchId).position;
            touchId = -1;
        }
#endif

        UIHelper uiHelper = eventData.selectedObject.GetComponentInParent<UIHelper>();

        #region OLD SCROLL SNAP SYSTEM
        //int maxPageIndex = uiHelper.content.childCount - 1;

        //int currentPageIndex = Mathf.RoundToInt(Mathf.Abs(uiHelper.content.anchoredPosition.x / canvasScaler.referenceResolution.x));


        //if (Mathf.Abs(pointerEndDragPos.x - pointerStartDragPos.x) > requiredXOffset)
        //{
        //    int targetPageIndex = 0;

        //    bool scrollRight = false;

        //    // Swiping Left or Right
        //    if (pointerEndDragPos.x < pointerStartDragPos.x)
        //    {
        //        targetPageIndex = Mathf.Clamp(currentPageIndex + 1, 0, maxPageIndex);
        //        scrollRight = true;
        //    }
        //    else
        //    {
        //        targetPageIndex = Mathf.Clamp(currentPageIndex - 1, 0, maxPageIndex);
        //        scrollRight = false;
        //    }

        //    if (currentPageIndex != targetPageIndex)
        //    {
        //        if(scrollRight)
        //        {
        //            if (uiHelper.onScrollRightStartCallback.GetPersistentEventCount() > 0)
        //                uiHelper.onScrollRightStartCallback.Invoke();
        //        }
        //        else
        //        {
        //            if (uiHelper.onScrollLeftStartCallback.GetPersistentEventCount() > 0)
        //                uiHelper.onScrollLeftStartCallback.Invoke();
        //        }

        //        uiHelper.GetComponent<EventTrigger>().enabled = false;
        //        if(uiHelper.imgBgToUnpopWindow)
        //            uiHelper.imgBgToUnpopWindow.raycastTarget = false;
        //        LeanTween.moveLocalX(uiHelper.content.gameObject, (targetPageIndex * -canvasScaler.referenceResolution.x) - canvasScaler.referenceResolution.x / 2, scrollDuration).setEaseOutCirc().setOnComplete(() =>
        //        {
        //            uiHelper.GetComponent<EventTrigger>().enabled = true;
        //            if(uiHelper.imgBgToUnpopWindow)
        //                uiHelper.imgBgToUnpopWindow.raycastTarget = true;
        //            if(scrollRight)
        //            {
        //                if (uiHelper.onScrollRightCompleteCallback != null)
        //                    uiHelper.onScrollRightCompleteCallback.Invoke();
        //            }
        //            else
        //            {
        //                if (uiHelper.onScrollLeftCompleteCallback != null)
        //                    uiHelper.onScrollLeftCompleteCallback.Invoke();
        //            }
        //        });
        //    }
        //}
        #endregion

        #region NEW SCROLL SNAP SYSTEM

        int maxPageIndex = uiHelper.content.childCount - 1;

        float scrollRectXValue = uiHelper.scrollRect.horizontalNormalizedPosition;

        int targetPageIndex = Mathf.RoundToInt(scrollRectXValue * maxPageIndex);
        targetPageIndex = Mathf.Clamp(targetPageIndex, 0, maxPageIndex);

        float targetPageHorizontalNormalizedPosition = (float)targetPageIndex / maxPageIndex;

        uiHelper.scrollRect.velocity = Vector2.zero;
        LeanTween.cancel(uiHelper.scrollRect.gameObject);
        LeanTween.value(uiHelper.scrollRect.gameObject, uiHelper.scrollRect.horizontalNormalizedPosition, targetPageHorizontalNormalizedPosition, 0.15f).setOnUpdate((value) => uiHelper.scrollRect.horizontalNormalizedPosition = value).setOnComplete(() => uiHelper.onEndDrag?.Invoke());
        
        #endregion
    }

    //------------------------------ OLD SCROLL SNAP SYSTEM ------------------------------//

    public void ScrollSnapping(UIHelper targetUI, bool scrollRight, UnityAction callbackOnStart = null, UnityAction callbackOnCompleted = null)
    {
        int maxPageIndex = targetUI.content.childCount - 1;

        int currentPageIndex = Mathf.RoundToInt(Mathf.Abs(targetUI.content.anchoredPosition.x / canvasScaler.referenceResolution.x));

        int targetPageIndex = Mathf.Clamp(currentPageIndex + (scrollRight ? 1 : -1), 0, maxPageIndex);

        if (currentPageIndex != targetPageIndex)
        {
            if (callbackOnStart != null)
                callbackOnStart.Invoke();

            targetUI.GetComponent<EventTrigger>().enabled = false;
            if (targetUI.imgBgToUnpopWindow)
                targetUI.imgBgToUnpopWindow.raycastTarget = false;
            LeanTween.moveLocalX(targetUI.content.gameObject, (targetPageIndex * -canvasScaler.referenceResolution.x) - canvasScaler.referenceResolution.x / 2, scrollDuration).setEaseOutCirc().setOnComplete(() =>
            {
                targetUI.GetComponent<EventTrigger>().enabled = true;
                if (targetUI.imgBgToUnpopWindow)
                    targetUI.imgBgToUnpopWindow.raycastTarget = true;
                if (callbackOnCompleted != null)
                {
                    callbackOnCompleted.Invoke();
                }
            });
        }
    }

    //------------------------------ NEW SCROLL SNAP SYSTEM ------------------------------//

    public void ScrollSnapping(UIHelper targetUI, bool scrollRight)
    {
        int maxPageIndex = targetUI.content.childCount - 1;

        int currentPageIndex = Mathf.RoundToInt(targetUI.scrollRect.horizontalNormalizedPosition * maxPageIndex);

        int targetPageIndex = Mathf.Clamp(currentPageIndex + (scrollRight ? 1 : -1), 0, maxPageIndex);

        float targetNormalizedPosX = (float)targetPageIndex / maxPageIndex;

        targetUI.onBeginDrag?.Invoke();

        if(currentPageIndex != targetPageIndex)
        {
            targetUI.onBeginDrag?.Invoke();
            if (targetUI.imgBgToUnpopWindow)
                targetUI.imgBgToUnpopWindow.raycastTarget = false;
            LeanTween.cancel(targetUI.scrollRect.gameObject);
            LeanTween.value(targetUI.scrollRect.gameObject, targetUI.scrollRect.horizontalNormalizedPosition, targetNormalizedPosX, 0.25f).setOnUpdate((value) => targetUI.scrollRect.horizontalNormalizedPosition = value).setOnComplete(() => targetUI.onEndDrag?.Invoke());
            targetUI.scrollRect.horizontalNormalizedPosition = 0.5f;
        }
        else
        {
            targetUI.onEndDrag?.Invoke();
        }
    }
}