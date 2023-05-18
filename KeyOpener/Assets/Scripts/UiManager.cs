using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject trophyPanel;
    public GameObject trophyPanelON;

    public void Start()
    {
        trophyPanel.SetActive(false);
    }
    // Start is called before the first frame update
    public void TrophyShow()
    {
        trophyPanel.SetActive(true);
        trophyPanelON.SetActive(false);
    }

    public void TrophyHide()
    {
        trophyPanel.SetActive(false);
        trophyPanelON.SetActive(true);
    }
}
