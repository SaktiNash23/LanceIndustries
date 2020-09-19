using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    [BoxGroup("Debug Toggle")]
    public bool DebugMode_PC; //True: Activates PC controls for debugging. False: Activates touch controls. Ensure this is marked 'false' when creating mobile build

    [BoxGroup("Gimmicks Toggle")]
    public bool gimmick_LaserSpeedDecrease;

    [InfoBox("Ensure the Reflector Scriptable Objects are placed in the right order in the array", EInfoBoxType.Normal)]
    [ReorderableList]
    public Reflector_SO[] allReflectorSO;

    public static GameManager gameManagerInstance; //Game Manager Instance

    [BoxGroup("Reflectors & Grids GameObject Lists")]
    public List<GameObject> allReflectorsInScene = new List<GameObject>();
    [BoxGroup("Reflectors & Grids GameObject Lists")]
    public List<GameObject> allGridInScene = new List<GameObject>();
    [BoxGroup("Buttons")]
    public List<Button> allReflectorButtons = new List<Button>();
    [BoxGroup("Buttons")]
    public List<Button> allReflectorColorButtons = new List<Button>();

    [HideInInspector]
    public bool activationToggle_Grid = false;
    [HideInInspector]
    public bool activationToggle_Reflector = false;

    

    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Basic_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Translucent_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_DoubleWay_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Split_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_ThreeWay_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI TimerSuccessText;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_White_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Red_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Blue_Text;
    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI ReflectorStock_Yellow_Text;

    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Basic_White;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Basic_Red;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Basic_Blue;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Basic_Yellow;

    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Translucent_White;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Translucent_Red;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Translucent_Blue;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Translucent_Yellow;

    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_DoubleWay_White;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_DoubleWay_Red;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_DoubleWay_Blue;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_DoubleWay_Yellow;

    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_Split;

    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_ThreeWay_White;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_ThreeWay_Red;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_ThreeWay_Blue;
    [BoxGroup("Reflector Sprites")]
    public Sprite reflectorSprite_ThreeWay_Yellow;

    [BoxGroup("Reflector Color Panel Variables")]
    public GameObject reflectorColorsPanel; //Panel that contains the buttons for the different reflector color buttons
    [BoxGroup("Reflector Color Panel Variables")]
    public bool isReflectorColorPanelActive = false;

    [BoxGroup("Stage Clear References")] [SerializeField] UIHelper uiHelperPanelClearStage;
    

    #region End Point Variables

    private GameObject[] allEndPoints;
    private int numOfEndPoints = 0;
    private int numOfSuccessEndPoints = 0;
    private int numOfHitEndPoints = 0;
    private bool allLasersHaveReached = false;
    public bool AllCorrectLasersHaveReached { get; private set; } = false;

    #endregion

    #region Gameplay Related Variables

    private float currentWindowTime;
    private float maxWindowTime;
    [HideInInspector]
    public bool beginCountDown = false;

    #endregion

    #region Dissolve Effect Variables

    [BoxGroup("Dissolve Effect Variables")]
    public Material dissolveMaterial;
    [BoxGroup("Dissolve Effect Variables")]
    public Material reflectorPanel_DissolveMaterial;

    private float dissolveFade = 0.0f;
    private float reflectorPanel_dissolveFade = 0.0f;

    [HideInInspector]
    public bool activateDissolve = true;
    [HideInInspector]
    public bool fadeIn = false;

    #endregion

    #region Reflector Stock Variables

    private int ReflectorStock_Basic_White;
    private int ReflectorStock_Basic_Red;
    private int ReflectorStock_Basic_Blue;
    private int ReflectorStock_Basic_Yellow;

    private int ReflectorStock_Translucent_White;
    private int ReflectorStock_Translucent_Red;
    private int ReflectorStock_Translucent_Blue;
    private int ReflectorStock_Translucent_Yellow;

    private int ReflectorStock_DoubleWay_White;
    private int ReflectorStock_DoubleWay_Red;
    private int ReflectorStock_DoubleWay_Blue;
    private int ReflectorStock_DoubleWay_Yellow;

    private int ReflectorStock_Split_White;
    private int ReflectorStock_Split_Red;
    private int ReflectorStock_Split_Blue;
    private int ReflectorStock_Split_Yellow;

    private int ReflectorStock_ThreeWay_White;
    private int ReflectorStock_ThreeWay_Red;
    private int ReflectorStock_ThreeWay_Blue;
    private int ReflectorStock_ThreeWay_Yellow;

    //private int ReflectorStock_Basic;
    //private int ReflectorStock_Translucent;
    //private int ReflectorStock_DoubleWay;
    //private int ReflectorStock_Split;
    //private int ReflectorStock_ThreeWay;

    #endregion

    private Color tempColor;

    //Bool to check if game is paused. If paused, gameplay updates won't run
    public bool gameIsPaused;

    void Awake()
    {
        #region Initialize GM instance
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        #endregion
    }

    void Update()
    {
        //If the game is paused, this stops the checks for touch or clicks to close reflector panels, else it runs the checks normally
        if (gameIsPaused == false)
        {
            if (dissolveFade < 1.0f)
            {
                dissolveFade += Time.deltaTime;
                dissolveMaterial.SetFloat("_Fade", dissolveFade);
            }

            #region Reflector Color Panel Dissolve Test Code
            /*
            if (activateDissolve == true)
            {
                if(fadeIn == false)
                {
                    if (reflectorPanel_dissolveFade < 1.0f)
                    {
                        Debug.LogWarning("DISSOLVE");
                        reflectorPanel_dissolveFade += Time.deltaTime;
                        reflectorPanel_DissolveMaterial.SetFloat("_Fade", reflectorPanel_dissolveFade);
                    }
                    else
                    {
                        reflectorPanel_dissolveFade = 1.0f;
                        fadeIn = true;
                        activateDissolve = false;
                    }               
                }
                else if(fadeIn == true)
                {
                    if(reflectorPanel_dissolveFade > 0.0f)
                    {
                        reflectorPanel_dissolveFade -= Time.deltaTime;
                        reflectorPanel_DissolveMaterial.SetFloat("_Fade", reflectorPanel_dissolveFade);
                    }
                    else
                    {
                        reflectorPanel_dissolveFade = 0.0f;
                        fadeIn = false;
                        reflectorColorsPanel.SetActive(false);
                        activateDissolve = false;

                    }
                }
            }
            */
            #endregion

            if (beginCountDown == true)
            {
                checkTimingWindowForLaser();
            }

            if (Input.touchCount == 1)
            {
                checkForTouchToCloseReflectorPanel();
            }

            #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                checkForClickToCloseReflectorPanel();
            }
            #endif

        }
    }

    //This function checks for a touch when the reflector color panel is active. If a touch is detected on the grid or empty space in the level
    //, while the reflector color panel is active, the reflector color panel will be deactivated
    private void checkForTouchToCloseReflectorPanel()
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
                                reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);
                                
                                //Below 2 lines will be executed using an Animation Event
                                //reflectorColorsPanel.SetActive(false);
                                //isReflectorColorPanelActive = false;
                            }
                            else if (hit.collider.tag == "UI")
                            {
                                Debug.LogWarning("Hit something, but don't know what it is");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Hit nothing. So still close Reflectors Color Panel");
                            reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);
                            
                            //Below 2 lines will be executed using an Animation Event
                            //reflectorColorsPanel.SetActive(false);
                            //isReflectorColorPanelActive = false;
                        }

                        break;
                }
            }
        }
    }

    #if UNITY_EDITOR
    private void checkForClickToCloseReflectorPanel()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if(isReflectorColorPanelActive == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, -transform.up, 0.4f);

                if (hit)
                {
                    if (hit.collider.tag == "Grid")
                    {
                        Debug.LogWarning("Hit Grid while Reflector Color panel is active");
                        reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);

                        //Below 2 lines will be executed using an Animation Event
                        //reflectorColorsPanel.SetActive(false);
                        //isReflectorColorPanelActive = false;
                    }
                    else if (hit.collider.tag == "UI")
                    {
                        Debug.LogWarning("Hit something, but don't know what it is");
                    }
                }
                else
                {
                    Debug.LogWarning("Hit nothing. So still close Reflectors Color Panel");
                    reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);

                    //Below 2 lines will be executed using an Animation Event
                    //reflectorColorsPanel.SetActive(false);
                    //isReflectorColorPanelActive = false;
                }
            }
        }
    }
    #endif

    //Toggles the active state of the grid's 2D Colliders. If a grid is currently occupied by a reflector, the 2D Collider is deactivated.
    //If the grid is currently unoccupied, the 2D Collider is activated
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

        activationToggle_Reflector = true; //True: The reflectors have been toggled accordingly
    }

    //Resets all reflector colliders to their original state, meaning it will set all reflector 2D Colliders to be active
    public void resetReflectorColliders()
    {
        for (int i = 0; i < allReflectorsInScene.Count; ++i)
        {
            allReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        //Debug.LogWarning("Reset Reflector Colliders");

        activationToggle_Reflector = false;
    }

    //ATTN: This function is not being used at the moment, so it is likely to be removed in the future
    //
    //This function checks if the stock for a type of reflector is available or not and returns a bool based on the result.
    //The parameter is a string which helps to identify which button is pressed so the function knows which part of the code to execute.
    //
    //Ex: If the button, ReflectorButton_Basic which has the tag, "ReflectorButton_Basic" is pressed, the tag is passed as the argument to this
    //function. Then, the function checks for the if statement that has the same tag and executes the corressponding code
    //
    //Note: The tag for each button can be set in the editor
    /*
    public bool checkReflectorStockAvailability(string pressedReflectorTypeButtonTag)
    {
        bool isReflectorInStock = false;

        #region ReflectorButton_Basic
        if (pressedReflectorTypeButtonTag == "ReflectorButton_Basic")
        {
            if (GameManager.gameManagerInstance.ReflectorStock_Basic > 0)
            {
                isReflectorInStock = true;
            }
            else
            {
                Debug.LogWarning("OUT OF STOCK: Basic Reflector");
                isReflectorInStock = false;
            }
        }
        #endregion

        #region ReflectorButton_Translucent
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
        #endregion

        #region ReflectorButton_DoubleWay
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
        #endregion

        #region ReflectorButton_Split
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
        #endregion

        #region ReflectorButton_ThreeWay
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
        #endregion

        return isReflectorInStock;
    }
    */
    //This function sets the color of the reflector to be spawned by the player. This is achieved by returning a string which determines which
    //object pool the game will 'need to pull' the reflector from. The parameters passed are strings which help identify the reflector type
    //and reflector color. Based on these parameters, a string with the appropriate object pool name is returned. The returned string acts a 
    //Key (Key-Value Pair) which is used to access a specific entry in the reflectorPoolDictionary
    //
    //The parameters passed, reflectorType and reflectorColor can be set in the editor on the appropriate Reflector Color UI Buttons
    public string setSelectedColorReflector(string reflectorType, string reflectorColor)
    {
        string reflectorPoolTag = System.String.Empty;

        if (reflectorType == "Basic")
        {
            if (reflectorColor == "White")
            {
                reflectorPoolTag = "ReflectorPool_Basic_White";
            }
            else if (reflectorColor == "Red")
            {
                reflectorPoolTag = "ReflectorPool_Basic_Red";
            }
            else if (reflectorColor == "Blue")
            {
                reflectorPoolTag = "ReflectorPool_Basic_Blue";
            }
            else if (reflectorColor == "Yellow")
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

    //This function returns a reflector to its stock by returning the reflector to its appropriate reflector object pool
    //The function checks the reflector GameObject which is passed as an argument, for a specific attached script. The script helps to 
    //identify whether the reflector is a Basic, Translucent, Double Way, Split or Three Way type reflector
    public void returnReflectorToStock(GameObject reflector)
    {
        #region Original Code
        /*
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
        */
        #endregion

        #region TEST CODE

        if (reflector.transform.GetChild(0).GetComponent<Reflector_Translucent>())
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_White"].Enqueue(reflector);
                ReflectorStock_Translucent_White++;
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Red"].Enqueue(reflector);
                ReflectorStock_Translucent_Red++;
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Blue"].Enqueue(reflector);
                ReflectorStock_Translucent_Blue++;
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Yellow"].Enqueue(reflector);
                ReflectorStock_Translucent_Yellow++;
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            //ReflectorStock_Translucent++;
            //ReflectorStock_Translucent_Text.text = ReflectorStock_Translucent.ToString();

        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_DoubleWay>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_White"].Enqueue(reflector);
                ReflectorStock_DoubleWay_White++;
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Red"].Enqueue(reflector);
                ReflectorStock_DoubleWay_Red++;
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Blue"].Enqueue(reflector);
                ReflectorStock_DoubleWay_Blue++;
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Yellow"].Enqueue(reflector);
                ReflectorStock_DoubleWay_Yellow++;
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            //ReflectorStock_DoubleWay++;
            //ReflectorStock_DoubleWay_Text.text = ReflectorStock_DoubleWay.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_Split>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_White"].Enqueue(reflector);
                ReflectorStock_Split_White++;
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Red"].Enqueue(reflector);
                ReflectorStock_Split_Red++;
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Blue"].Enqueue(reflector);
                ReflectorStock_Split_Blue++;
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Split_Yellow"].Enqueue(reflector);
                ReflectorStock_Split_Yellow++;
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            //ReflectorStock_Split++;
            //ReflectorStock_Split_Text.text = ReflectorStock_Split.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector_ThreeWay>() != null)
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_White"].Enqueue(reflector);
                ReflectorStock_ThreeWay_White++;
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Red"].Enqueue(reflector);
                ReflectorStock_ThreeWay_Red++;
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Blue"].Enqueue(reflector);
                ReflectorStock_ThreeWay_Blue++;
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Yellow"].Enqueue(reflector);
                ReflectorStock_ThreeWay_Yellow++;
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            //ReflectorStock_ThreeWay++;
            //ReflectorStock_ThreeWay_Text.text = ReflectorStock_ThreeWay.ToString();
        }

        else if (reflector.transform.GetChild(0).GetComponent<Reflector>())
        {
            if (reflector.name.Contains("White"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_White"].Enqueue(reflector);
                ReflectorStock_Basic_White++;
            }
            else if (reflector.name.Contains("Red"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Red"].Enqueue(reflector);
                ReflectorStock_Basic_Red++;
            }
            else if (reflector.name.Contains("Blue"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Blue"].Enqueue(reflector);
                ReflectorStock_Basic_Blue++;
            }
            else if (reflector.name.Contains("Yellow"))
            {
                ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Yellow"].Enqueue(reflector);
                ReflectorStock_Basic_Yellow++;
            }
            else
            {
                Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
            }

            //ReflectorStock_Basic++;
            //ReflectorStock_Basic_Text.text = ReflectorStock_Basic.ToString();

            Debug.Log("I WAS HERE");
        }

        reflector.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
        reflector.transform.rotation = Quaternion.identity;
        reflector.SetActive(false);

        #endregion
    }

    //This function removes a reflector from the List, allReflectorsInScene. The reflector that is removed from the list is the reflector that
    //the player does not want to place in the grid.
    public void removeReflector(GameObject reflector)
    {
        for (int i = 0; i < allReflectorsInScene.Count; ++i)
        {
            if (allReflectorsInScene[i].GetInstanceID() == reflector.GetInstanceID())
            {
                allReflectorsInScene.RemoveAt(i);
                Debug.LogWarning("Removed Reflector");
            }
        }
    }

    //This function sets all the alpha values of the grids to 0 (transparent)
    public void resetGridAlpha()
    {

        for (int j = 0; j < allGridInScene.Count; ++j)
        {
            tempColor = allGridInScene[j].GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.0f;
            allGridInScene[j].GetComponent<SpriteRenderer>().color = tempColor;
        }
    }

    //Sets the reflector buttons to be interactable
    public void activateAllButtons()
    {
        for (int i = 0; i < allReflectorButtons.Count; ++i)
        {
            allReflectorButtons[i].interactable = true;
        }
    }

    //Set the reflector buttons to be uninteractable
    public void deactivateAllButtons()
    {
        for (int i = 0; i < allReflectorButtons.Count; ++i)
        {
            allReflectorButtons[i].interactable = false;
        }
    }

    //Whenever a laser hits an EndPoint, this function is called to evaluate whether the laser that hit the EndPoint is correct or not.
    //Hence, this function is usually called in the EndPoint script
    public void updateEndPointStatus(bool hitSuccessEndPoint)
    {
        numOfHitEndPoints++;

        if(hitSuccessEndPoint == true)
        {
            numOfSuccessEndPoints++;
        }

        if(numOfHitEndPoints == numOfEndPoints)
        {
            if (numOfSuccessEndPoints == numOfEndPoints)
            {
                // Show Stage Clear Panel
                uiHelperPanelClearStage.ExecuteUIHandlingAction(true);

                AllCorrectLasersHaveReached = true;
                Debug.Log("All Correct Lasers Have Reached!");
            }
            else
            {
                allLasersHaveReached = true;
                Debug.Log("All Lasers Have Reached, but not all correct");
            }
        }
    }

    //When a laser is fired and the countdown begins, this function performs various operations
    //
    // 1.) Checks if all the lasers have reached end points before time limit expires, then subsequently checks if the lasers hit are the correct ones
    //
    // 2.) Checks if the all the laser have NOT reached the end point when time limit expires, then it destroys any lasers that are still on screen and resets the timer
    //
    // 3.) Updates the Countdown Timer UI with the current time
    public void checkTimingWindowForLaser()
    {  
         if(currentWindowTime > 0.0f)
         {
             if (AllCorrectLasersHaveReached == true)
             {
                 TimerSuccessText.text = "WIN";
                 beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                 findAndReturnLasersToPool();
                 //Reset();
             }
             else if(allLasersHaveReached == true)
             {
                TimerSuccessText.text = "FAIL";
                beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                findAndReturnLasersToPool();
                //Reset();
             }
             else
             {
                currentWindowTime -= Time.smoothDeltaTime;
                TimerSuccessText.text = currentWindowTime.ToString("F2");
             }
         }
         else if (currentWindowTime <= 0.0f)
         {
             TimerSuccessText.text = "FAIL";
             findAndReturnLasersToPool();
             beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
             //Reset();
        }
    }

    //This function resets the variables related to the game state, so that a new laser can be fired again
    public void Reset()
    {
        currentWindowTime = maxWindowTime;

        foreach (GameObject endPoint in allEndPoints)
        {
            endPoint.GetComponent<EndPoint>().isHitByLaser_Accessor = false;
            endPoint.GetComponent<EndPoint>().isHitByCorrectLaser_Accessor = false;
            endPoint.GetComponent<EndPoint>().resetEndPoint();
        }

        numOfHitEndPoints = 0;
        numOfSuccessEndPoints = 0;
        AllCorrectLasersHaveReached = false;
        allLasersHaveReached = false;
        beginCountDown = false;

        /*
        Debug.Log("Current Window Time : " + currentWindowTime);
        Debug.Log("Num of Hit End Points : " + numOfHitEndPoints);
        Debug.Log("Num of Success End Points : " + numOfSuccessEndPoints);
        Debug.Log("All Correct Lasers have reached : " + allCorrectLasersHaveReached);
        Debug.Log("All Lasers Have Reached : " + allLasersHaveReached);
        Debug.Log("Begin Countdown : " + beginCountDown);
        */
    }

    //If there are still lasers in the scene, game continues as usual
    //If there are no more lasers in the scene, the timer is directly set to 0, therefore resetting the game state.
    //
    //This function was created to ensure if all the lasers were destroyed and none of them reached all the end points, the player would not have to
    //wait until the timer expired to shoot another laser
    public void checkForAnyLasersInScene()
    {
        GameObject[] activeLasers = GameObject.FindGameObjectsWithTag("Laser");

        //Set to <= 1 because when OnDisable() is called on a projectile, (OnDisable() is where we call this function), it still counts the laser that calls this function. 
        //Meaning even if there is only 1 laser left in the scene and it calls this function upon disable, the FindGameObjectsWithTag() function will still count the invoking laser.
        if (activeLasers.Length <= 1) 
        {
            currentWindowTime = 0.0f;
        }
        else
        {
            //Debug.Log("Still got lasers");
            //Debug.Log("Active Lasers : " + activeLasers.Length);
        }
    }

    public void findAndReturnLasersToPool()
    {
        GameObject[] activeLasers = GameObject.FindGameObjectsWithTag("Laser");

        if(activeLasers.Length > 0)
        {
            foreach (GameObject laser in activeLasers)
            {
                //Destroy(laser);
                laser.GetComponent<Proto_Projectile>().returnLaserToPool(laser);
                laser.SetActive(false);
            }

            Debug.Log("Lasers In Stock : " + LaserPooler.instance_LaserPoolList.laserPoolDictionary["LaserStock"].Count);
        }
    }

    //This function is called in ReflectorUIButton, this function updates all 20 Reflector Color stocks UI before opening the Reflector Color Panel
    public void updateReflectorColorStocks(string pressedReflectorTypeButtonTag)
    {
        switch(pressedReflectorTypeButtonTag)
        {
            case "ReflectorButton_Basic":
                ReflectorStock_White_Text.text = ReflectorStock_Basic_White.ToString();
                ReflectorStock_Red_Text.text = ReflectorStock_Basic_Red.ToString();
                ReflectorStock_Blue_Text.text = ReflectorStock_Basic_Blue.ToString();
                ReflectorStock_Yellow_Text.text = ReflectorStock_Basic_Yellow.ToString();
                break;

            case "ReflectorButton_Translucent":
                ReflectorStock_White_Text.text = ReflectorStock_Translucent_White.ToString();
                ReflectorStock_Red_Text.text = ReflectorStock_Translucent_Red.ToString();
                ReflectorStock_Blue_Text.text = ReflectorStock_Translucent_Blue.ToString();
                ReflectorStock_Yellow_Text.text = ReflectorStock_Translucent_Yellow.ToString();
                break;

            case "ReflectorButton_DoubleWay":
                ReflectorStock_White_Text.text = ReflectorStock_DoubleWay_White.ToString();
                ReflectorStock_Red_Text.text = ReflectorStock_DoubleWay_Red.ToString();
                ReflectorStock_Blue_Text.text = ReflectorStock_DoubleWay_Blue.ToString();
                ReflectorStock_Yellow_Text.text = ReflectorStock_DoubleWay_Yellow.ToString();
                break;

            case "ReflectorButton_Split":
                ReflectorStock_White_Text.text = ReflectorStock_Split_White.ToString();
                ReflectorStock_Red_Text.text = ReflectorStock_Split_Red.ToString();
                ReflectorStock_Blue_Text.text = ReflectorStock_Split_Blue.ToString();
                ReflectorStock_Yellow_Text.text = ReflectorStock_Split_Yellow.ToString();
                break;

            case "ReflectorButton_ThreeWay":
                ReflectorStock_White_Text.text = ReflectorStock_ThreeWay_White.ToString();
                ReflectorStock_Red_Text.text = ReflectorStock_ThreeWay_Red.ToString();
                ReflectorStock_Blue_Text.text = ReflectorStock_ThreeWay_Blue.ToString();
                ReflectorStock_Yellow_Text.text = ReflectorStock_ThreeWay_Yellow.ToString();
                break;
        }
    }

    //This function is called when a reflector color UI button is pressed, it checks if the specific reflector type and color is available by passing in 
    //the reflector type tag and reflector color tag. If it is available, the reflector will be taken from the pool and the player can control the reflector
    public bool checkReflectorColorsStock(string reflectorTypeTag, string reflectorColorTag)
    {
        switch(reflectorTypeTag)
        {
            case "Basic":
                switch (reflectorColorTag)
                {
                    case "White":
                        if (ReflectorStock_Basic_White > 0)
                            return true;
                        break;

                    case "Red":
                        if (ReflectorStock_Basic_Red > 0)
                            return true;
                        break;

                    case "Blue":
                        if (ReflectorStock_Basic_Blue > 0)
                            return true;
                        break;

                    case "Yellow":
                        if (ReflectorStock_Basic_Yellow > 0)
                            return true;
                        break;

                    default:
                        break;
                }
                break;

            case "Translucent":
                switch (reflectorColorTag)
                {
                    case "White":
                        if (ReflectorStock_Translucent_White > 0)
                            return true;
                        break;

                    case "Red":
                        if (ReflectorStock_Translucent_Red > 0)
                            return true;
                        break;

                    case "Blue":
                        if (ReflectorStock_Translucent_Blue > 0)
                            return true;
                        break;

                    case "Yellow":
                        if (ReflectorStock_Translucent_Yellow > 0)
                            return true;
                        break;

                    default:
                        break;
                }
                break;

            case "DoubleWay":
                switch (reflectorColorTag)
                {
                    case "White":
                        if (ReflectorStock_DoubleWay_White > 0)
                            return true;
                        break;

                    case "Red":
                        if (ReflectorStock_DoubleWay_Red > 0)
                            return true;
                        break;

                    case "Blue":
                        if (ReflectorStock_DoubleWay_Blue > 0)
                            return true;
                        break;

                    case "Yellow":
                        if (ReflectorStock_DoubleWay_Yellow > 0)
                            return true;
                        break;

                    default:
                        break;
                }
                break;

            case "Split":
                switch (reflectorColorTag)
                {
                    case "White":
                        if (ReflectorStock_Split_White > 0)
                            return true;
                        break;

                    case "Red":
                        if (ReflectorStock_Split_Red > 0)
                            return true;
                        break;

                    case "Blue":
                        if (ReflectorStock_Split_Blue > 0)
                            return true;
                        break;

                    case "Yellow":
                        if (ReflectorStock_Split_Yellow > 0)
                            return true;
                        break;

                    default:
                        break;
                }
                break;

            case "ThreeWay":
                switch (reflectorColorTag)
                {
                    case "White":
                        if (ReflectorStock_ThreeWay_White > 0)
                            return true;
                        break;

                    case "Red":
                        if (ReflectorStock_ThreeWay_Red > 0)
                            return true;
                        break;

                    case "Blue":
                        if (ReflectorStock_ThreeWay_Blue > 0)
                            return true;
                        break;

                    case "Yellow":
                        if (ReflectorStock_ThreeWay_Yellow > 0)
                            return true;
                        break;

                    default:
                        break;
                }
                break;
        }

        return false;
    }

    public void Initialization(MapDataHolder mapDataHolder)
    {
        #region Initialize Grids
        GameObject[] foundGrids = GameObject.FindGameObjectsWithTag("Grid");

        for (int j = 0; j < foundGrids.Length; ++j)
        {
            allGridInScene.Add(foundGrids[j]);
        }

        resetGridAlpha();
        #endregion

        reflectorColorsPanel.SetActive(false);

        #region Initialize End Points

        allEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        numOfEndPoints = allEndPoints.Length;

        #endregion

        #region Initialize Max Window Time

        currentWindowTime = maxWindowTime = mapDataHolder.timeLimit;
        TimerSuccessText.text = maxWindowTime.ToString("F2");

        #endregion

        #region Initialize Reflector Amount

        ReflectorStock_Basic_White = mapDataHolder.basicReflectorAmount;
        ReflectorStock_Basic_Red = mapDataHolder.redBasicReflectorAmount;
        ReflectorStock_Basic_Yellow = mapDataHolder.yellowBasicReflectorAmount;
        ReflectorStock_Basic_Blue = mapDataHolder.blueBasicReflectorAmount;
        ReflectorStock_Translucent_White = mapDataHolder.translucentReflectorAmount;
        ReflectorStock_Translucent_Red = mapDataHolder.redTranslucentReflectorAmount;
        ReflectorStock_Translucent_Yellow = mapDataHolder.yellowTranslucentReflectorAmount;
        ReflectorStock_Translucent_Blue = mapDataHolder.blueTranslucentReflectorAmount;
        ReflectorStock_DoubleWay_White = mapDataHolder.doubleWayReflectorAmount;
        ReflectorStock_DoubleWay_Red = mapDataHolder.redDoubleWayReflectorAmount;
        ReflectorStock_DoubleWay_Yellow = mapDataHolder.yellowDoubleWayReflectorAmount;
        ReflectorStock_DoubleWay_Blue = mapDataHolder.blueDoubleWayReflectorAmount;
        ReflectorStock_Split_White = mapDataHolder.splitReflectorAmount;
        ReflectorStock_Split_Red = mapDataHolder.redSplitReflectorAmount;
        ReflectorStock_Split_Yellow = mapDataHolder.yellowSplitReflectorAmount;
        ReflectorStock_Split_Blue = mapDataHolder.blueSplitReflectorAmount;
        ReflectorStock_ThreeWay_White = mapDataHolder.threeWayReflectorAmount;
        ReflectorStock_ThreeWay_Red = mapDataHolder.redThreeWayReflectorAmount;
        ReflectorStock_ThreeWay_Yellow = mapDataHolder.yellowThreeWayReflectorAmount;
        ReflectorStock_ThreeWay_Blue = mapDataHolder.blueThreeWayReflectorAmount;

        #endregion

        #region Spawn Pool Objects

        ReflectorPooler.instance_reflectorPooler.Initialization();

        #endregion
    }

    #region Accessor Functions
    /*
    public int ReflectorStockBasic_Accessor
    {
        get
        { return ReflectorStock_Basic; }

        set
        { ReflectorStock_Basic = value; }
    }

    public int ReflectorStockTranslucent_Accessor
    {
        get
        { return ReflectorStock_Translucent; }

        set
        { ReflectorStock_Translucent = value; }
    }

    public int ReflectorStockDoubleWay_Accessor
    {
        get
        { return ReflectorStock_DoubleWay; }

        set
        { ReflectorStock_DoubleWay = value; }
    }

    public int ReflectorStockSplit_Accessor
    {
        get
        { return ReflectorStock_Split; }

        set
        { ReflectorStock_Split = value; }
    }

    public int ReflectorStockThreeWay_Accessor
    {
        get
        { return ReflectorStock_ThreeWay; }

        set 
        { ReflectorStock_ThreeWay = value; }
    }
    */
    public float currentWindowTime_Accessor
    {
        get
        { return currentWindowTime; }

        set
        { currentWindowTime = value; }
    }

    public int ReflectorStockBasicWhite_Accessor
    {
        get
        { return ReflectorStock_Basic_White; }

        set
        { ReflectorStock_Basic_White = value;}
    }

    public int ReflectorStockBasicRed_Accessor
    {
        get
        { return ReflectorStock_Basic_Red; }

        set
        { ReflectorStock_Basic_Red = value; }
    }

    public int ReflectorStockBasicBlue_Accessor
    {
        get
        { return ReflectorStock_Basic_Blue; }

        set
        { ReflectorStock_Basic_Blue = value; }
    }

    public int ReflectorStockBasicYellow_Accessor
    {
        get
        { return ReflectorStock_Basic_Yellow; }

        set
        { ReflectorStock_Basic_Yellow = value; }
    }

    public int ReflectorStockTranslucentWhite_Accessor
    {
        get
        { return ReflectorStock_Translucent_White; }

        set
        { ReflectorStock_Translucent_White = value; }
    }

    public int ReflectorStockTranslucentRed_Accessor
    {
        get
        { return ReflectorStock_Translucent_Red; }

        set
        { ReflectorStock_Translucent_Red = value; }
    }

    public int ReflectorStockTranslucentBlue_Accessor
    {
        get
        { return ReflectorStock_Translucent_Blue; }

        set
        { ReflectorStock_Translucent_Blue = value; }
    }

    public int ReflectorStockTranslucentYellow_Accessor
    {
        get
        { return ReflectorStock_Translucent_Yellow; }

        set
        { ReflectorStock_Translucent_Yellow = value; }
    }

    public int ReflectorStockDoubleWayWhite_Accessor
    {
        get
        { return ReflectorStock_DoubleWay_White; }

        set
        { ReflectorStock_DoubleWay_White = value; }
    }

    public int ReflectorStockDoubleWayRed_Accessor
    {
        get
        { return ReflectorStock_DoubleWay_Red; }

        set
        { ReflectorStock_DoubleWay_Red = value; }
    }

    public int ReflectorStockDoubleWayBlue_Accessor
    {
        get
        { return ReflectorStock_DoubleWay_Blue; }

        set
        { ReflectorStock_DoubleWay_Blue = value; }
    }

    public int ReflectorStockDoubleWayYellow_Accessor
    {
        get
        { return ReflectorStock_DoubleWay_Yellow; }

        set
        { ReflectorStock_DoubleWay_Yellow = value; }
    }

    public int ReflectorStockSplitWhite_Accessor
    {
        get
        { return ReflectorStock_Split_White; }

        set
        { ReflectorStock_Split_White = value; }
    }

    public int ReflectorStockSplitRed_Accessor
    {
        get
        { return ReflectorStock_Split_Red; }

        set
        { ReflectorStock_Split_Red = value; }
    }

    public int ReflectorStockSplitBlue_Accessor
    {
        get
        { return ReflectorStock_Split_Blue; }

        set
        { ReflectorStock_Split_Blue = value; }
    }

    public int ReflectorStockSplitYellow_Accessor
    {
        get
        { return ReflectorStock_Split_Yellow; }

        set
        { ReflectorStock_Split_Yellow = value; }
    }

    public int ReflectorStockThreeWayWhite_Accessor
    {
        get
        { return ReflectorStock_ThreeWay_White; }

        set
        { ReflectorStock_ThreeWay_White = value; }
    }

    public int ReflectorStockThreeWayRed_Accessor
    {
        get
        { return ReflectorStock_ThreeWay_Red; }

        set
        { ReflectorStock_ThreeWay_Red = value; }
    }

    public int ReflectorStockThreeWayBlue_Accessor
    {
        get
        { return ReflectorStock_ThreeWay_Blue; }

        set
        { ReflectorStock_ThreeWay_Blue = value; }
    }

    public int ReflectorStockThreeWayYellow_Accessor
    {
        get
        { return ReflectorStock_ThreeWay_Yellow; }

        set
        { ReflectorStock_ThreeWay_Yellow = value; }
    }

    #endregion

}
