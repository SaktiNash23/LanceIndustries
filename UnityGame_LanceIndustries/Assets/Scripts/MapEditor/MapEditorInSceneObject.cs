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
    DESTINATION_POINT = 4
}

public class MapEditorInSceneObject : MonoBehaviour
{
    [BoxGroup("MAP EDITOR IN SCENE OBJECT SETTINGS")] public IN_SCENE_OBJECT_TYPES inSceneObjectType;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT REFERNECES")] public EventTrigger eventTrigger;
    [BoxGroup("MAP EDITOR IN SCENE OBJECT REFERNECES")] public Collider2D col;

    public InSceneObjectData inSceneObjectData { get; set; } = new InSceneObjectData();

    //------------------------------ MONOBEHAVIOUR FUNCTIONS -----------------------------//

    private void Start()
    {
        EventTrigger.Entry onMouseButtonDownEntry = new EventTrigger.Entry();
        onMouseButtonDownEntry.eventID = EventTriggerType.PointerClick;
        onMouseButtonDownEntry.callback.AddListener((BaseEventData data) => PointerClickAction((PointerEventData)data));
        eventTrigger.triggers.Add(onMouseButtonDownEntry);
        inSceneObjectData.inSceneObjectType = inSceneObjectType;
        UpdateInSceneObjectData();
    }

    //------------------------------ IN SCENE OBJECT FUNCTIONS -----------------------------//

    private void PointerClickAction(PointerEventData pointerEventData)
    {
        if (MapEditorInputManager.Instance.CurrentInputMode == INPUT_MODE.NONE)
        {
            MapEditorInputManager.Instance.SelectObject(this);
        }
    }

    public void ToggleColliderFor(bool targetBool, float duration)
    {
        StartCoroutine(ToggleColliderForCoroutine(targetBool, duration));
    }

    private IEnumerator ToggleColliderForCoroutine(bool targetBool, float duration)
    {
        col.enabled = targetBool;
        yield return new WaitForSeconds(duration);
        col.enabled = !targetBool;
    }

    public void UpdateInSceneObjectData()
    {
        inSceneObjectData.position = transform.position;
        inSceneObjectData.rotation = transform.rotation;
        inSceneObjectData.scale = transform.localScale;
    }
}

[Serializable]
public class InSceneObjectData
{
    public IN_SCENE_OBJECT_TYPES inSceneObjectType;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}