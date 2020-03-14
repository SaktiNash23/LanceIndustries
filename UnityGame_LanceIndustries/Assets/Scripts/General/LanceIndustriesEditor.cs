using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LanceIndustriesEditor
{
    public static void CreateAsset<T>(string path) where T : ScriptableObject
    {
        string projectRelativePath = FileUtil.GetProjectRelativePath(path);
        T asset = ScriptableObject.CreateInstance<T>();

        AssetDatabase.CreateAsset(asset, projectRelativePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }

    [MenuItem("LanceIndustries/Map/MapInfo")]
    public static void CreateMapInfo()
    {
        string path = EditorUtility.SaveFilePanel("Create a Map Info.", "Assets/", "New Map Info.asset", "asset");

        if (path == "")
            return;

        CreateAsset<MapInfo>(path);
    }
}
