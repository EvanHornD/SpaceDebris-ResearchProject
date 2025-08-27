using UnityEngine;
using System.Collections.Generic;
using TMPro;

public abstract class DropdownUpdater<T> : MonoBehaviour
{
    [SerializeField] public TMP_Dropdown dropdown;
    [SerializeField] public RuntimeSet<T> set;
    [SerializeField] public GameEvent<T> selectEvent;
    [SerializeField] public VoidEvent deselectEvent;
    void OnEnable()
    {
        RefreshList();
    }

    public void AddOption(T item) 
    {
        string itemName = GetString(item);

        List<string> newOptions = new List<string> {itemName};

        dropdown.AddOptions(newOptions);
        dropdown.RefreshShownValue();
    }

    public void RefreshList()
    {
        dropdown.ClearOptions();

        List<string> noneOption = new List<string> {"None"};
        dropdown.AddOptions(noneOption);

        List<string> newOptions = set.Items.ConvertAll(i => GetString(i));

        dropdown.AddOptions(newOptions);
        dropdown.RefreshShownValue();
    }

    public void SelectOption(int optionNumber)
    {
        deselectEvent.Raise();
        if (optionNumber > 0)
        {
            selectEvent.Raise(set.Items[optionNumber - 1]);
        }
    }

    private string GetString(T item)
    {
        string name = item.ToString();
        int index = name.IndexOf(" (UnityEngine.");
        if (index != -1)
        {
            name = name.Substring(0, index);
        }
        return name;
    }
}
