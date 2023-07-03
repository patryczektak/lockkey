using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;

public class LocalCSVLoader : MonoBehaviour
{
    public TextAsset csvFile; // Plik CSV z list¹ prefabrykatów
    public int columnToLoad; // Indeks kolumny do ³adowania jako prefabrykat
    public List<GameObject> prefabList; // Lista prefabrykatów
    public int currentPrefabIndex = 0; // Indeks prefabrykatu, który ma zostaæ utworzony
    private GameObject objectToKill;
    public panelAnim anim;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelTextShadow;

    private void Start()
    {
        ColumnCheck();
        LoadPrefabsFromCSV();
        CreateNextPrefab();        
    }
    public void Update()
    {
        levelText.text = PlayerPrefs.GetInt("exp").ToString();
        levelTextShadow.text = PlayerPrefs.GetInt("exp").ToString();
    }
    public void ColumnCheck()
    {
        //ustalanie indexu wed³ug exp gracza
        if (PlayerPrefs.GetInt("exp") == 0)
        {
            columnToLoad = 0;
        }

        if (PlayerPrefs.GetInt("exp") >= 1)
        {
            columnToLoad = 1;
        }

        if (PlayerPrefs.GetInt("exp") >= 15)
        {
            columnToLoad = 2;
        }
        if (PlayerPrefs.GetInt("exp") >= 20)
        {
            columnToLoad = 3;
        }
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
                        Debug.LogWarning("Nie mo¿na znaleŸæ prefabrykatu: " + prefabName);
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
        ColumnCheck();
        LoadPrefabsFromCSV();
        anim.Hide();
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
            Debug.Log("Lista prefabrykatów jest pusta.");
        }
    }

    //public void ExportToCSV()
    //{
    //    string csvFilePath = AssetDatabase.GetAssetPath(csvFile); // Œcie¿ka do pliku CSV

    //    using (StreamWriter writer = new StreamWriter(csvFilePath))
    //    {
    //        for (int i = 0; i < prefabList.Count; i++)
    //        {
    //            string prefabName = AssetDatabase.GetAssetPath(prefabList[i]);
    //            writer.WriteLine(prefabName);
    //        }
    //    }

    //    AssetDatabase.Refresh();

    //    Debug.Log("Plik CSV zosta³ wyeksportowany.");
    //}
}
