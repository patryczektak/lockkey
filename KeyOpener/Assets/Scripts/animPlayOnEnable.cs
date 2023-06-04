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
    }

    public void Hide()
    {        
        TresureUI = false;
        directorHide.Stop();
        directorHide.time = 0f;
        directorHide.Play();
    }
}
