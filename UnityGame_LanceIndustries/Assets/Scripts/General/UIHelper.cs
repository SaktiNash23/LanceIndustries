using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum UI_HELPER_FUNCTION_TYPES
{
    NONE = 0,
    POP = 1,
    MOVE_IN_OUT = 2
}

[ExecuteInEditMode]
public class UIHelper : MonoBehaviour
{
    public UI_HELPER_FUNCTION_TYPES helperFunctionType;

    public CanvasGroup cgParent;
    public Image imgBgToUnpopWindow;
    public Button btnBgToUnpopWindow;
    public CanvasGroup cgPopTarget;
    public float maxAlphaImgToUnpopWindow = 0.5f;
    public Vector3 maxScale;
    public float popDuration;
    public UnityEvent callbacksAfterPop = null;
    public UnityEvent callbacksAfterUnPop = null;
    public UnityEvent onScrollLeftStartCallback = null;
    public UnityEvent onScrollRightStartCallback = null;
    public UnityEvent onScrollLeftCompleteCallback = null;
    public UnityEvent onScrollRightCompleteCallback = null;
    public ScrollRect scrollRect;
    public RectTransform content;

    public RectTransform rtTargetToMove;
    public Vector3 moveOffset;
    public float moveDuration;
    public bool useBgToClose = true;

    private UnityAction<bool> btnCallbackActionBool = null;
    private UnityAction btnCallbackActionNoParam = null;
   
    public bool InTransition { get; set; } = false;
    
    private bool moved;
    private Vector3 originPos;

    public int ScrollContentCurrentPageIndex { get; set; }

    //----------------------------- MONOBEHAVIOUR FUNCTIONS -----------------------------//

    private void Start()
    {
        if (helperFunctionType == UI_HELPER_FUNCTION_TYPES.POP)
        {
            btnCallbackActionBool += Pop;
        }
        else if(helperFunctionType == UI_HELPER_FUNCTION_TYPES.MOVE_IN_OUT)
        {
            originPos = rtTargetToMove.anchoredPosition;
            btnCallbackActionNoParam += Move;
        }
    }

    //----------------------------- CALLBACK HANDLING FUNCTIONS -----------------------------//

    public void ExecuteUIHandlingAction(bool targetBool)
    {
        btnCallbackActionBool.Invoke(targetBool);
    }

    public void ExecuteUIHandlingAction(bool targetBool, UnityAction callback = null)
    {
        if (helperFunctionType == UI_HELPER_FUNCTION_TYPES.POP)
        {
            btnCallbackActionBool.Invoke(targetBool);
            if (callback != null)
                callback.Invoke();
            //if (targetBool && callbacksAfterPop != null)
            //    callbacksAfterPop.Invoke();
            //else if (callbacksAfterUnPop != null)
            //    callbacksAfterUnPop.Invoke();
        }
    }

    public void ExecuteUIHandlingAction()
    {
        if (helperFunctionType == UI_HELPER_FUNCTION_TYPES.MOVE_IN_OUT)
        {
            btnCallbackActionNoParam.Invoke();
        }
    }

    //----------------------------- POP HANDLING FUNCTIONS -----------------------------//

    private void Pop(bool pop)
    {
        if (pop)
        {
            Vector3 initialScale = new Vector3(0f, 0f, 0f);

            if (imgBgToUnpopWindow)
            {
                imgBgToUnpopWindow.raycastTarget = true;
                btnBgToUnpopWindow.onClick.RemoveAllListeners();
            }

            LeanTween.value(this.gameObject, initialScale, maxScale, popDuration).setEaseOutBack().setOnUpdate((Vector3 targetScale) => WindowScaleUpdate(targetScale)).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                cgPopTarget.interactable = true;
                cgPopTarget.blocksRaycasts = true;

                if(useBgToClose)
                    btnBgToUnpopWindow.onClick.AddListener(() => ExecuteUIHandlingAction(false));

                if (callbacksAfterPop != null)
                    callbacksAfterPop.Invoke();

                if (scrollRect)
                    scrollRect.enabled = true;

            });

            if(imgBgToUnpopWindow)
                LeanTween.value(0f, maxAlphaImgToUnpopWindow, popDuration).setOnUpdate(ImgParentBgAlphaUpdate).setIgnoreTimeScale(true);

            if (scrollRect)
                scrollRect.content.anchoredPosition = Vector2.zero;
        }
        else
        {
            Vector3 initialScale = cgPopTarget.transform.localScale;

            if (imgBgToUnpopWindow)
            {
                btnBgToUnpopWindow.onClick.RemoveAllListeners();
            }

            cgPopTarget.interactable = false;
            cgPopTarget.blocksRaycasts = false;
            LeanTween.value(this.gameObject, initialScale, new Vector3(0f, 0f, 0f), popDuration).setEaseOutCubic().setOnUpdate((Vector3 targetScale) => WindowScaleUpdate(targetScale)).setIgnoreTimeScale(true).setOnComplete(() => imgBgToUnpopWindow.raycastTarget = false);

            if (imgBgToUnpopWindow)
            {
                LeanTween.value(maxAlphaImgToUnpopWindow, 0.0f, popDuration).setOnUpdate(ImgParentBgAlphaUpdate).setIgnoreTimeScale(true).setOnComplete(() =>
                {
                    if (callbacksAfterUnPop != null)
                        callbacksAfterUnPop.Invoke();
                });
            }

            if (scrollRect)
                scrollRect.enabled = false;
        }
    }

    private void WindowScaleUpdate(Vector3 scale)
    {
        cgPopTarget.transform.localScale = scale;
    }

    private void ImgParentBgAlphaUpdate(float value)
    {
        imgBgToUnpopWindow.color = new Color(imgBgToUnpopWindow.color.r, imgBgToUnpopWindow.color.g, imgBgToUnpopWindow.color.b, value);
    }

    //----------------------------- MOVE HANDLING FUNCTIONS -----------------------------//

    private void Move()
    {
        if(!moved)
        {
            if (!InTransition)
            {
                moved = !moved;
                Vector3 targetPos = originPos + moveOffset;
                InTransition = true;
                LeanTween.value(this.gameObject, originPos, targetPos, moveDuration).setOnUpdate((Vector3 pos) => WindowMoveUpdate(pos)).setOnComplete(() => InTransition = false);
            }
        }
        else
        {
            if (!InTransition)
            {
                moved = !moved;
                Vector3 targetPos = originPos;
                InTransition = true;
                LeanTween.value(this.gameObject, originPos + moveOffset, targetPos, moveDuration).setOnUpdate((Vector3 pos) => WindowMoveUpdate(pos)).setOnComplete(() => InTransition = false);
            }
        }
    }

    private void WindowMoveUpdate(Vector3 pos)
    {
        rtTargetToMove.anchoredPosition = pos;
    }
}
