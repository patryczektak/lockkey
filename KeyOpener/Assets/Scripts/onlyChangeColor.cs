using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onlyChangeColor : MonoBehaviour
{
    public Material newMaterial;
    public GameObject[] objectsToChangeColor;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToChangeColor)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material[] materials = renderer.materials;
                    if (materials.Length >= 2)
                    {
                        materials[1] = newMaterial;
                        renderer.materials = materials;
                    }
                }
            }
        }
    }
}
