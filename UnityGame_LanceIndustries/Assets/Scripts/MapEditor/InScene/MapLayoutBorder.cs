using NaughtyAttributes;
using System.Linq;
using UnityEngine;

public enum SNAPPING_TYPE
{
    NONE = 0,
    HORIZONTAL = 1,
    VERTICAL = 2
}

public enum SNAPPING_DIR
{
    NONE = 0,
    LEFT = 1,
    RIGHT = 2,
    UP = 3,
    DOWN = 4
}

public class MapLayoutBorder : MonoBehaviour
{
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] SNAPPING_TYPE borderType;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] SNAPPING_DIR snappingDir;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float snappingDistance;
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
        Vector3 borderPos = transform.position;
        Vector3 offset = Vector3.zero;
        if(borderType == SNAPPING_TYPE.HORIZONTAL)
        {
            if (snappingDir == SNAPPING_DIR.UP)
                offset = new Vector3(0f, snappingDistance, 0f);
            else if(snappingDir == SNAPPING_DIR.DOWN)
                offset = new Vector3(0f, -snappingDistance, 0f);
        }
        else if(borderType == SNAPPING_TYPE.VERTICAL)
        {
            if (snappingDir == SNAPPING_DIR.LEFT)
                offset = new Vector3(-snappingDistance, 0f, 0f);
            else if (snappingDir == SNAPPING_DIR.RIGHT)
                offset = new Vector3(snappingDistance, 0f, 0f);
        }

        return borderPos + offset;
    }

    public void Initialization()
    {
        BoxCollider boxCol = GetComponent<BoxCollider>();
        Collider[] overlappedCols = Physics.OverlapBox(transform.position + boxCol.center, boxCol.size / 2f, transform.rotation, LayerMask.GetMask("MapEditorBorder"));
        
        if(overlappedCols.Length > 0)
        {
            if (overlappedCols[0].TryGetComponent(out MapEditorInSceneObject inSceneObj))
            {
                IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
                // Consider change to using bit operator (Don't do it for now as it will make the json data of old map wrong)
                bool isVerticalLine = objType == IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE 
                    || objType == IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE;
                bool isHorizontalLine = objType == IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE 
                    || objType == IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE;
                if (borderType == SNAPPING_TYPE.VERTICAL && isVerticalLine)
                {
                    if (!inSceneObj.SnappedTargetBorder)
                    {
                        GotSnappedObject = true;

                        inSceneObj.SnappedTargetBorder = this;
                        inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                        inSceneObj.InSceneObjData.borderDir = snappingDir;
                    }
                }
                else if (borderType == SNAPPING_TYPE.HORIZONTAL && isHorizontalLine)
                {
                    if (!inSceneObj.SnappedTargetBorder)
                    {
                        GotSnappedObject = true;

                        inSceneObj.SnappedTargetBorder = this;
                        inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                        inSceneObj.InSceneObjData.borderDir = snappingDir;
                    }
                }
            }
        }
        else
        {
            GotSnappedObject = false;
        }
    }

    public void CheckSnapping(MapEditorInSceneObject inSceneObj)
    {
        if (!GotSnappedObject)
        {
            IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
            bool isVerticalLine = objType == IN_SCENE_OBJECT_TYPES.NORMAL_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.RED_VERTICAL_LINE
    || objType == IN_SCENE_OBJECT_TYPES.YELLOW_VERTICAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_VERTICAL_LINE;
            bool isHorizontalLine = objType == IN_SCENE_OBJECT_TYPES.NORMAL_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.WHITE_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.RED_HORIZONTAL_LINE
                || objType == IN_SCENE_OBJECT_TYPES.YELLOW_HORIZONTAL_LINE || objType == IN_SCENE_OBJECT_TYPES.BLUE_HORIZONTAL_LINE;
            if (borderType == SNAPPING_TYPE.VERTICAL && isVerticalLine)
            {
                if (!inSceneObj.SnappedTargetBorder)
                {
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetBorder = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
                else
                {
                    inSceneObj.SnappedTargetBorder.GotSnappedObject = false;
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetBorder = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
            }
            else if (borderType == SNAPPING_TYPE.HORIZONTAL && isHorizontalLine)
            {
                if (!inSceneObj.SnappedTargetBorder)
                {
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetBorder = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
                else
                {
                    inSceneObj.SnappedTargetBorder.GotSnappedObject = false;
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetBorder = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
            }
        }
    }
}
