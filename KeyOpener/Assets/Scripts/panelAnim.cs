using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class panelAnim : MonoBehaviour
{
    public PlayableDirector timeline;
    public PlayableDirector timelineBack;

    public void Show()
    {
        timeline.Stop();
        timeline.time = 0f;
        timeline.Play();
    }

    public void Hide()
    {
        timelineBack.Stop();
        timelineBack.time = 0f;
        timelineBack.Play();
    }

    private void OnEnable()
    {
        Show();
    }
}