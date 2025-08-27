
using System;
using UnityEngine;
using UnityEngine.Events;

public class BooleanEventListener : MonoBehaviour, IGameEventListener<Boolean>
{
    [Tooltip("Event to register with.")]
    public BooleanEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Boolean> Response;

    private void OnEnable()
    {
        if (Event == null) return;
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (Event == null) return;
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Boolean item)
    {
        if (Response == null) return;
        Response.Invoke(item);
    }
}
