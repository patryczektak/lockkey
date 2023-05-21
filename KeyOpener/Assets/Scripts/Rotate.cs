using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed; // Prêdkoœæ obrotu w stopniach na sekundê
    private float timer = 0f;
    private Vector3 randomAxis;

    void Start()
    {
        SetRandomRotationAxis();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;
            SetRandomRotationAxis();
        }

        float rotationAmount = rotationSpeed * Time.deltaTime;

        Vector3 rotation = randomAxis * rotationAmount;

        transform.Rotate(rotation);
    }

    void SetRandomRotationAxis()
    {
        randomAxis = Random.insideUnitSphere.normalized;
    }
}