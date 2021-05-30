using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorColor_UIButton : MonoBehaviour
{
    #region Old Implementation
    // public string reflectorTypeTag; //Determines the the type of reflector
    // public string reflectorColorTag; //Determines the color of the reflector
    // private string reflectorPoolTag = System.String.Empty;
    // private bool reflectorColorInStock = false;
    // public GameObject returnedReflector = null;
    #endregion

    public Image Image { get; private set; }
    public REFLECTOR_TYPE ReflectorType { private get; set; }
    public LASER_COLOR ReflectorColor { private get; set; }

    private Vector3 point;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }

    void Start()
    {
        #region Initializing Event Triggers

        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryOnPointerDown = new EventTrigger.Entry();
        //EventTrigger.Entry entryOnPointerUp = new EventTrigger.Entry();

        entryOnPointerDown.eventID = EventTriggerType.PointerDown;
        //entryOnPointerUp.eventID = EventTriggerType.PointerUp;

        entryOnPointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        //entryOnPointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });

        eventTrigger.triggers.Add(entryOnPointerDown);
        //eventTrigger.triggers.Add(entryOnPointerUp);

        #endregion
    }

    //This function 'pulls' the appropriate reflector from its object pool and positions it at the player's finger, where it is ready for the player
    //to drag & drop the reflector. Also, once the reflector is 'pulled' from its object pool, this function will also update the 
    //appropriate reflector stock
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        #region Touch Input

        if (GameManager.Instance.DebugMode_PC == false)
        {
            if (Input.touchCount == 1)//Used to be Input.touchCount <= 1
            {
                Touch touch = Input.GetTouch(0);

                #region Old Implementation
                // reflectorColorInStock = GameManager.Instance.CheckReflectorColorStock(reflectorTypeTag, reflectorColorTag);

                // if (reflectorColorInStock == true)
                // {
                //     reflectorPoolTag = GameManager.Instance.setSelectedColorReflector(reflectorTypeTag, reflectorColorTag);

                //     // returnedReflector = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Dequeue();

                //     GameManager.Instance.allReflectorsInScene.Add(returnedReflector);

                //     returnedReflector.SetActive(true);
                //     returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
                //     returnedReflector.GetComponent<Raycast>().timeUntilHold = 0.1f;
                //     returnedReflector.GetComponent<Raycast>().isHoldingDownAccessor = true;

                //     point = Camera.main.ScreenToWorldPoint(touch.position);
                //     point = new Vector3(point.x, point.y, 0.0f);
                //     returnedReflector.transform.position = point;

                //     switch (reflectorTypeTag)
                //     {
                //         case "Basic":
                //             switch (reflectorColorTag)
                //             {
                //                 case "White":
                //                     GameManager.Instance.ReflectorStockBasicWhite_Accessor--;
                //                     break;

                //                 case "Red":
                //                     GameManager.Instance.ReflectorStockBasicRed_Accessor--;
                //                     break;

                //                 case "Blue":
                //                     GameManager.Instance.ReflectorStockBasicBlue_Accessor--;
                //                     break;

                //                 case "Yellow":
                //                     GameManager.Instance.ReflectorStockBasicYellow_Accessor--;
                //                     break;
                //             }
                //             break;

                //         case "Translucent":
                //             switch (reflectorColorTag)
                //             {
                //                 case "White":
                //                     GameManager.Instance.ReflectorStockTranslucentWhite_Accessor--;
                //                     break;

                //                 case "Red":
                //                     GameManager.Instance.ReflectorStockTranslucentRed_Accessor--;
                //                     break;

                //                 case "Blue":
                //                     GameManager.Instance.ReflectorStockTranslucentBlue_Accessor--;
                //                     break;

                //                 case "Yellow":
                //                     GameManager.Instance.ReflectorStockTranslucentYellow_Accessor--;
                //                     break;
                //             }
                //             break;

                //         case "DoubleWay":
                //             switch (reflectorColorTag)
                //             {
                //                 case "White":
                //                     GameManager.Instance.ReflectorStockDoubleWayWhite_Accessor--;
                //                     break;

                //                 case "Red":
                //                     GameManager.Instance.ReflectorStockDoubleWayRed_Accessor--;
                //                     break;

                //                 case "Blue":
                //                     GameManager.Instance.ReflectorStockDoubleWayBlue_Accessor--;
                //                     break;

                //                 case "Yellow":
                //                     GameManager.Instance.ReflectorStockDoubleWayYellow_Accessor--;
                //                     break;
                //             };
                //             break;

                //         case "Split":
                //             switch (reflectorColorTag)
                //             {
                //                 case "White":
                //                     GameManager.Instance.ReflectorStockSplitWhite_Accessor--;
                //                     break;

                //                 case "Red":
                //                     GameManager.Instance.ReflectorStockSplitRed_Accessor--;
                //                     break;

                //                 case "Blue":
                //                     GameManager.Instance.ReflectorStockSplitBlue_Accessor--;
                //                     break;

                //                 case "Yellow":
                //                     GameManager.Instance.ReflectorStockSplitYellow_Accessor--;
                //                     break;
                //             }
                //             break;

                //         case "ThreeWay":
                //             switch (reflectorColorTag)
                //             {
                //                 case "White":
                //                     GameManager.Instance.ReflectorStockThreeWayWhite_Accessor--;
                //                     break;

                //                 case "Red":
                //                     GameManager.Instance.ReflectorStockThreeWayRed_Accessor--;
                //                     break;

                //                 case "Blue":
                //                     GameManager.Instance.ReflectorStockThreeWayBlue_Accessor--;
                //                     break;

                //                 case "Yellow":
                //                     GameManager.Instance.ReflectorStockThreeWayYellow_Accessor--;
                //                     break;
                //             }
                //             break;

                //         default:
                //             Debug.LogWarning("No such reflectorTypeTag exists. Check whether the ReflectorTypeTag is spelled properly in editor");
                //             break;
                //     }

                //     GameManager.Instance.reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);

                //     //The below 2 lines will be executed in an Animation Event
                //     //GameManager.gameManagerInstance.reflectorColorsPanel.SetActive(false);
                //     //GameManager.gameManagerInstance.isReflectorColorPanelActive = false;
                // }
                // else if (reflectorColorInStock == false)
                // {
                //     Debug.LogWarning("Reflector Color not in stock");
                // }
                #endregion
            }
        }

        #endregion

#if UNITY_EDITOR

        #region Mouse Input

        if (GameManager.Instance.DebugMode_PC == true)
        {
            Reflector reflector = null;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GameManager.Instance.IsReflectorInStock(ReflectorType, ReflectorColor))
            {
                switch (ReflectorType)
                {
                    case REFLECTOR_TYPE.BASIC:
                        reflector = ObjectPooler.Instance.PopOrCreate(GameManager.Instance.GetBasicReflectorPrefab, mousePos, Quaternion.identity);
                        reflector.Initialization(ReflectorColor);
                        reflector.RefreshReflectorColor();
                        switch (ReflectorColor)
                        {
                            case LASER_COLOR.WHITE:
                                GameManager.Instance.BasicWhiteReflectorStock--;
                                break;
                            case LASER_COLOR.RED:
                                GameManager.Instance.BasicRedReflectorStock--;
                                break;
                            case LASER_COLOR.BLUE:
                                GameManager.Instance.BasicBlueReflectorStock--;
                                break;
                            case LASER_COLOR.YELLOW:
                                GameManager.Instance.BasicYellowReflectorStock--;
                                break;
                        }
                        break;
                    case REFLECTOR_TYPE.TRANSLUCENT:
                        reflector = ObjectPooler.Instance.PopOrCreate(GameManager.Instance.GetReflectorTranslucentPrefab, mousePos, Quaternion.identity);
                        reflector.Initialization(ReflectorColor);
                        reflector.RefreshReflectorColor();
                        switch (ReflectorColor)
                        {
                            case LASER_COLOR.WHITE:
                                GameManager.Instance.TranslucentWhiteReflectorStock--;
                                break;
                            case LASER_COLOR.RED:
                                GameManager.Instance.TranslucentRedReflectorStock--;
                                break;
                            case LASER_COLOR.BLUE:
                                GameManager.Instance.TranslucentBlueReflectorStock--;
                                break;
                            case LASER_COLOR.YELLOW:
                                GameManager.Instance.TranslucentYellowReflectorStock--;
                                break;
                        }
                        break;
                    case REFLECTOR_TYPE.DOUBLE_WAY:
                        reflector = ObjectPooler.Instance.PopOrCreate(GameManager.Instance.GetReflectorDoubleWayPrefab, mousePos, Quaternion.identity);
                        reflector.Initialization(ReflectorColor);
                        reflector.RefreshReflectorColor();
                        switch (ReflectorColor)
                        {
                            case LASER_COLOR.WHITE:
                                GameManager.Instance.DoubleWayWhiteReflectorStock--;
                                break;
                            case LASER_COLOR.RED:
                                GameManager.Instance.DoubleWayRedReflectorStock--;
                                break;
                            case LASER_COLOR.BLUE:
                                GameManager.Instance.DoubleWayBlueReflectorStock--;
                                break;
                            case LASER_COLOR.YELLOW:
                                GameManager.Instance.DoubleWayYellowReflectorStock--;
                                break;
                        }
                        break;
                    case REFLECTOR_TYPE.THREE_WAY:
                        reflector = ObjectPooler.Instance.PopOrCreate(GameManager.Instance.GetReflectorThreeWayPrefab, mousePos, Quaternion.identity);
                        reflector.Initialization(ReflectorColor);
                        reflector.RefreshReflectorColor();
                        switch (ReflectorColor)
                        {
                            case LASER_COLOR.WHITE:
                                GameManager.Instance.ThreeWayWhiteReflectorStock--;
                                break;
                            case LASER_COLOR.RED:
                                GameManager.Instance.ThreeWayRedReflectorStock--;
                                break;
                            case LASER_COLOR.BLUE:
                                GameManager.Instance.ThreeWayBlueReflectorStock--;
                                break;
                            case LASER_COLOR.YELLOW:
                                GameManager.Instance.ThreeWayYellowReflectorStock--;
                                break;
                        }
                        break;
                    case REFLECTOR_TYPE.DEFAULT:
                        break;
                }

                GameManager.Instance.AllReflectorsInScene.Add(reflector);
                GameplayInputManager.Instance.SelectReflector(reflector);

                // reflector.RaycastComponent.timeUntilHold = 0.1f;
                // reflector.RaycastComponent.IsHoldingDown = true;
                // reflector.RaycastComponent.currentMousePos = mousePos;
                // reflector.RaycastComponent.mouseIsDown = true;
                // reflector.RaycastComponent.reflectorAttached = true;
                // GameManager.Instance.toggleReflectorColliders(); //Ensures reflectors don't overlap when taking a reflector from the pool 

                GameManager.Instance.reflectorColorsPanel.ReflectorColorPanelAnimator.SetBool("ReflectorColorPanelDisplayed", false);
            }

            #region Old Implementation
            // #region TEST CODE

            // //Check if enough reflector for the specific type and color by passing in the 2 arguments, reflectorTypeTag & reflectorColorTag
            // reflectorColorInStock = GameManager.gameManagerInstance.CheckReflectorColorStock(reflectorTypeTag, reflectorColorTag);

            // if(reflectorColorInStock == true)
            // {
            //     reflectorPoolTag = GameManager.gameManagerInstance.setSelectedColorReflector(reflectorTypeTag, reflectorColorTag);

            //     returnedReflector = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Dequeue();

            //     GameManager.gameManagerInstance.allReflectorsInScene.Add(returnedReflector);

            //     returnedReflector.SetActive(true);
            //     returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
            //     returnedReflector.GetComponent<Raycast>().timeUntilHold = 0.1f;
            //     returnedReflector.GetComponent<Raycast>().isHoldingDownAccessor = true;

            //     Vector2 pointVec2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //     returnedReflector.transform.position = pointVec2;
            //     returnedReflector.GetComponent<Raycast>().currentMousePos = pointVec2;
            //     returnedReflector.GetComponent<Raycast>().mouseIsDown = true;

            //     returnedReflector.GetComponent<Raycast>().reflectorAttached = true;
            //     GameManager.gameManagerInstance.toggleReflectorColliders(); //Ensures reflectors don't overlap when taking a reflector from the pool 

            //     switch (reflectorTypeTag)
            //     {
            //         case "Basic":
            //             switch (reflectorColorTag)
            //             {
            //                 case "White":
            //                     GameManager.gameManagerInstance.ReflectorStockBasicWhite_Accessor--;
            //                     break;

            //                 case "Red":
            //                     GameManager.gameManagerInstance.ReflectorStockBasicRed_Accessor--;
            //                     break;

            //                 case "Blue":
            //                     GameManager.gameManagerInstance.ReflectorStockBasicBlue_Accessor--;
            //                     break;

            //                 case "Yellow":
            //                     GameManager.gameManagerInstance.ReflectorStockBasicYellow_Accessor--;
            //                     break;
            //             }
            //             break;

            //         case "Translucent":
            //             switch (reflectorColorTag)
            //             {
            //                 case "White":
            //                     GameManager.gameManagerInstance.ReflectorStockTranslucentWhite_Accessor--;
            //                     break;

            //                 case "Red":
            //                     GameManager.gameManagerInstance.ReflectorStockTranslucentRed_Accessor--;
            //                     break;

            //                 case "Blue":
            //                     GameManager.gameManagerInstance.ReflectorStockTranslucentBlue_Accessor--;
            //                     break;

            //                 case "Yellow":
            //                     GameManager.gameManagerInstance.ReflectorStockTranslucentYellow_Accessor--;
            //                     break;
            //             }
            //             break;

            //         case "DoubleWay":
            //             switch (reflectorColorTag)
            //             {
            //                 case "White":
            //                     GameManager.gameManagerInstance.ReflectorStockDoubleWayWhite_Accessor--;
            //                     break;

            //                 case "Red":
            //                     GameManager.gameManagerInstance.ReflectorStockDoubleWayRed_Accessor--;
            //                     break;

            //                 case "Blue":
            //                     GameManager.gameManagerInstance.ReflectorStockDoubleWayBlue_Accessor--;
            //                     break;

            //                 case "Yellow":
            //                     GameManager.gameManagerInstance.ReflectorStockDoubleWayYellow_Accessor--;
            //                     break;
            //             };
            //             break;

            //         case "Split":
            //             switch (reflectorColorTag)
            //             {
            //                 case "White":
            //                     GameManager.gameManagerInstance.ReflectorStockSplitWhite_Accessor--;
            //                     break;

            //                 case "Red":
            //                     GameManager.gameManagerInstance.ReflectorStockSplitRed_Accessor--;
            //                     break;

            //                 case "Blue":
            //                     GameManager.gameManagerInstance.ReflectorStockSplitBlue_Accessor--;
            //                     break;

            //                 case "Yellow":
            //                     GameManager.gameManagerInstance.ReflectorStockSplitYellow_Accessor--;
            //                     break;
            //             }
            //             break;

            //         case "ThreeWay":
            //             switch (reflectorColorTag)
            //             {
            //                 case "White":
            //                     GameManager.gameManagerInstance.ReflectorStockThreeWayWhite_Accessor--;
            //                     break;

            //                 case "Red":
            //                     GameManager.gameManagerInstance.ReflectorStockThreeWayRed_Accessor--;
            //                     break;

            //                 case "Blue":
            //                     GameManager.gameManagerInstance.ReflectorStockThreeWayBlue_Accessor--;
            //                     break;

            //                 case "Yellow":
            //                     GameManager.gameManagerInstance.ReflectorStockThreeWayYellow_Accessor--;
            //                     break;
            //             }
            //             break;

            //         default:
            //             Debug.LogWarning("No such reflectorTypeTag exists. Check whether the ReflectorTypeTag is spelled properly in editor");
            //             break;
            //     }

            //     GameManager.gameManagerInstance.reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", false);

            //     //The 2 lines of code below will be executed as an Animation Event
            //     //GameManager.gameManagerInstance.reflectorColorsPanel.SetActive(false);
            //     //GameManager.gameManagerInstance.isReflectorColorPanelActive = false;
            // }
            // else if(reflectorColorInStock == false)
            // {
            //     Debug.LogWarning("Reflector Color not in stock");
            // }

            // #endregion
            #endregion
        }

        #endregion

#endif

    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {

    }

}
