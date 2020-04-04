using UnityEngine;

public class Raycast : MonoBehaviour
{
    
    private Vector3 point;
    private Vector3 startPos;
    public bool isHoldingDown = false;
    public float rayLength;
    public float timeUntilHold;
    public float minTimeHold;
    private RaycastHit2D hit;
    RaycastHit2D[] hitArr;
    public bool inGrid = false;
    private int layerMask;
    public bool isOccupied = false;
    public GameObject gridReference = null;

    void Awake()
    {
        layerMask = LayerMask.GetMask("ReflectorPlacement");
    }

    void OnMouseUp()
    {
        if (inGrid && !isHoldingDown)
            rotateReflector(transform.rotation.eulerAngles.z);
    }

    void OnMouseOver()
    {
        
        if (timeUntilHold < minTimeHold)
        {
            timeUntilHold += Time.deltaTime;
        }
        else if (timeUntilHold >= minTimeHold)
        {
            isHoldingDown = true;
        }
        
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);

        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Touch touch = Input.GetTouch(0);

            if (isHoldingDown == true)
            {
                if (GameManager.gameManagerInstance.activationToggle_Grid == false)
                {
                    //If this shape is currently attached to a grid
                    if (isOccupied == true)
                    {
                        gridReference.GetComponent<Proto_Grid>().reflectorStored_Grid = null;
                        gridReference.GetComponent<Proto_Grid>().isOccupied_Grid = false;

                        inGrid = false;

                        gridReference = null;
                        isOccupied = false;

                    }
                    else if (isOccupied == false)
                    {
                        Debug.Log("This reflector is not attached to any grid");
                    }

                    GameManager.gameManagerInstance.toggleGridColliders();
                }

                if (GameManager.gameManagerInstance.activationToggle_Reflector == false)
                    GameManager.gameManagerInstance.toggleReflectorColliders();

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        break;

                    case TouchPhase.Moved:
                        point = Camera.main.ScreenToWorldPoint(touch.position);
                        transform.position = new Vector3(point.x, point.y);
                        break;

                    case TouchPhase.Ended:
                        hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                        if (hit)
                        {

                            if (hit.collider.tag == "Grid")
                            {
                                Debug.Log("GRID");
                                
                                if (hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid == false)
                                {
                                    transform.position = hit.transform.position;

                                    hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid = true;
                                    hit.transform.gameObject.GetComponent<Proto_Grid>().reflectorStored_Grid = this.gameObject;
                                    hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                                    isOccupied = true;
                                    gridReference = hit.transform.gameObject;

                                    inGrid = true;
                                }
                            }
                            else
                            {   
                                inGrid = false;                   
                                Debug.Log(hit.transform.name);
                            }
                        }
                        else
                        {
                            inGrid = false;
                            Debug.Log("Hit nothing");
                        }

                        isHoldingDown = false;
                        break;
                }
            }
        }
        else
        {
            if (timeUntilHold != 0.0f)
            {
                timeUntilHold = 0.0f;
                isHoldingDown = false;
                GameManager.gameManagerInstance.resetReflectorColliders();
                GameManager.gameManagerInstance.activationToggle_Grid = false;

            }
        }
    }



    public void rotateReflector(float zRotation)
    {
        if (zRotation != 270.0f)
            transform.Rotate(0.0f, 0.0f, 90.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }


    public bool isHoldingDownAccessor
    {
        get
        {
            return isHoldingDown;
        }

        set
        {
            isHoldingDown = value;
        }
    }

}
