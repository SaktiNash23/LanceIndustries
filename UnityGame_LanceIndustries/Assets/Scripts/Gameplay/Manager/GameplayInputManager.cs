using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInputManager : MonoBehaviour
{
    private static GameplayInputManager instance;
    public static GameplayInputManager Instance { get { return instance; } }

    [Header("SETTINGS")]
    [SerializeField] private LayerMask reflectorLayerMask;
    [SerializeField] private LayerMask reflectorPlacementBoxLayerMask;

    private Camera mainCamera;
    private Reflector selectedReflector;
    private Proto_Grid highlightedGrid;

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
            DontDestroyOnLoad(instance);
        }
    }

    private bool acceptInput = true;
    private void Update()
    {
        if (!acceptInput)
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

                    gridOutline.ShowGrid(true);
                    highlightedGrid = gridOutline;
                }
            }

            // Placement of Reflector
            if (Input.GetMouseButtonDown(0))
            {
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
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorLayerMask))
                {
                    Reflector reflector = hit.transform.GetComponentInParent<Reflector>();
                    if (reflector && reflector.Interactable)
                    {
                        reflector.OccupiedGridOutline.OccupyReflector(null);
                        reflector.OccupyGridOutline(null);
                        SelectReflector(reflector);
                    }
                }
            }
            else if(Input.GetMouseButtonDown(1))
            {
                Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Abs(mainCamera.transform.position.z), reflectorLayerMask))
                {
                    Reflector reflector = hit.transform.GetComponentInParent<Reflector>();
                    if(reflector && reflector.Interactable)
                        reflector.Rotate90();
                }
            }
        }
#elif UNITY_IOS || UNITY_ANDROID

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
}
