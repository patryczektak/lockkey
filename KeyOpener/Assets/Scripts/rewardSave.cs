using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewardSave : MonoBehaviour
{
    public string PrizeName;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(PrizeName, 1);
    }

}
