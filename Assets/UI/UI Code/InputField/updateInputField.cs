
using TMPro;
using UnityEngine;

public class updateInputField : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    public void updateText(string item)
    {
        inputField.text = item;
    }

    public void updateText(float item)
    {
        inputField.text = item.ToString();
    }
}
