using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [Header("PREFABS")]
    [SerializeField] protected Reflector reflectorPrefab;
    [SerializeField] protected ReflectorTranslucent reflectorTranslucentPrefab;
    [SerializeField] protected ReflectorDoubleWay reflectorDoubleWayPrefab;
    [SerializeField] protected ReflectorThreeWay reflectorThreeWayPrefab;

    [HideInInspector]
    public bool activationToggle_Grid = false;
    [HideInInspector]
    public bool activationToggle_Reflector = false;

    [BoxGroup("TextMeshPro UI")]
    public TextMeshProUGUI TimerSuccessText;

    [BoxGroup("Reflector Color Panel Variables")]
    [SerializeField] protected ReflectorColorPanel reflectorColorPanel;

    public ReflectorColorPanel ReflectorColorPanel { get => reflectorColorPanel; }
    public Reflector GetBasicReflectorPrefab { get { return reflectorPrefab; } }
    public ReflectorTranslucent GetReflectorTranslucentPrefab { get { return reflectorTranslucentPrefab; } }
    public ReflectorDoubleWay GetReflectorDoubleWayPrefab { get { return reflectorDoubleWayPrefab; } }
    public ReflectorThreeWay GetReflectorThreeWayPrefab { get { return reflectorThreeWayPrefab; } }

    public List<Laser> Lasers { get; private set; } = new List<Laser>();
    public List<Reflector> Reflectors { get; private set; } = new List<Reflector>();
    public List<Proto_Grid> GridOutlines { get; private set; } = new List<Proto_Grid>();

    private float elapsedTime;
    private float timeLimit;

    private GameObject[] allEndPoints;
    private int numOfEndPoints = 0;
    private int numOfSuccessEndPoints = 0;
    private int numOfHitEndPoints = 0;
    private bool allLasersHaveReached = false;
    public bool AllCorrectLasersHaveReached { get; private set; } = false;

    [HideInInspector]
    public bool beginCountDown = false;


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

    // Basic Reflector Stocks
    public int BasicWhiteReflectorStock { get; private set; }
    public int BasicRedReflectorStock { get; private set; }
    public int BasicBlueReflectorStock { get; private set; }
    public int BasicYellowReflectorStock { get; private set; }

    // Translucent Reflector Stocks
    public int TranslucentWhiteReflectorStock { get; private set; }
    public int TranslucentRedReflectorStock { get; private set; }
    public int TranslucentBlueReflectorStock { get; private set; }
    public int TranslucentYellowReflectorStock { get; private set; }

    // DOuble Way Reflector Stocks
    public int DoubleWayWhiteReflectorStock { get; private set; }
    public int DoubleWayRedReflectorStock { get; private set; }
    public int DoubleWayBlueReflectorStock { get; private set; }
    public int DoubleWayYellowReflectorStock { get; private set; }

    // Split Reflector Stocks
    public int SplitWhiteReflectorStock { get; private set; }
    public int SplitRedReflectorStock { get; private set; }
    public int SplitBlueReflectorStock { get; private set; }
    public int SplitYellowReflectorStock { get; private set; }

    // Three Way Reflector Stocks
    public int ThreeWayWhiteReflectorStock { get; private set; }
    public int ThreeWayRedReflectorStock { get; private set; }
    public int ThreeWayBlueReflectorStock { get; private set; }
    public int ThreeWayYellowReflectorStock { get; private set; }

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
        if (IsGamePaused == false)
        {
            if (dissolveFade < 1.0f)
            {
                dissolveFade += Time.deltaTime;
                dissolveMaterial.SetFloat("_Fade", dissolveFade);
            }

            if (beginCountDown == true)
            {
                checkTimingWindowForLaser();
            }
        }
    }
    #endregion

    public void Pause(bool pause)
    {
        IsGamePaused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
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
                GameplayUIManager.Instance.ShowGameClearPanel();
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
        if (elapsedTime > 0.0f)
        {
            if (AllCorrectLasersHaveReached == true)
            {
                TimerSuccessText.text = "WIN";
                beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                GameplayInputManager.Instance.EnableInput = true;
                //Reset();
            }
            else if (allLasersHaveReached == true)
            {
                TimerSuccessText.text = "FAIL";
                beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                GameplayInputManager.Instance.EnableInput = true;
                //Reset();
            }
            else
            {
                elapsedTime -= Time.smoothDeltaTime;
                TimerSuccessText.text = elapsedTime.ToString("F2");
            }
        }
        else if (elapsedTime <= 0.0f)
        {
            TimerSuccessText.text = "FAIL";
            beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
                                    //Reset();
            GameplayInputManager.Instance.EnableInput = true;
        }
    }

    //This function resets the variables related to the game state, so that a new laser can be fired again
    public void Reset()
    {
        elapsedTime = timeLimit;

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

    public void EndGameCheck()
    {
        if (Lasers.Count <= 0)
            elapsedTime = 0.0f;
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
        for (int i = 0; i < gridOutlines.Length; i++)
        {
            this.GridOutlines.Add(gridOutlines[i]);
            gridOutlines[i].ShowGrid(false);
        }
        #endregion

        #region Initialize End Points
        allEndPoints = GameObject.FindGameObjectsWithTag("EndPoint");
        numOfEndPoints = allEndPoints.Length;
        #endregion

        #region Initialize Max Window Time
        elapsedTime = timeLimit = mapDataHolder.timeLimit;
        TimerSuccessText.text = timeLimit.ToString("F2");
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
