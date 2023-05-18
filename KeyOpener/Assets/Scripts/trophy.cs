using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trophy : MonoBehaviour
{
    public string nameTrophy;
    public TextMeshProUGUI textMeshPro;
    public string nameID;
    public GameObject myself;
    // Start is called before the first frame update
    void Start()
    {
        //textMeshPro = GetComponent<TextMeshProUGUI>();
        Check();
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
        }
    }
}
