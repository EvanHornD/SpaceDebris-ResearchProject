
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SFB;
using System.Security.Cryptography;

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

	#endregion

	[Space]

    #region Events

    [SerializeField]
    UnityEvent<DataSetLoadRequest> loadDataSet;
    [SerializeField]
    UnityEvent<string> unloadDataSet;

    #endregion

    private ConfigManager configManager;
	private List<DataSet> dataSets = new List<DataSet>();
	bool fillWithRandomData = false;

	private void Awake()
	{
		configManager = new ConfigManager(configFilePath);

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
		addDataSetButton.transform.SetAsLastSibling();
    }

	public void updateDataSet(string fileName, DebrisParameter[] headers)
	{
		configManager.changeConfig(fileName, headers);
	}

    public void toggleDataSet(bool load, string dataSetName) 
	{
		if (load)
		{
			loadDataSet.Invoke(new DataSetLoadRequest(configManager.getConfigParameters(dataSetName),dataSetName,fillWithRandomData));
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

	private void addDataSet() 
	{
		string filePath = getFile();

		if (!DataSetReader.isCSV(filePath)) 
		{
			Debug.Log("invalid file type");
			return;
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

	public void toggleFillWithRandomData() 
	{
		fillWithRandomData = !fillWithRandomData;
	}
}
public struct DataSetLoadRequest
{
    public DebrisParameter[] parameters;
    public string dataSetFilePath;
	public bool fillWithRandom;
    public DataSetLoadRequest(DebrisParameter[] parameters, string dataSetFilePath, bool fillWithRandom = false)
    {
        this.parameters = parameters;
        this.dataSetFilePath = dataSetFilePath;
		this.fillWithRandom = fillWithRandom;
    }
}

