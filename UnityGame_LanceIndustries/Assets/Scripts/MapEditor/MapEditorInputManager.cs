using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum INPUT_MODE
{
    NONE = 0,
    SELECTING = 1
}

public class MapEditorInputManager : MonoBehaviour
{
    public INPUT_MODE CurrentInputMode { get; set; } = INPUT_MODE.NONE;

    private Camera cachedMainCamera = null;

    private static MapEditorInputManager _instance;
    public static MapEditorInputManager Instance
    {
        get { return _instance; }
    }

    private MapEditorInSceneObject selectingObject = null;

    [BoxGroup("INPUT MANAGER SETTINGS")] [SerializeField] float mapEditorScenePosZ;

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
        if(CurrentInputMode == INPUT_MODE.SELECTING)
        {
            Vector3 cameraPos = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
            selectingObject.transform.position = cameraPos;
            if(Input.GetMouseButtonDown(0))
            {
                SwitchInputMode(INPUT_MODE.NONE);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Destroy(selectingObject.gameObject);
                selectingObject = null;
                SwitchInputMode(INPUT_MODE.NONE);
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
                    selectingObject.ToggleColliderFor(false, 0.1f);
                    selectingObject.UpdateInSceneObjectData();
                }
                selectingObject = null;
                break;
        }
    }

    public Vector3 GetMouseCursorPosWorldVector3()
    {
        Vector3 mouseVec3 = cachedMainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cachedMainCamera.farClipPlane));
        return mouseVec3;
    }

    public void SelectObject(MapEditorInSceneObject selectedObj)
    {
        SwitchInputMode(INPUT_MODE.SELECTING);
        selectingObject = selectedObj;
    }
}
