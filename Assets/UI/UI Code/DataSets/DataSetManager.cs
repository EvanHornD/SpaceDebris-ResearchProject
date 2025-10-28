using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SFB;

public class DataSetManager : MonoBehaviour
{
	#region Required Components

	[SerializeField]
	VerticalLayoutGroup verticalLayoutGroup;

	[SerializeField]
	Button addDataSetButton;

    #endregion

    [Space]

    #region Prefabs

    [SerializeField]
	DataSet dataSetPrefab;

    #endregion

    [Space]

    #region filePaths

    [SerializeField]
	string configFilePath;
    [SerializeField]
	List<string> dataSetsToAdd = new List<string>();

	#endregion

	[Space]

    #region Events

    [SerializeField]
    UnityEvent<string> loadDataSet;
    [SerializeField]
    UnityEvent<string> unloadDataSet;

    #endregion

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
			CreateDataSetElement(filePaths[i]);
        }
    }

	private void CreateDataSetElement(string filePath) 
	{
        DebrisParameter[] parsedHeaders = configManager.getConfigParameters(filePath);
        string[] headers = DataSetReader.getHeader(filePath);

        DataSet dataSet = Instantiate(dataSetPrefab);
        dataSet.init(filePath, headers, parsedHeaders);
        dataSets.Add(dataSet);

        dataSet.deleteButton.onClick.AddListener(() => removeDataSet(dataSet));
        dataSet.toggle.onValueChanged.AddListener((load) => toggleDataSet(load, dataSet.getName()));
        dataSet.manager = this;

        dataSet.transform.SetParent(verticalLayoutGroup.transform);
    }

	public void updateDataSet(string fileName, DebrisParameter[] headers)
	{
		configManager.changeConfig(fileName, headers);
	}

    public void toggleDataSet(bool load, string dataSetName) 
	{
		if (load)
		{
			loadDataSet.Invoke(dataSetName);
		}
        else
        {
			unloadDataSet.Invoke(dataSetName);
        }
    }

	public void removeDataSet(DataSet dataSet) 
	{
		configManager.removeConfig(dataSet.getName());
		dataSets.Remove(dataSet);
		unloadDataSet.Invoke(dataSet.getName());
		Destroy(dataSet.gameObject);
	}

	// todo implement the ability to open the file explorer and add a get a files path
	private void addDataSet() 
	{
		string filePath = getFile();

		if (!DataSetReader.isCSV(filePath)) 
		{
			Debug.Log("invalid file type");
		}

        string[] headers = DataSetReader.parseHeader(filePath);
        bool alreadyExists = configManager.changeConfig(filePath, headers);

		if (alreadyExists) 
		{
            foreach (DataSet dataSet in dataSets )
            {
				if (dataSet.getName() == filePath) removeDataSet(dataSet);
            }
		}
		CreateDataSetElement(filePath);
    }

	private string getFile() 
	{
        ExtensionFilter[] extensions = new ExtensionFilter[] {
			new ExtensionFilter("Text", "txt", "csv")
		};
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Data Set", "", extensions, false);
		return paths[0];
    }
}

