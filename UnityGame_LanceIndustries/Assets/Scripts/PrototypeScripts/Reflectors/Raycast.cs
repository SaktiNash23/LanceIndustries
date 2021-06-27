 using UnityEngine;
public class Raycast : MonoBehaviour
{ 
    private Vector3 point;
    private Vector3 startPos;
    public bool isHoldingDown = false;
    public float rayLength;
    public float timeUntilHold;
    public float minTimeHold;
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

    void OnMouseUpAsButton()
    {
        if (GameManager.Instance.IsGamePaused == false)
        {
            //#if UNITY_EDITOR

            if (GameManager.Instance.DebugMode_PC == true)
            {
                //Used to be mouseIsDown == true (Test Code)     
                //When mouse button is released, if there is a reflector 'attached' to the mouse, run this code
                if (reflectorAttached == true)
                {
                    mousePhase = MousePhase.ENDED;

                    #region MOUSE PHASE ENDED CODE
                    // hit = Physics.Raycast(transform.position, -transform.up, rayLength, layerMask);

                    if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, rayLength, layerMask))
                    {
                        Debug.Log("HIT OBJ: " + hit.transform.name);
                        if (hit.collider.tag == "Grid")
                        {
                            if (hit.transform.gameObject.GetComponent<Proto_Grid>().IsOccupied == false)
                            {
                                transform.position = hit.transform.position;

                                hit.transform.gameObject.GetComponent<Proto_Grid>().IsOccupied = true;
                                hit.transform.gameObject.GetComponent<Proto_Grid>().reflectorStored_Grid = this.gameObject;
                                hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                tempColor.a = 0.0f;
                                hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                                isOccupied = true;
                                gridReference = hit.transform.gameObject;

                                inGrid = true;

                                gameObject.GetComponent<ReflectorAnimation>().activateBuildAnimation(gameObject.transform.rotation.eulerAngles.z); //Displays the hammer building animation
                            }
                        }
                        else
                        {
                            inGrid = false;
                            //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                            GameManager.Instance.removeReflector(gameObject);
                            //Check which Reflector it is and return it to the stock / return back to the pool
                            GameManager.Instance.returnReflectorToStock(gameObject);

                            GameManager.Instance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
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
                        GameManager.Instance.removeReflector(gameObject);
                        //Check which Reflector it is and return it to the stock / return back to the pool
                        GameManager.Instance.returnReflectorToStock(gameObject);

                        GameManager.Instance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                                   //for any reflectors that are still in the scene before this object gets 
                                                                                   //destroyed, else player cannot control the reflectors that are in the scene
                                                                                   //since their Box Colliders have not been reenabled.

                        //Destroy(gameObject); //Uncomment if using traditional Instantiate & Destroy. Object Pooling technique does not require this
                    }

                    #endregion

                    if (GameManager.Instance.activationToggle_Reflector == true)
                        GameManager.Instance.resetReflectorColliders();

                    GameManager.Instance.activationToggle_Grid = false;

                    isHoldingDown = false;
                    mouseIsDown = false;

                    reflectorAttached = false;

                }

                //Used to be mouseIsDown == false (Test Code).  
                //When mouse button is released, if there is no reflector 'attached' to the mouse, run this code
                else if (reflectorAttached == false)
                {
                    mouseIsDown = true;
                    isHoldingDown = true;

                    mousePhase = MousePhase.BEGAN;

                    if (GameManager.Instance.activationToggle_Grid == false)
                    {
                        //If this shape is currently attached to a grid
                        if (isOccupied == true)
                        {
                            gridReference.GetComponent<Proto_Grid>().reflectorStored_Grid = null;
                            gridReference.GetComponent<Proto_Grid>().IsOccupied = false;
                            gridReference.GetComponent<SpriteRenderer>().color = Color.white;

                            inGrid = false;
                            gridReference = null;
                            isOccupied = false;

                        }
                        else if (isOccupied == false)
                        {
                            ;
                        }

                        GameManager.Instance.toggleGridColliders();

                        GameManager.Instance.activationToggle_Grid = true;
                    }

                    if (GameManager.Instance.activationToggle_Reflector == false)
                        GameManager.Instance.toggleReflectorColliders();

                    reflectorAttached = true; //Test Code 
                }
            }

            if (GameManager.Instance.DebugMode_PC == false)
            {
                if (inGrid && !isHoldingDown)
                {
                    if (gameObject.name.Contains("ThreeWay"))
                    {
                        //Do nothing
                    }
                    else
                    {
                        rotateReflector(transform.rotation.eulerAngles.z);
                    }
                }
            }

            //#endif
        }
    }

    void OnMouseOver()
    {
        if (GameManager.Instance.IsGamePaused == false)
        {

            #if UNITY_ANDROID

            //The original code did not have if(Input.touchCount == 1)
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

            #endif

            #if UNITY_EDITOR

            if (GameManager.Instance.DebugMode_PC == true)
            {
                if (Input.GetMouseButtonUp(1))
                {
                    if (inGrid)
                    {
                        if (gameObject.name.Contains("ThreeWay"))
                        {
                            //Do nothing
                        }
                        else
                        {
                            rotateReflector(transform.rotation.eulerAngles.z); //Original
                        }
                    }
                }
            }

            #endif

        }
    }

    private void Update()
    {
        #if UNITY_EDITOR
        //Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
        //Debug.Log("Activation Toggle Grid :" + GameManager.gameManagerInstance.activationToggle_Grid);
        //Debug.Log("isHoldingDown : " + isHoldingDown);
        //Debug.Log("MOUSE PHASE : " + mousePhase);
        //Debug.Log("Reflector Attached : " + reflectorAttached);
        #endif

        if (GameManager.Instance.IsGamePaused == false)
        {
            #if UNITY_EDITOR

            //All the following code that is within the #region Mouse Input is only to control the movement of any reflector attached to the mouse
            #region Mouse Input

            if (GameManager.Instance.DebugMode_PC == true)
            {
                Debug.DrawRay(transform.position - Vector3.forward, Vector3.forward * rayLength, Color.green, 0.0f);
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

                    RaycastHit hit;
                    switch (mousePhase)
                    {
                        #region STATIONARY

                        case MousePhase.STATIONARY:
                            transform.position = new Vector3(currentMousePos.x, currentMousePos.y, 0.0f);

                            // hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                            if (Physics.Raycast(transform.position - Vector3.forward, Vector3.forward, out hit, rayLength, layerMask))
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
                                    GameManager.Instance.ResetGridAlpha();
                                }
                            }
                            else
                                GameManager.Instance.ResetGridAlpha();
                            break;

                        #endregion

                        #region MOVED   

                        case MousePhase.MOVED:
                            transform.position = new Vector3(currentMousePos.x, currentMousePos.y, 0.0f);

                            // hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                            if (Physics.Raycast(transform.position - Vector3.forward, Vector3.forward, out hit, rayLength, layerMask))
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
                                    GameManager.Instance.ResetGridAlpha();
                                }
                            }
                            else
                                GameManager.Instance.ResetGridAlpha();
                            break;

                            #endregion
                    }
                }
            }

            #endregion

            #endif

            //ATTN: Remember that any code you add in the Mouse Input must also be added in the respective parts for the Touch Input
            #region Touch Input

            if (GameManager.Instance.DebugMode_PC == false)
            {
                if (Input.touchCount > 0)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    Touch touch = Input.GetTouch(0);

                    if (isHoldingDown == true)
                    {
                        if (GameManager.Instance.activationToggle_Grid == false)//Purpose of this bool is to ensure the code only runs once in Update()
                        {
                            //If the reflector is currently attached to a grid
                            if (isOccupied == true)
                            {
                                gridReference.GetComponent<Proto_Grid>().reflectorStored_Grid = null;
                                gridReference.GetComponent<Proto_Grid>().IsOccupied = false;
                                gridReference.GetComponent<SpriteRenderer>().color = Color.white;

                                inGrid = false;

                                gridReference = null;
                                isOccupied = false;

                            }
                            else if (isOccupied == false)
                            {
                                ;
                            }

                            GameManager.Instance.toggleGridColliders(); //If a grid is occupied, deactivate its 2D Collider and vice versa
                        }

                        if (GameManager.Instance.activationToggle_Reflector == false) //Purpose of this bool is to ensure the code only runs once in Update()
                            GameManager.Instance.toggleReflectorColliders();

                        RaycastHit hit;
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                point = Camera.main.ScreenToWorldPoint(touch.position);
                                transform.position = new Vector3(point.x, point.y, 0.0f);

                                break;

                            case TouchPhase.Moved:
                                point = Camera.main.ScreenToWorldPoint(touch.position);
                                transform.position = new Vector3(point.x, point.y);
                                // hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                                if (Physics.Raycast(transform.position - Vector3.forward, Vector3.forward, out hit, rayLength, layerMask))
                                {
                                    if (hit.collider.tag == "Grid")//If reflector collides with Grid, highlight the grid outline (blue box)
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
                                        GameManager.Instance.ResetGridAlpha(); //Reset all the alpha values of all grids to 0 when reflecto not hitting any grid
                                    }
                                }
                                else
                                    GameManager.Instance.ResetGridAlpha(); //Reset all the alpha values of all grids to 0 when reflector not hitting any grid

                                break;

                            case TouchPhase.Stationary:
                                // hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                                if (Physics.Raycast(transform.position - Vector3.forward, Vector3.forward, out hit, rayLength, layerMask))
                                {
                                    if (hit.collider.tag == "Grid") //If reflector collides with Grid, highlight the grid outline (blue box)
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
                                        GameManager.Instance.ResetGridAlpha();
                                    }
                                }
                                else
                                    GameManager.Instance.ResetGridAlpha();

                                break;

                            case TouchPhase.Ended:
                                // hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                                if (Physics.Raycast(transform.position - Vector3.forward, Vector3.forward, out hit, rayLength, layerMask))
                                {
                                    if (hit.collider.tag == "Grid")//If reflector is let go over a grid, place the reflector in the grid and perform other related actions
                                    {
                                        if (hit.transform.gameObject.GetComponent<Proto_Grid>().IsOccupied == false)
                                        {
                                            transform.position = hit.transform.position;

                                            hit.transform.gameObject.GetComponent<Proto_Grid>().IsOccupied = true;
                                            hit.transform.gameObject.GetComponent<Proto_Grid>().reflectorStored_Grid = this.gameObject;
                                            hit.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                                            tempColor = hit.transform.gameObject.GetComponent<SpriteRenderer>().color;
                                            tempColor.a = 0.0f;
                                            hit.transform.gameObject.GetComponent<SpriteRenderer>().color = tempColor;

                                            isOccupied = true;
                                            gridReference = hit.transform.gameObject;

                                            inGrid = true;

                                            gameObject.GetComponent<ReflectorAnimation>().activateBuildAnimation(gameObject.transform.rotation.eulerAngles.z); //Displays the hammer building animation
                                        }
                                    }
                                    else
                                    {
                                        inGrid = false;
                                        //Remove Reflector from allReflectorsInScene List from GameManager before destroying it
                                        GameManager.Instance.removeReflector(gameObject);
                                        //Check which Reflector it is and return it to the stock / return back to the pool
                                        GameManager.Instance.returnReflectorToStock(gameObject);

                                        GameManager.Instance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
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
                                    GameManager.Instance.removeReflector(gameObject);
                                    //Check which Reflector it is and return it to the stock / return back to the pool
                                    GameManager.Instance.returnReflectorToStock(gameObject);

                                    GameManager.Instance.resetReflectorColliders(); //If the reflector hits an invalid spot, we need to reset the colliders
                                                                                               //for any reflectors that are still in the scene before this object gets 
                                                                                               //destroyed, else player cannot control the reflectors that are in the scene
                                                                                               //since their Box Colliders have not been reenabled.

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

                        if (GameManager.Instance.activationToggle_Reflector == true)
                            GameManager.Instance.resetReflectorColliders(); //Reactivates all reflector's 2D Colliders that are in the grids

                        GameManager.Instance.activationToggle_Grid = false; //Reset the bool for the next time the function, toggleGridColliders() is called

                    }
                }

            }

            #endregion

        }
    }

    private void rotateReflector(float zRotation)
    {
        if (zRotation != 270.0f)
        {
            transform.Rotate(0.0f, 0.0f, 90.0f);
            transform.Find("ReferencePoint").localRotation = Quaternion.Euler(0.0f, 0.0f, transform.Find("ReferencePoint").localEulerAngles.z + 90.0f);
            //Debug.Log("ROTATE REFLECTOR");        
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            transform.Find("ReferencePoint").localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }

        switch (transform.rotation.eulerAngles.z /*transform.Find("ReferencePoint").localEulerAngles.z*/)
        {
            case 0.0f:
                GetComponent<Animator>().SetFloat("currentRotation", 0.0f);
                break;

            case 90.0f:
                GetComponent<Animator>().SetFloat("currentRotation", 90.0f);
                break;

            case 180.0f:
                GetComponent<Animator>().SetFloat("currentRotation", 180.0f);
                break;

            case 270.0f:
                GetComponent<Animator>().SetFloat("currentRotation", 270.0f);
                break;
        }        
    }

    public bool IsHoldingDown
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
