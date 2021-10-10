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

    private float timeLeft;
    private float timeLimit;

    private float oneStarRequiredTime;
    private float twoStarRequiredTime;
    private float threeStarRequiredTime;

    private List<Laser> lasers = new List<Laser>();
    private List<LaserOrigin> laserOrigins = new List<LaserOrigin>();
    private List<LaserDestination> laserDestinations = new List<LaserDestination>();
    private List<Reflector> reflectors = new List<Reflector>();
    private List<Proto_Grid> gridOutlines = new List<Proto_Grid>();

    public bool IsGamePaused { get; private set; }
    public bool GameStarted { get; private set; }
    public bool GameClear { get; private set; }
    public bool CanStartGame
    {
        get
        {
            return !IsGamePaused && !GameStarted && !GameClear;
        }
    }

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
        }

        if (!IsGamePaused && GameStarted)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0.0f)
            {
                TimerSuccessText.text = "FAIL";
                GameStarted = false;
            }
            else
            {
                TimerSuccessText.text = timeLeft.ToString("F2");
            }
        }
    }
    #endregion

    public void AddLaser(Laser laser) { lasers.Add(laser); }
    public void RemoveLaser(Laser laser) { lasers.Remove(laser); }

    public void AddReflector(Reflector reflector) { reflectors.Add(reflector); }
    public void RemoveReflector(Reflector reflector) { reflectors.Remove(reflector); }

    public void Pause(bool pause)
    {
        IsGamePaused = pause;
        Time.timeScale = pause ? 0.0f : 1.0f;
    }

    public void EndGameCheck()
    {
        if (!GameStarted)
            return;

        bool allLaserDestinationsOn = true;
        foreach (var laserDestination in laserDestinations)
        {
            if (!laserDestination.IsOn)
            {
                allLaserDestinationsOn = false;
                break;
            }
        }

        if (allLaserDestinationsOn)
        {
            TimerSuccessText.text = "WIN";
            GameClear = true;
            GameStarted = false;
            GameplayUIManager.Instance.ShowGameClearPanel();
            if(timeLeft >= threeStarRequiredTime)
                Debug.Log("Three Stars");
            else if(timeLeft >= twoStarRequiredTime)
                Debug.Log("Two Stars");
            else
                Debug.Log("One Star");
            return;
        }

        if (lasers.Count <= 0)
        {
            TimerSuccessText.text = "FAIL";
            GameStarted = false;
            GameplayInputManager.Instance.EnableInput = true;
        }
    }

    // private void EndGameCheck()
    // {
    //     if(Lasers.Count <= 0 || timeLeft <= 0.0f)
    //     {
    //         TimerSuccessText.text = "FAIL";
    //         beginCountDown = false;
    //         GameplayInputManager.Instance.EnableInput = true;
    //     }



    //     // if (timeLeft > 0.0f)
    //     // {
    //     //     if (AllCorrectLasersHaveReached == true)
    //     //     {
    //     //         TimerSuccessText.text = "WIN";
    //     //         beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
    //     //         GameplayInputManager.Instance.EnableInput = true;
    //     //         //Reset();
    //     //     }
    //     //     else if (allLasersHaveReached == true)
    //     //     {
    //     //         TimerSuccessText.text = "FAIL";
    //     //         beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
    //     //         GameplayInputManager.Instance.EnableInput = true;
    //     //         //Reset();
    //     //     }
    //     //     else
    //     //     {
    //     //         timeLeft -= Time.smoothDeltaTime;
    //     //         TimerSuccessText.text = timeLeft.ToString("F2");
    //     //     }
    //     // }
    //     // else if (timeLeft <= 0.0f)
    //     // {
    //     //     TimerSuccessText.text = "FAIL";
    //     //     beginCountDown = false; //Only when beginCountdown is false, we can reset the game by clicking on any of the starting points again
    //     //                             //Reset();
    //     //     GameplayInputManager.Instance.EnableInput = true;
    //     // }
    // }

    public void StartGame()
    {
        GameStarted = true;
        foreach (var laserOrigin in laserOrigins)
            laserOrigin.Fire();
        GameplayInputManager.Instance.EnableInput = false;
    }

    public void Reset()
    {
        timeLeft = timeLimit;

        foreach (var laserDestination in laserDestinations)
            laserDestination.Reset();

        GameStarted = false;
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
        Proto_Grid[] gridOutlines = FindObjectsOfType<Proto_Grid>();
        for (int i = 0; i < gridOutlines.Length; i++)
        {
            this.gridOutlines.Add(gridOutlines[i]);
            gridOutlines[i].ShowGrid(false);
        }

        laserOrigins = new List<LaserOrigin>(FindObjectsOfType<LaserOrigin>());

        laserDestinations = new List<LaserDestination>(FindObjectsOfType<LaserDestination>());

        timeLeft = timeLimit = mapDataHolder.timeLimit;
        TimerSuccessText.text = timeLimit.ToString("F2");

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

        oneStarRequiredTime = mapDataHolder.oneStarRequiredTime;
        twoStarRequiredTime = mapDataHolder.twoStarRequiredTime;
        threeStarRequiredTime = mapDataHolder.threeStarRequiredTime;
    }
}
