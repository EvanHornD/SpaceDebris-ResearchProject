using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
	DebrisParameter parameter 
	{
		get { return parameter; } set {}
	}

	private void Start()
	{
		initialize("NULL");
	}

	public void initialize(string parameter) 
	{


		string[] names = Enum.GetNames(typeof(DebrisParameter));
		int parameterIndex = -1;
		if (names.Contains(parameter))
		{
			parameterIndex = Array.IndexOf(names, parameter);
		}
		if (parameterIndex < 0) parameterIndex = names.Length - 1;

        dropdown.ClearOptions();
		dropdown.AddOptions(new List<string>(names));
		dropdown.value = parameterIndex;
        dropdown.onValueChanged.AddListener(updateParameter);
    }

	private void updateParameter(int num) 
	{
		parameter = (DebrisParameter)Enum.GetValues(typeof(DebrisParameter)).GetValue(num);
		updatedParameter.Invoke();
	}
}
