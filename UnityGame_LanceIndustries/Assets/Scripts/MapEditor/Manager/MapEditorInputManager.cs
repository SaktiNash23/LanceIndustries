using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum INPUT_MODE
{
    NONE = 0,
    SELECTING = 1
}

public enum GIZMO_MODE
{
    NONE = 0,
    MOVE = 1,
    ROTATE = 2,
    SCALE = 3
}

public class MapEditorInputManager : MonoBehaviour
{
    public INPUT_MODE CurrentInputMode { get; set; } = INPUT_MODE.NONE;
    public GIZMO_MODE CurrentGizmoMode { get; set; } = GIZMO_MODE.MOVE;

    private Camera cachedMainCamera = null;

    private static MapEditorInputManager _instance;
    public static MapEditorInputManager Instance
    {
        get { return _instance; }
    }

    private MapEditorInSceneObject selectingObject = null;

    [BoxGroup("INPUT MANAGER SETTINGS")] [SerializeField] float mapEditorScenePosZ;

    public bool MapEditing { get; set; } = false;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        cachedMainCamera = Camera.main;
    }

    private void Update()
    {
        if (MapEditing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("MapEditorInSceneObject");

                if (selectingObject == null)
                {
                    if (Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane + 5.0f, layerMask))
                    {
                        SelectObject(hit.collider.GetComponent<MapEditorInSceneObject>());
                    }
                }
                else
                {
                    if (!Physics.Raycast(mouseRay, out hit, cachedMainCamera.farClipPlane + 5.0f, layerMask))
                    {
                        SwitchInputMode(INPUT_MODE.NONE);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                SwitchInputMode(INPUT_MODE.NONE);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CurrentGizmoMode == GIZMO_MODE.MOVE)
                    return;
                CurrentGizmoMode = GIZMO_MODE.MOVE;
                if (selectingObject)
                    SwitchGizmo();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (CurrentGizmoMode == GIZMO_MODE.ROTATE)
                    return;
                CurrentGizmoMode = GIZMO_MODE.ROTATE;
                if (selectingObject)
                    SwitchGizmo();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (CurrentGizmoMode == GIZMO_MODE.SCALE)
                    return;
                CurrentGizmoMode = GIZMO_MODE.SCALE;
                if (selectingObject)
                    SwitchGizmo();
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (selectingObject)
                    DeleteObject();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (selectingObject)
                    SwitchInputMode(INPUT_MODE.NONE);
                SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (selectingObject)
                    SwitchInputMode(INPUT_MODE.NONE);
                SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.VERTICAL_LINE));
            }
        }

        if(MapEditing && CurrentInputMode == INPUT_MODE.SELECTING && CurrentGizmoMode == GIZMO_MODE.MOVE)
        {
            Ray ray = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100.0f, LayerMask.GetMask("MapEditorSnapper")))
            {
                if (raycastHit.transform.TryGetComponent(out MapLayoutBorder mapLayoutBorder))
                {
                    mapLayoutBorder.CheckSnapping(selectingObject);
                }
                else if(raycastHit.transform.TryGetComponent(out MapLayoutBoxSnapper mapLayoutBoxSnapper))
                {
                    mapLayoutBoxSnapper.CheckSnapping(selectingObject);
                }
            }
        }
    }

    //------------------------------ INPUT MANAGER FUNCTIONS ------------------------------//

    private void SwitchInputMode(INPUT_MODE inputMode)
    {
        CurrentInputMode = inputMode;
        switch (CurrentInputMode)
        {
            case INPUT_MODE.NONE:
                if (selectingObject)
                {
                    selectingObject.ChangeColor(selectingObject.defaultColor);
                    selectingObject.UpdateInSceneObjectData();
                    selectingObject.RemoveGizmo();
                }
                selectingObject = null;
                break;
            case INPUT_MODE.SELECTING:
                selectingObject.CreateGizmo(CurrentGizmoMode);
                break;
        }
    }

    private void SwitchGizmo()
    {
        selectingObject.RemoveGizmo();
        selectingObject.CreateGizmo(CurrentGizmoMode);
    }

    public Vector3 GetMouseCursorPosWorldVector3()
    {
        Vector3 mouseVec3 = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
        return mouseVec3;
    }

    public void SelectObject(MapEditorInSceneObject selectedObj)
    {
        selectedObj.ChangeColor(selectedObj.selectedColor);
        selectingObject = selectedObj;
        SwitchInputMode(INPUT_MODE.SELECTING);
    }

    public void DeleteObject()
    {
        if(selectingObject.SnappedTargetBorder)
        {
            selectingObject.SnappedTargetBorder.GotSnappedObject = false;
        }
        if(selectingObject.SnappedTargetBox)
        {
            selectingObject.SnappedTargetBox.GotSnappedObject = false;
        }
        Destroy(selectingObject.gameObject);
        selectingObject = null;
        SwitchInputMode(INPUT_MODE.NONE);
    }

    public MapEditorInSceneObject GetSelectingInSceneObject()
    {
        return selectingObject;
    }
}
