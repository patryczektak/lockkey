using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVMenu : MonoBehaviour
{
    [MenuItem("Game/CSV/Export")]
    private static void ExportCSV()
    {
        string csvData = GenerateCSVData(); // Generowanie danych CSV na podstawie kolejnoœci prefabów

        string filePath = EditorUtility.SaveFilePanel("Export CSV", "", "exported_data.csv", "csv");
        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, csvData);
            Debug.Log("Plik CSV zosta³ wyeksportowany: " + filePath);
        }
    }

    [MenuItem("Game/CSV/Import")]
    private static void ImportCSV()
    {
        string filePath = EditorUtility.OpenFilePanel("Import CSV", "", "csv");
        if (!string.IsNullOrEmpty(filePath))
        {
            string csvData = File.ReadAllText(filePath);
            // Przetwarzanie danych CSV (dostosuj kod do swoich potrzeb)

            Debug.Log("Plik CSV zosta³ zaimportowany: " + filePath);
        }
    }

    private static string GenerateCSVData()
    {
        List<string> csvRows = new List<string>();

        // Pobieranie listy prefabów z kolejnoœci¹
        List<GameObject> prefabList = GetPrefabList();

        // Dodawanie wierszy do pliku CSV na podstawie kolejnoœci prefabów
        for (int i = 0; i < prefabList.Count; i++)
        {
            string prefabPath = GetPrefabPath(prefabList[i]); // Funkcja do pobierania œcie¿ki prefabu
            csvRows.Add(prefabPath);
        }

        return string.Join("\n", csvRows);
    }

    private static List<GameObject> GetPrefabList()
    {
        // Implementacja funkcji do pobierania listy prefabów
        // Zwróæ listê prefabów wed³ug kolejnoœci, z której korzystasz w projekcie
        List<GameObject> prefabList = new List<GameObject>();
        // Dodaj tutaj logikê pobierania prefabów wed³ug kolejnoœci

        return prefabList;
    }

    private static string GetPrefabPath(GameObject prefab)
    {
        string prefabName = prefab.name; // Pobieranie nazwy prefabu
        string prefabPath = "Assets/Resources/" + prefabName + ".prefab"; // Generowanie œcie¿ki prefabu

        return prefabPath;
    }
}
