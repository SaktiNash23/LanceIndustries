using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveGizmo : GizmoBase
{
    private Vector3 inSceneObjStartPos;
    private Vector3 mouseStartPos;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS ------------------------------//

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(selectingAxis)
        {
            Vector3 targetPos = Vector3.zero;
            Vector3 currMousePos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));

            float xOffset = 0f;
            float yOffset = 0f;
            float targetX = 0f;
            float targetY = 0f;

            switch (currSelectedAxis)
            {
                case SELECTED_AXIS.X_AXIS:
                    xOffset = currMousePos.x - mouseStartPos.x;
                    targetX = inSceneObjStartPos.x + xOffset;
                    targetPos = new Vector3(targetX, inSceneObjStartPos.y, inSceneObjStartPos.z);
                    break;
                case SELECTED_AXIS.Y_AXIS:
                    yOffset = currMousePos.y - mouseStartPos.y;
                    targetY = inSceneObjStartPos.y + yOffset;
                    targetPos = new Vector3(inSceneObjStartPos.x, targetY, inSceneObjStartPos.z);
                    break;
                case SELECTED_AXIS.XY_AXIS:
                    xOffset = currMousePos.x - mouseStartPos.x;
                    yOffset = currMousePos.y - mouseStartPos.y;
                    targetX = inSceneObjStartPos.x + xOffset;
                    targetY = inSceneObjStartPos.y + yOffset;
                    targetPos = new Vector3(targetX, targetY, inSceneObjStartPos.z);
                    break;
            }

            if (attachedInSceneObj.SnappedTarget)
            {
                if ((targetPos - attachedInSceneObj.transform.position).magnitude > attachedInSceneObj.SnappedTarget.UnsnapDistance)
                {
                    attachedInSceneObj.SnappedTarget = null;
                }
            }
            else
            {
                attachedInSceneObj.transform.position = targetPos;
            }
        }
    }

    protected override void XAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        base.XAxisPointerEnterCallback(pointerEventData);
    }

    protected override void XAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        base.XAxisPointerExitCallback(pointerEventData);
    }

    protected override void XAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        base.XAxisPointerDownCallback(pointerEventData);
        inSceneObjStartPos = attachedInSceneObj.transform.position;
        mouseStartPos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
    }

    protected override void XAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        base.XAxisPointerUpCallback(pointerEventData);
        inSceneObjStartPos = Vector3.zero;
        mouseStartPos = Vector3.zero;
    }

    protected override void YAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        base.YAxisPointerEnterCallback(pointerEventData);
    }

    protected override void YAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        base.YAxisPointerExitCallback(pointerEventData);
    }

    protected override void YAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        base.YAxisPointerDownCallback(pointerEventData);
        inSceneObjStartPos = attachedInSceneObj.transform.position;
        mouseStartPos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
    }

    protected override void YAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        base.YAxisPointerUpCallback(pointerEventData);
        inSceneObjStartPos = Vector3.zero;
        mouseStartPos = Vector3.zero;
    }

    protected override void XYAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        base.XYAxisPointerEnterCallback(pointerEventData);
    }

    protected override void XYAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        base.XYAxisPointerExitCallback(pointerEventData);
    }

    protected override void XYAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        base.XYAxisPointerDownCallback(pointerEventData);
        inSceneObjStartPos = attachedInSceneObj.transform.position;
        mouseStartPos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
    }

    protected override void XYAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        base.XYAxisPointerUpCallback(pointerEventData);
        inSceneObjStartPos = Vector3.zero;
        mouseStartPos = Vector3.zero;
    }
}
