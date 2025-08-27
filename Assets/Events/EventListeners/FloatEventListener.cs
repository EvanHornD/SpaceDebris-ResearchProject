
using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : MonoBehaviour, IGameEventListener<float>
{
    [Tooltip("Event to register with.")]
    public FloatEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<float> Response;

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

    public void OnEventRaised(float item)
    {
        if (Response == null) return;
        Response.Invoke(item);
    }
}
