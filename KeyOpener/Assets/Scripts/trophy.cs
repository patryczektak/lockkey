using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Analytics;

public class trophy : MonoBehaviour
{
    public string nameTrophy;
    public TextMeshProUGUI textMeshPro;
    public string nameID;
    public GameObject myself;

    public delegate void CheckDelegate();

    public static event CheckDelegate OnCheck;
    // Start is called before the first frame update
    void Start()
    {
        //textMeshPro = GetComponent<TextMeshProUGUI>();
        Check();
        FirebaseAnalytics.LogEvent("GameOpen");
    }

    private void OnEnable()
    {
        Check();
    }
    public void Check()
    {
        myself.SetActive(false);
        textMeshPro.text = "???";
        if (PlayerPrefs.GetInt(nameID) == 1)
        {
            myself.SetActive(true);
            textMeshPro.text = nameTrophy;
            Debug.Log("sprawdzam");
        }
    }
}
