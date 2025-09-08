using System;
using UnityEngine;


public class DayCycle : MonoBehaviour
{
    public SimulationTime time;

    void Start()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, -360f*time.dayPercentage, 0);
    }
}
