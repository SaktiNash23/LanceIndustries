using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    [Header("PREFABS")]
    [SerializeField] protected Reflector reflectorPrefab;
    [SerializeField] protected ReflectorTranslucent reflectorTranslucentPrefab;
    [SerializeField] protected ReflectorDoubleWay reflectorDoubleWayPrefab;
    [SerializeField] protected ReflectorThreeWay reflectorThreeWayPrefab;

    [BoxGroup("Debug Toggle")]
    public bool DebugMode_PC; //True: Activates PC controls for debugging. False: Activates touch controls. Ensure this is marked 'false' when creating mobile build

    [BoxGroup("Gimmicks Toggle")]
    public bool gimmick_LaserSpeedDecrease;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } } //Game Manager Instance

    [BoxGroup("Reflectors & Grids GameObject Lists")]
    public List<Proto_Grid> gridOutlines = new List<Proto_Grid>();
    [BoxGroup("Buttons")]
    public List<Button> allReflectorButtons = new List<Button>();
    [BoxGroup("Buttons")]
    public List<ReflectorColor_UIButton> allReflectorColorButtons = new List<ReflectorColor_UIButton>();

    [HideInInspector]
    public bool activationToggle_Grid = false;
    [HideInInspector]
    public bool activationToggle_Reflector = false;

    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI TimerSuccessText;

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
    [SerializeField] protected ReflectorColorPanel reflectorColorPanel;
    [BoxGroup("Reflector Color Panel Variables")]
    public bool isReflectorColorPanelActive = false;

    [BoxGroup("Stage Clear References")] [SerializeField] UIHelper uiHelperPanelClearStage;

    public ReflectorColorPanel ReflectorColorPanel { get => reflectorColorPanel; }
    public Reflector GetBasicReflectorPrefab { get { return reflectorPrefab; } }
    public ReflectorTranslucent GetReflectorTranslucentPrefab { get { return reflectorTranslucentPrefab; } }
    public ReflectorDoubleWay GetReflectorDoubleWayPrefab { get { return reflectorDoubleWayPrefab; } }
    public ReflectorThreeWay GetReflectorThreeWayPrefab { get { return reflectorThreeWayPrefab; } }

    public List<Reflector> AllReflectorsInScene { get; private set; } = new List<Reflector>();

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

    public int BasicWhiteReflectorStock { get; private set; }
    public int BasicRedReflectorStock { get; private set; }
    public int BasicBlueReflectorStock { get; private set; }
    public int BasicYellowReflectorStock { get; private set; }

    public int TranslucentWhiteReflectorStock { get; private set; }
    public int TranslucentRedReflectorStock { get; private set; }
    public int TranslucentBlueReflectorStock { get; private set; }
    public int TranslucentYellowReflectorStock { get; private set; }

    public int DoubleWayWhiteReflectorStock { get; private set; }
    public int DoubleWayRedReflectorStock { get; private set; }
    public int DoubleWayBlueReflectorStock { get; private set; }
    public int DoubleWayYellowReflectorStock { get; private set; }

    public int SplitWhiteReflectorStock { get; private set; }
    public int SplitRedReflectorStock { get; private set; }
    public int SplitBlueReflectorStock { get; private set; }
    public int SplitYellowReflectorStock { get; private set; }

    public int ThreeWayWhiteReflectorStock { get; private set; }
    public int ThreeWayRedReflectorStock { get; private set; }
    public int ThreeWayBlueReflectorStock { get; private set; }
    public int ThreeWayYellowReflectorStock { get; private set; }

    #endregion

    private Color tempColor;

    //Bool to check if game is paused. If paused, gameplay updates won't run
    public bool IsGamePaused { get; set; }

    #region MonoBehaviour
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        //If the game is paused, this stops the checks for touch or clicks to close reflector panels, else it runs the checks normally
        if (IsGamePaused == false)
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
        }
    }
    #endregion

    //Toggles the active state of the grid's 2D Colliders. If a grid is currently occupied by a reflector, the 2D Collider is deactivated.
    //If the grid is currently unoccupied, the 2D Collider is activated
    public void toggleGridColliders()
    {
        for (int i = 0; i < GameManager.Instance.gridOutlines.Count; ++i)
        {
            if (gridOutlines[i].GetComponent<Proto_Grid>().IsOccupied == true)
                GameManager.Instance.gridOutlines[i].GetComponentInParent<BoxCollider2D>().enabled = false;
            else if (gridOutlines[i].GetComponent<Proto_Grid>().IsOccupied == false)
                GameManager.Instance.gridOutlines[i].GetComponentInParent<BoxCollider2D>().enabled = true;
        }
    }

    //Turns off all reflector colliders except for the one the player is currently in control of    
    public void toggleReflectorColliders()
    {
        for (int i = 0; i < AllReflectorsInScene.Count; ++i)
        {
            AllReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = false;
        }


        for (int j = 0; j < AllReflectorsInScene.Count; ++j)
        {
            if (AllReflectorsInScene[j].GetComponent<Raycast>().IsHoldingDown == true)
            {
                AllReflectorsInScene[j].GetComponent<BoxCollider2D>().enabled = true;
                break;
            }
        }

        activationToggle_Reflector = true; //True: The reflectors have been toggled accordingly
    }

    //Resets all reflector colliders to their original state, meaning it will set all reflector 2D Colliders to be active
    public void resetReflectorColliders()
    {
        for (int i = 0; i < AllReflectorsInScene.Count; ++i)
        {
            AllReflectorsInScene[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        //Debug.LogWarning("Reset Reflector Colliders");

        activationToggle_Reflector = false;
    }

    //This function returns a reflector to its stock by returning the reflector to its appropriate reflector object pool
    //The function checks the reflector GameObject which is passed as an argument, for a specific attached script. The script helps to 
    //identify whether the reflector is a Basic, Translucent, Double Way, Split or Three Way type reflector
    public void returnReflectorToStock(GameObject reflector)
    {
        #region TEST CODE

        // if (reflector.transform.GetChild(0).GetComponent<ReflectorTranslucent>())
        // {
        //     if (reflector.name.Contains("White"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_White"].Enqueue(reflector);
        //         ReflectorStock_Translucent_White++;
        //     }
        //     else if (reflector.name.Contains("Red"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Red"].Enqueue(reflector);
        //         ReflectorStock_Translucent_Red++;
        //     }
        //     else if (reflector.name.Contains("Blue"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Blue"].Enqueue(reflector);
        //         ReflectorStock_Translucent_Blue++;
        //     }
        //     else if (reflector.name.Contains("Yellow"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Translucent_Yellow"].Enqueue(reflector);
        //         ReflectorStock_Translucent_Yellow++;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
        //     }

        //     //ReflectorStock_Translucent++;
        //     //ReflectorStock_Translucent_Text.text = ReflectorStock_Translucent.ToString();

        // }

        // else if (reflector.transform.GetChild(0).GetComponent<ReflectorDoubleWay>() != null)
        // {
        //     if (reflector.name.Contains("White"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_White"].Enqueue(reflector);
        //         ReflectorStock_DoubleWay_White++;
        //     }
        //     else if (reflector.name.Contains("Red"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Red"].Enqueue(reflector);
        //         ReflectorStock_DoubleWay_Red++;
        //     }
        //     else if (reflector.name.Contains("Blue"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Blue"].Enqueue(reflector);
        //         ReflectorStock_DoubleWay_Blue++;
        //     }
        //     else if (reflector.name.Contains("Yellow"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_DoubleWay_Yellow"].Enqueue(reflector);
        //         ReflectorStock_DoubleWay_Yellow++;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
        //     }

        //     //ReflectorStock_DoubleWay++;
        //     //ReflectorStock_DoubleWay_Text.text = ReflectorStock_DoubleWay.ToString();
        // }

        // else if (reflector.transform.GetChild(0).GetComponent<ReflectorThreeWay>() != null)
        // {
        //     if (reflector.name.Contains("White"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_White"].Enqueue(reflector);
        //         ReflectorStock_ThreeWay_White++;
        //     }
        //     else if (reflector.name.Contains("Red"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Red"].Enqueue(reflector);
        //         ReflectorStock_ThreeWay_Red++;
        //     }
        //     else if (reflector.name.Contains("Blue"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Blue"].Enqueue(reflector);
        //         ReflectorStock_ThreeWay_Blue++;
        //     }
        //     else if (reflector.name.Contains("Yellow"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_ThreeWay_Yellow"].Enqueue(reflector);
        //         ReflectorStock_ThreeWay_Yellow++;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
        //     }

        //     //ReflectorStock_ThreeWay++;
        //     //ReflectorStock_ThreeWay_Text.text = ReflectorStock_ThreeWay.ToString();
        // }

        // else if (reflector.transform.GetChild(0).GetComponent<Reflector>())
        // {
        //     if (reflector.name.Contains("White"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_White"].Enqueue(reflector);
        //         ReflectorStock_Basic_White++;
        //     }
        //     else if (reflector.name.Contains("Red"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Red"].Enqueue(reflector);
        //         ReflectorStock_Basic_Red++;
        //     }
        //     else if (reflector.name.Contains("Blue"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Blue"].Enqueue(reflector);
        //         ReflectorStock_Basic_Blue++;
        //     }
        //     else if (reflector.name.Contains("Yellow"))
        //     {
        //         ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary["ReflectorPool_Basic_Yellow"].Enqueue(reflector);
        //         ReflectorStock_Basic_Yellow++;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("No such reflector exists. Check to ensure the reflector contains the string you are checking for in the editor. Ensure it is case sensitive.");
        //     }

        //     //ReflectorStock_Basic++;
        //     //ReflectorStock_Basic_Text.text = ReflectorStock_Basic.ToString();

        //     Debug.Log("I WAS HERE");
        // }

        // reflector.transform.position = GameObject.FindGameObjectWithTag("InactivePooledReflectors").transform.position;
        // reflector.transform.rotation = Quaternion.identity;
        // reflector.SetActive(false);

        #endregion
    }

    //This function removes a reflector from the List, allReflectorsInScene. The reflector that is removed from the list is the reflector that
    //the player does not want to place in the grid.
    public void removeReflector(GameObject reflector)
    {
        for (int i = 0; i < AllReflectorsInScene.Count; ++i)
        {
            if (AllReflectorsInScene[i].GetInstanceID() == reflector.GetInstanceID())
            {
                AllReflectorsInScene.RemoveAt(i);
                Debug.LogWarning("Removed Reflector");
            }
        }
    }

    public void ResetGridAlpha()
    {
        foreach (var gridOutline in gridOutlines)
            gridOutline.ShowGrid(false);
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

    public void UpdateEndPointStatus(bool hitSuccessEndPoint)
    {
        numOfHitEndPoints++;

        if (hitSuccessEndPoint)
            numOfSuccessEndPoints++;

        if (numOfHitEndPoints == numOfEndPoints)
        {
            if (numOfSuccessEndPoints == numOfEndPoints)
            {
                // Show Stage Clear Panel
                uiHelperPanelClearStage.ExecuteUIHandlingAction(true);

                AllCorrectLasersHaveReached = true;
            }
            else
            {
                allLasersHaveReached = true;
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
        if (currentWindowTime > 0.0f)
        {
            if (AllCorrectLasersHaveReached == true)
            {
                TimerSuccessText.text = "WIN";
                beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                findAndReturnLasersToPool();
                GameplayInputManager.Instance.EnableInput = true;
                //Reset();
            }
            else if (allLasersHaveReached == true)
            {
                TimerSuccessText.text = "FAIL";
                beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                findAndReturnLasersToPool();
                GameplayInputManager.Instance.EnableInput = true;
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
            GameplayInputManager.Instance.EnableInput = true;
        }
    }

    //This function resets the variables related to the game state, so that a new laser can be fired again
    public void Reset()
    {
        currentWindowTime = maxWindowTime;

        foreach (GameObject endPoint in allEndPoints)
        {
            endPoint.GetComponent<LaserDestination>().Reset();
        }

        numOfHitEndPoints = 0;
        numOfSuccessEndPoints = 0;
        AllCorrectLasersHaveReached = false;
        allLasersHaveReached = false;
        beginCountDown = false;
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

        if (activeLasers.Length > 0)
        {
            foreach (GameObject laser in activeLasers)
            {
                //Destroy(laser);
                laser.GetComponent<Laser>().returnLaserToPool(laser);
                laser.SetActive(false);
            }
        }
    }

    public void UpdateReflectorStock(REFLECTOR_TYPE reflectorType, LASER_COLOR reflectorColor, int amount)
    {
        switch (reflectorType)
        {
            case REFLECTOR_TYPE.BASIC:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        BasicWhiteReflectorStock += amount;
                        break;
                    case LASER_COLOR.RED:
                        BasicRedReflectorStock += amount;
                        break;
                    case LASER_COLOR.BLUE:
                        BasicBlueReflectorStock += amount;
                        break;
                    case LASER_COLOR.YELLOW:
                        BasicYellowReflectorStock += amount;
                        break;
                }
                break;
            case REFLECTOR_TYPE.TRANSLUCENT:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        TranslucentWhiteReflectorStock += amount;
                        break;
                    case LASER_COLOR.RED:
                        TranslucentRedReflectorStock += amount;
                        break;
                    case LASER_COLOR.BLUE:
                        TranslucentBlueReflectorStock += amount;
                        break;
                    case LASER_COLOR.YELLOW:
                        TranslucentYellowReflectorStock += amount;
                        break;
                }
                break;
            case REFLECTOR_TYPE.DOUBLE_WAY:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        DoubleWayWhiteReflectorStock += amount;
                        break;
                    case LASER_COLOR.RED:
                        DoubleWayRedReflectorStock += amount;
                        break;
                    case LASER_COLOR.BLUE:
                        DoubleWayBlueReflectorStock += amount;
                        break;
                    case LASER_COLOR.YELLOW:
                        DoubleWayYellowReflectorStock += amount;
                        break;
                }
                break;
            case REFLECTOR_TYPE.THREE_WAY:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        ThreeWayWhiteReflectorStock += amount;
                        break;
                    case LASER_COLOR.RED:
                        ThreeWayRedReflectorStock += amount;
                        break;
                    case LASER_COLOR.BLUE:
                        ThreeWayBlueReflectorStock += amount;
                        break;
                    case LASER_COLOR.YELLOW:
                        ThreeWayYellowReflectorStock += amount;
                        break;
                }
                break;
        }
    }

    public bool IsReflectorInStock(REFLECTOR_TYPE reflectorType, LASER_COLOR reflectorColor)
    {
        switch (reflectorType)
        {
            case REFLECTOR_TYPE.BASIC:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        return BasicWhiteReflectorStock > 0;
                    case LASER_COLOR.RED:
                        return BasicRedReflectorStock > 0;
                    case LASER_COLOR.BLUE:
                        return BasicBlueReflectorStock > 0;
                    case LASER_COLOR.YELLOW:
                        return BasicYellowReflectorStock > 0;
                    default:
                        break;
                }
                break;
            case REFLECTOR_TYPE.TRANSLUCENT:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        return TranslucentWhiteReflectorStock > 0;
                    case LASER_COLOR.RED:
                        return TranslucentRedReflectorStock > 0;
                    case LASER_COLOR.BLUE:
                        return TranslucentBlueReflectorStock > 0;
                    case LASER_COLOR.YELLOW:
                        return TranslucentYellowReflectorStock > 0;
                    default:
                        break;
                }
                break;
            case REFLECTOR_TYPE.DOUBLE_WAY:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        return DoubleWayWhiteReflectorStock > 0;
                    case LASER_COLOR.RED:
                        return DoubleWayRedReflectorStock > 0;
                    case LASER_COLOR.BLUE:
                        return DoubleWayBlueReflectorStock > 0;
                    case LASER_COLOR.YELLOW:
                        return DoubleWayYellowReflectorStock > 0;
                    default:
                        break;
                }
                break;
            case REFLECTOR_TYPE.THREE_WAY:
                switch (reflectorColor)
                {
                    case LASER_COLOR.WHITE:
                        return ThreeWayWhiteReflectorStock > 0;
                    case LASER_COLOR.RED:
                        return ThreeWayRedReflectorStock > 0;
                    case LASER_COLOR.BLUE:
                        return ThreeWayBlueReflectorStock > 0;
                    case LASER_COLOR.YELLOW:
                        return ThreeWayYellowReflectorStock > 0;
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
        Proto_Grid[] gridOutlines = FindObjectsOfType<Proto_Grid>();
        for (int j = 0; j < gridOutlines.Length; ++j)
        {
            this.gridOutlines.Add(gridOutlines[j]);
        }
        ResetGridAlpha();
        #endregion

        #region Initialize End Points
        allEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        numOfEndPoints = allEndPoints.Length;
        #endregion

        #region Initialize Max Window Time
        currentWindowTime = maxWindowTime = mapDataHolder.timeLimit;
        TimerSuccessText.text = maxWindowTime.ToString("F2");
        #endregion

        #region Initialize Reflector Amount
        BasicWhiteReflectorStock = mapDataHolder.basicReflectorAmount;
        BasicRedReflectorStock = mapDataHolder.redBasicReflectorAmount;
        BasicYellowReflectorStock = mapDataHolder.yellowBasicReflectorAmount;
        BasicBlueReflectorStock = mapDataHolder.blueBasicReflectorAmount;
        TranslucentWhiteReflectorStock = mapDataHolder.translucentReflectorAmount;
        TranslucentRedReflectorStock = mapDataHolder.redTranslucentReflectorAmount;
        TranslucentYellowReflectorStock = mapDataHolder.yellowTranslucentReflectorAmount;
        TranslucentBlueReflectorStock = mapDataHolder.blueTranslucentReflectorAmount;
        DoubleWayWhiteReflectorStock = mapDataHolder.doubleWayReflectorAmount;
        DoubleWayRedReflectorStock = mapDataHolder.redDoubleWayReflectorAmount;
        DoubleWayYellowReflectorStock = mapDataHolder.yellowDoubleWayReflectorAmount;
        DoubleWayBlueReflectorStock = mapDataHolder.blueDoubleWayReflectorAmount;
        SplitWhiteReflectorStock = mapDataHolder.splitReflectorAmount;
        SplitRedReflectorStock = mapDataHolder.redSplitReflectorAmount;
        SplitYellowReflectorStock = mapDataHolder.yellowSplitReflectorAmount;
        SplitBlueReflectorStock = mapDataHolder.blueSplitReflectorAmount;
        ThreeWayWhiteReflectorStock = mapDataHolder.threeWayReflectorAmount;
        ThreeWayRedReflectorStock = mapDataHolder.redThreeWayReflectorAmount;
        ThreeWayYellowReflectorStock = mapDataHolder.yellowThreeWayReflectorAmount;
        ThreeWayBlueReflectorStock = mapDataHolder.blueThreeWayReflectorAmount;
        #endregion
    }
}
