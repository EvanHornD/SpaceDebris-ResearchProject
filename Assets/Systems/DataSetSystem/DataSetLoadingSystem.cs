using System.Collections.Generic;
using UnityEngine;

public class DataSetLoadingSystem : MonoBehaviour
{
    [SerializeField] DebrisManagementSystem debrisManagementSystem;
    [SerializeField] DebrisSpawningSystem debrisSpawningSystem;

    Dictionary<string, List<GameObject>> dataSets = new Dictionary<string, List<GameObject>>();

    public void LoadDataSet(DataSetLoadRequest dataSet) 
    {
        Debug.Log(dataSet.dataSetFilePath);
        if (dataSets.ContainsKey(dataSet.dataSetFilePath)) return;

        DebrisEntry[] entries = DataSetReader.parseDataEntries(dataSet.dataSetFilePath, 100, dataSet.parameters);

        List<GameObject> debris = new List<GameObject>();

        foreach (DebrisEntry entry in entries)
        {
            if (entry == null) continue;
            Debug.Log(debris.Count);
            GameObject newDebris = debrisSpawningSystem.spawnDebris(entry, dataSet.fillWithRandom);
            debrisManagementSystem.addDebris(newDebris);
            debris.Add(newDebris);
        }

        dataSets.Add(dataSet.dataSetFilePath, debris);
    }

    public void UnloadDataSet(string dataSetFilePath)
    {
        if (!dataSets.ContainsKey(dataSetFilePath)) return;

        List<GameObject> debris = dataSets[dataSetFilePath];
        foreach (var item in debris)
        {
            debrisManagementSystem.removeDebris(item);
            Destroy(item);
        }

        dataSets.Remove(dataSetFilePath);
    }
}
