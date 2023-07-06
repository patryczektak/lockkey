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
        public List<float> spawnChances;
    }

    public TextAsset csvFile; // Plik CSV z list¹ prefabrykatów
    public List<PrefabData> prefabList; // Lista prefabrykatów i ich szans na spawn
    public int currentPrefabIndex = 0; // Indeks prefabrykatu, który ma zostaæ utworzony
    public int spawnChanceColumn; // Indeks kolumny z szansami na spawn

    private void Start()
    {
        if (PlayerPrefs.GetInt("exp") < 30)
        {
            spawnChanceColumn = 2;
        }

        if (PlayerPrefs.GetInt("exp") >= 30)
        {
            spawnChanceColumn = 1;
        }
        LoadPrefabsFromCSV();
        CreateNextPrefab();
        //wczytywanie szansy wewd³ug poziomu gracza
        
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

                if (prefabData.Length >= spawnChanceColumn + 1)
                {
                    string prefabName = prefabData[0];

                    GameObject prefab = Resources.Load<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PrefabData data = new PrefabData();
                        data.prefab = prefab;
                        data.spawnChances = new List<float>();

                        for (int j = spawnChanceColumn; j < prefabData.Length; j++)
                        {
                            if (float.TryParse(prefabData[j], out float spawnChance))
                            {
                                if (spawnChance > 0f)
                                {
                                    data.spawnChances.Add(spawnChance);
                                }
                            }
                            else
                            {
                                Debug.LogWarning($"Nieprawid³owy format szansy na spawn dla prefabrykatu {prefabName} w kolumnie {j}");
                            }
                        }

                        if (data.spawnChances.Count > 0)
                        {
                            prefabList.Add(data);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Nie mo¿na znaleŸæ prefabrykatu: " + prefabName);
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
        if (prefabList.Count > 0)
        {
            int randomIndex = Random.Range(0, prefabList.Count);
            currentPrefabIndex = randomIndex;

            PrefabData prefabData = prefabList[currentPrefabIndex];

            if (prefabData.spawnChances.Count > 0)
            {
                // Tworzenie wa¿onej listy prefabrykatów
                List<GameObject> weightedPrefabs = new List<GameObject>();
                List<float> weightedChances = new List<float>();

                for (int i = 0; i < prefabData.spawnChances.Count; i++)
                {
                    if (prefabData.spawnChances[i] > 0f)
                    {
                        weightedPrefabs.Add(prefabData.prefab);
                        weightedChances.Add(prefabData.spawnChances[i]);
                    }
                }

                // Sprawdzanie, czy s¹ dostêpne prefabrykaty do wylosowania
                if (weightedPrefabs.Count > 0)
                {
                    // Sumowanie szans na spawn
                    float totalSpawnChance = 0f;
                    foreach (float chance in weightedChances)
                    {
                        totalSpawnChance += chance;
                    }

                    // Losowanie prefabrykatu
                    float randomChance = Random.Range(0f, totalSpawnChance);
                    float cumulativeChance = 0f;
                    int chosenIndex = 0;

                    for (int i = 0; i < weightedChances.Count; i++)
                    {
                        cumulativeChance += weightedChances[i];
                        if (randomChance <= cumulativeChance)
                        {
                            chosenIndex = i;
                            break;
                        }
                    }

                    GameObject prefab = weightedPrefabs[chosenIndex];
                    Instantiate(prefab, transform.position, Quaternion.identity, this.transform);
                }
                else
                {
                    Debug.LogWarning("Nie ma dostêpnych prefabrykatów do wylosowania.");
                }
            }
            else
            {
                Debug.LogWarning("Lista szans na spawn dla prefabrykatu jest pusta.");
            }
        }
        else
        {
            Debug.LogWarning("Lista prefabrykatów jest pusta.");
        }
    }

}