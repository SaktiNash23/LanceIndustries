using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;

[CustomEditor(typeof(UIHelper))]
public class UIHelperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIHelper targetScript = (UIHelper)target;

        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(targetScript), typeof(UIHelper), false);
        GUI.enabled = true;

        targetScript.helperFunctionType = (UI_HELPER_FUNCTION_TYPES)EditorGUILayout.EnumPopup("Helper Function Type", targetScript.helperFunctionType);

        switch (targetScript.helperFunctionType)
        {
            case UI_HELPER_FUNCTION_TYPES.NONE:
                EditorGUILayout.LabelField("---SCROLL SNAPPING SETTINGS (OPTIONAL)---", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollRect"));
                EditorGUILayout.LabelField("--OLD SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftCompleteCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightCompleteCallback"));
                EditorGUILayout.LabelField("--NEW SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onBeginDrag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onEndDrag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingBegin"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingEnd"));
                serializedObject.ApplyModifiedProperties();
                break;
            case UI_HELPER_FUNCTION_TYPES.POP:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cgParent"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("imgBgToUnpopWindow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("btnBgToUnpopWindow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cgPopTarget"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("maxAlphaImgToUnpopWindow"));
                targetScript.maxScale = EditorGUILayout.Vector3Field("Max Scale", targetScript.maxScale);
                targetScript.popDuration = EditorGUILayout.FloatField("Pop Duration", targetScript.popDuration);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("useBgToClose"));
                EditorGUILayout.LabelField("---SCROLL SNAPPING SETTINGS (OPTIONAL)---", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollRect"));
                EditorGUILayout.LabelField("--OLD SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftCompleteCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightCompleteCallback"));
                EditorGUILayout.LabelField("--NEW SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onBeginDrag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onEndDrag"));
                EditorGUILayout.LabelField("---POP SETTINGS (OPTIONAL)---", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("callbacksAfterPop"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("callbacksAfterUnPop"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingBegin"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingEnd"));
                serializedObject.ApplyModifiedProperties();
                break;
            case UI_HELPER_FUNCTION_TYPES.MOVE_IN_OUT:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rtTargetToMove"));
                targetScript.moveOffset = EditorGUILayout.Vector3Field("Move Offset", targetScript.moveOffset);
                targetScript.moveDuration = EditorGUILayout.FloatField("Move Duration", targetScript.moveDuration);
                EditorGUILayout.LabelField("---SCROLL SNAPPING SETTINGS (OPTIONAL)---", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollRect"));
                EditorGUILayout.LabelField("--OLD SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightStartCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollLeftCompleteCallback"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onScrollRightCompleteCallback"));
                EditorGUILayout.LabelField("--NEW SCROLL SNAPPING SYSTEM CALLBACKS--", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onBeginDrag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onEndDrag"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingBegin"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("onSnappingEnd"));
                serializedObject.ApplyModifiedProperties();
                break;
        }  
    }
}
