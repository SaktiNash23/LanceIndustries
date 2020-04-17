using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReflectorUIButton : MonoBehaviour
{

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
        bool reflectorInStock = false;
        int reflectorToSpawnIndex = 0;

        Touch touch = Input.GetTouch(0);

        if(Input.touchCount <= 1)
        { 

            Vector3 point = Camera.main.ScreenToWorldPoint(touch.position);

            reflectorInStock = GameManager.gameManagerInstance.checkReflectorStockAvailability(gameObject.name, out reflectorToSpawnIndex);

           //GameManager.gameManagerInstance.deactivateAllButtons();

           if (reflectorInStock)
           {
             returnedReflector = Instantiate(GameManager.gameManagerInstance.allReflectorGO[reflectorToSpawnIndex], new Vector2(point.x, point.y), Quaternion.identity);

              GameManager.gameManagerInstance.allReflectorsInScene.Add(returnedReflector);
              Debug.Log("Newly added reflector : " + returnedReflector.GetInstanceID());

              returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
              returnedReflector.GetComponent<Raycast>().timeUntilHold = 0.1f;
              returnedReflector.GetComponent<Raycast>().isHoldingDownAccessor = true;
           }
        }
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //GameManager.gameManagerInstance.activateAllButtons();
    }
}
