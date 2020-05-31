using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using NaughtyAttributes;

public enum IN_SCENE_OBJECT_TYPES
{
    NONE = 0,
    NORMAL_VERTICAL_LINE = 1,
    NORMAL_HORIZONTAL_LINE = 2,
    ORIGIN_POINT = 3,
    DESTINATION_POINT_WHITE = 4,
    DESTINATION_POINT_RED = 5,
    DESTINATION_POINT_YELLOW = 6,
    DESTINATION_POINT_BLUE = 7,
    WHITE_VERTICAL_LINE = 8,
    RED_VERTICAL_LINE = 9,
    YELLOW_VERTICAL_LINE = 10,
    BLUE_VERTICAL_LINE = 11,
    WHITE_HORIZONTAL_LINE = 12,
    RED_HORIZONTAL_LINE = 13,
    YELLOW_HORIZONTAL_LINE = 14,
    BLUE_HORIZONTAL_LINE = 15,
    PORTAL_1ST_SET_VERTICAL = 16,
    PORTAL_1ST_SET_HORIZONTAL = 17,
    PORTAL_2ND_SET_VERTICAL = 18,
    PORTAL_2ND_SET_HORIZONTAL = 19
}

public class MapEditorInSceneObject : MonoBehaviour
{
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public IN_SCENE_OBJECT_TYPES inSceneObjectType;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public SpriteRenderer spriteRend;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public Color defaultColor;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public Color selectedColor;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT REFERENCES")] public EventTrigger eventTrigger;

    public InSceneObjectData InSceneObjData { get; set; } = new InSceneObjectData();

    // Can consider use inherited classed so that only need 1 of this var
    public MapLayoutBorder SnappedTargetBorder { get; set; } = null;
    public MapLayoutPortalSnapper SnappedTargetPortalSnapper { get; set; } = null;
    public MapLayoutBoxSnapper SnappedTargetBox { get; set; } = null;

    private GizmoBase attachedGizmo;

    //------------------------------ MONOBEHAVIOUR FUNCTIONS -----------------------------//

    private void Start()
    {
        EventTrigger.Entry onMouseButtonDownEntry = new EventTrigger.Entry();
        onMouseButtonDownEntry.eventID = EventTriggerType.PointerClick;
        onMouseButtonDownEntry.callback.AddListener((BaseEventData data) => PointerClickAction((PointerEventData)data));
        eventTrigger.triggers.Add(onMouseButtonDownEntry);
        InSceneObjData.inSceneObjectType = inSceneObjectType;
        UpdateInSceneObjectData();
    }

    private void LateUpdate()
    {
        if (SnappedTargetBorder)
        {
            transform.position = SnappedTargetBorder.GetSnappingPosition();
        }
        else if (SnappedTargetBox)
        {
            transform.position = SnappedTargetBox.GetSnappingPosition();
        }
        else if(SnappedTargetPortalSnapper)
        {
            transform.position = SnappedTargetPortalSnapper.GetSnappingPosition();
        }
    }

    //------------------------------ IN SCENE OBJECT FUNCTIONS -----------------------------//

    private void PointerClickAction(PointerEventData pointerEventData)
    {
        //if (MapEditorInputManager.Instance.CurrentInputMode == INPUT_MODE.NONE)
        //{
        //    MapEditorInputManager.Instance.SelectObject(this);
        //}
    }

    public void UpdateInSceneObjectData()
    {
        InSceneObjData.position = transform.position;
        InSceneObjData.rotation = transform.rotation;
        InSceneObjData.scale = transform.localScale;
    }

    public void CreateGizmo(GIZMO_MODE gizmoMode)
    {
        switch (gizmoMode)
        {
            case GIZMO_MODE.MOVE:
                attachedGizmo = Instantiate(MapEditorManager.Instance.moveGizmoPrefab, transform.position, Quaternion.identity, transform);
                attachedGizmo.AssignInSceneObject(this);
                break;
            case GIZMO_MODE.ROTATE:
                attachedGizmo = Instantiate(MapEditorManager.Instance.rotationGizmoPrefab, transform.position, Quaternion.identity, transform);
                attachedGizmo.AssignInSceneObject(this);
                break;
        }
    }

    public void RemoveGizmo()
    {
        Destroy(attachedGizmo.gameObject);
        attachedGizmo = null;
    }

    public void ChangeColor(Color targetColor)
    {
        spriteRend.color = targetColor;
    }
}

[Serializable]
public class InSceneObjectData
{
    public int mapGridIndex;
    public IN_SCENE_OBJECT_TYPES inSceneObjectType;
    public SNAPPING_DIR borderDir;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}