using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LaserPooler : MonoBehaviour
{
    [System.Serializable]
    public class LaserPool
    {
        public string laserColor;
        public int laserStock;
        public GameObject basicLaserGO;
    }

    [InfoBox("List of Laser Pools", EInfoBoxType.Normal)]
    [ReorderableList]
    public List<LaserPool> laserPoolList = new List<LaserPool>();

    public Dictionary<string, Queue<GameObject>> laserPoolDictionary = new Dictionary<string, Queue<GameObject>>();

    public static LaserPooler instance_LaserPoolList;

    private void Awake()
    {
        instance_LaserPoolList = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        addLasersToPool();
    }

    public void addLasersToPool()
    {
        foreach (LaserPool laserPool in laserPoolList)
        {
            Queue<GameObject> laserGO_Queue = new Queue<GameObject>();

            for (int i = 0; i < laserPool.laserStock; ++i)
            {
                GameObject instantiatedLaser = Instantiate(laserPool.basicLaserGO, Vector2.zero, Quaternion.identity);
                instantiatedLaser.transform.position = GameObject.FindGameObjectWithTag("InactivePooledLasers").transform.position;
                instantiatedLaser.SetActive(false);
                laserGO_Queue.Enqueue(instantiatedLaser);
            }

            if (laserPoolDictionary.ContainsKey("LaserStock"))
            {
                laserPoolDictionary.Remove("LaserStock");
            }

            laserPoolDictionary.Add("LaserStock", laserGO_Queue);         
        }

        Debug.Log("Lasers In Pool after restock : " + laserPoolList[0].laserStock);
    }

}
