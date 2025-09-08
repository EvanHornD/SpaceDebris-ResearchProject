using System;
using UnityEngine;

public class TimeEventsHandler: MonoBehaviour
{
    [SerializeField] public TimeSystem timeSystem;

    public void pause(bool paused)
    {
        timeSystem.pause(paused);
    }

    public void setTimeScale(float scale)
    {
        timeSystem.setTimeScale(scale);
    }
}
