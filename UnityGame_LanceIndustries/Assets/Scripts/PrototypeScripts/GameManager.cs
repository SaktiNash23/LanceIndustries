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

    public static GameManager gameManagerInstance; //Game Manager Instance

    public List<GameObject> allReflectorsInScene = new List<GameObject>();
    public List<GameObject> allGridInScene = new List<GameObject>();
    public List<Button> allReflectorButtons = new List<Button>();
    public List<Button> allReflectorColorButtons = new List<Button>();

    public bool activationToggle_Grid = false;
    public bool activationToggle_Reflector = false;

    [SerializeField]
    private int ReflectorStock_Basic;
    [SerializeField]
    private int ReflectorStock_Translucent;
    [SerializeField]
    private int ReflectorStock_DoubleWay;
    [SerializeField]
    private int ReflectorStock_Split;
    [SerializeField]
    private int ReflectorStock_ThreeWay;

    public TextMeshProUGUI ReflectorStock_Basic_Text;
    public TextMeshProUGUI ReflectorStock_Translucent_Text;
    public TextMeshProUGUI ReflectorStock_DoubleWay_Text;
    public TextMeshProUGUI ReflectorStock_Split_Text;
    public TextMeshProUGUI ReflectorStock_ThreeWay_Text;
    public TextMeshProUGUI TimerSuccessText;

    public Sprite reflectorSprite_Basic;
    public Sprite reflectorSprite_Translucent;
    public Sprite reflectorSprite_DoubleWay;
    public Sprite reflectorSprite_Split;
    public Sprite reflectorSprite_ThreeWay;

    public GameObject reflectorColorsPanel; //Panel that contains the buttons for the different reflector color buttons

    Color tempColor;

    public bool isReflectorColorPanelActive = false;

    #region Variables: End Point

    private GameObject[] allEndPoints;
    private int numOfEndPoints = 0;
    private int numOfSuccessEndPoints = 0;
    private int numOfHitEndPoints = 0;
    private bool allCorrectLasersHaveReached = false;
    private bool allLasersHaveReached = false;

    #endregion

    private float currentWindowTime;
    public float maxWindowTime;
    public bool beginCountDown = false;

    //Function to do something once the window closes, such as saying time expired, reset the currentWindowTime, set ifWindowIsOpen = false

    void Awake()
    {
        #region Initialize GM instance
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        #endregion

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

        currentWindowTime = maxWindowTime;
        TimerSuccessText.text = maxWindowTime.ToString("F2");
    }

    void Start()
    {
        #region Initialize Reflector Stock Text UI

        ReflectorStock_Basic_Text.text = ReflectorStock_Basic.ToString();
        ReflectorStock_Translucent_Text.text = ReflectorStock_Translucent.ToString();
        ReflectorStock_DoubleWay_Text.text = ReflectorStock_DoubleWay.ToString();
        ReflectorStock_Split_Text.text = ReflectorStock_Split.ToString();
        ReflectorStock_ThreeWay_Text.text = ReflectorStock_ThreeWay.ToString();

        #endregion
    }

    void Update()
    {
        //Debug.Log("IsReflectorColorPanelActive " + isReflectorColorPanelActive);
        Debug.Log("Begin Countdown : " + beginCountDown);

        if(beginCountDown == true)
        {
            checkTimingWindowForLaser();
        }

        if (Input.touchCount == 1)
        {
            checkForTouchToCloseReflectorPanel();
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

        activationToggle_Reflector = true;
    }

    //Resets all reflector colliders to their original state, meaning it will set all reflector 2D Colliders to be active
    public void resetReflectorColliders()
    {
        for (int i = 0; i < allReflectorsInScene.Count; ++i)
        {
            allReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        Debug.LogWarning("Reset Reflector Colliders");

        activationToggle_Reflector = false;
    }

    //This function checks if the stock for a type of reflector is available or not and returns a bool based on the result.
    //The parameter is a string which helps to identify which button is pressed so the function knows which part of the code to execute.
    //
    //Ex: If the button, ReflectorButton_Basic which has the tag, "ReflectorButton_Basic" is pressed, the tag is passed as the argument to this
    //function. Then, the function checks for the if statement that has the same tag and executes the corressponding code
    //
    //Note: The tag for each button can be set in the editor
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
                allCorrectLasersHaveReached = true;
                Debug.Log("All Correct Lasers Have Reached!");
            }
            else
            {
                allLasersHaveReached = true;
                Debug.Log("All Lasers Have Reached, but not all correct");
            }
        }


        /*
        numOfSuccessEndPoints++;
        Debug.Log("Num of Success End Points : " + numOfSuccessEndPoints);

        if(numOfSuccessEndPoints == numOfEndPoints)
        {
            allLasersHaveReached = true;
            Debug.Log("All Lasers Have Reached!");
        }
        */
    }

    public void checkTimingWindowForLaser()
    {  
         if(currentWindowTime > 0.0f)
         {
             if (allCorrectLasersHaveReached == true)
             {
                 Debug.Log("All Correct Laser Have Reached earlier than time limit!!");
                 TimerSuccessText.text = "SUCCESS";
                 Reset();
             }
             else if(allLasersHaveReached == true)
             {
                Debug.LogWarning("All Lasers Reached, but not all correct");
                TimerSuccessText.text = "FAIL";
                Reset();
             }
             else
             {
                Debug.Log("COUNT");
                currentWindowTime -= Time.smoothDeltaTime;
                TimerSuccessText.text = currentWindowTime.ToString("F2");
             }
         }
         else if (currentWindowTime <= 0.0f)
         {
             Debug.LogWarning("Timing Window is closed!!!");
             TimerSuccessText.text = "FAIL";
             Reset();
         }
    }

    public void Reset()
    {
        currentWindowTime = maxWindowTime;

        foreach (GameObject endPoint in allEndPoints)
        {
            endPoint.GetComponent<EndPoint>().isHitByLaser_Accessor = false;
            endPoint.GetComponent<EndPoint>().isHitByCorrectLaser_Accessor = false;
        }

        numOfHitEndPoints = 0;
        numOfSuccessEndPoints = 0;
        allCorrectLasersHaveReached = false;
        allLasersHaveReached = false;
        beginCountDown = false;

        Debug.Log("Current Window Time : " + currentWindowTime);
        Debug.Log("Num of Hit End Points : " + numOfHitEndPoints);
        Debug.Log("Num of Success End Points : " + numOfSuccessEndPoints);
        Debug.Log("All Correct Lasers have reached : " + allCorrectLasersHaveReached);
        Debug.Log("All Lasers Have Reached : " + allLasersHaveReached);
        Debug.Log("Begin Countdown : " + beginCountDown);

    }

    #region Accessor Functions

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

    #endregion

}
