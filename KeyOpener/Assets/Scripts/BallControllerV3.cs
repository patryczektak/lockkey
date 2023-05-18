using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class BallControllerV3 : MonoBehaviour
{
    public float rotationAngle = 90.0f;
    public float rotationTime = 1.0f;
    public Transform ballTransform;
    public Transform target;
    public float totalRotationTime = 5f;


    public bool canRotateLeft;
    public bool canRotateRight;
    public bool canRotateUp;
    public bool canRotateDown;

    private bool isRotating = false;
    private float rotationTimer = 0.0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private Stack<Quaternion> previousRotations = new Stack<Quaternion>();

    
    //public int[] back;
    public int currentPoint = 0;
    public GameObject[] neon;

    public bool GameStart = false;
    public bool endGame = false;

    public int SuccesGame;

    //game time elements
    public bool runTime;
    public float gameTimeLimit;
    public float gameTime;
    public float extraTime;
    private float gameTimeReset;

    public AudioSource move;
    public AudioSource reset;
    public AudioSource wrong;

    public bool[,,] visitedRotations;
    public int visitedRotationSize;


    [System.Serializable]
    public class RotationObject
    {
        public GameObject obj;
        public int xIndex;
        public int yIndex;
        public int zIndex;
    }

    public RotationObject[] rotationObjects;

    public Slider slider;
    public Image fill;
    public Gradient gradient;

    public PlayableDirector director;

    public GameObject canvas;
    private void Start()
    {
        // Store the initial rotation of the ball
        canvas.SetActive(false);
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0f, 0f, 0f);
        gameTimeReset = gameTime;
        SetMaxTime();
        MoveCheck();
        director = GetComponent<PlayableDirector>();
        visitedRotations = new bool[visitedRotationSize, visitedRotationSize, visitedRotationSize];
    }



    private void Update()
    {
        SetTime();
        // Rotate left
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canRotateLeft && !isRotating && !endGame)
        {
            LeftMove();
        }

        // Rotate right
        if (Input.GetKeyDown(KeyCode.RightArrow) && canRotateRight && !isRotating && !endGame)
        {
            RightMove();
        }

        // Rotate up
        if (Input.GetKeyDown(KeyCode.UpArrow) && canRotateUp && !isRotating && !endGame)
        {
            UpMove();
        }

        // Rotate down
        if (Input.GetKeyDown(KeyCode.DownArrow) && canRotateDown && !isRotating && !endGame)
        {
            DownMove();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !isRotating)
        {
            ResetGame();
        }

            //Rotate the ball over time
            if (isRotating)
            {
            rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTimer * 0.1f / rotationTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);

            if (t >= 0.1f)
            {
                // End the rotation and update the initial rotation for the next rotation
                isRotating = false;
                startRotation = targetRotation;
                MoveCheck();
                if (!visitedRotations[Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90)] == true && currentPoint !=0) 
                {
                    MarkVisitedRotation();
                    gameTime += extraTime;
                    if (visitedRotations[Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90)])
                    {
                        ActivateObjects();
                    }
                    Debug.Log("zaznaczone");
                }
                if (visitedRotations[Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90)] == true)
                {
                    Debug.Log("zwiedzone ju�");
                }

                if (currentPoint == SuccesGame-1)
                {
                    endGame = true;
                    MoveClear();
                    Debug.Log("wygrana");
                    //director.Play();
                }
            }
        }


        if (runTime == true && endGame == false)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                runTime = false;
                ResetGame();
                gameTime = gameTimeReset;
            }
        }

        if(endGame == true || runTime == false)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
        
    }

    private void ResetGame()
    {
        Vector3 relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);

        isRotating = true;
        rotationTimer = 0.0f;
        currentPoint = 0;
        runTime = false;
        gameTime = gameTimeReset;
        reset.Play();
        MoveClear();
        ClearVisitedRotations();
    }


    public void UpMove()
    {
        if (canRotateUp && !isRotating && !endGame)
        {
            //if (currentPoint > 0 && canRotateDown[currentPoint - 1])
            //{
            //    currentPoint--;
            //}
            //else if (canRotateUp[currentPoint])
            //{
            //    currentPoint++;
            //}
            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(-rotationAngle, 0, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            VibrationManager.Instance.Vibrate(0.1f, 0.5f);
        }
    }

    public void DownMove()
    {
        if (canRotateDown && !isRotating && !endGame)
        {
            //if (currentPoint > 0 && canRotateUp[currentPoint - 1])
            //{
            //    currentPoint--;
            //}
            //else if (canRotateDown[currentPoint])
            //{
            //    currentPoint++;
            //}

            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(rotationAngle, 0, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            VibrationManager.Instance.Vibrate(0.1f, 0.5f);
        }
    }

    public void LeftMove()
    {
        if (canRotateLeft && !isRotating && !endGame)
        {
            //if (currentPoint > 0 && canRotateRight[currentPoint - 1])
            //{
            //    currentPoint--;
            //}
            //else if (canRotateLeft[currentPoint])
            //{
            //    currentPoint++;
            //}



            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(0, -rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            VibrationManager.Instance.Vibrate(0.1f, 0.5f);
        }
    }

    public void RightMove()
    {
        if (canRotateRight && !isRotating && !endGame)
        {
            //if (currentPoint > 0 && canRotateLeft[currentPoint - 1])
            //{
            //    currentPoint--;
            //}
            //else if (canRotateRight[currentPoint])
            //{
            //    currentPoint++;
            //}

            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(0, rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            VibrationManager.Instance.Vibrate(0.1f, 0.5f);
        }
    }

    public void MoveCheck()
    {
        if (canRotateLeft && !isRotating && !endGame)
        {
            neon[0].SetActive(true);
        }
        

        if (canRotateRight && !isRotating && !endGame)
        {
            neon[1].SetActive(true);
        }
        

        if (canRotateUp && !isRotating && !endGame)
        {
            neon[2].SetActive(true);
        }
        

        if (canRotateDown && !isRotating && !endGame)
        {
            neon[3].SetActive(true);
        }
        
    } 

    public void MoveClear()
    {
        neon[0].SetActive(false);
        neon[1].SetActive(false);
        neon[2].SetActive(false);
        neon[3].SetActive(false);
    }


    private void MarkVisitedRotation()
    {
        int x = Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90);
        int y = Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90);
        int z = Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90);

        visitedRotations[x, y, z] = true;
    }

    private void ClearVisitedRotations()
    {
        for (int x = 0; x < visitedRotations.GetLength(0); x++)
        {
            for (int y = 0; y < visitedRotations.GetLength(1); y++)
            {
                for (int z = 0; z < visitedRotations.GetLength(2); z++)
                {
                    visitedRotations[x, y, z] = false; // Oznacz wszystkie rotacje jako nieodwiedzone
                }
            }
        }
    }

    private void ActivateObjects()
    {
        //foreach (RotationObject rotationObj in rotationObjects)
        //{
        //    if (visitedRotations[rotationObj.xIndex, rotationObj.yIndex, rotationObj.zIndex])
        //    {
        //        rotationObj.obj.SetActive(true);
        //    }
        //}
    }

    public void SetMaxTime()
    {
        slider.maxValue = gameTimeLimit;
        slider.value = gameTime;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetTime()
    {
        slider.value = gameTime;
        fill.color = gradient.Evaluate(1f);
    }
}
