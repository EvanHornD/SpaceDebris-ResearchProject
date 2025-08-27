using UnityEngine;
using UnityEngine.Events;

public class ObjectEventListener : MonoBehaviour, IGameEventListener<object>
{
    [Tooltip("Event to register with.")]
    public ObjectEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<object> Response;

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

    public void OnEventRaised(object item)
    {
        if (Response == null) return;
        Response.Invoke(item);
    }
}
