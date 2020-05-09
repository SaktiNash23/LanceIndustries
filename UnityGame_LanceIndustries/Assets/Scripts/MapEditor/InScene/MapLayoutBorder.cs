using NaughtyAttributes;
using UnityEngine;

public enum MAP_LAYOUT_BORDER_TYPE
{
    NONE = 0,
    HORIZONTAL_BORDER = 1,
    VERTICAL_BORDER = 2
}

public enum BORDER_DIRECTION
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
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] BORDER_DIRECTION snappingDir;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float snappingDistance;
    [BoxGroup("MAP LAYOUT BORDER SETTINGS")] [SerializeField] float unsnapDistance;

    public bool GotSnappedObject = false;

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
            if (snappingDir == BORDER_DIRECTION.UP)
                offset = new Vector3(0f, snappingDistance, 0f);
            else if(snappingDir == BORDER_DIRECTION.DOWN)
                offset = new Vector3(0f, -snappingDistance, 0f);
        }
        else if(borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER)
        {
            if (snappingDir == BORDER_DIRECTION.LEFT)
                offset = new Vector3(-snappingDistance, 0f, 0f);
            else if (snappingDir == BORDER_DIRECTION.RIGHT)
                offset = new Vector3(snappingDistance, 0f, 0f);
        }

        return borderPos + offset;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    MapEditorInSceneObject inSceneObj = null;
    //    if (!GotSnappedObject)
    //    {
    //        if (other.TryGetComponent(out inSceneObj))
    //        {
    //            if (borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.VERTICAL_LINE)
    //            {
    //                if (!inSceneObj.SnappedTargetBorder)
    //                {
    //                    GotSnappedObject = true;

    //                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
    //                    inSceneObj.InSceneObjData.borderDir = snappingDir;
    //                }
    //            }
    //            else if (borderType == MAP_LAYOUT_BORDER_TYPE.HORIZONTAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE)
    //            {
    //                if (!inSceneObj.SnappedTargetBorder)
    //                {
    //                    GotSnappedObject = true;

    //                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
    //                    inSceneObj.InSceneObjData.borderDir = snappingDir;
    //                }
    //            }
    //        }
    //    }
    //}

    public void Initialization()
    {
        BoxCollider boxCol = GetComponent<BoxCollider>();
        Collider[] overlappedCols = Physics.OverlapBox(transform.position + boxCol.center, boxCol.size / 2f, transform.rotation, LayerMask.GetMask("MapEditorInSceneObject"));
        if(overlappedCols.Length > 0)
        {
            if (overlappedCols[0].TryGetComponent(out MapEditorInSceneObject inSceneObj))
            {
                if (borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.VERTICAL_LINE)
                {
                    if (!inSceneObj.SnappedTargetBorder)
                    {
                        GotSnappedObject = true;

                        inSceneObj.SnappedTargetBorder = this;
                        inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                        inSceneObj.InSceneObjData.borderDir = snappingDir;
                    }
                }
                else if (borderType == MAP_LAYOUT_BORDER_TYPE.HORIZONTAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE)
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
            if (borderType == MAP_LAYOUT_BORDER_TYPE.VERTICAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.VERTICAL_LINE)
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
            else if (borderType == MAP_LAYOUT_BORDER_TYPE.HORIZONTAL_BORDER && inSceneObj.inSceneObjectType == IN_SCENE_OBJECT_TYPES.HORIZONTAL_LINE)
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
