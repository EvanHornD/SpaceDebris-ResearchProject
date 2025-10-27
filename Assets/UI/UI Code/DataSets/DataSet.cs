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

    [SerializeField]
    Button DeleteButton;

    [SerializeField]
    Toggle toggle;
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

    private void Awake()
    {
        init("test config",new string[] { "name", "inclination", "eccentricity", "type", "sisterDebris" });
    }

    private Parameter[] parameters;

    [HideInInspector]
    public DebrisParameter[] headerParameters;


    public void init(string fileName, string[] parameterNames) 
    {
        header.text = fileName;

        parameters = new Parameter[parameterNames.Length];
        headerParameters = new DebrisParameter[parameterNames.Length];

        for (int i = 0; i < parameterNames.Length; i++)
        {
            Parameter parameter = Instantiate(parameterPrefab);
            parameter.initialize(parameterNames[i], i, headerParameters);

            parameter.transform.SetParent(horizontalLayoutGroup.transform);
            parameter.updatedParameter.AddListener(parameterUpdated);
            parameters[i] = parameter;
        }
    }

    private void parameterUpdated() 
    {

    }

}


