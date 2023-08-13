using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class againShowHide : MonoBehaviour
{

    public PlayableDirector show;
    public PlayableDirector hide;

    private BallControllerV3 ballController;
    // Start is called before the first frame update
    public void Show()
    {
        ballController = GameObject.FindObjectOfType<BallControllerV3>();
        ballController.GameStart = false;
        show.Stop();
        show.time = 0f;
        show.Play();
    }

    public void Hide()
    {
        ballController = GameObject.FindObjectOfType<BallControllerV3>();
        ballController.GameStart = true;
        hide.Stop();
        hide.time = 0f;
        hide.Play();
    }
}
