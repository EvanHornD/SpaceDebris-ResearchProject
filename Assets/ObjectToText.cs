using TMPro;
using UnityEngine;

public class ObjectToText : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    public void setText(object item) 
    {
        text.text = item.ToString();
    }
}
