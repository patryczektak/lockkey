using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class tresureEnable : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject newDot;

    public void Tresure()
    {        
        timeline.Play();
        newDot.SetActive(true);
    }
}
