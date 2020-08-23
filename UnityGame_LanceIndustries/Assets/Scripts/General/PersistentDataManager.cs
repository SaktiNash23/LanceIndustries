using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PersistentDataManager : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;

    private static PersistentDataManager _instance;
    public static PersistentDataManager Instance { get => _instance; }

    public List<MapDataHolderNamePair> MapDataHolderNamePairs = new List<MapDataHolderNamePair>();
    public MapDataHolderNamePair SelectedMapDataHolderNamePair { get; set; }

    public int selectedMapIndex = -1;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        PopulateMapDataHolderNamePairs();
    }

    public void PopulateMapDataHolderNamePairs()
    {
        List<MapInfo> mapInfos = new List<MapInfo>(Resources.LoadAll<MapInfo>("Map Infos"));
        foreach(var mapInfo in mapInfos)
        {
            MapDataHolderNamePairs.Add(new MapDataHolderNamePair() { mapName = mapInfo.mapName, mapDataHolder = JsonUtility.FromJson<MapDataHolder>(mapInfo.mapData.text) });
        }
    }

    public MapDataHolder GetMapDataHolder(string mapName)
    {
        foreach(var mapDataHolderNamePair in MapDataHolderNamePairs)
        {
            if (mapDataHolderNamePair.mapName == mapName)
                return mapDataHolderNamePair.mapDataHolder;
        }

        return null;
    }

    public MapDataHolderNamePair GetMapDataHolderNamePair(string mapName)
    {
        foreach(var mapDataHolderNamePair in MapDataHolderNamePairs)
        {
            if (mapDataHolderNamePair.mapName == mapName)
                return mapDataHolderNamePair;
        }

        return null;
    }

    public MapDataHolderNamePair GetMapDataHolderNamePair(int index)
    {
        if (index < MapDataHolderNamePairs.Count)
        {
            return MapDataHolderNamePairs[index];
        }
        else
        {
            return null;
        }
    }

    public void UpdateSelectedMapIndex()
    {
        selectedMapIndex = MapDataHolderNamePairs.IndexOf(SelectedMapDataHolderNamePair);
    }

    public int GetSelectedMapIndex() => selectedMapIndex;

    public void SetTimeScale(float value) => Time.timeScale = value;
}

[System.Serializable]
public class MapDataHolderNamePair
{
    public string mapName;
    public MapDataHolder mapDataHolder;
}

