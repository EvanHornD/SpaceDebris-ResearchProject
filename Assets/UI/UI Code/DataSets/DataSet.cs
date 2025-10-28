using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataSet : MonoBehaviour
{
    #region Required Components
    [SerializeField]
    TMP_Text header;

    [SerializeField]
    HorizontalLayoutGroup horizontalLayoutGroup;

    public Button deleteButton;

    public Toggle toggle;
    #endregion

    [Space]

    #region Prefabs
    [SerializeField]
    Parameter parameterPrefab;
    #endregion

    [SerializeField]
    GameObject placeHolderVisual;

    private void Start()
    {
        if (placeHolderVisual == null) return;
        Destroy(placeHolderVisual);
    }
    [HideInInspector]
    public DataSetManager manager;
    private Parameter[] parameters;

    private string dataSetName;
    private DebrisParameter[] headerParameters;


    public string getName() 
    {
        return dataSetName;
    }

    public void init(string fileName, string[] parameterNames, DebrisParameter[] headerParameters) 
    {
        dataSetName = fileName;
        header.text = Path.GetFileName(fileName);

        parameters = new Parameter[parameterNames.Length];
        this.headerParameters = headerParameters;

        for (int i = 0; i < parameterNames.Length; i++)
        {
            Parameter parameter = Instantiate(parameterPrefab);
            parameter.initialize(parameterNames[i], i, headerParameters);

            parameter.parentDataSet = this;
            parameter.transform.SetParent(horizontalLayoutGroup.transform);
            parameters[i] = parameter;
        }
    }

    public void parameterUpdated()
    {
        manager.updateDataSet(dataSetName, headerParameters);
    }
}


