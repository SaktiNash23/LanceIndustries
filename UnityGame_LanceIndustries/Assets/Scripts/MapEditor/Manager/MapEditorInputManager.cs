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

public enum MAP_EDITING_PANEL
{
    NONE = 0,
    HORIZONTAL_LINE = 1,
    VERTICAL_LINE = 2,
    DESTINATION_POINT = 3,
    PORTAL = 4,
    PORTAL_1ST_SET = 5,
    PORTAL_2ND_SET = 6
}

public class MapEditorInputManager : MonoBehaviour
{
    public INPUT_MODE CurrentInputMode { get; set; } = INPUT_MODE.NONE;
    public GIZMO_MODE CurrentGizmoMode { get; set; } = GIZMO_MODE.MOVE;
    public MAP_EDITING_PANEL CurrentSelectedPanel { get; set; } = MAP_EDITING_PANEL.NONE;

    private Camera cachedMainCamera = null;

    private static MapEditorInputManager _instance;
    public static MapEditorInputManager Instance
    {
        get { return _instance; }
    }

    private MapEditorInSceneObject selectingObject = null;

    [BoxGroup("SINGLETON SETTINGS")] [SerializeField] bool dontDestroyOnLoad;

    [BoxGroup("INPUT MANAGER SETTINGS")] [SerializeField] float mapEditorScenePosZ;

    public bool MapEditing { get; set; } = false;
    public bool OptionMenuVisibility { get; set; } = false;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        cachedMainCamera = Camera.main;
    }

    private void Update()
    {
        // If map finish initializing, option menu is off and the user not editing the value in the input fields
        if (MapEditing && !OptionMenuVisibility && !MapEditorManager.Instance.EditingIF)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("MapEditorBorder", "MapEditorOriginEndPoint", "MapEditorPortal");

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
                        if (CurrentGizmoMode == GIZMO_MODE.MOVE)
                            SwitchInputMode(INPUT_MODE.NONE);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                // If currently picking from sub-panel
                if (CurrentSelectedPanel != MAP_EDITING_PANEL.NONE)
                {
                    // Reset it to none
                    SwitchSelectedPanel(MAP_EDITING_PANEL.NONE);
                }

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
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                if (selectingObject)
                    DeleteObject();
            }

            // Accept Hot Key Input
            switch (CurrentSelectedPanel)
            {
                // No Panel Selected
                case MAP_EDITING_PANEL.NONE:
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.HORIZONTAL_LINE);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.VERTICAL_LINE);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.ORIGIN_POINT));
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.DESTINATION_POINT);
                    }
                    else if(Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.PORTAL);
                    }
                    break;
                case MAP_EDITING_PANEL.HORIZONTAL_LINE:     // Horizontal Line Panel Selected
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    break;
                case MAP_EDITING_PANEL.VERTICAL_LINE:     // Vertical Line Panel Selected
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha5))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE));
                        SwitchSelectedPanel(0);
                    }
                    break;
                case MAP_EDITING_PANEL.DESTINATION_POINT:   // Destination Point Panel Selected
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE));
                        SwitchSelectedPanel(0);
                    }
                    break;
                case MAP_EDITING_PANEL.PORTAL:
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.PORTAL_1ST_SET);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SwitchSelectedPanel(MAP_EDITING_PANEL.PORTAL_2ND_SET);
                    }
                    break;
                case MAP_EDITING_PANEL.PORTAL_1ST_SET:
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL));
                        SwitchSelectedPanel(0);
                    }
                    break;
                case MAP_EDITING_PANEL.PORTAL_2ND_SET:
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL));
                        SwitchSelectedPanel(0);
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        if (selectingObject)
                            SwitchInputMode(INPUT_MODE.NONE);
                        SelectObject(MapEditorManager.Instance.CreateInSceneObj(IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL));
                        SwitchSelectedPanel(0);
                    }
                    break;
            }
        }

        // If map finish initializing and the user not editing input fields
        if(MapEditing && !MapEditorManager.Instance.EditingIF)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if (selectingObject)
                    SwitchInputMode(INPUT_MODE.NONE);
                MapEditorManager.Instance.ToggleOptionMenu();
            }    
        }

        if(MapEditing && CurrentInputMode == INPUT_MODE.SELECTING && CurrentGizmoMode == GIZMO_MODE.MOVE)
        {
            Ray ray = cachedMainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            IN_SCENE_OBJECT_TYPES objType = selectingObject.inSceneObjectType;
            LayerMask targetMask = default;
            switch (objType)
            {
                case IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE:
                case IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE:
                case IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE:
                case IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE:
                case IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE:
                case IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE:
                    targetMask = LayerMask.GetMask("MapEditorBorderSnapper");
                    break;
                case IN_SCENE_OBJECT_TYPES.ORIGIN_POINT:
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE:
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED:
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW:
                case IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE:
                    targetMask = LayerMask.GetMask("MapEditorBoxSnapper");
                    break;
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL:
                case IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL:
                    targetMask = LayerMask.GetMask("MapEditorPortalSnapper");
                    break;
            }
            if (Physics.Raycast(ray, out raycastHit, 100.0f, targetMask))
            {
                if (raycastHit.transform.TryGetComponent(out MapLayoutBorder mapLayoutBorder))
                {
                    mapLayoutBorder.CheckSnapping(selectingObject);
                }
                else if(raycastHit.transform.TryGetComponent(out MapLayoutBoxSnapper mapLayoutBoxSnapper))
                {
                    mapLayoutBoxSnapper.CheckSnapping(selectingObject);
                }
                else if (raycastHit.transform.TryGetComponent(out MapLayoutPortalSnapper mapLayoutPortalSnapper))
                {
                    mapLayoutPortalSnapper.CheckSnapping(selectingObject);
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

    public void SwitchSelectedPanel(MAP_EDITING_PANEL targetPanel)
    {
        CurrentSelectedPanel = targetPanel;
        MapEditorManager.Instance.ShowSubPanel(targetPanel);
    }

    public void SwitchSelectedPanel(int targetPanelIndex)
    {
        CurrentSelectedPanel = (MAP_EDITING_PANEL)targetPanelIndex;
        MapEditorManager.Instance.ShowSubPanel(CurrentSelectedPanel);
    }

    public void SwitchSelectedPanelUI(int targetPanelIndex)
    {
        CurrentSelectedPanel = (MAP_EDITING_PANEL)targetPanelIndex;
        MapEditorManager.Instance.ShowSubPanel(CurrentSelectedPanel, true);
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
        if(selectingObject.SnappedTargetPortalSnapper)
        {
            selectingObject.SnappedTargetPortalSnapper.GotSnappedObject = false;
        }
        Destroy(selectingObject.gameObject);
        selectingObject = null;
        SwitchInputMode(INPUT_MODE.NONE);
    }

    public MapEditorInSceneObject GetSelectingInSceneObject()
    {
        return selectingObject;
    }

    public void ToggleOptionMenuVisibility()
    {
        OptionMenuVisibility = !OptionMenuVisibility;
    }
}
