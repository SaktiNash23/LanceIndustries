using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


    public class GameManager : MonoBehaviour
    {
        /*
         Index References for allReflectorSO array 

         Index 0: Base Reflector
         Index 1: Translucent Reflector
         Index 2: Double Way Reflector
         Index 3: Split Reflector
         Index 4: Three-way Reflector

         */
        [InfoBox("Ensure the Reflector Scriptable Objects are placed in the right order in the array", EInfoBoxType.Normal)]
        [ReorderableList]
        public Reflector_SO[] allReflectorSO;

        public static GameManager gameManagerInstance;
        public List<GameObject> allReflectorsInScene = new List<GameObject>();
        public List<GameObject> allGridInScene = new List<GameObject>();
        public bool activationToggle_Grid = false;
        public bool activationToggle_Reflector = false;

        void Awake()
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = this;
            }

           GameObject[] foundReflectors = GameObject.FindGameObjectsWithTag("ReflectorGM");
            
            for(int i = 0; i < foundReflectors.Length; ++i)
            {
                allReflectorsInScene.Add(foundReflectors[i]);
                Debug.Log("Reflector Added : " + allReflectorsInScene[i].name);
            }

            GameObject[] foundGrids = GameObject.FindGameObjectsWithTag("Grid");

            for(int j = 0; j < foundGrids.Length; ++j)
            {
                allGridInScene.Add(foundGrids[j]);
            }
        }

        public void toggleGridColliders()
        {
            for (int i = 0; i < GameManager.gameManagerInstance.allGridInScene.Count; ++i)
            {
                if (allGridInScene[i].GetComponent<Proto_Grid>().isOccupied_GridAccessor == true)
                    GameManager.gameManagerInstance.allGridInScene[i].GetComponentInParent<BoxCollider2D>().enabled = false;
                else if (allGridInScene[i].GetComponent<Proto_Grid>().isOccupied_GridAccessor == false)
                    GameManager.gameManagerInstance.allGridInScene[i].GetComponentInParent<BoxCollider2D>().enabled = true;
            }
        }
        

        public void toggleReflectorColliders()
        {
            for (int i = 0; i < allReflectorsInScene.Count; ++i)
            {
               allReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = false;
            }


            for (int j = 0; j < allReflectorsInScene.Count; ++j)
            {
                if (allReflectorsInScene[j].GetComponent<Raycast>().isHoldingDownAccessor == true)
                {            
                    allReflectorsInScene[j].GetComponent<BoxCollider2D>().enabled = true;
                    break;
                }                   
            }

            activationToggle_Reflector = true;
        }

        public void resetReflectorColliders()
        {
            for (int i = 0; i < allReflectorsInScene.Count; ++i)
            {
                allReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = true;
            }

            activationToggle_Reflector = false;
        }

    }
