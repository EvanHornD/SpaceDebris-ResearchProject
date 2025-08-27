
using UnityEngine;
using UnityEngine.Events;

public class StringEventListener : MonoBehaviour, IGameEventListener<string>
{
    [Tooltip("Event to register with.")]
    public StringEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<string> Response;

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

    public void OnEventRaised(string item)
    {
        if (Response == null) return;
        Response.Invoke(item);
    }
}