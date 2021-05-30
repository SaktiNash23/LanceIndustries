using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class ObjectPoolPreset
    {
        public PoolObject poolObject;
        public int startingAmount;
    }

    [SerializeField] protected List<ObjectPoolPreset> objectPoolPresets;

    [Header("SETTINGS")]
    [SerializeField] protected Transform inactivePoolObjectsParent;

    private Dictionary<string, Stack<PoolObject>> objectPoolDict = new Dictionary<string, Stack<PoolObject>>();

    private static ObjectPooler instance;
    public static ObjectPooler Instance { get { return instance; } }

    #region MonoBehaviour
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        foreach (var objectPoolPreset in objectPoolPresets)
        {
            for (int i = 0; i < objectPoolPreset.startingAmount; i++)
            {
                Create(objectPoolPreset.poolObject);
            }
        }
    }
    #endregion

    private void Create(PoolObject prefab, Transform parent = null)
    {
        PoolObject poolObject = Instantiate(prefab, parent);
        if (!objectPoolDict.TryGetValue(prefab.PoolName, out Stack<PoolObject> pool))
            objectPoolDict.Add(prefab.PoolName, new Stack<PoolObject>());

        poolObject.Push();
    }

    public T PopOrCreate<T>(T prefab, Transform parent = null) where T : PoolObject
    {
        return PopOrCreate(prefab, Vector3.zero, Quaternion.identity, parent);
    }

    public T PopOrCreate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : PoolObject
    {
        if (objectPoolDict.TryGetValue(prefab.PoolName, out Stack<PoolObject> targetObjectPool))
        {
            if (targetObjectPool.Count > 0)
            {
                T poolObject = targetObjectPool.Pop() as T;
                poolObject.transform.position = position;
                poolObject.transform.rotation = rotation;
                poolObject.transform.SetParent(parent);
                poolObject.gameObject.SetActive(true);
                return poolObject;
            }
            else
            {
                Create(prefab);
                T poolObject = targetObjectPool.Pop() as T;
                poolObject.transform.position = position;
                poolObject.transform.rotation = rotation;
                poolObject.transform.SetParent(parent);
                poolObject.gameObject.SetActive(true);
                return poolObject;
            }
        }
        else
        {
            objectPoolDict.Add(prefab.PoolName, new Stack<PoolObject>());
            Create(prefab);
            T poolObject = objectPoolDict[prefab.PoolName].Pop() as T;
            poolObject.transform.position = position;
            poolObject.transform.rotation = rotation;
            poolObject.transform.SetParent(parent);
            poolObject.gameObject.SetActive(true);
            return poolObject;
        }
    }

    public void Push(PoolObject poolObject)
    {
        poolObject.gameObject.SetActive(false);
        poolObject.transform.SetParent(inactivePoolObjectsParent);
        objectPoolDict[poolObject.PoolName].Push(poolObject);
    }
}
