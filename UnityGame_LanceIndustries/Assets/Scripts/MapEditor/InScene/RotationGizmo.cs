using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public class RotationGizmo : GizmoBase
{
    [BoxGroup("ROTATION GIZMO SETTINGS")] [SerializeField] float rotationSpeed = 1.0f;

    private Vector3 inSceneObjStartRotEuler;
    private Vector3 mouseStartPos;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS ------------------------------//

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (selectingAxis)
        {
            Vector3 currMousePos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));

            float xOffset = 0f;
            float yOffset = 0f;

            Vector3 newRotEuler = Vector3.zero;

            if(currSelectedAxis == SELECTED_AXIS.Z_AXIS)
            {
                xOffset = (currMousePos.x - mouseStartPos.x) * -1f;     // -1f is used to invert the as -ve value of xOffset as it will increase the rot in +ve
                yOffset = currMousePos.y - mouseStartPos.y;
                float totalOffset = xOffset + yOffset;
                newRotEuler = new Vector3(0f, 0f, inSceneObjStartRotEuler.z + totalOffset * rotationSpeed);
            }

            attachedInSceneObj.transform.rotation = Quaternion.Euler(newRotEuler);
        }
    }

    protected override void ZAxisPointerEnterCallback(PointerEventData pointerEventData)
    {
        base.ZAxisPointerEnterCallback(pointerEventData);
    }

    protected override void ZAxisPointerExitCallback(PointerEventData pointerEventData)
    {
        base.ZAxisPointerExitCallback(pointerEventData);
    }

    protected override void ZAxisPointerDownCallback(PointerEventData pointerEventData)
    {
        base.ZAxisPointerDownCallback(pointerEventData);
        inSceneObjStartRotEuler = attachedInSceneObj.transform.rotation.eulerAngles;
        mouseStartPos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
    }

    protected override void ZAxisPointerUpCallback(PointerEventData pointerEventData)
    {
        base.ZAxisPointerUpCallback(pointerEventData);
        inSceneObjStartRotEuler = Vector3.zero;
        mouseStartPos = Vector3.zero;
    }
}
