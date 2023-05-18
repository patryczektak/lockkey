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
        // Sprawd�, czy kolizja wykryta przez stref� wyzwalaj�c� dotyczy naszego obiektu
        if (other.CompareTag("Player"))
        {
            // Pobierz komponent MeshRenderer z tego obiektu
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

            // Je�li komponent MeshRenderer istnieje, ustaw jego warto�� na true
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }
    }
}
