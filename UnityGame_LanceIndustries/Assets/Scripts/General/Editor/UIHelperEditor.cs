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

        targetScript.helperFunctionType = (UI_HELPER_FUNCTION_TYPES)EditorGUILayout.EnumPopup("Helper Function Type", targetScript.helperFunctionType);

        switch (targetScript.helperFunctionType)
        {
            case UI_HELPER_FUNCTION_TYPES.POP:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cgParent"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("imgCgParentToUnpopWindow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("btnCgParentToUnpopWindow"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("cgPopTarget"));
                targetScript.maxScale = EditorGUILayout.Vector3Field("Max Scale", targetScript.maxScale);
                targetScript.popDuration = EditorGUILayout.FloatField("Pop Duration", targetScript.popDuration);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("callbacksAfterPop"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("callbacksAfterUnPop"));
                serializedObject.ApplyModifiedProperties();
                break;
            case UI_HELPER_FUNCTION_TYPES.MOVE_IN_OUT:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rtTargetToMove"));
                targetScript.moveOffset = EditorGUILayout.Vector3Field("Move Offset", targetScript.moveOffset);
                targetScript.moveDuration = EditorGUILayout.FloatField("Pop Duration", targetScript.moveDuration);
                serializedObject.ApplyModifiedProperties();
                break;
        }  
    }
}
