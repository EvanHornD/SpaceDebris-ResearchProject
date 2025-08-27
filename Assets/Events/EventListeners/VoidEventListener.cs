using System;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public VoidEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

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

    public void OnEventRaised()
    {
        if (Response == null) return;
        Response.Invoke();
    }
}