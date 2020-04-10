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
        EventTrigger.Entry entry = new EventTrigger.Entry();

        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Touch touch = Input.GetTouch(0);
        Vector3 point = Camera.main.ScreenToWorldPoint(touch.position);

        GameObject returnedReflector = GameManager.gameManagerInstance.checkReflectorStockAvailability(gameObject.name);

        if (returnedReflector != null)
        {
            Instantiate(returnedReflector, new Vector2(point.x, point.y), Quaternion.identity);
            returnedReflector.GetComponent<BoxCollider2D>().enabled = true;
            GameManager.gameManagerInstance.allReflectorsInScene.Add(returnedReflector);
            Debug.LogWarning("Reflector Instance ID : " + returnedReflector.GetInstanceID());
        }
    }
}
