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
    }

    public void SnappingOnEndDrag(BaseEventData eventData)
    {
        UIHelper uiHelper = eventData.selectedObject.GetComponentInParent<UIHelper>();

        int maxPageIndex = uiHelper.content.childCount - 1;

        int currentPageIndex = Mathf.RoundToInt(Mathf.Abs(uiHelper.content.anchoredPosition.x / canvasScaler.referenceResolution.x));

#if UNITY_EDITOR
        pointerEndDragPos = ((PointerEventData)eventData).position;
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.GetTouch(touchId).phase == TouchPhase.Ended)
        {
            pointerEndDragPos = Input.GetTouch(touchId).position;
            touchId = -1;
        }
#endif

        if (Mathf.Abs(pointerEndDragPos.x - pointerStartDragPos.x) > requiredXOffset)
        {
            int targetPageIndex = 0;

            bool scrollRight = false;

            // Swiping Left or Right
            if (pointerEndDragPos.x < pointerStartDragPos.x)
            {
                targetPageIndex = Mathf.Clamp(currentPageIndex + 1, 0, maxPageIndex);
                scrollRight = true;
            }
            else
            {
                targetPageIndex = Mathf.Clamp(currentPageIndex - 1, 0, maxPageIndex);
                scrollRight = false;
            }

            if (currentPageIndex != targetPageIndex)
            {
                if(scrollRight)
                {
                    if (uiHelper.onScrollRightStartCallback.GetPersistentEventCount() > 0)
                        uiHelper.onScrollRightStartCallback.Invoke();
                }
                else
                {
                    if (uiHelper.onScrollLeftStartCallback.GetPersistentEventCount() > 0)
                        uiHelper.onScrollLeftStartCallback.Invoke();
                }

                uiHelper.GetComponent<EventTrigger>().enabled = false;
                if(uiHelper.imgBgToUnpopWindow)
                    uiHelper.imgBgToUnpopWindow.raycastTarget = false;
                LeanTween.moveLocalX(uiHelper.content.gameObject, (targetPageIndex * -canvasScaler.referenceResolution.x) - canvasScaler.referenceResolution.x / 2, scrollDuration).setEaseOutCirc().setOnComplete(() =>
                {
                    uiHelper.GetComponent<EventTrigger>().enabled = true;
                    if(uiHelper.imgBgToUnpopWindow)
                        uiHelper.imgBgToUnpopWindow.raycastTarget = true;
                    if(scrollRight)
                    {
                        if (uiHelper.onScrollRightCompleteCallback != null)
                            uiHelper.onScrollRightCompleteCallback.Invoke();
                    }
                    else
                    {
                        if (uiHelper.onScrollLeftCompleteCallback != null)
                            uiHelper.onScrollLeftCompleteCallback.Invoke();
                    }
                });
            }
        }
    }

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
}