using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewardSave : MonoBehaviour
{
    public string PrizeName;

    private tresureEnable tresureScript;
    // Start is called before the first frame update
    void Start()
    {
        tresureScript = GameObject.FindObjectOfType<tresureEnable>();
        if(PlayerPrefs.GetInt(PrizeName)== 0)
        {
            tresureScript.Tresure();

        }
        PlayerPrefs.SetInt(PrizeName, 1);
    }

}
