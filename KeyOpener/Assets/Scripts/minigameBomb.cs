using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameBomb : MonoBehaviour
{
    public BallControllerV3 ballController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Boom()
    {
        ballController.gameTime = 0.1f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Boom();
        }
    }
}
