using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Parameter : MonoBehaviour
{
	#region Required Components
	[SerializeField]
	TMP_Text header;

	[SerializeField]
	TMP_Dropdown dropdown;
	#endregion

	[Space]
    [HideInInspector]
    public DataSet parentDataSet;
	private int paramNumber = 0;
	private DebrisParameter[] headerParameters;

    public void initialize(string parameter, int paramNumber, DebrisParameter[] headerParameters)
	{
		this.headerParameters = headerParameters;
		this.paramNumber = paramNumber;
		header.text = parameter;

		string[] names = Enum.GetNames(typeof(DebrisParameter));
		int parameterIndex = Array.IndexOf(names, headerParameters[paramNumber].ToString());
		if (parameterIndex == -1) parameterIndex = names.Length-1;

        dropdown.ClearOptions();
		dropdown.AddOptions(new List<string>(names));
		dropdown.value = parameterIndex;
        dropdown.onValueChanged.AddListener(updateParameter);
    }

	private void updateParameter(int num) 
	{
		headerParameters[paramNumber] = (DebrisParameter)Enum.GetValues(typeof(DebrisParameter)).GetValue(num);
		parentDataSet.parameterUpdated();
	}
}
