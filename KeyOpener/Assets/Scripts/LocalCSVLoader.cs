using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class LocalCSVLoader : MonoBehaviour
{
    public TextAsset csvFile; // Plik CSV z list� prefabrykat�w
    public int columnToLoad; // Indeks kolumny do �adowania jako prefabrykat
    public List<GameObject> prefabList; // Lista prefabrykat�w
    public int currentPrefabIndex = 0; // Indeks prefabrykatu, kt�ry ma zosta� utworzony
    private GameObject objectToKill;

    private void Start()
    {
        LoadPrefabsFromCSV();
        CreateNextPrefab();
    }

    public void LoadPrefabsFromCSV()
    {
        prefabList = new List<GameObject>();

        if (csvFile != null)
        {
            string[] csvLines = csvFile.text.Split('\n');
            for (int i = 0; i < csvLines.Length; i++)
            {
                string line = csvLines[i];
                string[] columns = line.Split(',');

                if (columnToLoad >= 0 && columnToLoad < columns.Length)
                {
                    string prefabName = columns[columnToLoad].Trim();

                    GameObject prefab = Resources.Load<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        prefabList.Add(prefab);
                    }
                    else
                    {
                        Debug.LogWarning("Nie mo�na znale�� prefabrykatu: " + prefabName);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Nie wczytano pliku CSV.");
        }
    }

    public void CreateNextPrefab()
    {
        if (objectToKill != null)
        {
            Destroy(objectToKill);
        }

        if (prefabList.Count > 0)
        {
            if (currentPrefabIndex >= prefabList.Count)
            {
                currentPrefabIndex = 0;
            }

            GameObject prefab = prefabList[currentPrefabIndex];
            objectToKill = Instantiate(prefab, transform.position, Quaternion.identity, transform);

            currentPrefabIndex++;
        }
        else
        {
            Debug.Log("Lista prefabrykat�w jest pusta.");
        }
    }

    //public void ExportToCSV()
    //{
    //    string csvFilePath = AssetDatabase.GetAssetPath(csvFile); // �cie�ka do pliku CSV

    //    using (StreamWriter writer = new StreamWriter(csvFilePath))
    //    {
    //        for (int i = 0; i < prefabList.Count; i++)
    //        {
    //            string prefabName = AssetDatabase.GetAssetPath(prefabList[i]);
    //            writer.WriteLine(prefabName);
    //        }
    //    }

    //    AssetDatabase.Refresh();

    //    Debug.Log("Plik CSV zosta� wyeksportowany.");
    //}
}
