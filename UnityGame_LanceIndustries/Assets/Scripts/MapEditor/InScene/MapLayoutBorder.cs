using NaughtyAttributes;
using UnityEngine;

public enum MAP_LAYOUT_BORDER_TYPE
{
    NONE = 0,
    HORIZONTAL_BORDER = 1,
    VERTICAL_BORDER = 2
}

public enum SNAPPING_DIRECTION
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    UP = 3,
    DOWN = 4
}

public class MapLayoutBorder : MonoBehaviour
{
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] MAP_LAYOUT_BORDER_TYPE borderType;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] SNAPPING_DIRECTION snappingDir;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float snappingDistance;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float unsnapDistance;

    public float UnsnapDistance
    {
        get
        {
            return unsnapDistance;
        }
    }

    public Vector3 GetSnappingPosition()
    {
        Vector3 borderPos = transform.position;
        Vector3 offset = Vector3.zero;
        if(borderType == MAP_LAYOUT_BORDER_TYPE.HORIZONTAL_BORDER)
        {
            if (snappingDir == SNAPPING_DIRECTION.UP)
                offset = new Vector3(0f, snappingDistance, 0f);
            else if(snappingDir == SNAPPING_DIRECTION.DOWN)
                offset = new Vector3(0f, -snappingDistance, 0f);
        }
        else if(borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER)
        {
            if (snappingDir == SNAPPING_DIRECTION.LEFT)
                offset = new Vector3(-snappingDistance, 0f, 0f);
            else if (snappingDir == SNAPPING_DIRECTION.RIGHT)
                offset = new Vector3(snappingDistance, 0f, 0f);
        }

        return borderPos + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        MapEditorInSceneObject inSceneObj = null;
        if (other.TryGetComponent<MapEditorInSceneObject>(out inSceneObj))
        {
            if (MapEditorInputManager.Instance.GetSelectingInSceneObject() == inSceneObj)
            {
                if (borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.VERTICAL_LINE)
                {
                    if (!inSceneObj.SnappedTarget)
                    {
                        inSceneObj.SnappedTarget = this;
                    }
                }
                else if (borderType == MAP_LAYOUT_BORDER_TYPE.HORIZONTAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE)
                {
                    if (!inSceneObj.SnappedTarget)
                    {
                        inSceneObj.SnappedTarget = this;
                    }
                }
            }
        }
    }
}
