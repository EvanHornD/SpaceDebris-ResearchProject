using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMP;
    [SerializeField] RectTransform RectTransform;

    public void updateText(string text) 
    {
        TMP.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);
    }
}
