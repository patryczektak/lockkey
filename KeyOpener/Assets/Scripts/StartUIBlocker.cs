using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIBlocker : MonoBehaviour
{
    //bool blokuj�cy input scrolla dla minigry a� do zamkni�cia okna powitalnego
    public bool StartGame = true;
    // Start is called before the first frame update
    void Start()
    {
        StartGame = true;
    }

    public void offStart()
    {
        StartGame = false;
    }
}
