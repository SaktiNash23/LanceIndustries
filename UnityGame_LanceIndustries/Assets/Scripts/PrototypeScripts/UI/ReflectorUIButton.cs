using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorUIButton : MonoBehaviour
{
    [SerializeField] protected REFLECTOR_TYPE reflectorType;

    // Start is called before the first frame update
    private void Start()
    {
        #region Initializing Event Triggers

        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryOnPointerUp = new EventTrigger.Entry();

        entryOnPointerUp.eventID = EventTriggerType.PointerUp;
        entryOnPointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });

        eventTrigger.triggers.Add(entryOnPointerUp);

        #endregion
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        GameplayUIManager.Instance.RefreshReflectorStockUIs(reflectorType);
        ActivateReflectorColorUIPanel();
    }

    public void ActivateReflectorColorUIPanel()
    {
        switch (reflectorType)
        {
            case REFLECTOR_TYPE.BASIC:
                for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Basic_White;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.WHITE;
                            break;
                        case 1:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Basic_Red;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.RED;
                            break;
                        case 2:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Basic_Blue;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.BLUE;
                            break;
                        case 3:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Basic_Yellow;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.YELLOW;
                            break;
                    }

                    GameManager.Instance.allReflectorColorButtons[i].ReflectorType = REFLECTOR_TYPE.BASIC;
                }
                break;

            case REFLECTOR_TYPE.TRANSLUCENT:
                for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Translucent_White;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.WHITE;
                            break;
                        case 1:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Translucent_Red;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.RED;
                            break;
                        case 2:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Translucent_Blue;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.BLUE;
                            break;
                        case 3:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_Translucent_Yellow;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.YELLOW;
                            break;
                    }

                    GameManager.Instance.allReflectorColorButtons[i].ReflectorType = REFLECTOR_TYPE.TRANSLUCENT;
                }
                break;

            case REFLECTOR_TYPE.DOUBLE_WAY:
                for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_DoubleWay_White;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.WHITE;
                            break;
                        case 1:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_DoubleWay_Red;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.RED;
                            break;
                        case 2:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_DoubleWay_Blue;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.BLUE;
                            break;
                        case 3:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_DoubleWay_Yellow;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.YELLOW;
                            break;
                    }

                    GameManager.Instance.allReflectorColorButtons[i].ReflectorType = REFLECTOR_TYPE.DOUBLE_WAY;
                }
                break;

            case REFLECTOR_TYPE.THREE_WAY:
                for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_ThreeWay_White;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.WHITE;
                            break;
                        case 1:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_ThreeWay_Red;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.RED;
                            break;
                        case 2:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_ThreeWay_Blue;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.BLUE;
                            break;
                        case 3:
                            GameManager.Instance.allReflectorColorButtons[i].Image.sprite = GameManager.Instance.reflectorSprite_ThreeWay_Yellow;
                            GameManager.Instance.allReflectorColorButtons[i].ReflectorColor = LASER_COLOR.YELLOW;
                            break;
                    }

                    GameManager.Instance.allReflectorColorButtons[i].ReflectorType = REFLECTOR_TYPE.THREE_WAY;
                }
                break;
        }

        GameManager.Instance.ReflectorColorPanel.EnablePanel(true);
    }
}
