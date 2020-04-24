using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ReflectorPooler : MonoBehaviour
{
    //Class to define a reflector pool (Each class definition of the pool must include a tag & prefab (reflector prefab)
    //Dictionary to store the different reflector pools 

    [System.Serializable]//To ensure the ReflectorPool class is visible in the Inspector
    public class ReflectorPool
    {
        public string reflectorPoolTag; //The tag that will be used as the Key to access this specific pool in the reflectorPoolDictionary
        public GameObject reflectorPrefab; //The reflector prefab that we want to add to the pool
    }

    //string is the tag we provide (from reflectorPoolTag). Based on tag provided, the associated Queue is outputted
    public Dictionary<string, Queue<GameObject>> reflectorPoolDictionary = new Dictionary<string, Queue<GameObject>>();

    //This List's purpose is to allow the designer to add different ReflectorPool classes in the editor and specify their tags, prefabs
    //Then, each reflectorPool is used to create Queue of reflectors and each of those created queues are added to the reflectorPoolDictionary (refer to Start method for more info)

    [InfoBox("List of Reflector Pools", EInfoBoxType.Normal)]
    [ReorderableList]
    public List<ReflectorPool> reflectorPoolList = new List<ReflectorPool>();

    public static ReflectorPooler instance_reflectorPooler;

    private void Awake()
    {
        instance_reflectorPooler = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (ReflectorPool reflectorList in reflectorPoolList)
        {
            Queue<GameObject> reflectorQueue = new Queue<GameObject>();

            switch(reflectorList.reflectorPoolTag)
            {
                case "ReflectorPool_Basic_White":
                case "ReflectorPool_Basic_Red":
                case "ReflectorPool_Basic_Blue":
                case "ReflectorPool_Basic_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStock_Basic; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }


                    if (reflectorList.reflectorPoolTag == "ReflectorPool_Basic_White")
                    {
                        reflectorPoolDictionary.Add("ReflectorPool_Basic_White", reflectorQueue);
                    }
                    if (reflectorList.reflectorPoolTag == "ReflectorPool_Basic_Red")
                    {
                        reflectorPoolDictionary.Add("ReflectorPool_Basic_Red", reflectorQueue);
                    }
                    if (reflectorList.reflectorPoolTag == "ReflectorPool_Basic_Blue")
                    {
                        reflectorPoolDictionary.Add("ReflectorPool_Basic_Blue", reflectorQueue);
                    }
                    if(reflectorList.reflectorPoolTag == "ReflectorPool_Basic_Yellow")
                    {
                        reflectorPoolDictionary.Add("ReflectorPool_Basic_Yellow", reflectorQueue);
                    }

                    break;

            }
        }
    }
}
