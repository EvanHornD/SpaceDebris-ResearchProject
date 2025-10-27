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

	public UnityEvent updatedParameter;

	private int paramNumber = 0;

	private DebrisParameter[] headerParameters;

    public void initialize(string parameter, int paramNumber, DebrisParameter[] headerParameters) 
	{
		this.paramNumber = paramNumber;
		header.text = parameter;

		string[] names = Enum.GetNames(typeof(DebrisParameter));
		int parameterIndex = -1;
		if (names.Contains(parameter.Trim().ToUpper()))
		{
			parameterIndex = Array.IndexOf(names, parameter.Trim().ToUpper());
		}
		if (parameterIndex < 0) parameterIndex = names.Length - 1;
        headerParameters[paramNumber] = (DebrisParameter)Enum.GetValues(typeof(DebrisParameter)).GetValue(parameterIndex);

        dropdown.ClearOptions();
		dropdown.AddOptions(new List<string>(names));
		dropdown.value = parameterIndex;
        dropdown.onValueChanged.AddListener(updateParameter);
    }

	private void updateParameter(int num) 
	{
		headerParameters[paramNumber] = (DebrisParameter)Enum.GetValues(typeof(DebrisParameter)).GetValue(num);
		updatedParameter.Invoke();
	}
}
