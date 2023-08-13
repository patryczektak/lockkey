using System.Collections.Generic;
using System.Collections;
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
    //skosne
    public bool canRotateDLeft;
    public bool canRotateDRight;
    public bool canRotateULeft;
    public bool canRotateURight;

    public bool isRotating = false;
    private float rotationTimer = 0.0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private Stack<Quaternion> previousRotations = new Stack<Quaternion>();
    private GameObject objectToEnable;

    private againShowHide againPanel;
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
    public GameObject prefabOrigin;

    public PlayableDirector director;
    public PlayableDirector directorDissolve;
    public PlayableDirector directorUP;
    public PlayableDirector directorDOWN;
    public PlayableDirector directorLEFT;
    public PlayableDirector directorRight;

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
        //director = GetComponent<PlayableDirector>();
        visitedRotations = new bool[visitedRotationSize, visitedRotationSize, visitedRotationSize];

        objectToEnable = GameObject.Find("NextPanel");
        if (objectToEnable != null)
        {
            Debug.Log("znalaz³em");
            StartCoroutine(DisableObjectWithDelay());
        }
    }

    public IEnumerator DisableObjectWithDelay()
    {
        yield return new WaitForSeconds(1f);
        objectToEnable.SetActive(false);
    }

    private void Update()
    {
        SetTime();
        
        //Rotate the ball over time
        if (isRotating)
        {
            rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(rotationTimer * 2f);
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (rotationTimer >= 0.5f)
            {
                // Koniec obrotu i aktualizacja pocz¹tkowego obrotu dla kolejnego obrotu
                isRotating = false;
                startRotation = targetRotation;
                MoveCheck();
                if (!visitedRotations[Mathf.RoundToInt(transform.rotation.eulerAngles.x / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.y / 90), Mathf.RoundToInt(transform.rotation.eulerAngles.z / 90)] == true && currentPoint != 0)
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
                    Debug.Log("zwiedzone ju¿");
                }

                if (currentPoint == SuccesGame - 1)
                {
                    endGame = true;
                    MoveClear();
                    Debug.Log("wygrana");
                    //odnajdywanie przycisku do nastêpnego prefabu

                    objectToEnable.SetActive(true);

                    //zabicie siebie samego
                    //Destroy(prefabOrigin);
                    director.Play();
                    directorDissolve.Play();

                    PlayerPrefs.SetInt("exp", PlayerPrefs.GetInt("exp") + 1);
                    //Firebase event
                    Firebase.Analytics.FirebaseAnalytics.LogEvent(
  Firebase.Analytics.FirebaseAnalytics.EventLevelUp,
  new Firebase.Analytics.Parameter[] {
    new Firebase.Analytics.Parameter(
      Firebase.Analytics.FirebaseAnalytics.ParameterCharacter, "Player"),
    new Firebase.Analytics.Parameter(
      Firebase.Analytics.FirebaseAnalytics.ParameterLevel, PlayerPrefs.GetInt("exp")),
  }
);
                }
                rotationTimer = 0f;
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

        againPanel = GameObject.FindObjectOfType<againShowHide>();
        againPanel.Show();
    }


    public void UpMove()
    {
        if (canRotateUp && !isRotating && !endGame)
        {
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
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        if (!canRotateUp && !isRotating && !endGame)
        {
            directorUP.Play();
            wrong.Play();
        }
    }

    public void DownMove()
    {
        if (canRotateDown && !isRotating && !endGame)
        {
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
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        if (!canRotateDown && !isRotating && !endGame)
        {
            directorDOWN.Play();
            wrong.Play();
        }
    }

    public void LeftMove()
    {
        if (canRotateLeft && !isRotating && !endGame)
        {
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
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        if (!canRotateLeft && !isRotating && !endGame)
        {
            directorLEFT.Play();
            wrong.Play();
        }
    }

    public void DownLeftMove()
    {
        if (canRotateDLeft && !isRotating && !endGame)
        {
            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(-rotationAngle, -rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        //if (!canRotateDLeft && !isRotating && !endGame)
        //{
        //    directorLEFT.Play();
        //    wrong.Play();
        //}
    }

    public void UpLeftMove()
    {
        if (canRotateULeft && !isRotating && !endGame)
        {
            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(rotationAngle, -rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        //if (!canRotateULeft && !isRotating && !endGame)
        //{
        //    directorLEFT.Play();
        //    wrong.Play();
        //}
    }

    public void RightMove()
    {
        if (canRotateRight && !isRotating && !endGame)
        {
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
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        if (!canRotateRight && !isRotating && !endGame)
        {
            directorRight.Play();
            wrong.Play();
        }
    }

    public void UpRightMove()
    {
        if (canRotateURight && !isRotating && !endGame)
        {
            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(rotationAngle, rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        //if (!canRotateURight && !isRotating && !endGame)
        //{
        //    directorRight.Play();
        //    wrong.Play();
        //}
    }

    public void DownRightMove()
    {
        if (canRotateDRight && !isRotating && !endGame)
        {
            // Set the target rotation based on the current global rotation and the desired rotation angle
            targetRotation = Quaternion.Euler(-rotationAngle, rotationAngle, 0) * transform.rotation;

            // Add the previous rotation to the stack
            previousRotations.Push(transform.rotation);

            // Start the rotation
            isRotating = true;
            rotationTimer = 0.0f;
            runTime = true;
            move.Play();
            MoveClear();
            //VibrationManager.Instance.Vibrate(0.05f, 0.2f);
        }

        //if (!canRotateDRight && !isRotating && !endGame)
        //{
        //    directorRight.Play();
        //    wrong.Play();
        //}
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
        Debug.Log("start clear");
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
