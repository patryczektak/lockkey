using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tresureActivate : MonoBehaviour
{
    public GameObject[] objectsToDeactivate;
    public GameObject[] firstTresure;
    public GameObject[] secondTresure;
    public GameObject[] thirdTresure;
    public GameObject[] shine;

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateAllObjects()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }

    public void FirstTresurePack()
    {
        DeactivateAllObjects();

        foreach (GameObject obj in shine)
        {
            obj.SetActive(true);
        }

        int randomIndex = Random.Range(0, firstTresure.Length);

        for (int i = 0; i < firstTresure.Length; i++)
        {
            firstTresure[i].SetActive(i == randomIndex);
        }
    }

    public void SecondTresurePack()
    {
        DeactivateAllObjects();

        foreach (GameObject obj in shine)
        {
            obj.SetActive(true);
        }

        int randomIndex = Random.Range(0, secondTresure.Length);

        for (int i = 0; i < secondTresure.Length; i++)
        {
            secondTresure[i].SetActive(i == randomIndex);
        }
    }

    public void ThirdTresurePack()
    {
        DeactivateAllObjects();

        foreach (GameObject obj in shine)
        {
            obj.SetActive(true);
        }

        int randomIndex = Random.Range(0, thirdTresure.Length);

        for (int i = 0; i < thirdTresure.Length; i++)
        {
            thirdTresure[i].SetActive(i == randomIndex);
        }
    }
}
