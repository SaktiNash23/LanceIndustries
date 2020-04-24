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
                     GameManager.gameManagerInstance.ReflectorStock_Basic--;
                     GameManager.gameManagerInstance.ReflectorStock_Basic_Text.text = GameManager.gameManagerInstance.ReflectorStock_Basic.ToString();
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
