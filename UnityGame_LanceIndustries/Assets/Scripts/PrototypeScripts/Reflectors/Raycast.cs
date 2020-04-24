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
    private GameObject highlightedGrid = null;
    Color tempColor;

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
                        transform.position = new Vector3(point.x, point.y);
                        break;

                    case TouchPhase.Moved:
                        point = Camera.main.ScreenToWorldPoint(touch.position);
                        transform.position = new Vector3(point.x, point.y);

                        hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

                        if (hit)
                        {
                            if (hit.collider.tag == "Grid")
                            {
                                if(highlightedGrid != null)
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



    private void rotateReflector(float zRotation)
    {
        if (zRotation != 270.0f)
            transform.Rotate(0.0f, 0.0f, 90.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    //Temporary implementation for easier testing purposes
    private void returnReflectorToStartingPosition()
    {
        if(gameObject.transform.GetChild(0).tag == "Reflector")
        {
            gameObject.transform.position = new Vector3(-2.05f, -3.48f, 0.0f);
        }

        if(gameObject.transform.GetChild(0).tag == "ReflectorDoubleWay")
        {
            gameObject.transform.position = new Vector3(-0.96f, -3.48f, 0.0f);
        }

        if (gameObject.transform.GetChild(0).tag == "ReflectorSplit")
        {
            gameObject.transform.position = new Vector3(0.12f, -3.48f, 0.0f);
        }

        if (gameObject.transform.GetChild(0).tag == "ReflectorThreeWay")
        {
            gameObject.transform.position = new Vector3(2.2f, -3.48f, 0.0f);
        }

        if (gameObject.transform.GetChild(0).tag == "ReflectorTranslucent")
        {
            gameObject.transform.position = new Vector3(1.19f, -3.48f, 0.0f);
        }
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
