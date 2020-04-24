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
            GameManager.gameManagerInstance.isReflectorColorPanelActive = false;
        }
    }

    public void activateReflectorColorUIPanel(string buttonTag)
    {
        switch (buttonTag)
        {
            case "ReflectorButton_Basic":
                for(int i = 0; i < GameManager.gameManagerInstance.allReflectorColorButtons.Count; ++i)
                {
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<SpriteRenderer>().sprite = GameManager.gameManagerInstance.reflectorSprite_Basic;
                    GameManager.gameManagerInstance.allReflectorColorButtons[i].GetComponent<ReflectorColor_UIButton>().reflectorTypeTag = "Basic";
                }
                break;
        }        
    }

    #region Code Storage 0
    /*
        GameObject returnedReflector = null;
        bool reflectorInStock = false;
        string reflectorPoolTag = System.String.Empty;

        Touch touch = Input.GetTouch(0);

            if(Input.touchCount <= 1)
            { 

                Vector3 point = Camera.main.ScreenToWorldPoint(touch.position);

                reflectorInStock = GameManager.gameManagerInstance.checkReflectorStockAvailability(buttonTypeTag, out reflectorPoolTag);

            if (reflectorInStock)
            { 
                #region Test code for Reflector Pooler



                
                returnedReflector = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Dequeue();

                GameManager.gameManagerInstance.allReflectorsInScene.Add(returnedReflector);

                returnedReflector.SetActive(true);
                returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
                returnedReflector.GetComponent<Raycast>().timeUntilHold = 0.1f;
                returnedReflector.GetComponent<Raycast>().isHoldingDownAccessor = true;
                

                //switch(buttonTypeTag)
                //{
                    //case "Button_BasicReflector":
                        
                        //GameManager.gameManagerInstance.ReflectorStock_Basic_Text.text = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Count.ToString();
                        //break;
                    
                    case "Button_TranslucentReflector":
                        GameManager.gameManagerInstance.ReflectorStock_Translucent_Text.text = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Count.ToString();
                        break;

                    case "Button_DoubleWayReflector":
                        GameManager.gameManagerInstance.ReflectorStock_DoubleWay_Text.text = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Count.ToString();
                        break;

                    case "Button_SplitReflector":
                        GameManager.gameManagerInstance.ReflectorStock_Split_Text.text = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Count.ToString();
                        break;

                    case "Button_ThreeWayReflector":
                        GameManager.gameManagerInstance.ReflectorStock_ThreeWay_Text.text = ReflectorPooler.instance_reflectorPooler.reflectorPoolDictionary[reflectorPoolTag].Count.ToString();
                        break;

                    
                }

                #endregion

           }
        }
     */
    #endregion
}
