using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PersistentDataManager : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;

    private static PersistentDataManager _instance;
    public static PersistentDataManager Instance { get => _instance; }

    public List<MapDataHolderNamePair> MapDataHolderNamePairs { get; set; } = new List<MapDataHolderNamePair>();

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
}

public class MapDataHolderNamePair
{
    public string mapName;
    public MapDataHolder mapDataHolder;
}

