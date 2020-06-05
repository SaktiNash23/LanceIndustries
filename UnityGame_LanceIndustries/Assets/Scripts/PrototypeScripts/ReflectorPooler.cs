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
    public void Initialization()
    {
        
        #region Currently used code
        /*
        foreach (ReflectorPool reflectorList in reflectorPoolList)
        {
            Queue<GameObject> reflectorQueue = new Queue<GameObject>();

            switch(reflectorList.reflectorPoolTag)
            {
                #region Basic Reflector Pooling

                case "ReflectorPool_Basic_White":
                case "ReflectorPool_Basic_Red":
                case "ReflectorPool_Basic_Blue":
                case "ReflectorPool_Basic_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockBasic_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);                   
                    break;

                #endregion

                #region Translucent Reflector Pooling

                case "ReflectorPool_Translucent_White":
                case "ReflectorPool_Translucent_Red":
                case "ReflectorPool_Translucent_Blue":
                case "ReflectorPool_Translucent_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockTranslucent_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion

                #region Double Way Reflector Pooling

                case "ReflectorPool_DoubleWay_White":
                case "ReflectorPool_DoubleWay_Red":
                case "ReflectorPool_DoubleWay_Blue":
                case "ReflectorPool_DoubleWay_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockDoubleWay_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion

                #region Split Reflector Pooling

                case "ReflectorPool_Split_White":
                case "ReflectorPool_Split_Red":
                case "ReflectorPool_Split_Blue":
                case "ReflectorPool_Split_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockSplit_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion

                #region Three Way Reflector Pooling

                case "ReflectorPool_ThreeWay_White":
                case "ReflectorPool_ThreeWay_Red":
                case "ReflectorPool_ThreeWay_Blue":
                case "ReflectorPool_ThreeWay_Yellow":

                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockThreeWay_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion
            }
        }
        */
        #endregion

        #region TEST CODE

        foreach (ReflectorPool reflectorList in reflectorPoolList)
        {
            Queue<GameObject> reflectorQueue = new Queue<GameObject>();

            switch (reflectorList.reflectorPoolTag)
            {
                #region Basic Reflector Pooling

                case "ReflectorPool_Basic_White":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockBasicWhite_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Basic_Red":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockBasicRed_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Basic_Blue":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockBasicBlue_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Basic_Yellow":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockBasicYellow_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;


                #endregion

                #region Translucent Reflector Pooling

                case "ReflectorPool_Translucent_White":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockTranslucentWhite_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;
                case "ReflectorPool_Translucent_Red":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockTranslucentRed_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;
                case "ReflectorPool_Translucent_Blue":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockTranslucentBlue_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;
                case "ReflectorPool_Translucent_Yellow":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockTranslucentYellow_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;



                #endregion

                #region Double Way Reflector Pooling

                case "ReflectorPool_DoubleWay_White":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockDoubleWayWhite_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_DoubleWay_Red":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockDoubleWayRed_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_DoubleWay_Blue":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockDoubleWayBlue_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_DoubleWay_Yellow":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockDoubleWayYellow_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion

                #region Split Reflector Pooling

                case "ReflectorPool_Split_White":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockSplitWhite_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Split_Red":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockSplitRed_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Split_Blue":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockSplitBlue_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_Split_Yellow":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockSplitYellow_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                #endregion

                #region Three Way Reflector Pooling

                case "ReflectorPool_ThreeWay_White":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockThreeWayWhite_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_ThreeWay_Red":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockThreeWayRed_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;
                case "ReflectorPool_ThreeWay_Blue":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockThreeWayBlue_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                case "ReflectorPool_ThreeWay_Yellow":
                    for (int i = 0; i < GameManager.gameManagerInstance.ReflectorStockThreeWayYellow_Accessor; ++i)
                    {
                        GameObject reflectorToAddToQueue = Instantiate(reflectorList.reflectorPrefab, Vector2.zero, Quaternion.identity);
                        reflectorToAddToQueue.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
                        reflectorToAddToQueue.SetActive(false);
                        reflectorQueue.Enqueue(reflectorToAddToQueue);
                    }

                    reflectorPoolDictionary.Add(reflectorList.reflectorPoolTag.ToString(), reflectorQueue);
                    break;

                    #endregion
            }
        }

        #endregion
    }
}
