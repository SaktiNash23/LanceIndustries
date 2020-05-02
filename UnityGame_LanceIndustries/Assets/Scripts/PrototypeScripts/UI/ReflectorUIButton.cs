using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorUIButton : MonoBehaviour
{
    public string buttonTypeTag;
    bool isReflectorInStock;

    // Start is called before the first frame update
    void Start()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryOnPointerDown = new EventTrigger.Entry();
        EventTrigger.Entry entryOnPointerUp = new EventTrigger.Entry();

        entryOnPointerDown.eventID = EventTriggerType.PointerDown;
        entryOnPointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });

        entryOnPointerUp.eventID = EventTriggerType.PointerUp;
        entryOnPointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });

        eventTrigger.triggers.Add(entryOnPointerDown);
        eventTrigger.triggers.Add(entryOnPointerUp);


    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isReflectorInStock = GameManager.gameManagerInstance.checkReflectorStockAvailability(buttonTypeTag);

        if (isReflectorInStock)
        {
            activateReflectorColorUIPanel(buttonTypeTag);
            GameManager.gameManagerInstance.reflectorColorsPanel.SetActive(true);
            GameManager.gameManagerInstance.isReflectorColorPanelActive = true;
        }
        else if (!isReflectorInStock)
        {
            Debug.Log("No reflector in stock");
        }
    }

    //This function updates the buttons in Reflector Color Panel UI with the appropriate images and changes the tag of the buttons as well
    //
    //Ex: If the button for Reflector type Basic is pressed, the string tag from that button is passed to this function as an argument. Based
    // on the argument passed, the Reflector Color Buttons are updated with the appropriate sprites and tags
    public void activateReflectorColorUIPanel(string buttonTag)
    {
        switch (buttonTag)
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
    }
}
