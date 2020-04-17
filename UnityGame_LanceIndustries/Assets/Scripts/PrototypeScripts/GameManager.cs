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

    public List<GameObject> allReflectorsInScene = new List<GameObject>();
    public List<GameObject> allGridInScene = new List<GameObject>(); 

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

    public TextMeshProUGUI ReflectorStock_Basic_Text;
    public TextMeshProUGUI ReflectorStock_Translucent_Text;
    public TextMeshProUGUI ReflectorStock_DoubleWay_Text;
    public TextMeshProUGUI ReflectorStock_Split_Text;
    public TextMeshProUGUI ReflectorStock_ThreeWay_Text;

    public List<Button> allReflectorButtons = new List<Button>();

    Color tempColor;


    void Awake()
    {
        
        if (gameManagerInstance == null)
        {
             gameManagerInstance = this;
        }

        GameObject[] foundGrids = GameObject.FindGameObjectsWithTag("Grid");

        for(int j = 0; j < foundGrids.Length; ++j)
        {
            allGridInScene.Add(foundGrids[j]);
        }

        //Set all the alpha values of the grids to 0 (transparent)
        for(int j = 0; j < allGridInScene.Count; ++j)
        {
            tempColor = allGridInScene[j].GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.0f;
            allGridInScene[j].GetComponent<SpriteRenderer>().color = tempColor;
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

        Debug.LogWarning("Reset Reflector Colliders");

        activationToggle_Reflector = false;
    }
    
    public bool checkReflectorStockAvailability(string reflectorButtonPressedName, out int reflectorToSpawnIndex)
    {
        bool isReflectorInStock = false;
        reflectorToSpawnIndex = 0;

        if(reflectorButtonPressedName == "ReflectorButton_Basic")
        {
            if(reflectorStock_Basic > 0)
            {
                reflectorToSpawnIndex = 0;
                reflectorStock_Basic--;
                ReflectorStock_Basic_Text.text = reflectorStock_Basic.ToString();
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Basic Reflector");
                isReflectorInStock = false;
            }
        }

        if(reflectorButtonPressedName == "ReflectorButton_Translucent")
        {
            if (reflectorStock_Translucent > 0)
            {
                reflectorToSpawnIndex = 1; 
                reflectorStock_Translucent--;
                ReflectorStock_Translucent_Text.text = reflectorStock_Translucent.ToString();
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Translucent Reflector");
                isReflectorInStock = false;
            }
        }

        if(reflectorButtonPressedName == "ReflectorButton_DoubleWay")
        {
            if (reflectorStock_DoubleWay > 0)
            {
                reflectorToSpawnIndex = 2;
                reflectorStock_DoubleWay--;
                ReflectorStock_DoubleWay_Text.text = reflectorStock_DoubleWay.ToString();
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Double Way Reflector");
                isReflectorInStock = false;
            }
        }

        if(reflectorButtonPressedName == "ReflectorButton_Split")
        {
            if (reflectorStock_Split > 0)
            {
                reflectorToSpawnIndex = 3;
                reflectorStock_Split--;
                ReflectorStock_Split_Text.text = reflectorStock_Split.ToString();
                isReflectorInStock = true;
            }
            else
            {
                Debug.Log("OUT OF STOCK: Split Reflector");
                isReflectorInStock = false;
            }
        }

        if(reflectorButtonPressedName == "ReflectorButton_ThreeWay")
        {
            if (reflectorStock_ThreeWay > 0)
            {
                reflectorToSpawnIndex = 4;
                reflectorStock_ThreeWay--;
                ReflectorStock_ThreeWay_Text.text = reflectorStock_ThreeWay.ToString();
                isReflectorInStock = true;
                
            }
            else
            {
                Debug.Log("OUT OF STOCK: Three Way Reflector");
                isReflectorInStock = false;
            }
        }

        return isReflectorInStock;
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

    public void removeReflector(GameObject reflector)
    {

        for(int i = 0; i < allReflectorsInScene.Count; ++i)
        {
            if(allReflectorsInScene[i].GetInstanceID() == reflector.GetInstanceID())
            {
                allReflectorsInScene.RemoveAt(i);
                Debug.LogWarning("Removed Reflector");
            }
        }
    }

    public void resetGridAlpha()
    {
        //Set all the alpha values of the grids to 0 (transparent)
        for (int j = 0; j < allGridInScene.Count; ++j)
        {
            tempColor = allGridInScene[j].GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.0f;
            allGridInScene[j].GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    public void activateAllButtons()
    {
        for (int i = 0; i < allReflectorButtons.Count; ++i)
        {
            allReflectorButtons[i].interactable = true;
        }
    }

    public void deactivateAllButtons()
    {
        for(int i = 0; i < allReflectorButtons.Count; ++i)
        {
            allReflectorButtons[i].interactable = false;
        }
    }

}
