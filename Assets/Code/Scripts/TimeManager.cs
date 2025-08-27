using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] public SimulationTime time;

    void Update()
    {
        stepSimulation();
    }

    public void pause(bool paused)
    {
        time.paused = paused;
    }

    public void setTimeScale(float scale)
    {
        time.timeScale = scale;
    }

    void stepSimulation()
    {
        if (time.paused) { return; }

        time.changeInTime = Time.deltaTime * time.timeScale;
        time.simulationTime = time.simulationTime.AddSeconds(time.changeInTime);
        time.dayPercentage = (((((time.simulationTime.Second / 60f) + time.simulationTime.Minute) / 60f) + time.simulationTime.Hour) / 24f);
    }
}
