using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorColor_UIButton : MonoBehaviour
{
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

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (GameManager.Instance.DebugMode_PC == false)
        {
            if (Input.touchCount == 1)//Used to be Input.touchCount <= 1
            {
                Touch touch = Input.GetTouch(0);

            }
        }

#if UNITY_EDITOR
        if (GameManager.Instance.DebugMode_PC == true)
        {
            Reflector reflector = null;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!GameplayInputManager.Instance.SelectingReflector && GameManager.Instance.IsReflectorInStock(ReflectorType, ReflectorColor))
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
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.WHITE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.WHITE, -1);
                                break;
                            case LASER_COLOR.RED:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.RED, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.RED, -1);
                                break;
                            case LASER_COLOR.BLUE:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.BLUE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.BLUE, -1);
                                break;
                            case LASER_COLOR.YELLOW:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.YELLOW, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.BASIC, LASER_COLOR.YELLOW, -1);
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
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.WHITE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.WHITE, -1);
                                break;
                            case LASER_COLOR.RED:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.RED, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.RED, -1);
                                break;
                            case LASER_COLOR.BLUE:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.BLUE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.BLUE, -1);
                                break;
                            case LASER_COLOR.YELLOW:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.YELLOW, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.TRANSLUCENT, LASER_COLOR.YELLOW, -1);
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
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.WHITE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.WHITE, -1);
                                break;
                            case LASER_COLOR.RED:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.RED, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.RED, -1);
                                break;
                            case LASER_COLOR.BLUE:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.BLUE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.BLUE, -1);
                                break;
                            case LASER_COLOR.YELLOW:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.YELLOW, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.DOUBLE_WAY, LASER_COLOR.YELLOW, -1);
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
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.WHITE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.WHITE, -1);
                                break;
                            case LASER_COLOR.RED:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.RED, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.RED, -1);
                                break;
                            case LASER_COLOR.BLUE:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.BLUE, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.BLUE, -1);
                                break;
                            case LASER_COLOR.YELLOW:
                                reflector.onPush += () => GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.YELLOW, 1);
                                GameManager.Instance.UpdateReflectorStock(REFLECTOR_TYPE.THREE_WAY, LASER_COLOR.YELLOW, -1);
                                break;
                        }
                        break;
                    case REFLECTOR_TYPE.DEFAULT:
                        break;
                }

                GameManager.Instance.AllReflectorsInScene.Add(reflector);
                GameplayInputManager.Instance.SelectReflector(reflector);

                GameManager.Instance.ReflectorColorPanel.EnablePanel(false);
            }
        }
#endif
    }
}
