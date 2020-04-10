using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

   [InfoBox("Ensure the Reflector Gameobjects are placed in the right order", EInfoBoxType.Normal)]
   [ReorderableList]
   public GameObject[] allReflectorGO;

        public static GameManager gameManagerInstance; //Game Manager Instance

        public List<GameObject> allReflectorsInScene = new List<GameObject>(); //Stores all the reflectors that are in the scene
        public List<GameObject> allGridInScene = new List<GameObject>(); //

        public bool activationToggle_Grid = false;
        public bool activationToggle_Reflector = false;

        [SerializeField]
        private int reflectorStock_Basic;
        [SerializeField]
        private int reflectorStock_Translucent;
        [SerializeField]
        private int reflectorStock_DoubleWay;
        [SerializeField]
        private int reflectorStock_Split;
        [SerializeField]
        private int reflectorStock_ThreeWay;

    GameObject instantiatedReflector = null;

    public TextMeshProUGUI ReflectorStock_Basic_Text;
    public TextMeshProUGUI ReflectorStock_Translucent_Text;
    public TextMeshProUGUI ReflectorStock_DoubleWay_Text;
    public TextMeshProUGUI ReflectorStock_Split_Text;
    public TextMeshProUGUI ReflectorStock_ThreeWay_Text;

    public Button ReflectorBasicButton;

    void Awake()
        {
            if (gameManagerInstance == null)
            {
                gameManagerInstance = this;
            }

            /*
            GameObject[] foundReflectors = GameObject.FindGameObjectsWithTag("ReflectorGM");
            
            for(int i = 0; i < foundReflectors.Length; ++i)
            {
                allReflectorsInScene.Add(foundReflectors[i]);
                Debug.Log("Reflector Added : " + allReflectorsInScene[i].name);
            }
            */

            GameObject[] foundGrids = GameObject.FindGameObjectsWithTag("Grid");

            for(int j = 0; j < foundGrids.Length; ++j)
            {
                allGridInScene.Add(foundGrids[j]);
            }
        }

    void Start()
    {
        ReflectorStock_Basic_Text.text = reflectorStock_Basic.ToString();
        ReflectorStock_Translucent_Text.text = reflectorStock_Translucent.ToString();
        ReflectorStock_DoubleWay_Text.text = reflectorStock_DoubleWay.ToString();
        ReflectorStock_Split_Text.text = reflectorStock_Split.ToString();
        ReflectorStock_ThreeWay_Text.text = reflectorStock_ThreeWay.ToString();
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
        
    //Turns off all reflector colliders except for the one the player is currently in control of
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

    //Resets all refelctor colliders to their original state
    public void resetReflectorColliders()
    {
         for (int i = 0; i < allReflectorsInScene.Count; ++i)
         {
            allReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = true;
         }

        activationToggle_Reflector = false;
    }

    public GameObject checkReflectorStockAvailability(string reflectorButtonPressedName)
    {
        if(reflectorButtonPressedName == "ReflectorButton_Basic")
        {
            if(reflectorStock_Basic > 0)
            {
                instantiatedReflector = allReflectorGO[0];
                reflectorStock_Basic--;
                ReflectorStock_Basic_Text.text = reflectorStock_Basic.ToString();
            }
            else
            {
                Debug.Log("OUT OF STOCK: Basic Reflector");
                instantiatedReflector = null; //No reflector is instantiated to be returned
            }
        }

        return instantiatedReflector;
    }

    public void returnReflectorToStock(GameObject reflector)
    {
        if (reflector.name.Contains("Basic"))
        {
            reflectorStock_Basic++;
            ReflectorStock_Basic_Text.text = reflectorStock_Basic.ToString();
        }
        if (reflector.name.Contains("Translucent"))
        {
            reflectorStock_Translucent++;
            ReflectorStock_Translucent_Text.text = reflectorStock_Translucent.ToString();
        }
        if (reflector.name.Contains("DoubleWay"))
        {
            reflectorStock_DoubleWay++;
            ReflectorStock_DoubleWay_Text.text = reflectorStock_DoubleWay.ToString();
        }
        if (reflector.name.Contains("Split"))
        {
            reflectorStock_Split++;
            ReflectorStock_Split_Text.text = reflectorStock_Split.ToString();
        }
        if (reflector.name.Contains("ThreeWay"))
        {
            reflectorStock_ThreeWay++;
            ReflectorStock_ThreeWay_Text.text = reflectorStock_ThreeWay.ToString();
        }
    }

    public void removeReflector(GameObject reflectorToBeRemoved)
    {
        for(int j = 0; j < allReflectorsInScene.Count; ++j)
        {
            Debug.LogWarning(allReflectorsInScene[j].GetInstanceID());
        }

        for(int i = 0; i < allReflectorsInScene.Count; ++i)
        {
            if(reflectorToBeRemoved.GetInstanceID() == allReflectorsInScene[i].GetInstanceID())
            {
                Debug.Log("Reflector Removed");
                allReflectorsInScene.RemoveAt(i);
            }
            else
            {
                Debug.Log("No such reflector");
            }
        }
    }

}
