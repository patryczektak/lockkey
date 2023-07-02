using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Material newMaterial;
    public Material resetMaterial;
    public GameObject[] objectsToChangeColor;
    public bool correctPath;
    public bool finalPath;
    private bool wasHere;
    public BallControllerV3 controller;

    //wartoœæ ile warstw musi miec grac by zmieni³o kolor globalnie
    public int colorMove;

    public bool UpMove;
    public bool DownMove;
    public bool LeftMove;
    public bool RightMove;
    // Start is called before the first frame update
    public void Update()
    {
        if(controller.currentPoint == 0)
        {
            wasHere = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !finalPath && controller.runTime == false)
        {
            controller.canRotateLeft = LeftMove;
            controller.canRotateRight = RightMove;
            controller.canRotateUp = UpMove;
            controller.canRotateDown = DownMove;
        }
            if (other.CompareTag("Player") && !finalPath && controller.runTime == true)
        {
            controller.canRotateLeft = LeftMove;
            controller.canRotateRight = RightMove;
            controller.canRotateUp = UpMove;
            controller.canRotateDown = DownMove;

            if (correctPath == true && !wasHere && controller.endGame == false)
            {
                controller.currentPoint += 1;
                wasHere = true;
                //if (finalPath)
                //{
                //    controller.director.Play();
                //}
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


        if (other.CompareTag("Player") && finalPath && controller.currentPoint == (controller.SuccesGame - colorMove) && correctPath == true && !wasHere && controller.endGame == false)
        {
            controller.currentPoint += 1;
            wasHere = true;
            controller.director.Play();

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

        if (other.CompareTag("Player") && finalPath && controller.currentPoint == 0 && correctPath == true && !wasHere && controller.endGame == false)
        {
            foreach (GameObject obj in objectsToChangeColor)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material[] materials = renderer.materials;
                    if (materials.Length >= 2)
                    {
                        materials[1] = resetMaterial;
                        renderer.materials = materials;
                    }
                }
            }
        }


    }
    
}
