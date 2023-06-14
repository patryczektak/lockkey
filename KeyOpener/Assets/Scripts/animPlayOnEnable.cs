using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class animPlayOnEnable : MonoBehaviour
{
    public PlayableDirector directorShow;
    public PlayableDirector directorHide;
    public bool TresureUI;

    public void Show()
    {        
        TresureUI = true;
        directorShow.Stop();
        directorShow.time = 0f;
        directorShow.Play();

        GameObject[] trophyObjects = GameObject.FindGameObjectsWithTag("trophy");

        foreach (GameObject trophyObject in trophyObjects)
        {
            trophy trophyScript = trophyObject.GetComponent<trophy>();
            if (trophyScript != null)
            {
                trophyScript.Check(); // Wywo³aj funkcjê Check() na danym obiekcie Trophy
            }
        }
    }

    public void Hide()
    {        
        TresureUI = false;
        directorHide.Stop();
        directorHide.time = 0f;
        directorHide.Play();
    }
}
