using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class LineRendererCreator : MonoBehaviour
{
    // Lista przechowuj�ca wybrane GameObjecty
    public List<GameObject> selectedObjects;

    // Referencja do LineRenderera, kt�ry b�dzie tworzony
    [HideInInspector]
    public LineRenderer lineRenderer;

    // Funkcja wywo�ywana po klikni�ciu na GameObject
    public void OnObjectClicked(GameObject clickedObject)
    {
        // Sprawdza, czy wybrany obiekt jest ju� na li�cie
        if (selectedObjects.Contains(clickedObject))
        {
            // Je�li tak, usuwa go z listy i usuwa LineRenderera
            selectedObjects.Remove(clickedObject);
            if (lineRenderer != null)
            {
                DestroyImmediate(lineRenderer.gameObject);
                lineRenderer = null;
            }
        }
        else
        {
            // Je�li nie, dodaje go do listy i tworzy nowy LineRenderer lub dodaje punkt do istniej�cego LineRenderera
            selectedObjects.Add(clickedObject);

            if (lineRenderer == null)
            {
                GameObject lineObject = new GameObject("LineRenderer");
                lineObject.transform.parent = transform;
                lineRenderer = lineObject.AddComponent<LineRenderer>();
            }

            Vector3[] positions = new Vector3[selectedObjects.Count];
            for (int i = 0; i < selectedObjects.Count; i++)
            {
                positions[i] = selectedObjects[i].transform.position;
            }

            lineRenderer.positionCount = selectedObjects.Count;
            lineRenderer.SetPositions(positions);
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }
    }

#if UNITY_EDITOR
    // Funkcja rysuj�ca podgl�d linii w inspektorze
    private void OnDrawGizmos()
    {
        if (selectedObjects.Count < 2)
        {
            return;
        }

        Vector3[] positions = new Vector3[selectedObjects.Count];
        for (int i = 0; i < selectedObjects.Count; i++)
        {
            positions[i] = selectedObjects[i].transform.position;
        }

        // Rysuje linie mi�dzy wybranymi obiektami
        Gizmos.color = Color.yellow;
        for (int i = 1; i < positions.Length; i++)
        {
            Gizmos.DrawLine(positions[i - 1], positions[i]);
        }

        // Rysuje k�ka w miejscu wybranych obiekt�w
        float sphereSize = 0.2f;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i == 0)
            {
                Gizmos.color = Color.green;
            }
            else if (i == positions.Length - 1)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawSphere(positions[i], sphereSize);
        }
    }
#endif
}
