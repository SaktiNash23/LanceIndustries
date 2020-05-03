using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapLayoutBoxSnapper : MonoBehaviour
{
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float unsnapDistance;

    public bool GotSnappedObject { get; set; } = false;

    public float UnsnapDistance
    {
        get
        {
            return unsnapDistance;
        }
    }

    public Vector3 GetSnappingPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        MapEditorInSceneObject inSceneObj = null;
        if (!GotSnappedObject)
        {
            if (other.TryGetComponent<MapEditorInSceneObject>(out inSceneObj))
            {
                if (MapEditorInputManager.Instance.GetSelectingInSceneObject() == inSceneObj)
                {
                    if (inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.ORIGIN_POINT || inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_WHITE ||
                        inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_RED || inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_YELLOW ||
                        inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.DESTINATION_POINT_BLUE)
                    {
                        GotSnappedObject = true;
                        inSceneObj.SnappedTargetBox = this;
                    }
                }
            }
        }
    }
}
