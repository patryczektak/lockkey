using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUIBlocker : MonoBehaviour
{
    //bool blokuj¹cy input scrolla dla minigry a¿ do zamkniêcia okna powitalnego
    public bool StartGame = true;


    public int sperydexON;
    public TextMeshProUGUI sperydexText;
    public GameObject sperydex;
    // Start is called before the first frame update
    void Start()
    {
        StartGame = true;
        sperydex.SetActive(true);
        sperydexText.text = sperydexON.ToString();
    }

    public void offStart()
    {
        StartGame = false;
    }

    public void Check()
    {
        if (PlayerPrefs.GetInt("exp") >= sperydexON)
        {
            sperydex.SetActive(false);
        }
    }
}
