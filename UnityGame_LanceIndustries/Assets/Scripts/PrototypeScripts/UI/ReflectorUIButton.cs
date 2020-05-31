using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorUIButton : MonoBehaviour
{
    public string buttonTypeTag; //Stores a string that identifies the type of button that is pressed. This variable is used to determine the type of reflectors to display for the player to choose from
    private bool isReflectorInStock; //To check whether reflectors of a specific type are in stock or not

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
        isReflectorInStock = GameManager.gameManagerInstance.checkReflectorStockAvailability(buttonTypeTag);

        if (isReflectorInStock)
        {
            activateReflectorColorUIPanel();                 
        }
        else if (!isReflectorInStock)
        {
            Debug.LogWarning("No reflector in stock");
        }
    }

    //This function updates the buttons in Reflector Color Panel UI with the appropriate images and changes the tag of the buttons as well
    //
    //Ex: If the button for Reflector type Basic is pressed, the string tag from that button is passed to this function as an argument. Based
    // on the argument passed, the Reflector Color Buttons are updated with the appropriate sprites and tags
    public void activateReflectorColorUIPanel()
    {
        switch (buttonTypeTag)
        {
            case "ReflectorButton_Basic":
                for(int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Basic;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Basic";
                }
                break;

            case "ReflectorButton_Translucent":
                for (int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Translucent;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Translucent";
                }
                break;

            case "ReflectorButton_DoubleWay":
                for (int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_DoubleWay;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "DoubleWay";
                }
                break;

            case "ReflectorButton_Split":
                for (int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_Split;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Split";
                }
                break;

            case "ReflectorButton_ThreeWay":
                for (int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<Image>().sprite = GameManager.gameManagerInstance.reflectorSprite_ThreeWay;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "ThreeWay";
                }
                break;
        }

        GameManager.gameManagerInstance.reflectorColorsPanel.SetActive(true);
        GameManager.gameManagerInstance.isReflectorColorPanelActive = true;
    }
}
