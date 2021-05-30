using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    private static FxManager instance;
    public static FxManager Instance
    {
        get { return instance; }
    }

    [SerializeField] protected List<FxBase> fxList;

    #region MonoBehaviour
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    public T SpawnFx<T>(Vector3 position, Quaternion rotation, Transform parent = null) where T : FxBase
    {
        foreach(var fx in fxList)
        {
            if(fx.GetType() == typeof(T))
                return ObjectPooler.Instance.PopOrCreate(fx, position, rotation, parent) as T;
        }

        return null;
    }
}