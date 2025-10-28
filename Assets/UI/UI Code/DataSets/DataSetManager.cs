using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSetManager : MonoBehaviour
{
	#region Required Components

	[SerializeField]
	VerticalLayoutGroup verticalLayoutGroup;

	[SerializeField]
	Button addDataSetButton;

	#endregion

	#region Prefabs
	[SerializeField]
	DataSet dataSetPrefab;
	#endregion

	[SerializeField]
	string configFilePath;

	public List<string> dataSetsToAdd = new List<string>();

	private ConfigManager configManager;

	private List<DataSet> dataSets = new List<DataSet>();

	private void Awake()
	{
		configManager = new ConfigManager(configFilePath);

		foreach (string filePath in dataSetsToAdd)
		{
			if (!DataSetReader.isCSV(filePath)) continue; 

			string[] headers = DataSetReader.parseHeader(filePath);
			configManager.changeConfig(filePath, headers);
		}

		addDataSetButton.onClick.AddListener(addDataSet);

		init();
	}

	public void init() 
	{
		string[] filePaths = configManager.getConfigFilePaths();

        for (int i = 0; i < filePaths.Length; i++)
        {
            DebrisParameter[] parsedHeaders = configManager.getConfigParameters(filePaths[i]);
			string[] headers = DataSetReader.getHeader(filePaths[i]);

			DataSet dataSet = Instantiate(dataSetPrefab);
			dataSet.init(filePaths[i], headers, parsedHeaders);
			dataSets.Add(dataSet);

			dataSet.deleteButton.onClick.AddListener(() => removeDataSet(dataSet));
			dataSet.toggle.onValueChanged.AddListener(toggleDataSet);
			dataSet.manager = this;

			dataSet.transform.SetParent(verticalLayoutGroup.transform);

        }
    }

	public void updateDataSet(string fileName, DebrisParameter[] headers)
	{
		configManager.changeConfig(fileName, headers);
	}

    // todo create the ability to send 
    public void toggleDataSet(bool loadDataSet) 
	{
        Debug.Log("Unimplemented Function");
    }

	public void removeDataSet(DataSet dataSet) 
	{
		configManager.removeConfig(dataSet.getName());
		dataSets.Remove(dataSet);
		Destroy(dataSet.gameObject);
	}

	// todo implement the ability to open the file explorer and add a get a files path
	private void addDataSet() 
	{
		Debug.Log("Unimplemented Function");
	}
}

