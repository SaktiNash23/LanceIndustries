using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInputManager : MonoBehaviour
{
    private static GameplayInputManager instance;
    public static GameplayInputManager Instance { get { return instance; } }

    [Header("SETTINGS")]
    [SerializeField] protected float dragThreshold = 1000f;
    [SerializeField] private LayerMask reflectorLayerMask;
    [SerializeField] private LayerMask reflectorPlacementBoxLayerMask;

    private Camera mainCamera;
    private Reflector selectedReflector;
    private Proto_Grid highlightedGrid;
    public bool SelectingReflector => selectedReflector;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public bool EnableInput { get; set; } = true;
    private Vector3 pointerDownStartPos = Vector2.zero;
    Reflector hitReflector;
    private void Update()
    {
        if (!EnableInput)
            return;

#if UNITY_EDITOR
        if (selectedReflector)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = selectedReflector.transform.position.z;
            selectedReflector.transform.position = mouseWorldPos;

            Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Highlight of Grid Outline
            if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorPlacementBoxLayerMask))
            {
                Proto_Grid gridOutline = hit.collider.GetComponent<Proto_Grid>();
                if (gridOutline && !gridOutline.IsOccupied)
                {
                    if (highlightedGrid != null && highlightedGrid != gridOutline)
                        highlightedGrid.ShowGrid(false);

                    gridOutline.ShowGrid(true, true);
                    highlightedGrid = gridOutline;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // If highlighted grid available, start the placement
                // Else cancel the placement
                if (highlightedGrid && !highlightedGrid.IsOccupied)
                {
                    selectedReflector.transform.position = highlightedGrid.transform.position;
                    selectedReflector.Build();
                    selectedReflector.OccupyGridOutline(highlightedGrid);
                    highlightedGrid.OccupyReflector(selectedReflector);
                    highlightedGrid.ShowGrid(false);
                    selectedReflector = null;
                    highlightedGrid = null;
                }
                else
                {
                    selectedReflector.Push();
                    selectedReflector = null;
                    highlightedGrid?.ShowGrid(false);
                    highlightedGrid = null;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                pointerDownStartPos = Input.mousePosition;
                Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorLayerMask))
                    hitReflector = hit.transform.GetComponentInParent<Reflector>();
            }
            else if (Input.GetMouseButton(0) && hitReflector)
            {
                if (Vector3.SqrMagnitude(pointerDownStartPos - Input.mousePosition) > dragThreshold && hitReflector.Interactable)
                {
                    SelectReflector(hitReflector);
                    hitReflector.OccupiedGridOutline.OccupyReflector(null);
                    hitReflector.OccupyGridOutline(null);
                    hitReflector = null;
                }
            }

            if (Input.GetMouseButtonUp(0) && hitReflector)
            {
                if (hitReflector.Interactable)
                    hitReflector?.Rotate90();
                hitReflector = null;
            }
        }
#elif UNITY_IOS || UNITY_ANDROID
        if(Input.touchCount <= 0)
            return;

        if (selectedReflector)
        {
            Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchWorldPos.z = selectedReflector.transform.position.z;
            selectedReflector.transform.position = touchWorldPos;

            Ray touchRay = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

            // Highlight of Grid Outline
            if (Physics.Raycast(touchRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorPlacementBoxLayerMask))
            {
                Proto_Grid gridOutline = hit.collider.GetComponent<Proto_Grid>();
                if (gridOutline && !gridOutline.IsOccupied)
                {
                    if (highlightedGrid != null && highlightedGrid != gridOutline)
                        highlightedGrid.ShowGrid(false);

                    gridOutline.ShowGrid(true);
                    highlightedGrid = gridOutline;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                // If highlighted grid available, start the placement
                // Else cancel the placement
                if (highlightedGrid)
                {
                    selectedReflector.transform.position = highlightedGrid.transform.position;
                    selectedReflector.Build();
                    selectedReflector.OccupyGridOutline(highlightedGrid);
                    highlightedGrid.OccupyReflector(selectedReflector);
                    highlightedGrid.ShowGrid(false);
                    selectedReflector = null;
                    highlightedGrid = null;
                }
                else
                {
                    selectedReflector.Push();
                    selectedReflector = null;
                    highlightedGrid?.ShowGrid(false);
                    highlightedGrid = null;
                }
            }
        }
        else
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                pointerDownStartPos = Input.mousePosition;
                Ray touchRay = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(touchRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorLayerMask))
                    hitReflector = hit.transform.GetComponentInParent<Reflector>();
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && hitReflector)
            {
                if (Vector2.SqrMagnitude(new Vector2(pointerDownStartPos.x, pointerDownStartPos.y) - Input.GetTouch(0).position) > dragThreshold && hitReflector.Interactable)
                {
                    SelectReflector(hitReflector);
                    hitReflector.OccupiedGridOutline.OccupyReflector(null);
                    hitReflector.OccupyGridOutline(null);
                    hitReflector = null;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended && hitReflector)
            {
                if (hitReflector.Interactable)
                    hitReflector?.Rotate90();
                hitReflector = null;
            }
        }
#endif
    }

    public void SelectReflector(Reflector targetReflector)
    {
        selectedReflector = targetReflector;
    }

    public void HighlightGridOutline(Proto_Grid gridOutline)
    {
        highlightedGrid = gridOutline;
    }

    public void Reset()
    {
        mainCamera = Camera.main;
        selectedReflector = null;
        highlightedGrid = null;
    }
}
