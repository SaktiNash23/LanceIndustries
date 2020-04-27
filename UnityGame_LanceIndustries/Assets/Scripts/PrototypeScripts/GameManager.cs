using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{

   [InfoBox("Ensure the Reflector Scriptable Objects are placed in the right order in the array", EInfoBoxType.Normal)]
   [ReorderableList]
   public Reflector_SO[] allReflectorSO;

   //[InfoBox("Ensure the Reflector Gameobjects are placed in the right order", EInfoBoxType.Normal)]
   //[ReorderableList]
   //public GameObject[] allReflectorGO;

   public static GameManager gameManagerInstance; //Game Manager Instance

    public List<GameObject> allReflectorsInScene = new List<GameObject>();
    public List<GameObject> allGridInScene = new List<GameObject>(); 

    public bool activationToggle_Grid = false;
    public bool activationToggle_Reflector = false;

    public int ReflectorStock_Basic;
    public int ReflectorStock_Translucent;
    public int ReflectorStock_DoubleWay;
    public int ReflectorStock_Split;
    public int ReflectorStock_ThreeWay;
    
    public TextMeshProUGUI ReflectorStock_Basic_Text;
    public TextMeshProUGUI ReflectorStock_Translucent_Text;
    public TextMeshProUGUI ReflectorStock_DoubleWay_Text;
    public TextMeshProUGUI ReflectorStock_Split_Text;
    public TextMeshProUGUI ReflectorStock_ThreeWay_Text;

    public List<Button> allReflectorButtons = new List<Button>();
    public List<Button> allReflectorColorButtons = new List<Button>();

    public Sprite reflectorSprite_Basic;
    public Sprite reflectorSprite_Translucent;
    public Sprite reflectorSprite_DoubleWay;
    public Sprite reflectorSprite_Split;
    public Sprite reflectorSprite_ThreeWay;

    public GameObject reflectorColorsPanel; //Panel that contains the buttons for the different reflector color buttons

    Color tempColor;

    public bool isReflectorColorPanelActive = false;

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

        reflectorColorsPanel.SetActive(false);
    }

    void Start()
    {
        ReflectorStock_Basic_Text.text = ReflectorStock_Basic.ToString();
        ReflectorStock_Translucent_Text.text = ReflectorStock_Translucent.ToString();
        ReflectorStock_DoubleWay_Text.text = ReflectorStock_DoubleWay.ToString();
        ReflectorStock_Split_Text.text = ReflectorStock_Split.ToString();
        ReflectorStock_ThreeWay_Text.text = ReflectorStock_ThreeWay.ToString();
    }

    void Update()
    {
        Debug.Log("IsReflectorColorPanelActive " + isReflectorColorPanelActive);

        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (isReflectorColorPanelActive == true)
                {      
                    Debug.DrawRay(touch.position, -transform.up, Color.red, 3.0f);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            RaycastHit2D hit = Physics2D.Raycast(touch.position, -transform.up, 0.4f);

                            if (hit)
                            {
                                if (hit.collider.tag == "Grid")
                                {
                                    Debug.LogWarning("Hit Grid while Reflector Color panel is active");
                                    reflectorColorsPanel.SetActive(false);
                                    isReflectorColorPanelActive = false;
                                }
                                else if (hit.collider.tag == "UI")
                                {
                                    Debug.LogWarning("Hit something, but don't know what it is");
                                }
                            }
                            else
                            {
                                Debug.LogWarning("Hit nothing. So still close Reflectors Color Panel");
                                reflectorColorsPanel.SetActive(false);
                                isReflectorColorPanelActive = false;
                            }

                            break;
                    }
                }
            }
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
    
    public bool checkReflectorStockAvailability(string pressedReflectorTypeButtonTag)
    {
        bool isReflectorInStock = false;

        if(pressedReflectorTypeButtonTag == "ReflectorButton_Basic")
        {
            if(GameManager.gameManagerInstance.ReflectorStock_Basic > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Basic Reflector");
                isReflectorInStock = false;
            }
        }

        if (pressedReflectorTypeButtonTag == "ReflectorButton_Translucent")
        {
            if (GameManager.gameManagerInstance.ReflectorStock_Translucent > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Translucent Reflector");
                isReflectorInStock = false;
            }
        }

        if (pressedReflectorTypeButtonTag == "ReflectorButton_DoubleWay")
        {
            if (GameManager.gameManagerInstance.ReflectorStock_DoubleWay > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Double Way Reflector");
                isReflectorInStock = false;
            }
        }

        if (pressedReflectorTypeButtonTag == "ReflectorButton_Split")
        {
            if (GameManager.gameManagerInstance.ReflectorStock_Split > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Split Reflector");
                isReflectorInStock = false;
            }
        }

        if (pressedReflectorTypeButtonTag == "ReflectorButton_ThreeWay")
        {
            if (GameManager.gameManagerInstance.ReflectorStock_ThreeWay > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Three Way Reflector");
                isReflectorInStock = false;
            }
        }
        return isReflectorInStock;
    }

    public string setSelectedColorReflector(string reflectorType, string reflectorColor)
    {
        string reflectorPoolTag = System.String.Empty;

        if (reflectorType == "Basic")
        {
            if(reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_Basic_White";
            }
            else if(reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_Basic_Red";
            }
            else if(reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_Basic_Blue";
            }
            else if(reflectorColor == "Yellow")
            {
                reflectorPoolTag = "ReflectorPool_Basic_Yellow";
            }
            else
            {
                Debug.LogWarning("No such reflector color exist. Check if the reflectorColorTag is set properly in editor and if " +
                    "it matches the ones in the conditional statements");
            }
           
        }

        else if (reflectorType == "Translucent")
        {
            if (reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_Translucent_White";
            }
            else if (reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_Translucent_Red";
            }
            else if (reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_Translucent_Blue";
            }
            else if (reflectorColor == "Yellow")
            {
                reflectorPoolTag = "ReflectorPool_Translucent_Yellow";
            }
            else
            {
                Debug.LogWarning("No such reflector color exist. Check if the reflectorColorTag is set properly in editor and if " +
                    "it matches the ones in the conditional statements");
            }

        }

        else if (reflectorType == "DoubleWay")
        {
            if (reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_DoubleWay_White";
            }
            else if (reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_DoubleWay_Red";
            }
            else if (reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_DoubleWay_Blue";
            }
            else if (reflectorColor == "Yellow")
            {
                reflectorPoolTag = "ReflectorPool_DoubleWay_Yellow";
            }
            else
            {
                Debug.LogWarning("No such reflector color exist. Check if the reflectorColorTag is set properly in editor and if " +
                    "it matches the ones in the conditional statements");
            }

        }

        else if (reflectorType == "Split")
        {
            if (reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_Split_White";
            }
            else if (reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_Split_Red";
            }
            else if (reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_Split_Blue";
            }
            else if (reflectorColor == "Yellow")
            {
                reflectorPoolTag = "ReflectorPool_Split_Yellow";
            }
            else
            {
                Debug.LogWarning("No such reflector color exist. Check if the reflectorColorTag is set properly in editor and if " +
                    "it matches the ones in the conditional statements");
            }

        }

        else if (reflectorType == "ThreeWay")
        {
            if (reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_ThreeWay_White";
            }
            else if (reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_ThreeWay_Red";
            }
            else if (reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_ThreeWay_Blue";
            }
            else if (reflectorColor == "Yellow")
            {
                reflectorPoolTag = "ReflectorPool_ThreeWay_Yellow";
            }
            else
            {
                Debug.LogWarning("No such reflector color exist. Check if the reflectorColorTag is set properly in editor and if " +
                    "it matches the ones in the conditional statements");
            }

        }

        else
        {
            Debug.LogWarning("No such reflectorType exists. Check if the ReflectorTypeTag is set correctly in editor and if it matches in the" +
                "conditional statement");
        }

        return reflectorPoolTag;
    }

    public void returnReflectorToStock(GameObject reflector)
    {

        if (reflector.transform.GetChild(0).GetComponent<Reflector_Translucent>())
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_White"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Red"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Blue"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Yellow"].Enqueue(reflector);
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            ReflectorStock_Translucent++;
            ReflectorStock_Translucent_Text.text = ReflectorStock_Translucent.ToString();

        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_DoubleWay>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_White"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Red"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Blue"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Yellow"].Enqueue(reflector);
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            ReflectorStock_DoubleWay++;
            ReflectorStock_DoubleWay_Text.text = ReflectorStock_DoubleWay.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_Split>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_White"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Red"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Blue"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Yellow"].Enqueue(reflector);
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            ReflectorStock_Split++;
            ReflectorStock_Split_Text.text = ReflectorStock_Split.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_ThreeWay>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_White"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Red"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Blue"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Yellow"].Enqueue(reflector);
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            ReflectorStock_ThreeWay++;
            ReflectorStock_ThreeWay_Text.text = ReflectorStock_ThreeWay.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector>())
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_White"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Red"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Blue"].Enqueue(reflector);
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Yellow"].Enqueue(reflector);
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            ReflectorStock_Basic++;
            ReflectorStock_Basic_Text.text = ReflectorStock_Basic.ToString();

            Debug.Log("I WAS HERE");
        }

        reflector.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
        reflector.transform.rotation = Quaternion.identity;
        reflector.SetActive(false);
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
