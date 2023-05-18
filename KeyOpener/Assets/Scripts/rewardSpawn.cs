using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class rewardSpawn : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public GameObject prefab;
        public float spawnChance;
    }

    public TextAsset csvFile; // Plik CSV z list¹ prefabrykatów
    public List<PrefabData> prefabList; // Lista prefabrykatów i ich szans na spawn
    public int currentPrefabIndex = 0; // Indeks prefabrykatu, który ma zostaæ utworzony

    private void Start()
    {
        LoadPrefabsFromCSV();
        CreateNextPrefab();
    }

    public void LoadPrefabsFromCSV()
    {
        prefabList = new List<PrefabData>();

        if (csvFile != null)
        {
            string[] csvLines = csvFile.text.Split('\n');
            for (int i = 0; i < csvLines.Length; i++)
            {
                string line = csvLines[i];
                string[] prefabData = line.Trim().Split(',');

                if (prefabData.Length >= 2)
                {
                    string prefabName = prefabData[0];
                    float spawnChance = 0f;

                    if (float.TryParse(prefabData[1], out spawnChance))
                    {
                        GameObject prefab = Resources.Load<GameObject>(prefabName);
                        if (prefab != null)
                        {
                            PrefabData data = new PrefabData();
                            data.prefab = prefab;
                            data.spawnChance = spawnChance;

                            prefabList.Add(data);
                        }
                        else
                        {
                            Debug.LogWarning("Nie mo¿na znaleŸæ prefabrykatu: " + prefabName);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Nieprawid³owy format szansy na spawn dla prefabrykatu: " + prefabName);
                    }
                }
                else
                {
                    Debug.LogWarning("Nieprawid³owy format linii w pliku CSV: " + line);
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
        // Implementacja tworzenia prefabrykatu na podstawie szans na spawn
        if (prefabList.Count > 0)
        {
            if (currentPrefabIndex >= prefabList.Count)
            {
                currentPrefabIndex = 0;
            }

            // Tworzenie wa¿onej listy prefabrykatów
            List<GameObject> weightedPrefabs = new List<GameObject>();
            foreach (PrefabData prefabData in prefabList)
            {
                for (int i = 0; i < prefabData.spawnChance; i++)
                {
                    weightedPrefabs.Add(prefabData.prefab);
                }
            }

            // Losowanie prefabrykatu
            if (weightedPrefabs.Count > 0)
            {
                int randomIndex = Random.Range(0, weightedPrefabs.Count);
                GameObject prefab = weightedPrefabs[randomIndex];
                Instantiate(prefab, transform.position, Quaternion.identity, this.transform);
            }
            else
            {
                Debug.LogWarning("Lista prefabrykatów jest pusta.");
            }

            currentPrefabIndex++;
        }
        else
        {
            Debug.LogWarning("Lista prefabrykatów jest pusta.");
        }
    }

}
