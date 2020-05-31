using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NaughtyAttributes;

public enum UI_HELPER_FUNCTION_TYPES
{
    NONE = 0,
    POP = 1,
    MOVE_IN_OUT = 2
}

[ExecuteInEditMode]
public class UIHelper : MonoBehaviour
{
    [InfoBox("For POP, ExecuteUIHandlingAction with bool param should be called. For MOVE_IN_OUT, ExecuteUIHandlingAction without param should be called.")]
    public UI_HELPER_FUNCTION_TYPES helperFunctionType;

    [BoxGroup("POP SETTINGS")] public CanvasGroup cgParent;
    [BoxGroup("POP SETTINGS")] public Image imgCgParentToUnpopWindow;
    [BoxGroup("POP SETTINGS")] public Button btnCgParentToUnpopWindow;
    [BoxGroup("POP SETTINGS")] public CanvasGroup cgPopTarget;
    [BoxGroup("POP SETTINGS")] public Vector3 maxScale;
    [BoxGroup("POP SETTINGS")] public float popDuration;
    [BoxGroup("POP SETTINGS")] public UnityEvent callbacksAfterPop = null;
    [BoxGroup("POP SETTINGS")] public UnityEvent callbacksAfterUnPop = null;

    [BoxGroup("MOVE SETTINGS")] public RectTransform rtTargetToMove;
    [BoxGroup("MOVE SETTINGS")] public Vector3 moveOffset;
    [BoxGroup("MOVE SETTINGS")] public float moveDuration;

    private UnityAction<bool> btnCallbackActionBool = null;
    private UnityAction btnCallbackActionNoParam = null;

    public bool InTransition { get; set; } = false;
    
    private bool moved;
    private Vector3 originPos;

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
        if (helperFunctionType == UI_HELPER_FUNCTION_TYPES.POP)
        {
            btnCallbackActionBool.Invoke(targetBool);
            if (targetBool && callbacksAfterPop != null)
                callbacksAfterPop.Invoke();
            else if (callbacksAfterUnPop != null)
                callbacksAfterUnPop.Invoke();
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
            imgCgParentToUnpopWindow.raycastTarget = true;
            Vector3 initialScale = new Vector3(0f, 0f, 0f);
            LeanTween.value(this.gameObject, initialScale, maxScale, popDuration).setEaseOutBack().setOnUpdate((Vector3 targetScale) => WindowScaleUpdate(targetScale)).setOnComplete(() =>
            {
                cgPopTarget.interactable = true;
                cgPopTarget.blocksRaycasts = true;
                btnCgParentToUnpopWindow.onClick.AddListener(() => ExecuteUIHandlingAction(false));
            });
            LeanTween.value(0f, 0.5f, popDuration).setOnUpdate(ImgParentBgAlphaUpdate);
        }
        else
        {
            Vector3 initialScale = cgPopTarget.transform.localScale;
            cgPopTarget.interactable = false;
            cgPopTarget.blocksRaycasts = false;
            LeanTween.value(this.gameObject, initialScale, new Vector3(0f, 0f, 0f), popDuration).setEaseOutCubic().setOnUpdate((Vector3 targetScale) => WindowScaleUpdate(targetScale));
            btnCgParentToUnpopWindow.onClick.RemoveAllListeners();
            LeanTween.value(0.5f, 0.0f, popDuration).setOnUpdate(ImgParentBgAlphaUpdate).setOnComplete(() =>
            {
                imgCgParentToUnpopWindow.raycastTarget = false;
            });
        }
    }

    private void WindowScaleUpdate(Vector3 scale)
    {
        cgPopTarget.transform.localScale = scale;
    }

    private void ImgParentBgAlphaUpdate(float value)
    {
        imgCgParentToUnpopWindow.color = new Color(0f, 0f, 0f, value);
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
