using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform[] ballPoints;
    public Transform target;
    public float speed;
    public int currentPoint = 0;
    //resetowanie
    public bool isReseting;
    public bool hasReachedTarget = true;
    public float resetTime = 1f;
    public float resetTimer;

    public bool runTime;
    public float gameTimeLimit;
    public float gameTime;
    private float gameTimeReset;

    public bool[] TimeLonger;
    public float extraTime;

    public int SuccesGame;
    public bool endGame;


    public Transform[] leftTarget;
    public Transform[] righttTarget;
    public Transform[] upTarget;
    public Transform[] downTarget;

   
    public void Start()
    {
        endGame = false;
        ResetTimeLonger();
        gameTimeReset = gameTime;
    }
    void Update()
    {
        transform.LookAt(target);
        if (hasReachedTarget && Input.GetKeyDown(KeyCode.W) && isReseting == false && endGame == false)
        {
            hasReachedTarget = false;

            runTime = true;
            if (currentPoint < ballPoints.Length - 1)
            {
                if(TimeLonger[currentPoint] == false)
                {
                    TimeLonger[currentPoint] = true;
                    gameTime += extraTime;

                }
                currentPoint++;
                if (currentPoint == SuccesGame -1)
                {
                    Succes();
                }
            }

        }

        if (hasReachedTarget && Input.GetKeyDown(KeyCode.S) && isReseting == false && endGame == false)
        {
            hasReachedTarget = false;

            if (currentPoint > 0)
            {
                currentPoint--;
            }

        }
        if (isReseting == false && hasReachedTarget == false)
            transform.position = Vector3.MoveTowards(transform.position, ballPoints[currentPoint].position, speed * Time.deltaTime);

        Vector3 direction = (ballPoints[currentPoint].position - transform.position).normalized;


        if (isReseting == true && endGame == false)
        {
            hasReachedTarget = false;
            transform.position = Vector3.MoveTowards(transform.position, ballPoints[currentPoint].position, (speed * 5) * Time.deltaTime);
            resetTimer += 0.5f * Time.deltaTime;

            if (resetTimer >= resetTime)
            {
                currentPoint--;
                Debug.Log("Res " + currentPoint);
                resetTimer = 0;
            }

            if (currentPoint == 0)
            {
                isReseting = false;
                gameTime = gameTimeLimit;
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

        if (transform.position == ballPoints[currentPoint].position) // dodanie warunku
        {
            hasReachedTarget = true; // ustawienie warunku na true
        }
    }

    public void Reset()
    {

        isReseting = true;
        ResetTimeLonger();
        gameTime = gameTimeReset;
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

    public void BackMove()
    {
        hasReachedTarget = false;

        if (currentPoint > 0)
        {
            currentPoint--;
        }

    }

    public void UpMove()
    {
        if (hasReachedTarget && isReseting == false && endGame == false)
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
            
        }
    }

    public void DownMove()
    {
        if (hasReachedTarget && isReseting == false && endGame == false)
        {
            
                hasReachedTarget = false;

                if (currentPoint > 0)
                {
                    currentPoint--;
                }
            
        }
    }
}
