using UnityEngine;
using System.Collections.Generic;

public class BallControllerV2 : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float resetTime = 1f;
    public float resetTimer;
    public bool runTime;
    public float gameTimeLimit;
    public float gameTime;
    public int SuccesGame;
    public bool endGame;
    public Transform[] ballPoints;
    public bool isReseting;
    public bool hasReachedTarget = true;
    public bool[] TimeLonger;
    public float extraTime;

    public int currentPoint = 0;
    private bool isGoingForward = true;

    void Start()
    {
        endGame = false;
        ResetTimeLonger();
    }

    void Update()
    {
        transform.LookAt(target);
        if (isGoingForward && hasReachedTarget && Input.GetKeyDown(KeyCode.W) && isReseting == false && endGame == false)
        {
            hasReachedTarget = false;
            runTime = true;

            if (currentPoint < ballPoints.Length - 1)
            {
                if (TimeLonger[currentPoint] == false)
                {
                    TimeLonger[currentPoint] = true;
                    gameTime += extraTime;
                }

                currentPoint++;

                if (currentPoint == SuccesGame - 1)
                {
                    Succes();
                }
            }
            else
            {
                isGoingForward = false;
            }
        }

        if (!isGoingForward && hasReachedTarget && Input.GetKeyDown(KeyCode.S) && isReseting == false && endGame == false)
        {
            hasReachedTarget = false;

            if (currentPoint > 0)
            {
                currentPoint--;
            }

            if (currentPoint == 0)
            {
                isGoingForward = true;
            }
        }

        if (isReseting == false && hasReachedTarget == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, ballPoints[currentPoint].position, speed * Time.deltaTime);
        }

        if (isReseting == true && endGame == false)
        {
            hasReachedTarget = false;
            transform.position = Vector3.MoveTowards(transform.position, ballPoints[currentPoint].position, (speed * 5) * Time.deltaTime);
            resetTimer += 0.5f * Time.deltaTime;

            if (resetTimer >= resetTime)
            {
                currentPoint--;
                Debug.Log("minusek");
                resetTimer = 0;

                if (currentPoint <= 0)
                {
                    isGoingForward = true;
                    isReseting = false;
                    gameTime = gameTimeLimit;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && isReseting == false && endGame == false)
        {
            isReseting = true;
        }

        if (runTime == true && endGame == false)
        {
            gameTime -= Time.deltaTime;

            if (gameTime <= 0)
            {
                runTime = false;
                Reset();
            }
        }

        if (transform.position == ballPoints[currentPoint].position)
        {
            hasReachedTarget = true;
        }
    }

    public void Reset()
    {
        isReseting = true;
        ResetTimeLonger();
    }

    public void ResetTimeLonger()
    {
        for (int i = 0; i < TimeLonger.Length; i++)
        {
            TimeLonger[i] = false;
        }
    }

    public void Succes()
    {
        endGame = true;
        Debug.Log("Koniec");
    }

    public void AddPoint(Transform point)
    {
        //ballPoints.Add(point);
    }

    public void RemovePoint(Transform point)
    {
        //ballPoints.Remove(point);
    }

    public void ClearPoints()
    {
        ballPoints = new Transform[0];
    }

    public void ConnectPoints(bool loopPath)
    {
        if (ballPoints.Length < 2)
        {
            Debug.LogWarning("Cannot connect points. Not enough points.");
            return;
        }
        int pointCount = loopPath ? ballPoints.Length : ballPoints.Length - 1;

        for (int i = 0; i < pointCount; i++)
        {
            Transform startPoint = ballPoints[i];
            Transform endPoint = loopPath && i == ballPoints.Length - 1 ? ballPoints[0] : ballPoints[i + 1];

            if (startPoint == endPoint)
            {
                Debug.LogWarning("Cannot connect point to itself.");
                continue;
            }

            // create a LineRenderer to visualize the path
            GameObject path = new GameObject($"Path_{i}");
            path.transform.position = startPoint.position;
            LineRenderer pathRenderer = path.AddComponent<LineRenderer>();
            pathRenderer.material = new Material(Shader.Find("Sprites/Default"));
            pathRenderer.startColor = Color.white;
            pathRenderer.endColor = Color.white;
            pathRenderer.startWidth = 0.1f;
            pathRenderer.endWidth = 0.1f;
            pathRenderer.SetPosition(0, startPoint.position);
            pathRenderer.SetPosition(1, endPoint.position);

            // set the target to the end point of the path
            if (i == pointCount - 1 && !loopPath)
            {
                target = endPoint;
            }
        }

        // if the path is looped, set the target to the first point
        if (loopPath)
        {
            target = ballPoints[0];
        }

    }

    public void DisconnectPoints()
    {
        // destroy all LineRenderer objects
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        foreach (GameObject path in paths)
        {
            Destroy(path);
        }

        // Clear the ball points array
        ballPoints = new Transform[0];

        // Reset the target and current point
        target = null;
        currentPoint = 0;

        // Reset the game time and time longer array
        gameTime = gameTimeLimit;
        ResetTimeLonger();

        // Reset the game end flag
        endGame = false;
    }

    public void SetPath(List<Vector3> points, bool loop)
    {
        // Clear existing points
        ClearPoints();

        // Create new path gameobject
        GameObject path = new GameObject("Path");

        // Add a line renderer to the path gameobject
        LineRenderer line = path.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.positionCount = points.Count;

        // Set the line renderer positions
        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i]);
        }

        // Set the ball points array to the path points
        ballPoints = new Transform[points.Count];
        for (int i = 0; i < points.Count; i++)
        {
            ballPoints[i] = new GameObject("Ball Point").transform;
            ballPoints[i].position = points[i];
            ballPoints[i].parent = path.transform;
        }

        // Set the loop flag
        line.loop = loop;

        // Set the target to the first point and current point to 0
        target = ballPoints[0];
        currentPoint = 0;
    }

    private void OnDrawGizmos()
    {
        // Draw gizmos for the ball points
        Gizmos.color = Color.blue;
        if (ballPoints != null)
        {
            foreach (Transform point in ballPoints)
            {
                Gizmos.DrawSphere(point.position, 0.5f);
            }
        }
    }
}
