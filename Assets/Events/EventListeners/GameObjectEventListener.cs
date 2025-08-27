
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventListener : MonoBehaviour, IGameEventListener<GameObject>
{
    [Tooltip("Event to register with.")]
    public GameObjectEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<GameObject> Response;

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

    public void OnEventRaised(GameObject item)
    {
        if (Response == null) return;
        Response.Invoke(item);
    }
}