using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigameBomb : MonoBehaviour
{
    public BallControllerV3 ballController;
    public ParticleSystem first;
    public ParticleSystem second;
    public ParticleSystem third;
    public int TimeReduce;
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
        ballController.gameTime = ballController.gameTime - 2;
        first.Play();
        second.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Boom();
        }
    }
}
