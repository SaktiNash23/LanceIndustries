using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class MapLayoutPortalSnapper : MonoBehaviour
{
    [BoxGroup("MAP LAYOUT PORTAL SNAPPER SETTINGS")] [SerializeField] SNAPPING_TYPE portalSnappingType;
    [BoxGroup("MAP LAYOUT PORTAL SNAPPER SETTINGS")] [SerializeField] SNAPPING_DIR snappingDir;
    [BoxGroup("MAP LAYOUT PORTAL SNAPPER SETTINGS")] [SerializeField] float snappingDistance;
    [BoxGroup("MAP LAYOUT PORTAL SNAPPER SETTINGS")] [SerializeField] float unsnapDistance;

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
        if (portalSnappingType == SNAPPING_TYPE.HORIZONTAL)
        {
            if (snappingDir == SNAPPING_DIR.UP)
                offset = new Vector3(0f, snappingDistance, 0f);
            else if (snappingDir == SNAPPING_DIR.DOWN)
                offset = new Vector3(0f, -snappingDistance, 0f);
        }
        else if (portalSnappingType == SNAPPING_TYPE.VERTICAL)
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
        Collider[] overlappedCols = Physics.OverlapBox(transform.position + boxCol.center, boxCol.size / 2f, transform.rotation, LayerMask.GetMask("MapEditorPortal"));

        if (overlappedCols.Length > 0)
        {
            if (overlappedCols[0].TryGetComponent(out MapEditorInSceneObject inSceneObj))
            {
                IN_SCENE_OBJECT_TYPES objType = inSceneObj.inSceneObjectType;
                bool isVerticalPortal = objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL;
                bool isHorizontalPortal = objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL;
                if (portalSnappingType == SNAPPING_TYPE.VERTICAL && isVerticalPortal)
                {
                    if (!inSceneObj.SnappedTargetPortalSnapper)
                    {
                        GotSnappedObject = true;

                        inSceneObj.SnappedTargetPortalSnapper = this;
                        inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                        inSceneObj.InSceneObjData.borderDir = snappingDir;
                    }
                }
                else if (portalSnappingType == SNAPPING_TYPE.HORIZONTAL && isHorizontalPortal)
                {
                    if (!inSceneObj.SnappedTargetPortalSnapper)
                    {
                        GotSnappedObject = true;

                        inSceneObj.SnappedTargetPortalSnapper = this;
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
            bool isVerticalPortal = objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_VERTICAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_VERTICAL;
            bool isHorizontalPortal = objType == IN_SCENE_OBJECT_TYPES.PORTAL_1ST_SET_HORIZONTAL || objType == IN_SCENE_OBJECT_TYPES.PORTAL_2ND_SET_HORIZONTAL;
            if (portalSnappingType == SNAPPING_TYPE.VERTICAL && isVerticalPortal)
            {
                if (!inSceneObj.SnappedTargetPortalSnapper)
                {
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetPortalSnapper = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
                else
                {
                    inSceneObj.SnappedTargetPortalSnapper.GotSnappedObject = false;
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetPortalSnapper = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
            }
            else if (portalSnappingType == SNAPPING_TYPE.HORIZONTAL && isHorizontalPortal)
            {
                if (!inSceneObj.SnappedTargetPortalSnapper)
                {
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetPortalSnapper = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
                else
                {
                    inSceneObj.SnappedTargetPortalSnapper.GotSnappedObject = false;
                    GotSnappedObject = true;

                    inSceneObj.SnappedTargetPortalSnapper = this;
                    inSceneObj.InSceneObjData.mapGridIndex = transform.GetComponentInParent<MapGridMapEditor>().MapGridIndex;
                    inSceneObj.InSceneObjData.borderDir = snappingDir;
                }
            }
        }
    }
}
