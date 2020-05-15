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
    public bool inGrid = false;
    private int layerMask;
    public bool isOccupied = false;
    public GameObject gridReference = null;
    private GameObject highlightedGrid = null;
    Color tempColor;

    #region Mouse Input Variables

    public bool mouseIsDown = false;
    public Vector2 currentMousePos;
    public Vector2 lastMousePos;
    public Vector2 deltaMousePos;

    public enum MousePhase
    {
        BEGAN,
        MOVED,
        STATIONARY,
        ENDED
    };

    public MousePhase mousePhase;

    public bool reflectorAttached = false;

    #endregion

    void Awake()
    {
        layerMask = LayerMask.GetMask("ReflectorPlacement");
    }

    #if UNITY_EDITOR

    void OnMouseUpAsButton()
    {
        if (GameManager.gameManagerInstance.DebugMode_PC == true)
        {
            //Used to be mouseIsDown == true Test Code
            if (reflectorAttached == true)
            {
                Debug.Log("HERE BOI");

                mousePhase = MousePhase.ENDED;

                #region MOUSE PHASE ENDED CODE
                hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                if (hit)
                {

                    if (hit.collider.tag == "Grid")
                    {
                        if (hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid == false)
                        {
                            Debug.Log("HITTT");

                            transform.position = hit.transform.position;

                            hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid = true;
                            hit.transform.gameObject.GetComponent<Proto_Grid>().reflectorStored_Grid = this.gameObject;
                            hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                            tempColor.a = 0.0f;
                            hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                            isOccupied = true;
                            gridReference = hit.transform.gameObject;

                            inGrid = true;
                        }
                    }
                    else
                    {
                        inGrid = false;
                        //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                        GameManager.gameManagerInstance.removeReflector(gameObject);
                        //Check which Reflector it is and return it to the stock / return back to the pool
                        GameManager.gameManagerInstance.returnReflectorToStock(gameObject);

                        GameManager.gameManagerInstance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                                   //for any reflectors that are still in the scene before this object gets 
                                                                                   //destroyed, else player cannot control the reflectors that are in the scene
                                                                                   //since their Box Colliders have not been reenabled.

                        //Destroy(gameObject); //Uncomment if using traditional Instantiate & Destroy. Object Pooling technique does not require this
                        Debug.Log(hit.transform.name);
                    }
                }
                else
                {
                    inGrid = false;
                    //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                    GameManager.gameManagerInstance.removeReflector(gameObject);
                    //Check which Reflector it is and return it to the stock / return back to the pool
                    GameManager.gameManagerInstance.returnReflectorToStock(gameObject);

                    GameManager.gameManagerInstance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                               //for any reflectors that are still in the scene before this object gets 
                                                                               //destroyed, else player cannot control the reflectors that are in the scene
                                                                               //since their Box Colliders have not been reenabled.

                    //Destroy(gameObject); //Uncomment if using traditional Instantiate & Destroy. Object Pooling technique does not require this
                    Debug.Log("Hit nothing");
                }

                #endregion

                if (GameManager.gameManagerInstance.activationToggle_Reflector == true)
                    GameManager.gameManagerInstance.resetReflectorColliders();

                GameManager.gameManagerInstance.activationToggle_Grid = false;

                isHoldingDown = false;
                mouseIsDown = false;

                //Below line is Test Code
                reflectorAttached = false;

            }         
            else if (reflectorAttached == false) //Used to be mouseIsDown == false Test Code
            {
                mouseIsDown = true;
                isHoldingDown = true;

                mousePhase = MousePhase.BEGAN;
                Debug.Log("BEGAN");

                if (GameManager.gameManagerInstance.activationToggle_Grid == false)
                {
                    //If this shape is currently attached to a grid
                    if (isOccupied == true)
                    {
                        gridReference.GetComponent<Proto_Grid>().reflectorStored_Grid = null;
                        gridReference.GetComponent<Proto_Grid>().isOccupied_Grid = false;
                        gridReference.GetComponent<SpriteRenderer>().color = Color.white;

                        inGrid = false;
                        gridReference = null;
                        isOccupied = false;

                    }
                    else if (isOccupied == false)
                    {
                        ;
                    }

                    GameManager.gameManagerInstance.toggleGridColliders();

                    GameManager.gameManagerInstance.activationToggle_Grid = true;
                }

                if (GameManager.gameManagerInstance.activationToggle_Reflector == false)
                    GameManager.gameManagerInstance.toggleReflectorColliders();

                reflectorAttached = true; //Test Code 
            }
        }

        //This if statement below was previously in OnMouseUp()
        if (GameManager.gameManagerInstance.DebugMode_PC == false)
        {
            if (inGrid && !isHoldingDown)
                rotateReflector(transform.rotation.eulerAngles.z);
        }
    }

    #endif

    void OnMouseUp()
    {
        
    }

    void OnMouseOver()
    {
        if (GameManager.gameManagerInstance.DebugMode_PC == false)
        {
            //New Test Code. The original code did not have if(Input.touchCount == 1)
            if (Input.touchCount == 1)
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
        }

    #if UNITY_EDITOR

        if(GameManager.gameManagerInstance.DebugMode_PC == true)
        {
            if(Input.GetMouseButtonUp(1))
            {
                if(inGrid)
                    rotateReflector(transform.rotation.eulerAngles.z);
            }
        }

    #endif

    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
        //Debug.Log("Activation Toggle Grid :" + GameManager.gameManagerInstance.activationToggle_Grid);
        //Debug.Log("isHoldingDown : " + isHoldingDown);
        //Debug.Log("MOUSE PHASE : " + mousePhase);
        Debug.Log("Reflector Attached : " + reflectorAttached);

        #if UNITY_EDITOR

        #region Mouse Input

        if (GameManager.gameManagerInstance.DebugMode_PC == true)
        {
            if (mouseIsDown == true)
            {
                isHoldingDown = true;
                currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                #region Calculate Mouse Position

                currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                deltaMousePos = currentMousePos - lastMousePos;
                lastMousePos = currentMousePos;

                if (deltaMousePos == Vector2.zero)
                {
                    mousePhase = MousePhase.STATIONARY;
                }
                else
                {
                    mousePhase = MousePhase.MOVED;
                }

                #endregion

                switch (mousePhase)
                {
                    #region STATIONARY

                    case MousePhase.STATIONARY:
                        transform.position = new Vector3(currentMousePos.x, currentMousePos.y, 0.0f);

                        hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                        if (hit)
                        {
                            if (hit.collider.tag == "Grid")
                            {
                                if (highlightedGrid != null)
                                {
                                    tempColor = highlightedGrid.GetComponent<SpriteRenderer>().color;
                                    tempColor.a = 0.0f;
                                    highlightedGrid.GetComponent<SpriteRenderer>().color = tempColor;
                                }

                                highlightedGrid = hit.transform.gameObject;

                                tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                tempColor.a = 1.0f;
                                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                            }
                            else
                            {
                                GameManager.gameManagerInstance.resetGridAlpha();
                            }
                        }
                        else
                            GameManager.gameManagerInstance.resetGridAlpha();
                        break;

                    #endregion

                    #region MOVED   

                    case MousePhase.MOVED:
                        transform.position = new Vector3(currentMousePos.x, currentMousePos.y, 0.0f);

                        hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                        if (hit)
                        {
                            if (hit.collider.tag == "Grid")
                            {
                                if (highlightedGrid != null)
                                {
                                    tempColor = highlightedGrid.GetComponent<SpriteRenderer>().color;
                                    tempColor.a = 0.0f;
                                    highlightedGrid.GetComponent<SpriteRenderer>().color = tempColor;
                                }

                                highlightedGrid = hit.transform.gameObject;
                                tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                tempColor.a = 1.0f;
                                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                            }
                            else
                            {
                                GameManager.gameManagerInstance.resetGridAlpha();
                            }
                        }
                        else
                            GameManager.gameManagerInstance.resetGridAlpha();
                        break;

                        #endregion
                }
            }
        }

        #endregion

        #endif

        #region Touch Input

        if (GameManager.gameManagerInstance.DebugMode_PC == false)
        {

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
                            gridReference.GetComponent<SpriteRenderer>().color = Color.white;

                            inGrid = false;

                            gridReference = null;
                            isOccupied = false;

                        }
                        else if (isOccupied == false)
                        {
                            ;
                        }

                        GameManager.gameManagerInstance.toggleGridColliders();
                    }

                    if (GameManager.gameManagerInstance.activationToggle_Reflector == false)
                        GameManager.gameManagerInstance.toggleReflectorColliders();

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            //Orthographic Cam Code
                            point = Camera.main.ScreenToWorldPoint(touch.position);
                            transform.position = new Vector3(point.x, point.y, 0.0f);

                            break;

                        case TouchPhase.Moved:
                            //Orthographic Cam Test Code
                            point = Camera.main.ScreenToWorldPoint(touch.position);
                            transform.position = new Vector3(point.x, point.y);
                            hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);


                            if (hit)
                            {
                                if (hit.collider.tag == "Grid")
                                {
                                    if (highlightedGrid != null)
                                    {
                                        tempColor = highlightedGrid.GetComponent<SpriteRenderer>().color;
                                        tempColor.a = 0.0f;
                                        highlightedGrid.GetComponent<SpriteRenderer>().color = tempColor;
                                    }

                                    highlightedGrid = hit.transform.gameObject;
                                    tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                    tempColor.a = 1.0f;
                                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                                }
                                else
                                {
                                    GameManager.gameManagerInstance.resetGridAlpha();
                                }
                            }
                            else
                                GameManager.gameManagerInstance.resetGridAlpha();

                            break;

                        case TouchPhase.Stationary:
                            hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                            if (hit)
                            {
                                if (hit.collider.tag == "Grid")
                                {
                                    if (highlightedGrid != null)
                                    {
                                        tempColor = highlightedGrid.GetComponent<SpriteRenderer>().color;
                                        tempColor.a = 0.0f;
                                        highlightedGrid.GetComponent<SpriteRenderer>().color = tempColor;
                                    }

                                    highlightedGrid = hit.transform.gameObject;

                                    tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                    tempColor.a = 1.0f;
                                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                                }
                                else
                                {
                                    GameManager.gameManagerInstance.resetGridAlpha();
                                }
                            }
                            else
                                GameManager.gameManagerInstance.resetGridAlpha();

                            break;

                        case TouchPhase.Ended:
                            hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                            if (hit)
                            {

                                if (hit.collider.tag == "Grid")
                                {
                                    if (hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid == false)
                                    {
                                        transform.position = hit.transform.position;

                                        hit.transform.gameObject.GetComponent<Proto_Grid>().isOccupied_Grid = true;
                                        hit.transform.gameObject.GetComponent<Proto_Grid>().reflectorStored_Grid = this.gameObject;
                                        hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                        tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                        tempColor.a = 0.0f;
                                        hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                                        isOccupied = true;
                                        gridReference = hit.transform.gameObject;

                                        inGrid = true;
                                    }
                                }
                                else
                                {
                                    inGrid = false;
                                    //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                                    GameManager.gameManagerInstance.removeReflector(gameObject);
                                    //Check which Reflector it is and return it to the stock / return back to the pool
                                    GameManager.gameManagerInstance.returnReflectorToStock(gameObject);

                                    GameManager.gameManagerInstance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                                               //for any reflectors that are still in the scene before this object gets 
                                                                                               //destroyed, else player cannot control the reflectors that are in the scene
                                                                                               //since their Box Colliders have not been reenabled.

                                    //Destroy(gameObject); //Uncomment if using traditional Instantiate & Destroy. Object Pooling technique does not require this
                                    Debug.Log(hit.transform.name);
                                }
                            }
                            else
                            {
                                inGrid = false;
                                //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                                GameManager.gameManagerInstance.removeReflector(gameObject);
                                //Check which Reflector it is and return it to the stock / return back to the pool
                                GameManager.gameManagerInstance.returnReflectorToStock(gameObject);

                                GameManager.gameManagerInstance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                                           //for any reflectors that are still in the scene before this object gets 
                                                                                           //destroyed, else player cannot control the reflectors that are in the scene
                                                                                           //since their Box Colliders have not been reenabled.

                                //Destroy(gameObject); //Uncomment if using traditional Instantiate & Destroy. Object Pooling technique does not require this
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

                    if (GameManager.gameManagerInstance.activationToggle_Reflector == true)
                        GameManager.gameManagerInstance.resetReflectorColliders();

                    GameManager.gameManagerInstance.activationToggle_Grid = false;

                }
            }

        }

        #endregion
    }

    private void rotateReflector(float zRotation)
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
