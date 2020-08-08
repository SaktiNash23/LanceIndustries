using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For screen swipe, need array or List to keep track of all the panels and their position
//Use switch case and TouchPhase enum for touch input detection
//Variable for minimum swipe value, swipeDirection, touchPhase

public class TutorialMenuSwipe : MonoBehaviour
{
    private int currentPanelIndex;
    public GameObject parentPanel;

    private Vector2 startingTouchPosition;
    private Vector2 swipeDelta;

    float xVal = 0.0f;
    float OldXVal = 0.0f;
    public float screenSwipeSpeed;
    public float screenSnapSpeed;
    public float screenSwipeThreshold;
    float totalSwipedDistance = 0.0f;

    bool moveScreen = false;
    bool touchDisabled = false;

    private enum SWIPEDIRECTION
    {
        LEFT,
        RIGHT,
        NEUTRAL
    };

    private SWIPEDIRECTION swipeDirection;


    // Start is called before the first frame update
    void Start()
    {
        currentPanelIndex = 0;
        Debug.Log("Screen Width : " + Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && touchDisabled == false)//Must be 1 touch only to initiate screen swiping
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startingTouchPosition = touch.position;
                    //Debug.Log("Starting Touch Position : " + startingTouchPosition);
                    break;

                case TouchPhase.Moved:
                    swipeDelta = touch.position - startingTouchPosition;
                    //Debug.Log("Swipe Delta : " + swipeDelta);

                    OldXVal = xVal;
                    xVal = swipeDelta.x;
                    Debug.Log("XVal : " + xVal);

                    if (xVal > OldXVal)//If swiping to the right
                    {
                        if (currentPanelIndex != 0)
                            transform.localPosition += Vector3.right * screenSwipeSpeed * Time.deltaTime;
                        else
                            transform.localPosition = Vector3.zero;

                    }
                    else if (xVal < OldXVal)//If swiping to the left
                    {
                        if(currentPanelIndex != 2)
                            transform.localPosition -= Vector3.right * screenSwipeSpeed * Time.deltaTime;
                        else
                            transform.localPosition = new Vector3(-1600.0f, 0.0f, 0.0f);
                    }

                    totalSwipedDistance = xVal;

                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    Debug.Log("Total Swipe Distance : " + totalSwipedDistance);

                    if (totalSwipedDistance != 0.0f)
                    {
                        if (totalSwipedDistance < (screenSwipeThreshold * -1.0f))
                            swipeDirection = SWIPEDIRECTION.LEFT;
                        else if (totalSwipedDistance > screenSwipeThreshold)
                            swipeDirection = SWIPEDIRECTION.RIGHT;
                        else
                            swipeDirection = SWIPEDIRECTION.NEUTRAL;

                        moveScreen = true;
                        
                    }
                    
                    //Debug.Log("Current Panel Index : " + currentPanelIndex);
                    break;               
            }      
        }

        screenSnap(ref moveScreen, ref totalSwipedDistance, ref currentPanelIndex);
    }


    private void screenSnap(ref bool moveScreen, ref float swipeDistance, ref int currentPanelIndex)
    {
        //touchDisabled = true;

        if (moveScreen)
        {
            switch (swipeDirection)
            {
                case SWIPEDIRECTION.LEFT:
                    if(currentPanelIndex == 0)
                    {
                        if (transform.localPosition.x > -800.0f)
                            transform.localPosition -= Vector3.right * screenSnapSpeed * Time.deltaTime;
                        else
                        {
                            transform.localPosition = new Vector3(-800.0f, 0.0f, 0.0f);                           
                            currentPanelIndex = 1;
                            touchDisabled = false;
                            moveScreen = false;
                        }
                    }
                    
                    if(currentPanelIndex == 1)
                    {
                        if (transform.localPosition.x > -1600.0f)
                            transform.localPosition -= Vector3.right * screenSnapSpeed * Time.deltaTime;
                        else
                        {
                            transform.localPosition = new Vector3(-1600.0f, 0.0f, 0.0f);
                            currentPanelIndex = 2;
                            touchDisabled = false;
                            moveScreen = false;
                        }
                    }
                    break;

                case SWIPEDIRECTION.RIGHT:
                    if(currentPanelIndex == 1)
                    {
                        if (transform.localPosition.x < 0.0f)
                            transform.localPosition += Vector3.right * screenSnapSpeed * Time.deltaTime;
                        else
                        {
                            transform.localPosition = Vector3.zero;
                            currentPanelIndex = 0;
                            touchDisabled = false;
                            moveScreen = false;

                        }
                    }

                    if(currentPanelIndex == 2)
                    {
                        if (transform.localPosition.x < -800.0f)
                            transform.localPosition += Vector3.right * screenSnapSpeed * Time.deltaTime;
                        else
                        {
                            currentPanelIndex = 1;
                            touchDisabled = false;
                            moveScreen = false;
                        }
                    }
                    break;

                case SWIPEDIRECTION.NEUTRAL:
                    if(currentPanelIndex == 0)
                    {
                        transform.localPosition = Vector3.zero;                      
                    }

                    if(currentPanelIndex == 1)
                    {
                        transform.localPosition = new Vector3(-800.0f, 0.0f, 0.0f);
                    }

                    if (currentPanelIndex == 2)
                    {
                        transform.localPosition = new Vector3(-1600.0f, 0.0f, 0.0f);
                    }

                    moveScreen = false;

                    break;
            }    
        }
    }
}
