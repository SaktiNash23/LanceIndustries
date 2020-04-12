using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public enum SELECTED_AXIS
{
    NONE = 0,
    X_AXIS = 1,
    Y_AXIS = 2,
    Z_AXIS = 3,
    XY_AXIS = 4
}

public class GizmoBase : MonoBehaviour
{
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected EventTrigger xAxisEventTrigger;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected EventTrigger yAxisEventTrigger;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected EventTrigger zAxisEventTrigger;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected EventTrigger xyAxisEventTrigger;

    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected SpriteRenderer xAxisSpriteRenderer;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected SpriteRenderer yAxisSpriteRenderer;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected SpriteRenderer zAxisSpriteRenderer;
    [BoxGroup("GIZMO REFERENCES")] [SerializeField] protected SpriteRenderer xyAxisSpriteRenderer;

    protected Camera cachedMainCamera;

    protected bool selectingAxis = false;
    protected SELECTED_AXIS currSelectedAxis = SELECTED_AXIS.NONE;

    protected MapEditorInSceneObject attachedInSceneObj;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS ------------------------------//

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        cachedMainCamera = Camera.main;

        if (xAxisEventTrigger)
        {
            EventTrigger.Entry xAxisPointerEnterEntry = new EventTrigger.Entry();
            xAxisPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            xAxisPointerEnterEntry.callback.AddListener((BaseEventData eventData) => XAxisPointerEnterCallback((PointerEventData)eventData));
            xAxisEventTrigger.triggers.Add(xAxisPointerEnterEntry);
            EventTrigger.Entry xAxisPointerExitEntry = new EventTrigger.Entry();
            xAxisPointerExitEntry.eventID = EventTriggerType.PointerExit;
            xAxisPointerExitEntry.callback.AddListener((BaseEventData eventData) => XAxisPointerExitCallback((PointerEventData)eventData));
            xAxisEventTrigger.triggers.Add(xAxisPointerExitEntry);
            EventTrigger.Entry xAxisPointerDownEntry = new EventTrigger.Entry();
            xAxisPointerDownEntry.eventID = EventTriggerType.PointerDown;
            xAxisPointerDownEntry.callback.AddListener((BaseEventData eventData) => XAxisPointerDownCallback((PointerEventData)eventData));
            xAxisEventTrigger.triggers.Add(xAxisPointerDownEntry);
            EventTrigger.Entry xAxisPointerUpEntry = new EventTrigger.Entry();
            xAxisPointerUpEntry.eventID = EventTriggerType.PointerUp;
            xAxisPointerUpEntry.callback.AddListener((BaseEventData eventData) => XAxisPointerUpCallback((PointerEventData)eventData));
            xAxisEventTrigger.triggers.Add(xAxisPointerUpEntry);
        }

        if (yAxisEventTrigger)
        {
            EventTrigger.Entry yAxisPointerEnterEntry = new EventTrigger.Entry();
            yAxisPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            yAxisPointerEnterEntry.callback.AddListener((BaseEventData eventData) => YAxisPointerEnterCallback((PointerEventData)eventData));
            yAxisEventTrigger.triggers.Add(yAxisPointerEnterEntry);
            EventTrigger.Entry yAxisPointerExitEntry = new EventTrigger.Entry();
            yAxisPointerExitEntry.eventID = EventTriggerType.PointerExit;
            yAxisPointerExitEntry.callback.AddListener((BaseEventData eventData) => YAxisPointerExitCallback((PointerEventData)eventData));
            yAxisEventTrigger.triggers.Add(yAxisPointerExitEntry);
            EventTrigger.Entry yAxisPointerDownEntry = new EventTrigger.Entry();
            yAxisPointerDownEntry.eventID = EventTriggerType.PointerDown;
            yAxisPointerDownEntry.callback.AddListener((BaseEventData eventData) => YAxisPointerDownCallback((PointerEventData)eventData));
            yAxisEventTrigger.triggers.Add(yAxisPointerDownEntry);
            EventTrigger.Entry yAxisPointerUpEntry = new EventTrigger.Entry();
            yAxisPointerUpEntry.eventID = EventTriggerType.PointerUp;
            yAxisPointerUpEntry.callback.AddListener((BaseEventData eventData) => YAxisPointerUpCallback((PointerEventData)eventData));
            yAxisEventTrigger.triggers.Add(yAxisPointerUpEntry);
        }

        if(zAxisEventTrigger)
        {
            EventTrigger.Entry zAxisPointerEnterEntry = new EventTrigger.Entry();
            zAxisPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            zAxisPointerEnterEntry.callback.AddListener((BaseEventData eventData) => ZAxisPointerEnterCallback((PointerEventData)eventData));
            zAxisEventTrigger.triggers.Add(zAxisPointerEnterEntry);
            EventTrigger.Entry zAxisPointerExitEntry = new EventTrigger.Entry();
            zAxisPointerExitEntry.eventID = EventTriggerType.PointerExit;
            zAxisPointerExitEntry.callback.AddListener((BaseEventData eventData) => ZAxisPointerExitCallback((PointerEventData)eventData));
            zAxisEventTrigger.triggers.Add(zAxisPointerExitEntry);
            EventTrigger.Entry zAxisPointerDownEntry = new EventTrigger.Entry();
            zAxisPointerDownEntry.eventID = EventTriggerType.PointerDown;
            zAxisPointerDownEntry.callback.AddListener((BaseEventData eventData) => ZAxisPointerDownCallback((PointerEventData)eventData));
            zAxisEventTrigger.triggers.Add(zAxisPointerDownEntry);
            EventTrigger.Entry yAxisPointerUpEntry = new EventTrigger.Entry();
            yAxisPointerUpEntry.eventID = EventTriggerType.PointerUp;
            yAxisPointerUpEntry.callback.AddListener((BaseEventData eventData) => ZAxisPointerUpCallback((PointerEventData)eventData));
            zAxisEventTrigger.triggers.Add(yAxisPointerUpEntry);
        }

        if (xyAxisEventTrigger)
        {
            EventTrigger.Entry xyAxisPointerEnterEntry = new EventTrigger.Entry();
            xyAxisPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            xyAxisPointerEnterEntry.callback.AddListener((BaseEventData eventData) => XYAxisPointerEnterCallback((PointerEventData)eventData));
            xyAxisEventTrigger.triggers.Add(xyAxisPointerEnterEntry);
            EventTrigger.Entry xyAxisPointerExitEntry = new EventTrigger.Entry();
            xyAxisPointerExitEntry.eventID = EventTriggerType.PointerExit;
            xyAxisPointerExitEntry.callback.AddListener((BaseEventData eventData) => XYAxisPointerExitCallback((PointerEventData)eventData));
            xyAxisEventTrigger.triggers.Add(xyAxisPointerExitEntry);
            EventTrigger.Entry xyAxisPointerDownEntry = new EventTrigger.Entry();
            xyAxisPointerDownEntry.eventID = EventTriggerType.PointerDown;
            xyAxisPointerDownEntry.callback.AddListener((BaseEventData eventData) => XYAxisPointerDownCallback((PointerEventData)eventData));
            xyAxisEventTrigger.triggers.Add(xyAxisPointerDownEntry);
            EventTrigger.Entry xyAxisPointerUpEntry = new EventTrigger.Entry();
            xyAxisPointerUpEntry.eventID = EventTriggerType.PointerUp;
            xyAxisPointerUpEntry.callback.AddListener((BaseEventData eventData) => XYAxisPointerUpCallback((PointerEventData)eventData));
            xyAxisEventTrigger.triggers.Add(xyAxisPointerUpEntry);
        }
    }

    protected virtual void Update()
    {

    }

    protected virtual void XAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        if(!selectingAxis)
            xAxisSpriteRenderer.color = Color.white;
    }

    protected virtual void XAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        if(!selectingAxis)
            xAxisSpriteRenderer.color = Color.red;
    }

    protected virtual void XAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        selectingAxis = true;
        currSelectedAxis = SELECTED_AXIS.X_AXIS;
        xAxisSpriteRenderer.color = Color.yellow;
    }

    protected virtual void XAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        selectingAxis = false;
        currSelectedAxis = SELECTED_AXIS.NONE;
        Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("MapEditorInSceneObject");
        if(Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane, layerMask))
        {
            if (hit.transform.name == "xAxisTrigger")
                xAxisSpriteRenderer.color = Color.white;
            else if (hit.transform.name == "yAxisTrigger")
            {
                yAxisSpriteRenderer.color = Color.white;
                xAxisSpriteRenderer.color = Color.red;
            }
            else if (hit.transform.name == "xyAxisTrigger")
            {
                xyAxisSpriteRenderer.color = Color.white;
                xAxisSpriteRenderer.color = Color.white;
                yAxisSpriteRenderer.color = Color.white;
            }
            else
            {
                xyAxisSpriteRenderer.color = Color.blue;
                xAxisSpriteRenderer.color = Color.red;
                yAxisSpriteRenderer.color = Color.green;
            }
        }
        else
        {
            xAxisSpriteRenderer.color = Color.red;
        }

        SaveInSceneObjectData();
    }

    protected virtual void YAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        if(!selectingAxis)
            yAxisSpriteRenderer.color = Color.white;
    }

    protected virtual void YAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        if(!selectingAxis)
            yAxisSpriteRenderer.color = Color.green;
    }

    protected virtual void YAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        selectingAxis = true;
        currSelectedAxis = SELECTED_AXIS.Y_AXIS;
        yAxisSpriteRenderer.color = Color.yellow;
    }

    protected virtual void YAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        selectingAxis = false;
        currSelectedAxis = SELECTED_AXIS.NONE;
        Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("MapEditorInSceneObject");
        if (Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane, layerMask))
        {
            if (hit.transform.name == "yAxisTrigger")
                yAxisSpriteRenderer.color = Color.white;
            else if (hit.transform.name == "xAxisTrigger")
            {
                xAxisSpriteRenderer.color = Color.white;
                yAxisSpriteRenderer.color = Color.green;
            }
            else if (hit.transform.name == "xyAxisTrigger")
            {
                xyAxisSpriteRenderer.color = Color.white;
                xAxisSpriteRenderer.color = Color.white;
                yAxisSpriteRenderer.color = Color.white;
            }
            else
            {
                xyAxisSpriteRenderer.color = Color.blue;
                xAxisSpriteRenderer.color = Color.red;
                yAxisSpriteRenderer.color = Color.green;
            }
        }
        else
        {
            yAxisSpriteRenderer.color = Color.green;
        }

        SaveInSceneObjectData();
    }

    protected virtual void ZAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        if (!selectingAxis)
            zAxisSpriteRenderer.color = Color.white;
    }

    protected virtual void ZAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        if (!selectingAxis)
            zAxisSpriteRenderer.color = Color.blue;
    }

    protected virtual void ZAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        selectingAxis = true;
        currSelectedAxis = SELECTED_AXIS.Z_AXIS;
        zAxisSpriteRenderer.color = Color.yellow;
    }

    protected virtual void ZAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        selectingAxis = false;
        currSelectedAxis = SELECTED_AXIS.NONE;
        Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("MapEditorInSceneObject");
        if (Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane, layerMask))
        {
            if (hit.transform.name == "zAxisTrigger")
                zAxisSpriteRenderer.color = Color.white;
        }
        else
        {
            zAxisSpriteRenderer.color = Color.blue;
        }
    }

    protected virtual void XYAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        if (!selectingAxis)
        {
            xAxisSpriteRenderer.color = Color.white;
            yAxisSpriteRenderer.color = Color.white;
            xyAxisSpriteRenderer.color = Color.white;
        }
    }

    protected virtual void XYAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        if (!selectingAxis)
        {
            xAxisSpriteRenderer.color = Color.red;
            yAxisSpriteRenderer.color = Color.green;
            xyAxisSpriteRenderer.color = Color.blue;
        }
    }

    protected virtual void XYAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        selectingAxis = true;
        currSelectedAxis = SELECTED_AXIS.XY_AXIS;
        xAxisSpriteRenderer.color = Color.yellow;
        yAxisSpriteRenderer.color = Color.yellow;
        xyAxisSpriteRenderer.color = Color.yellow;
    }

    protected virtual void XYAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        selectingAxis = false;
        currSelectedAxis = SELECTED_AXIS.NONE;
        Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("MapEditorInSceneObject");
        if (Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane, layerMask))
        {
            if (hit.transform.name == "xyAxisTrigger")
            {
                xAxisSpriteRenderer.color = Color.white;
                yAxisSpriteRenderer.color = Color.white;
                xyAxisSpriteRenderer.color = Color.white;
            }
            else if (hit.transform.name == "xAxisTrigger")
            {
                xAxisSpriteRenderer.color = Color.white;
                yAxisSpriteRenderer.color = Color.green;
                xyAxisSpriteRenderer.color = Color.blue;
            }
            else if (hit.transform.name == "yAxisTrigger")
            {
                xAxisSpriteRenderer.color = Color.red;
                yAxisSpriteRenderer.color = Color.white;
                xyAxisSpriteRenderer.color = Color.blue;
            }
            else
            {
                xAxisSpriteRenderer.color = Color.red;
                yAxisSpriteRenderer.color = Color.green;
                xyAxisSpriteRenderer.color = Color.blue;
            }
        }
        else
        {
            xAxisSpriteRenderer.color = Color.red;
            yAxisSpriteRenderer.color = Color.green;
            xyAxisSpriteRenderer.color = Color.blue;
        }

        SaveInSceneObjectData();
    }

    public void AssignInSceneObject(MapEditorInSceneObject inSceneObj)
    {
        attachedInSceneObj = inSceneObj;
    }

    public void SaveInSceneObjectData()
    {
        attachedInSceneObj.UpdateInSceneObjectData();
    }
}
