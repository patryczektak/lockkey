using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool fingerIsDown;

    [SerializeField]
    private float minSwipeDistance = 20f;

    public BallControllerV3 ballMove;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            // Swipe na urz¹dzeniach mobilnych
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
                fingerIsDown = true;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                CheckSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = Vector2.zero;
                fingerUpPosition = Vector2.zero;
                fingerIsDown = false;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Swipe na komputerze
            if (fingerDownPosition == Vector2.zero)
            {
                fingerDownPosition = Input.mousePosition;
                fingerUpPosition = Input.mousePosition;
                fingerIsDown = true;
            }
            else
            {
                fingerUpPosition = Input.mousePosition;
                CheckSwipe();
            }
        }
        else
        {
            fingerDownPosition = Vector2.zero;
            fingerUpPosition = Vector2.zero;
            fingerIsDown = false;
        }
    }

    private void CheckSwipe()
    {
        if (!fingerIsDown)
        {
            return;
        }

        if (SwipeDistanceCheck())
        {
            float swipeAngle = Mathf.Atan2(fingerUpPosition.y - fingerDownPosition.y, fingerUpPosition.x - fingerDownPosition.x) * Mathf.Rad2Deg;
            if (swipeAngle > -180 && swipeAngle < -90) // Swipe w lewy górny róg
            {
                //UpperLeftMove();
                Debug.Log("dolny lewyV2");
            }
            if (swipeAngle > 0 && swipeAngle < 90) // Swipe w górny prawy róg
            {
                //UpperRightMove();
                Debug.Log("górny prawyV2");
            }
            if (swipeAngle >= 90 && swipeAngle <= 180) // Swipe w dolny prawy róg
            {
                //LowerRightMove();
                Debug.Log("górny lewyV2");
            }
            if (swipeAngle > -90 && swipeAngle <= 0) // Swipe w dolny lewy róg
            {
                //LowerLeftMove();
                Debug.Log("dolny prawy");
            }
            if (swipeAngle > -45 && swipeAngle <= 45) // Swipe w prawo
            {
                RightMove();
                Debug.Log("prawy");
            }
            if (swipeAngle > 45 && swipeAngle <= 135) // Swipe w górê
            {
                UpMove();
                Debug.Log("górny");
            }
            if (swipeAngle > 135 || swipeAngle <= -135) // Swipe w lewo
            {
                LeftMove();
                Debug.Log("lewy");
            }
            if (swipeAngle > -135 && swipeAngle <= -45) // Swipe w dó³
            {
                DownMove();
                Debug.Log("dolny");
            }

            fingerDownPosition = fingerUpPosition;
            fingerIsDown = false;
        }
    }


    private bool SwipeDistanceCheck()
    {
        return Vector2.Distance(fingerDownPosition, fingerUpPosition) > minSwipeDistance;
    }

    public void UpMove()
    {
        ballMove.UpMove();
    }

    public void DownMove()
    {
        ballMove.DownMove();
    }

    public void LeftMove()
    {
        ballMove.LeftMove();
    }

    public void RightMove()
    {
        ballMove.RightMove();
    }
}
