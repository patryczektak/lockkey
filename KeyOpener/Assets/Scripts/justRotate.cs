using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justRotate : MonoBehaviour
{

    public float rotationSpeed;
    public Vector3 rotate;

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        Vector3 rotation = rotate * rotationAmount;

        transform.Rotate(rotation);
    }
}
