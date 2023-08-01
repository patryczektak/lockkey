using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class panelAnim : MonoBehaviour
{
    public PlayableDirector timeline;
    public PlayableDirector timelineBack;
    public PlayableDirector timelineStar;
    public AdManager AdShow;
    private bool startShow;

    public void Show()
    {
        timeline.Stop();
        timeline.time = 0f;
        timeline.Play();

        //AdShow.ShowAd();
    }

    public void Hide()
    {
        timelineBack.Stop();
        timelineBack.time = 0f;
        timelineBack.Play();
    }

    public void AdToShow()
    {
        AdShow.ShowAd();
    }

    public void AdToStartShow()
    {
        AdShow.ShowStartAd();
    }

    public void StarPrize()
    {
        timelineStar.Play();
    }

    private void OnEnable()
    {
        Show();
        if(startShow == true)
        {
            timelineStar.Play();
        }
        startShow = true;
    }
}
