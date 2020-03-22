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
    POP = 1
}

[ExecuteInEditMode]
public class UIHelper : MonoBehaviour
{
    [OnValueChanged("OnValueUiHelperFunctionTypeChangeCallback")]
    [SerializeField] UI_HELPER_FUNCTION_TYPES helperFunctionType;

    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] CanvasGroup cgParent;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] Image imgCgParentToUnpopWindow;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] Button btnCgParentToUnpopWindow;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] CanvasGroup cgPopTarget;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] Vector3 maxScale;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] float popDuration;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] UnityEvent callbacksAfterPop = null;
    [ShowIf("isPop")] [BoxGroup("POP SETTINGS")] [SerializeField] UnityEvent callbacksAfterUnPop = null;

    private UnityAction<bool> btnCallbackAction = null;

    private bool isPop;

    //----------------------------- MONOBEHAVIOUR FUNCTIONS -----------------------------//

    private void Start()
    {
        if (helperFunctionType == UI_HELPER_FUNCTION_TYPES.POP)
        {
            isPop = true;
            btnCallbackAction += Pop;
        }
    }

    //----------------------------- CALLBACK HANDLING FUNCTIONS -----------------------------//

    public void ExecuteUIHandlingAction(bool targetBool)
    {
        btnCallbackAction.Invoke(targetBool);
        if (targetBool && callbacksAfterPop != null)
            callbacksAfterPop.Invoke();
        else if (callbacksAfterUnPop != null)
            callbacksAfterUnPop.Invoke();
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

    private void WindowScaleUpdate(Vector2 scale)
    {
        cgPopTarget.transform.localScale = scale;
    }

    private void ImgParentBgAlphaUpdate(float value)
    {
        imgCgParentToUnpopWindow.color = new Color(0f, 0f, 0f, value);
    }

    //----------------------------- INSPECTOR HANDLING FUNCTIONS -----------------------------//

    private void OnValueUiHelperFunctionTypeChangeCallback(UI_HELPER_FUNCTION_TYPES oldValue, UI_HELPER_FUNCTION_TYPES newValue)
    {
        if (newValue == UI_HELPER_FUNCTION_TYPES.NONE)
        {
            isPop = false;
        }
        if (newValue == UI_HELPER_FUNCTION_TYPES.POP)
        {
            isPop = true;
        }
    }
}
