using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorColor_UIButton : MonoBehaviour
{
    public string reflectorTypeTag; //Determines the the type of reflector
    public string reflectorColorTag; //Determines the color of the reflector

    // Start is called before the first frame update
    void Start()
    {
        #region Initializing Event Triggers

        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryOnPointerDown = new EventTrigger.Entry();
        EventTrigger.Entry entryOnPointerUp = new EventTrigger.Entry();

        entryOnPointerDown.eventID = EventTriggerType.PointerDown;
        entryOnPointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });

        entryOnPointerUp.eventID = EventTriggerType.PointerUp;
        entryOnPointerUp.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });

        eventTrigger.triggers.Add(entryOnPointerDown);
        eventTrigger.triggers.Add(entryOnPointerUp);

        #endregion
    }

    //This function 'pulls' the appropriate reflector from its object pool and positions it at the player's finger, where it is ready for the player
    //to drag & drop the reflector. Also, once the reflector is 'pulled' from its object pool, this function will also update the 
    //appropriate reflector stock
    public void OnPointerDown(PointerEventData pointerEventData)
    {       
        GameObject returnedReflector = null;
        string reflectorPoolTag = System.String.Empty;

        Touch touch = Input.GetTouch(0);

         if(Input.touchCount <= 1)
         {
            reflectorPoolTag = GameManager.gameManagerInstance.setSelectedColorReflector(reflectorTypeTag, reflectorColorTag);

            returnedReflector = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Dequeue();

            GameManager.gameManagerInstance.allReflectorsInScene.Add(returnedReflector);

            returnedReflector.SetActive(true);
            returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
            returnedReflector.GetComponent<Raycast>().timeUntilHold = 0.1f;
            returnedReflector.GetComponent<Raycast>().isHoldingDownAccessor = true;

            switch (reflectorTypeTag)
            {
                case "Basic":
                     GameManager.gameManagerInstance.ReflectorStockBasic_Accessor--;
                     GameManager.gameManagerInstance.ReflectorStock_Basic_Text.text = GameManager.gameManagerInstance.ReflectorStockBasic_Accessor.ToString();
                     break;

                case "Translucent":
                    GameManager.gameManagerInstance.ReflectorStockTranslucent_Accessor--;
                    GameManager.gameManagerInstance.ReflectorStock_Translucent_Text.text = GameManager.gameManagerInstance.ReflectorStockTranslucent_Accessor.ToString();
                    break;

                case "DoubleWay":
                    GameManager.gameManagerInstance.ReflectorStockDoubleWay_Accessor--;
                    GameManager.gameManagerInstance.ReflectorStock_DoubleWay_Text.text = GameManager.gameManagerInstance.ReflectorStockDoubleWay_Accessor.ToString();
                    break;

                case "Split":
                    GameManager.gameManagerInstance.ReflectorStockSplit_Accessor--;
                    GameManager.gameManagerInstance.ReflectorStock_Split_Text.text = GameManager.gameManagerInstance.ReflectorStockSplit_Accessor.ToString();
                    break;

                case "ThreeWay":
                    GameManager.gameManagerInstance.ReflectorStockThreeWay_Accessor--;
                    GameManager.gameManagerInstance.ReflectorStock_ThreeWay_Text.text = GameManager.gameManagerInstance.ReflectorStockThreeWay_Accessor.ToString();
                    break;

                default:
                    Debug.LogWarning("No such reflectorTypeTag exists. Check whether the ReflectorTypeTag is spelled properly in editor");
                    break;
            }
            
         }

        GameManager.gameManagerInstance.reflectorColorsPanel.SetActive(false);
        GameManager.gameManagerInstance.isReflectorColorPanelActive = false;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {

    }
}
