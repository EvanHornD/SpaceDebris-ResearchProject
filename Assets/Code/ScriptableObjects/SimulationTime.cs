using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimulationTime", menuName = "Scriptable Objects/SimulationTime")]
public class SimulationTime : ScriptableObject
{
    public float timeScale = 1;
    public DateTime simulationTime = DateTime.Now;
    public bool paused = false;
    public float changeInTime = 0;
    public float dayPercentage = 0f;
}
