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

    public float[] firstTresureWeights;
    public float[] secondTresureWeights;
    public float[] thirdTresureWeights;
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

    public void ActivateShine()
    {
        foreach (GameObject obj in shine)
        {
            obj.SetActive(true);
        }
    }

    public void SelectRandomTresure(GameObject[] tresureObjects, float[] weights)
    {
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;

        for (int i = 0; i < tresureObjects.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                tresureObjects[i].SetActive(true);
                break;
            }
        }
    }

    public void FirstTresurePack()
    {
        DeactivateAllObjects();
        ActivateShine();
        SelectRandomTresure(firstTresure, firstTresureWeights);
    }

    public void SecondTresurePack()
    {
        DeactivateAllObjects();
        ActivateShine();
        SelectRandomTresure(secondTresure, secondTresureWeights);
    }

    public void ThirdTresurePack()
    {
        DeactivateAllObjects();
        ActivateShine();
        SelectRandomTresure(thirdTresure, thirdTresureWeights);
    }
}
