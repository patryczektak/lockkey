using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy kolizja wykryta przez strefê wyzwalaj¹c¹ dotyczy naszego obiektu
        if (other.CompareTag("Player"))
        {
            // Pobierz komponent MeshRenderer z tego obiektu
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Jeœli komponent MeshRenderer istnieje, ustaw jego wartoœæ na true
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }
}
