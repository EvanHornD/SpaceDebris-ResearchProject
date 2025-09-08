using System.Collections.Generic;

using UnityEngine;

public abstract class Collection<T> : ScriptableObject
{
    public List<T> Items = new List<T>();

    public GameEvent<T> OnItemAdded;
    public GameEvent<T> OnItemRemoved;
    public VoidEvent OnSetCleared;

    private void OnEnable()
    {
        Clear();
    }

    public void Add(T t) 
    {
        if (!Items.Contains(t))
        {
            Items.Add(t);
            if (OnItemAdded != null) { OnItemAdded.Raise(t); }
        }
    }

    public void Remove(T t)
    {
        if (Items.Contains(t))
        {
            Items.Remove(t);
            if (OnItemRemoved != null) OnItemRemoved.Raise(t);
        }
    }

    public void Clear()
    {
        Items.Clear();
        OnSetCleared.Raise();
    }
}
