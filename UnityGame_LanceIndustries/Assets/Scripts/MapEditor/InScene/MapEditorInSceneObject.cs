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
    VERTICAL_LINE = 1,
    HORIZONTAL_LINE = 2,
    ORIGIN_POINT = 3,
    DESTINATION_POINT_WHITE = 4,
    DESTINATION_POINT_RED = 5,
    DESTINATION_POINT_YELLOW = 6,
    DESTINATION_POINT_BLUE = 7
}

public class MapEditorInSceneObject : MonoBehaviour
{
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public IN_SCENE_OBJECT_TYPES inSceneObjectType;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public SpriteRenderer spriteRend;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public Color defaultColor;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public Color selectedColor;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT REFERENCES")] public EventTrigger eventTrigger;

    public InSceneObjectData InSceneObjData { get; set; } = new InSceneObjectData();

    public MapLayoutBorder SnappedTargetBorder { get; set; } = null;
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
    public BORDER_DIRECTION borderDir;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}