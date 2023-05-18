using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVMenu : MonoBehaviour
{
    [MenuItem("Game/CSV/Export")]
    private static void ExportCSV()
    {
        string csvData = GenerateCSVData(); // Generowanie danych CSV na podstawie kolejno�ci prefab�w

        string filePath = EditorUtility.SaveFilePanel("Export CSV", "", "exported_data.csv", "csv");
        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, csvData);
            Debug.Log("Plik CSV zosta� wyeksportowany: " + filePath);
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

            Debug.Log("Plik CSV zosta� zaimportowany: " + filePath);
        }
    }

    private static string GenerateCSVData()
    {
        List<string> csvRows = new List<string>();

        // Pobieranie listy prefab�w z kolejno�ci�
        List<GameObject> prefabList = GetPrefabList();

        // Dodawanie wierszy do pliku CSV na podstawie kolejno�ci prefab�w
        for (int i = 0; i < prefabList.Count; i++)
        {
            string prefabPath = GetPrefabPath(prefabList[i]); // Funkcja do pobierania �cie�ki prefabu
            csvRows.Add(prefabPath);
        }

        return string.Join("\n", csvRows);
    }

    private static List<GameObject> GetPrefabList()
    {
        // Implementacja funkcji do pobierania listy prefab�w
        // Zwr�� list� prefab�w wed�ug kolejno�ci, z kt�rej korzystasz w projekcie
        List<GameObject> prefabList = new List<GameObject>();
        // Dodaj tutaj logik� pobierania prefab�w wed�ug kolejno�ci

        return prefabList;
    }

    private static string GetPrefabPath(GameObject prefab)
    {
        string prefabName = prefab.name; // Pobieranie nazwy prefabu
        string prefabPath = "Assets/Resources/" + prefabName + ".prefab"; // Generowanie �cie�ki prefabu

        return prefabPath;
    }
}
