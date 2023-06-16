using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreatePrefabsFromCSV : MonoBehaviour
{
    public string csvFileName; // Nazwa pliku CSV w folderze "Assets/Resources"
    public string prefabFolderPath;
    public List<GameObject> prefabList; // Lista prefabów
    public int currentPrefabIndex = 0; // Indeks prefabu, który ma zostaæ utworzony

    private void Start()
    {
        LoadPrefabsFromCSV();
    }

    public void LoadPrefabsFromCSV()
    {
        prefabList = new List<GameObject>();

        string csvFilePath = "Assets/Resources/" + csvFileName + ".csv"; // Œcie¿ka do pliku CSV w folderze "Assets/Resources"

        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);
        if (csvFile != null)
        {
            string[] csvLines = csvFile.text.Split('\n');
            for (int i = 0; i < csvLines.Length; i++)
            {
                string line = csvLines[i];
                string[] columns = line.Split(',');

                string prefabName = columns[0];
                string prefabPath = prefabFolderPath + "/" + prefabName;
                GameObject prefab = Resources.Load<GameObject>(prefabPath);

                if (prefab != null)
                {
                    prefabList.Add(prefab);
                }
                else
                {
                    Debug.LogWarning("Nie mo¿na znaleŸæ prefabu: " + prefabPath);
                }
            }
        }
        else
        {
            Debug.LogWarning("Nie mo¿na znaleŸæ pliku CSV: " + csvFilePath);
        }
    }

    public void CreateNextPrefab()
    {
        if (prefabList.Count > 0)
        {
            int randomIndex = Random.Range(0, prefabList.Count);
            GameObject prefab = prefabList[randomIndex];
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
        //if (prefabList.Count > 0)
        //{
        //    if (currentPrefabIndex >= prefabList.Count)
        //    {
        //        currentPrefabIndex = 0;
        //    }

        //    GameObject prefab = prefabList[currentPrefabIndex];
        //    Instantiate(prefab, transform.position, Quaternion.identity);

        //    currentPrefabIndex++;
        //}
        else
        {
            Debug.Log("Lista prefabów jest pusta.");
        }
    }

}
