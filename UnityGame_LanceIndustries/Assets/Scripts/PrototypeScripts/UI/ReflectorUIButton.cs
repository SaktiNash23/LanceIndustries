using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorUIButton : MonoBehaviour
{
    #region Old Implementation
    // public string buttonTypeTag; //Stores a string that identifies the type of button that is pressed. This variable is used to determine the type of reflectors to display for the player to choose from
    // private bool isReflectorInStock; //To check whether reflectors of a specific type are in stock or not
    #endregion

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

    //When the button is pressed / clicked on, the game check if the reflector of the specific type is in stock. Then it activates or deactivates
    //the reflector color panel based on the result
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        #region TEST CODE

        // GameManager.Instance.updateReflectorColorStocks(buttonTypeTag);
        GameManager.Instance.UpdateReflectorStockUI(reflectorType);
        ActivateReflectorColorUIPanel();

        #endregion
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

        GameManager.Instance.reflectorColorsPanel.gameObject.SetActive(true);
        GameManager.Instance.isReflectorColorPanelActive = true;
        GameManager.Instance.reflectorColorsPanel.ReflectorColorPanelAnimator.SetBool("ReflectorColorPanelDisplayed", true);
    }

    #region Old Implementation
    //This function updates the buttons in Reflector Color Panel UI with the appropriate images and changes the tag of the buttons as well
    //
    //Ex: If the button for Reflector type Basic is pressed, the string tag from that button is passed to this function as an argument. Based
    // on the argument passed, the Reflector Color Buttons are updated with the appropriate sprites and tags
    // public void activateReflectorColorUIPanel()
    // {
    //     switch (buttonTypeTag)
    //     {
    //         case "ReflectorButton_Basic":
    //             for(int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Yellow;
    //                         break;
    //                 }

    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Basic;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Basic";

    //             }
    //             break;

    //         case "ReflectorButton_Translucent":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Yellow;
    //                         break;
    //                 }


    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Translucent;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Translucent";
    //             }
    //             break;

    //         case "ReflectorButton_DoubleWay":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Yellow;
    //                         break;
    //                 }

    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_DoubleWay;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "DoubleWay";
    //             }
    //             break;

    //         case "ReflectorButton_Split":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Split;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Split";
    //             }
    //             break;

    //         case "ReflectorButton_ThreeWay":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Yellow;
    //                         break;
    //                 }


    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_ThreeWay;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "ThreeWay";
    //             }
    //             break;
    //     }

    //     GameManager.Instance.reflectorColorsPanel.SetActive(true);
    //     GameManager.Instance.isReflectorColorPanelActive = true;
    //     GameManager.Instance.reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", true);
    // }

    // public void ActivateReflectorColorUIPanel()
    // {
    //     switch (buttonTypeTag)
    //     {
    //         case "ReflectorButton_Basic":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Basic_Yellow;
    //                         break;
    //                 }

    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Basic;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Basic";

    //             }
    //             break;

    //         case "ReflectorButton_Translucent":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Translucent_Yellow;
    //                         break;
    //                 }


    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Translucent;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Translucent";
    //             }
    //             break;

    //         case "ReflectorButton_DoubleWay":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_DoubleWay_Yellow;
    //                         break;
    //                 }

    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_DoubleWay;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "DoubleWay";
    //             }
    //             break;

    //         case "ReflectorButton_Split":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_Split;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Split";
    //             }
    //             break;

    //         case "ReflectorButton_ThreeWay":
    //             for (int i = 0; i < GameManager.Instance.allReflectorColorButtons.Count; ++i)
    //             {
    //                 switch (i)
    //                 {
    //                     case 0:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_White;
    //                         break;

    //                     case 1:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Red;
    //                         break;

    //                     case 2:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Blue;
    //                         break;

    //                     case 3:
    //                         GameManager.Instance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.Instance.reflectorSprite_ThreeWay_Yellow;
    //                         break;
    //                 }


    //                 //GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_ThreeWay;
    //                 GameManager.Instance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "ThreeWay";
    //             }
    //             break;
    //     }

    //     GameManager.Instance.reflectorColorsPanel.SetActive(true);
    //     GameManager.Instance.isReflectorColorPanelActive = true;
    //     GameManager.Instance.reflectorColorsPanel.GetComponent<Animator>().SetBool("ReflectorColorPanelDisplayed", true);
    // }
    #endregion
}
