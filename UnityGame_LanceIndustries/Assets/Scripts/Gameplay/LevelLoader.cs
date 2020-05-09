using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelLoader : MonoBehaviour
{
    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;
    
    [BoxGroup("DEBUGGING SETTINGS")] [SerializeField] bool debug;

    [BoxGroup("LEVEL SETTINGS")][SerializeField] TextAsset levelToLoad;

    private static LevelLoader _instance;
    public static LevelLoader Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            if(dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}
