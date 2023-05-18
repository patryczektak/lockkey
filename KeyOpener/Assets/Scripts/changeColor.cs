using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Material newMaterial;
    public GameObject[] objectsToChangeColor;
    public bool correctPath;
    public bool finalPath;
    private bool wasHere;
    public BallControllerV3 controller;

    public bool UpMove;
    public bool DownMove;
    public bool LeftMove;
    public bool RightMove;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.canRotateLeft = LeftMove;
            controller.canRotateRight = RightMove;
            controller.canRotateUp = UpMove;
            controller.canRotateDown = DownMove;

            if (correctPath == true && !wasHere && controller.endGame == false)
            {
                controller.currentPoint += 1;
                wasHere = true;
                if (finalPath)
                {
                    controller.director.Play();
                }
            }
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
