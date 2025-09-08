using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class FloatInputFieldHandler : MonoBehaviour
{
    TMP_InputField inputField;

    [SerializeField] List<GameEvent<float>> OnValueChanged;
    [SerializeField] List<GameEvent<float>> OnEndEdit;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(ValueChanged);
        inputField.onEndEdit.AddListener(EndEdit);
    }

    private void ValueChanged(string String)
    {
        if (OnValueChanged == null) return;
        if (float.TryParse(String, out float result))
        {
            foreach (GameEvent<float> e in OnValueChanged)
            {
                e.Raise(result);
            }
            return;
        }
        else
        {
            Debug.Log("Invalid float input");
        }
    }

    private void EndEdit(string String)
    {
        if (OnEndEdit == null) return;
        if (float.TryParse(String, out float result))
        {
            foreach (GameEvent<float> e in OnEndEdit)
            {
                e.Raise(result);
            }
            return;
        }
        else
        {
            Debug.Log("Invalid float input");
        }
    }
}
