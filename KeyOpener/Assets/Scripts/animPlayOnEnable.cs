using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class animPlayOnEnable : MonoBehaviour
{
    public PlayableDirector director;


    private void OnEnable()
    {
        director.Play(); 
    }
}
