using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class minigameBomb : MonoBehaviour
{
    public BallControllerV3 ballController;
    public ParticleSystem first;
    public ParticleSystem second;
    public ParticleSystem third;
    public int TimeReduce;
    private bool boom;
    public GameObject pref;
    public PlayableDirector boomUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ballController.gameTime == ballController.gameTimeLimit)
        {
            boom = false;
            pref.SetActive(true);
        }
    }

    public void Boom()
    {
        ballController.gameTime = ballController.gameTime - 2;
        first.Play();
        second.Play();
        third.Play();
        boom = true;
        pref.SetActive(false);
        boomUI.Play();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && boom == false)
        {
            Boom();
        }
    }
}
